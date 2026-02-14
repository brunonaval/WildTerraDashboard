using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization; // <- precisa da referência System.Web.Extensions

namespace WildTerraDashboard
{
    public partial class Form1 : Form
    {
        // SERVIÇOS
        private NetworkService rede;
        private PlayerStats statsJogador;
        private UdpClient enviadorUDP;

        // BOTS (Lógica)
        private BotMovement botMovimento;
        private RouteRecorder gravadorRota;
        private BotHarvest botColeta;
        private BotMount botMontaria;
        private BotHunter botCaçador;

        // DADOS
        private List<RadarEntity> entidadesRadar = new List<RadarEntity>();
        private bool indoParaBanco = false;
        private bool aguardandoDeposito = false;

        // CONTROLES DE ESTADO
        private string _ultimaListaComerEnviada = "";
        private string _ultimaListaLixoEnviada = "";
        private string _ultimaListaColetaEnviada = "";
        private int _ultimoThresholdEnviado = -1;
        private bool _ultimoEstadoMountEnviado = false;
        private bool isFishingRunning = false; // Controle de Pesca

        // Anti-spam: evita flood de HARVEST idêntico (o timer roda a cada ~150ms).
        // Se o mesmo comando foi enviado há pouco, não reenviar e nem cair para MOVE no mesmo tick.
        private DateTime _lastHarvestSentTime = DateTime.MinValue;
        private string _lastHarvestCmdSent = null;
        private const int HARVEST_SEND_COOLDOWN_MS = 700;


        // =========================
        // WATCHDOG (ANTI TRAVAMENTO)
        // =========================
        private Timer watchdogTimer;
        private Timer restartTimer;
        private bool _restartPending = false;

        private DateTime _lastStatsTime = DateTime.MinValue;

        // Âncora de movimento (para saber se realmente "andou")
        private DateTime _lastMovedTime = DateTime.MinValue;
        private float _lastWdX = 0f, _lastWdZ = 0f;

        // Timestamps de comandos (apenas os que indicam "progresso" do bot)
        private DateTime _lastMoveCmdTime = DateTime.MinValue;       // último MOVE;...
        private DateTime _lastWorkCmdTime = DateTime.MinValue;       // HARVEST/HUNT/DEPOSIT/RETURN_HOME
        private DateTime _lastProgressCmdTime = DateTime.MinValue;   // MOVE ou WORK (o que for mais recente)
        private DateTime _lastWatchdogRestartTime = DateTime.MinValue;

        // Ajustes do watchdog
        private const float WD_MIN_MOVE_METERS = 0.8f;               // deslocamento mínimo para considerar que “andou”
        private const int WD_RESTART_COOLDOWN_SECONDS = 60;          // evita loop de restart

        // Gatilho A (MOVE travado - rápido)
        private const int WD_MOVE_RECENT_WINDOW_SECONDS = 6;         // consideramos “está tentando mover” se MOVE foi enviado nos últimos Xs
        private const int WD_MOVE_STUCK_SECONDS = 12;                // se tentando mover e parado por Xs -> restart

        // Gatilho B (Idle travado - amplo e mais lento)
        private const int WD_IDLE_STUCK_SECONDS = 30;                // parado por Xs -> candidato a travamento
        private const int WD_IDLE_NO_PROGRESS_SECONDS = 15;          // sem comando de progresso por Xs -> realmente “sem fazer nada”

        // =========================
        // PERSISTÊNCIA (APENAS TEXTBOXES SELECIONADAS)
        // =========================
        private bool _isLoadingUiText = false;
        private readonly string _uiTextFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ui_textboxes.json");

        public Form1()
        {
            InitializeComponent();

            // 0. Carrega configurações ANTES de ligar eventos de TextChanged (evita disparar lógica)
            CarregarConfigBanco();           // (banco fica como está)
            CarregarListaLixo();             // compat (txtDropList)
            CarregarListaComer();            // compat (txtAutoEat)
            CarregarTextboxesSelecionadas(); // preferencial (sobrescreve se existir)

            // 1. INICIALIZAÇÃO DE DADOS E REDE (Primeiro)
            statsJogador = new PlayerStats();
            enviadorUDP = new UdpClient();
            rede = new NetworkService(8888);

            // 2. INICIALIZAÇÃO DAS CLASSES DE LÓGICA (Crítico: Antes de ligar eventos)
            botMovimento = new BotMovement();
            gravadorRota = new RouteRecorder();
            botColeta = new BotHarvest();
            botMontaria = new BotMount();
            botCaçador = new BotHunter();

            // 3. LIGAÇÃO DE EVENTOS DE REDE
            rede.OnPacoteRecebido += Rede_OnPacoteRecebido;
            rede.OnErro += Rede_OnErro;
            rede.OnIniciado += Rede_OnIniciado;

            // 4. LIGAÇÃO DE LOGS (Para vermos o que os bots fazem)
            botMovimento.OnLog += LogarMensagem;
            gravadorRota.OnLog += LogarMensagem;
            botColeta.OnLog += LogarMensagem;
            botMontaria.OnLog += LogarMensagem;
            botCaçador.OnLog += LogarMensagem;

            // 5. TIMERS PRINCIPAIS
            botTimer.Tick -= BotTimer_Tick; // Remove duplicatas por segurança
            recordTimer.Tick -= RecordTimer_Tick;

            botTimer.Tick += BotTimer_Tick;
            recordTimer.Tick += RecordTimer_Tick;

            botTimer.Interval = 150;
            recordTimer.Interval = 1000;

            // 5b. TIMERS DO WATCHDOG
            watchdogTimer = new Timer();
            watchdogTimer.Interval = 1000; // 1s
            watchdogTimer.Tick += WatchdogTimer_Tick;

            restartTimer = new Timer();
            restartTimer.Interval = 250; // delay curto OFF->ON
            restartTimer.Tick += RestartTimer_Tick;

            // 6. EVENTOS DE UI (INTERFACE)
            if (txtListaColeta != null) txtListaColeta.TextChanged += TxtListaColeta_TextChanged;
            if (chkAtivarColeta != null) chkAtivarColeta.CheckedChanged += ChkAtivarColeta_CheckedChanged;

            // TROCA: agora respeita _isLoadingUiText
            if (txtListaMobs != null) txtListaMobs.TextChanged += TxtListaMobs_TextChanged;

            if (chkAtivarHunt != null) chkAtivarHunt.CheckedChanged += (s, e) =>
            {
                botCaçador.IsAtivo = chkAtivarHunt.Checked;
                LogarMensagem($"Caçador: {(botCaçador.IsAtivo ? "ON" : "OFF")}");
            };

            if (chkUseMount != null) chkUseMount.CheckedChanged += ChkUseMount_CheckedChanged;

            if (btnSaveSafe != null) btnSaveSafe.Click += BtnSaveSafe_Click;
            if (btnLoadSafe != null) btnLoadSafe.Click += BtnLoadSafe_Click;
        }

        // =========================
        // PERSISTÊNCIA: APENAS TXT SELECIONADAS
        // =========================
        private void SalvarTextboxesSelecionadas()
        {
            try
            {
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                if (txtListaColeta != null) dict["txtListaColeta"] = txtListaColeta.Text ?? "";
                if (txtDropList != null) dict["txtDropList"] = txtDropList.Text ?? "";
                if (txtSafeList != null) dict["txtSafeList"] = txtSafeList.Text ?? "";
                if (txtWeaponName != null) dict["txtWeaponName"] = txtWeaponName.Text ?? "";
                if (txtListaMobs != null) dict["txtListaMobs"] = txtListaMobs.Text ?? "";
                if (txtAutoEat != null) dict["txtAutoEat"] = txtAutoEat.Text ?? "";
                if (txtRodName != null) dict["txtRodName"] = txtRodName.Text ?? "";
                if (txtBaitName != null) dict["txtBaitName"] = txtBaitName.Text ?? "";

                var ser = new JavaScriptSerializer();
                var json = ser.Serialize(dict);
                File.WriteAllText(_uiTextFile, json, Encoding.UTF8);
            }
            catch { }
        }

        private void CarregarTextboxesSelecionadas()
        {
            try
            {
                if (!File.Exists(_uiTextFile)) return;

                var json = File.ReadAllText(_uiTextFile, Encoding.UTF8);
                var ser = new JavaScriptSerializer();
                var dict = ser.Deserialize<Dictionary<string, string>>(json);
                if (dict == null) return;

                _isLoadingUiText = true;

                if (txtListaColeta != null && dict.TryGetValue("txtListaColeta", out var v1)) txtListaColeta.Text = v1 ?? "";
                if (txtDropList != null && dict.TryGetValue("txtDropList", out var v2)) txtDropList.Text = v2 ?? "";
                if (txtSafeList != null && dict.TryGetValue("txtSafeList", out var v3)) txtSafeList.Text = v3 ?? "";
                if (txtWeaponName != null && dict.TryGetValue("txtWeaponName", out var v4)) txtWeaponName.Text = v4 ?? "";
                if (txtListaMobs != null && dict.TryGetValue("txtListaMobs", out var v5)) txtListaMobs.Text = v5 ?? "";
                if (txtAutoEat != null && dict.TryGetValue("txtAutoEat", out var v6)) txtAutoEat.Text = v6 ?? "";
                if (txtRodName != null && dict.TryGetValue("txtRodName", out var v7)) txtRodName.Text = v7 ?? "";
                if (txtBaitName != null && dict.TryGetValue("txtBaitName", out var v8)) txtBaitName.Text = v8 ?? "";
            }
            catch { }
            finally
            {
                _isLoadingUiText = false;
            }
        }

        // --- MÉTODOS DE LOG E UI ---
        private void LogarMensagem(string msg)
        {
            if (lstLog == null || lstLog.IsDisposed) return;
            this.BeginInvoke((MethodInvoker)delegate
            {
                lstLog.Items.Add(msg);
                lstLog.TopIndex = lstLog.Items.Count - 1;
            });
        }

        private void ChkUseMount_CheckedChanged(object sender, EventArgs e)
        {
            if (botMontaria != null) botMontaria.AtualizarConfig(chkUseMount.Checked);
        }

        // --- CONFIGURAÇÃO E PERSISTÊNCIA (BANCO) ---
        private void SalvarConfigBanco()
        {
            try
            {
                string[] linhas = { txtBankX.Text, txtBankZ.Text, txtBankName.Text };
                File.WriteAllLines("config_banco.txt", linhas);
            }
            catch { }
        }

        private void CarregarConfigBanco()
        {
            try
            {
                if (File.Exists("config_banco.txt"))
                {
                    string[] linhas = File.ReadAllLines("config_banco.txt");
                    if (linhas.Length >= 1) txtBankX.Text = linhas[0];
                    if (linhas.Length >= 2) txtBankZ.Text = linhas[1];
                    if (linhas.Length >= 3) txtBankName.Text = linhas[2];
                }
            }
            catch { }
        }

        // (Mantidos para compatibilidade com seu formato antigo)
        private void SalvarListaLixo()
        {
            try
            {
                if (txtDropList != null) File.WriteAllText("lista_lixo.txt", txtDropList.Text);
            }
            catch { }
        }

        private void CarregarListaLixo()
        {
            try
            {
                if (txtDropList != null && File.Exists("lista_lixo.txt"))
                    txtDropList.Text = File.ReadAllText("lista_lixo.txt");
            }
            catch { }
        }

        private void SalvarListaComer()
        {
            try
            {
                if (txtAutoEat != null) File.WriteAllText("lista_comer.txt", txtAutoEat.Text);
            }
            catch { }
        }

        private void CarregarListaComer()
        {
            try
            {
                if (txtAutoEat != null && File.Exists("lista_comer.txt"))
                    txtAutoEat.Text = File.ReadAllText("lista_comer.txt");
            }
            catch { }
        }

        // --- BOTÕES DE LISTAS ---
        private void BtnSaveSafe_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Safe List|*.txt";
            sfd.FileName = "lista_segura.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(sfd.FileName, txtSafeList.Text);
                    MessageBox.Show("Salvo!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void BtnLoadSafe_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Safe List|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtSafeList.Text = File.ReadAllText(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void TxtListaColeta_TextChanged(object sender, EventArgs e)
        {
            if (_isLoadingUiText) return;
            if (botColeta != null) botColeta.DefinirLista(txtListaColeta.Text);
        }

        private void TxtListaMobs_TextChanged(object sender, EventArgs e)
        {
            if (_isLoadingUiText) return;
            if (botCaçador != null) botCaçador.DefinirLista(txtListaMobs.Text);
        }

        private void ChkAtivarColeta_CheckedChanged(object sender, EventArgs e)
        {
            if (botColeta != null)
            {
                botColeta.IsAtivo = chkAtivarColeta.Checked;
                LogarMensagem($"Coleta: {(botColeta.IsAtivo ? "ON" : "OFF")}");
            }
        }

        // ======================
        // LOOP PRINCIPAL DO BOT
        // ======================
        private void BotTimer_Tick(object sender, EventArgs e)
        {
            // 1. Envia Lista de Lixo
            if (txtDropList != null)
            {
                string atual = txtDropList.Text;
                if (atual != _ultimaListaLixoEnviada && atual.Length > 0)
                {
                    EnviarComandoJogo("DROP_LIST;" + atual.Replace("\r\n", "~").Replace("\n", "~"));
                    _ultimaListaLixoEnviada = atual;
                }
            }

            // 1b. Envia Lista de Coleta (txtListaColeta) para o Bot
            if (txtListaColeta != null)
            {
                string atual = txtListaColeta.Text;
                if (atual != _ultimaListaColetaEnviada)
                {
                    string payload = BuildListPayload(atual);
                    EnviarComandoJogo("HARVEST_LIST;" + payload);
                    _ultimaListaColetaEnviada = atual;
                }
            }

            // 2. Envia Lista de Comida
            if (txtAutoEat != null)
            {
                string atual = txtAutoEat.Text;
                if (atual != _ultimaListaComerEnviada && atual.Length > 0)
                {
                    EnviarComandoJogo("EAT_LIST;" + atual.Replace("\r\n", "~").Replace("\n", "~"));
                    _ultimaListaComerEnviada = atual;
                }
            }

            // 3. Envia Threshold
            if (numEatThreshold != null)
            {
                int val = (int)numEatThreshold.Value;
                if (val != _ultimoThresholdEnviado)
                {
                    EnviarComandoJogo($"EAT_THRESHOLD;{val}");
                    _ultimoThresholdEnviado = val;
                }
            }

            // 4. Envia Config Montaria
            if (botMontaria != null && botMontaria.IsAtivo != _ultimoEstadoMountEnviado)
            {
                EnviarComandoJogo($"MOUNT_CONFIG;{(botMontaria.IsAtivo ? "ON" : "OFF")}");
                _ultimoEstadoMountEnviado = botMontaria.IsAtivo;
            }

            if (aguardandoDeposito) return;

            // --- LÓGICA DE BANCO ---
            if (indoParaBanco)
            {
                try
                {
                    float bankX = float.Parse(txtBankX.Text);
                    float bankZ = float.Parse(txtBankZ.Text);
                    float dist = CalcularDistancia(statsJogador.X, statsJogador.Z, bankX, bankZ);

                    if (dist < 3.0f)
                    {
                        LogarMensagem("[SISTEMA] Cheguei na Cabana. Iniciando depósito...");
                        string nomeBau = txtBankName.Text.Trim();
                        if (string.IsNullOrEmpty(nomeBau)) EnviarComandoJogo("DEPOSIT_ALL");
                        else EnviarComandoJogo($"DEPOSIT_ALL;{nomeBau}");
                        indoParaBanco = false;
                        aguardandoDeposito = true;
                    }
                    else
                    {
                        EnviarComandoJogo($"MOVE;{bankX};{bankZ}");
                    }
                }
                catch { }
                return;
            }

            // PEGA O NOME DA ARMA
            string nomeArma = "";
            if (txtWeaponName != null) nomeArma = txtWeaponName.Text.Trim();

            // === PRIORIDADE 1: CAÇA ===
            string comandoHunt = botCaçador.VerificarRadar(entidadesRadar);
            if (comandoHunt != null)
            {
                EnviarComandoJogo($"{comandoHunt};{nomeArma}");
                return;
            }

            // === PRIORIDADE 2: COLETA ===
            string comandoColeta = botColeta.VerificarRadar(entidadesRadar);
            if (comandoColeta != null)
            {
                string fullHarvestCmd = $"{comandoColeta};{nomeArma}";

                // Dedup/Throttle: se já enviamos o mesmo HARVEST há menos de X ms, não reenviar.
                // Importante: retornar aqui impede o envio de MOVE no mesmo tick, evitando interferência na coleta em andamento.
                if (_lastHarvestCmdSent != null &&
                    string.Equals(_lastHarvestCmdSent, fullHarvestCmd, StringComparison.OrdinalIgnoreCase) &&
                    (DateTime.Now - _lastHarvestSentTime).TotalMilliseconds < HARVEST_SEND_COOLDOWN_MS)
                {
                    return;
                }

                _lastHarvestCmdSent = fullHarvestCmd;
                _lastHarvestSentTime = DateTime.Now;

                EnviarComandoJogo(fullHarvestCmd);
                return;
            }

            // === PRIORIDADE 3: ROTA ===
            string comandoMove = botMovimento.ProcessarLogica(statsJogador.X, statsJogador.Z, statsJogador.SP);
            if (comandoMove != null)
            {
                EnviarComandoJogo(comandoMove);
            }
        }

        // --- CÁLCULOS E ROTA ---
        private float CalcularDistancia(float x1, float z1, float x2, float z2)
        {
            return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(z1 - z2, 2));
        }

        private void RecordTimer_Tick(object sender, EventArgs e)
        {
            gravadorRota.ProcessarPosicao(statsJogador.X, statsJogador.Z);
        }

        // ==========================
        // START/STOP BOT (REFATORADO)
        // ==========================
        private void PararBotUI()
        {
            botMovimento.Parar();

            if (btnStartBot != null)
            {
                btnStartBot.Text = "INICIAR BOT";
                btnStartBot.BackColor = Color.Green;
            }

            botTimer.Stop();
            if (watchdogTimer != null) watchdogTimer.Stop();

            EnviarComandoJogo("BOT_STATUS;OFF");
            LogarMensagem("[SISTEMA] Bot Parado.");

            if (btnStartFishing != null) btnStartFishing.Enabled = true;
        }

        private void IniciarBotUI()
        {
            botMovimento.Iniciar();
            if (!botMovimento.IsRodando) return;

            if (btnStartBot != null)
            {
                btnStartBot.Text = "PARAR BOT";
                btnStartBot.BackColor = Color.Red;
            }

            botTimer.Start();
            if (watchdogTimer != null) watchdogTimer.Start();

            _ultimoEstadoMountEnviado = !botMontaria.IsAtivo;
            EnviarComandoJogo("BOT_STATUS;ON");
            LogarMensagem("[SISTEMA] Bot Iniciado.");

            if (btnStartFishing != null) btnStartFishing.Enabled = false;

            // reseta watchdog
            ResetWatchdogMovement();
            _lastMoveCmdTime = DateTime.Now;
            _lastWorkCmdTime = DateTime.Now;
            _lastProgressCmdTime = DateTime.Now;
        }

        private void btnStartBot_Click(object sender, EventArgs e)
        {
            if (isFishingRunning) { MessageBox.Show("Pare o Bot de Pesca antes!"); return; }
            if (gravadorRota.IsGravando) { MessageBox.Show("Pare a gravação antes!"); return; }

            if (botMovimento.IsRodando) PararBotUI();
            else IniciarBotUI();
        }

        // --- BOTÃO GRAVAR ROTA ---
        private void btnRecordRoute_Click(object sender, EventArgs e)
        {
            if (gravadorRota.IsGravando)
            {
                gravadorRota.PararGravacao();
                recordTimer.Stop();
                btnRecordRoute.Text = "GRAVAR ROTA";
                btnRecordRoute.BackColor = Color.Orange;
                btnRecordRoute.ForeColor = Color.Black;
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Arquivo de Rota|*.txt";
                sfd.FileName = "nova_rota.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    gravadorRota.IniciarGravacao(sfd.FileName);
                    recordTimer.Start();
                    btnRecordRoute.Text = "PARAR GRAVAÇÃO";
                    btnRecordRoute.BackColor = Color.Red;
                    btnRecordRoute.ForeColor = Color.White;
                }
            }
        }

        private void btnLoadRoute_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Arquivos de Texto|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                botMovimento.CarregarRota(ofd.FileName);
            }
        }

        // --- BOTÃO CONECTAR ---
        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            rede.Iniciar();
        }

        // --- BOTÕES DE LISTA DE COLETA ---
        private void btnSaveList_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivos de Texto|*.txt";
            sfd.FileName = "lista_coleta.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(sfd.FileName, txtListaColeta.Text);
                    MessageBox.Show("Salvo!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void btnLoadList_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Arquivos de Texto|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string txt = System.IO.File.ReadAllText(ofd.FileName);
                    txtListaColeta.Text = txt;
                    botColeta.DefinirLista(txt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        // --- REDE (UDP) ---
        private static string NormalizeListToken(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "";
            s = s.Trim();

            // Remove BOM / zero-width / bidi marks / NBSP
            s = s.Replace("\uFEFF", "")
                 .Replace("\u200B", "")
                 .Replace("\u200E", "")
                 .Replace("\u200F", "")
                 .Replace("\u00A0", " ");

            return s.Trim();
        }

        private static string BuildListPayload(string rawText)
        {
            if (string.IsNullOrWhiteSpace(rawText)) return "";
            var parts = rawText
                .Split(new[] { "\r\n", "\n", "\r", "~" }, StringSplitOptions.None)
                .Select(NormalizeListToken)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            return string.Join("~", parts);
        }

        private void EnviarComandoJogo(string comando)
        {
            try
            {
                // Marcação de progresso (para watchdog híbrido)
                if (IsMoveCommand(comando))
                {
                    _lastMoveCmdTime = DateTime.Now;
                    _lastProgressCmdTime = DateTime.Now;
                }
                else if (IsWorkCommand(comando))
                {
                    _lastWorkCmdTime = DateTime.Now;
                    _lastProgressCmdTime = DateTime.Now;
                }

                byte[] dados = Encoding.ASCII.GetBytes(comando);
                enviadorUDP.Send(dados, dados.Length, "127.0.0.1", 8889);
            }
            catch { }
        }

        private bool IsMoveCommand(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd)) return false;
            return cmd.StartsWith("MOVE;", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsWorkCommand(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd)) return false;

            // “Work/progresso de tarefa” (não inclui listas/config)
            return cmd.StartsWith("HARVEST;", StringComparison.OrdinalIgnoreCase)
                || cmd.StartsWith("HUNT;", StringComparison.OrdinalIgnoreCase)
                || cmd.StartsWith("DEPOSIT_ALL", StringComparison.OrdinalIgnoreCase)
                || cmd.StartsWith("RETURN_HOME", StringComparison.OrdinalIgnoreCase);
        }

        private void Rede_OnIniciado()
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (lstLog != null) lstLog.Items.Add("UDP Iniciado.");
                if (btnConnect != null) btnConnect.Text = "Ouvindo...";
            });
        }

        private void Rede_OnErro(string msg)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (lstLog != null) lstLog.Items.Add("Erro: " + msg);
                if (btnConnect != null) btnConnect.Enabled = true;
            });
        }

        private void Rede_OnPacoteRecebido(string mensagem)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                InterpretarComando(mensagem);
            });
        }

        // =========================================
        // WATCHDOG HÍBRIDO: MOVE travado + Idle travado
        // =========================================
        private void WatchdogTimer_Tick(object sender, EventArgs e)
        {
            if (botMovimento == null || !botMovimento.IsRodando) return;

            // Não mexer durante pesca / gravação
            if (isFishingRunning) return;
            if (gravadorRota != null && gravadorRota.IsGravando) return;

            // Não mexer durante banco
            if (indoParaBanco || aguardandoDeposito) return;

            // Não mexer em combate
            if (botMovimento.IsEmCombate)
            {
                ResetWatchdogMovement();
                return;
            }

            // Precisa estar recebendo stats recentes
            if (_lastStatsTime == DateTime.MinValue) return;
            if ((DateTime.Now - _lastStatsTime).TotalSeconds > 2.5) return;

            // Condição obrigatória: HP/SP >= 80%
            if (statsJogador.HP < 80 || statsJogador.SP < 80)
            {
                ResetWatchdogMovement();
                return;
            }

            // Se ainda não temos âncora válida
            if (_lastMovedTime == DateTime.MinValue)
            {
                ResetWatchdogMovement();
                return;
            }

            // Mede deslocamento desde última âncora
            double dx = statsJogador.X - _lastWdX;
            double dz = statsJogador.Z - _lastWdZ;
            double dist = Math.Sqrt(dx * dx + dz * dz);

            if (dist >= WD_MIN_MOVE_METERS)
            {
                _lastWdX = statsJogador.X;
                _lastWdZ = statsJogador.Z;
                _lastMovedTime = DateTime.Now;
                return;
            }

            double stuckSeconds = (DateTime.Now - _lastMovedTime).TotalSeconds;

            // Cooldown (evita loop)
            bool emCooldown = (DateTime.Now - _lastWatchdogRestartTime).TotalSeconds < WD_RESTART_COOLDOWN_SECONDS;
            if (emCooldown) return;

            // ---------
            // Gatilho A: MOVE travado (rápido)
            // ---------
            bool moveRecente = _lastMoveCmdTime != DateTime.MinValue &&
                               (DateTime.Now - _lastMoveCmdTime).TotalSeconds <= WD_MOVE_RECENT_WINDOW_SECONDS;

            if (moveRecente && stuckSeconds >= WD_MOVE_STUCK_SECONDS)
            {
                _lastWatchdogRestartTime = DateTime.Now;
                LogarMensagem($"[WATCHDOG] MOVE travado ({(int)stuckSeconds}s parado, MOVE recente). Reiniciando Bot...");
                ReiniciarBotPorTravamento();
                return;
            }

            // ---------
            // Gatilho B: Idle travado (amplo)
            // ---------
            bool semProgresso = _lastProgressCmdTime == DateTime.MinValue ||
                               (DateTime.Now - _lastProgressCmdTime).TotalSeconds >= WD_IDLE_NO_PROGRESS_SECONDS;

            if (stuckSeconds >= WD_IDLE_STUCK_SECONDS && semProgresso)
            {
                _lastWatchdogRestartTime = DateTime.Now;
                LogarMensagem($"[WATCHDOG] Idle travado ({(int)stuckSeconds}s parado, sem progresso). Reiniciando Bot...");
                ReiniciarBotPorTravamento();
                return;
            }
        }

        private void ResetWatchdogMovement()
        {
            _lastWdX = statsJogador.X;
            _lastWdZ = statsJogador.Z;
            _lastMovedTime = DateTime.Now;
        }

        private void ReiniciarBotPorTravamento()
        {
            if (_restartPending) return;
            _restartPending = true;

            // OFF imediato
            PararBotUI();

            // ON após pequeno delay
            restartTimer.Stop();
            restartTimer.Start();
        }

        private void RestartTimer_Tick(object sender, EventArgs e)
        {
            restartTimer.Stop();
            IniciarBotUI();
            _restartPending = false;
        }

        // --- INTERPRETAÇÃO DE PACOTES (RADAR, STATS, ETC) ---
        private void InterpretarComando(string linha)
        {
            try
            {
                string[] partes = linha.Split(';');
                string comando = partes[0];
                switch (comando)
                {
                    case "STATS":
                        statsJogador.Atualizar(partes);
                        _lastStatsTime = DateTime.Now; // watchdog

                        if (barHP != null) { barHP.Value = statsJogador.HP; barHP.Refresh(); }
                        if (barSP != null) { barSP.Value = statsJogador.SP; barSP.Refresh(); }
                        if (lblX != null) lblX.Text = "X: " + statsJogador.X;
                        if (lblY != null) lblY.Text = "Y: " + statsJogador.Y;
                        if (lblZ != null) lblZ.Text = "Z: " + statsJogador.Z;
                        if (btnConnect != null && btnConnect.Text != "Sincronizado") btnConnect.Text = "Sincronizado";
                        break;

                    case "RADAR":
                        if (partes.Length >= 2)
                        {
                            var ent = RadarSystem.ParsePacote(partes[1]);
                            entidadesRadar = ent;
                            if (visualRadar1 != null) visualRadar1.AtualizarEntidades(ent);
                            AtualizarLista(ent);
                        }
                        break;

                    case "BAG":
                        if (partes.Length >= 2 && listViewBag != null) AtualizarMochila(partes[1]);
                        break;

                    case "ERRO":
                        if (partes.Length >= 3 && partes[1] == "HARVEST")
                        {
                            botColeta.AdicionarBlacklist(partes[2]);
                            LogarMensagem($"[ALERTA] Falta ferramenta para {partes[2]}.");
                        }
                        break;

                    case "REQ_OK":
                        if (partes.Length >= 2)
                        {
                            LogarMensagem($"[SISTEMA] Requisito atendido: {partes[1]}");
                            botColeta.LimparBlacklist();
                        }
                        break;

                    case "BAG_FULL":
                        if (!indoParaBanco && !aguardandoDeposito && botMovimento.IsRodando)
                        {
                            indoParaBanco = true;
                            LogarMensagem("[SISTEMA] MOCHILA CHEIA! Indo para a Cabana...");
                            if (txtSafeList != null) EnviarComandoJogo("SAFE_LIST;" + txtSafeList.Text.Replace("\r\n", "~"));
                            botColeta.IsAtivo = false;
                            EnviarComandoJogo("RESET_MODES"); // libera montaria (reseta _modoColeta/_modoHunter no client)
                            if (txtBankX != null) EnviarComandoJogo($"MOVE;{txtBankX.Text};{txtBankZ.Text}");
                            ResetWatchdogMovement();
                        }
                        break;

                    case "BANK_FINISH":
                        LogarMensagem("[SISTEMA] Depósito Finalizado.");
                        aguardandoDeposito = false;
                        indoParaBanco = false;
                        botColeta.IsAtivo = true;
                        if (botMovimento.IsRodando)
                        {
                            string ret = botMovimento.ObterPontoRetomada(statsJogador.X, statsJogador.Z, 20.0f);
                            if (ret != null)
                            {
                                LogarMensagem("[NAVEGAÇÃO] Afastando-se...");
                                EnviarComandoJogo(ret);
                                ResetWatchdogMovement();
                            }
                        }
                        break;

                    case "COMBAT_FLAG":
                        if (partes.Length >= 2)
                        {
                            bool emCombate = (partes[1] == "ON");
                            botMovimento.IsEmCombate = emCombate;

                            // evita falso positivo ao entrar/ficar em combate
                            if (emCombate) ResetWatchdogMovement();

                            if (btnStartBot != null)
                            {
                                if (emCombate && btnStartBot.BackColor != Color.OrangeRed)
                                    btnStartBot.BackColor = Color.OrangeRed;
                                else if (!emCombate && btnStartBot.BackColor == Color.OrangeRed)
                                {
                                    if (botMovimento.IsRodando) btnStartBot.BackColor = Color.Red;
                                }
                            }
                        }
                        break;
                }
            }
            catch { }
        }

        private void AtualizarLista(List<RadarEntity> entidades)
        {
            if (listView1 == null) return;
            int indiceTopo = -1;
            if (listView1.TopItem != null) indiceTopo = listView1.TopItem.Index;
            var itensVisuais = RadarSystem.GerarItensLista(entidades);
            listView1.BeginUpdate();
            listView1.Items.Clear();
            listView1.Items.AddRange(itensVisuais.ToArray());
            listView1.EndUpdate();
            if (indiceTopo != -1 && indiceTopo < listView1.Items.Count) listView1.TopItem = listView1.Items[indiceTopo];
        }

        private void AtualizarMochila(string dadosBrutos)
        {
            if (listViewBag == null) return;
            int indiceTopo = -1;
            if (listViewBag.TopItem != null) indiceTopo = listViewBag.TopItem.Index;
            listViewBag.BeginUpdate();
            listViewBag.Items.Clear();
            string[] itens = dadosBrutos.Split('~');
            foreach (string item in itens)
            {
                string[] p = item.Split(':');
                if (p.Length >= 2)
                {
                    ListViewItem lvi = new ListViewItem(p[0]);
                    lvi.SubItems.Add(p[1]);
                    listViewBag.Items.Add(lvi);
                }
            }
            listViewBag.EndUpdate();
            if (indiceTopo != -1 && indiceTopo < listViewBag.Items.Count) listViewBag.TopItem = listViewBag.Items[indiceTopo];
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { if (watchdogTimer != null) watchdogTimer.Stop(); } catch { }
            try { if (restartTimer != null) restartTimer.Stop(); } catch { }

            if (rede != null) rede.Parar();
            if (enviadorUDP != null) enviadorUDP.Close();

            // Salva apenas as textboxes que você pediu
            SalvarTextboxesSelecionadas();

            // Mantém seus saves antigos (compatibilidade + banco do jeito que você quer)
            SalvarConfigBanco();
            SalvarListaLixo();
            SalvarListaComer();

            base.OnFormClosing(e);
        }

        private void Form1_Load(object sender, EventArgs e) { }

        // --- BOTÃO DE TESTE DE MONTARIA ---
        private void btnTestMount_Click(object sender, EventArgs e)
        {
            LogarMensagem("[TESTE] Enviando comando de Montar manual...");
            EnviarComandoJogo("TEST_MOUNT");
        }

        // --- BOTÃO DE TESTE DE CASA ---
        private void btnTestHome_Click(object sender, EventArgs e)
        {
            string x = txtBankX.Text.Trim();
            string z = txtBankZ.Text.Trim();
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(z))
            {
                LogarMensagem("[ERRO] Preencha as Coordenadas X e Z!");
                MessageBox.Show("Preencha as coordenadas X e Z!");
                return;
            }
            LogarMensagem($"[TESTE] Voltando para Casa ({x}, {z})...");
            EnviarComandoJogo($"RETURN_HOME;{x};{z}");
        }

        // --- BOTÃO DE PESCA ---
        private void btnStartFishing_Click(object sender, EventArgs e)
        {
            if (botMovimento.IsRodando)
            {
                MessageBox.Show("Pare o Bot Principal antes de iniciar a Pesca!");
                return;
            }

            if (isFishingRunning)
            {
                EnviarComandoJogo("FISHING;OFF");
                isFishingRunning = false;
                btnStartFishing.Text = "INICIAR PESCA";
                btnStartFishing.BackColor = Color.Navy;
                btnStartBot.Enabled = true;
                LogarMensagem("[PESCA] Sistema Desativado.");
            }
            else
            {
                string vara = txtRodName.Text.Trim();
                string isca = txtBaitName.Text.Trim();

                string local = cmbFishingSpot.Text.Trim();
                if (string.IsNullOrEmpty(local)) local = "River";

                if (string.IsNullOrEmpty(vara) || string.IsNullOrEmpty(isca))
                {
                    MessageBox.Show("Preencha Vara, Isca e Local!");
                    return;
                }

                string armaDef = (txtWeaponName != null) ? txtWeaponName.Text.Trim() : "";
                EnviarComandoJogo($"FISHING;ON;{vara};{isca};{local};{armaDef}");

                isFishingRunning = true;
                btnStartFishing.Text = "PARAR PESCA";
                btnStartFishing.BackColor = Color.Red;
                btnStartBot.Enabled = false;
                LogarMensagem($"[PESCA] Iniciando... Local: {local}, Isca: {isca}");
            }
        }

        // Placeholders
        private void txtWeaponName_TextChanged(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
    }
}
