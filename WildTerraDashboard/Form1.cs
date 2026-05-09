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

        private BotHealTrainer botCura = new BotHealTrainer();


        // SERVIÇOS
        private NetworkService rede;
        private PlayerStats statsJogador;
        private UdpClient enviadorUDP;
        private readonly int _portaEscutaDashboard;
        private readonly int _portaEnvioBot;


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
        private string _ultimaListaComerStatusEnviada = "";
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
        private const int HARVEST_LEASE_STALE_SECONDS = 35;
        private bool _harvestLeaseActive = false;
        private int _harvestLeaseWorldId = 0;
        private string _harvestLeaseName = null;
        private DateTime _harvestLeaseStartedAt = DateTime.MinValue;
        private DateTime _harvestLeaseLastSignalAt = DateTime.MinValue;


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
        private readonly string _dashboardProfileFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dashboard_profile_v1.json");

        private const int UDP_BASE_PORT = 8888;
        private const int UDP_MAX_INSTANCES = 10;


        public Form1() : this(0, 0)
        {

        }

        public Form1(int portaEscutaDashboard, int portaEnvioBot)
        {
            ResolverPortasDashboard(portaEscutaDashboard, portaEnvioBot, out _portaEscutaDashboard, out _portaEnvioBot);






            InitializeComponent();


            // UI responsiva para diferentes resoluções/escala do Windows:
            // habilita barras de rolagem quando a tela/escala não comporta o layout completo.
            this.AutoScroll = true;
            // Define o "tamanho ideal" do conteúdo (baseado no tamanho inicial do Designer).
            // Assim o WinForms sabe quando exibir scrollbars.
            this.AutoScrollMinSize = this.Size;

            // 0. Carrega configurações ANTES de ligar eventos de TextChanged (evita disparar lógica)
            CarregarConfigBanco();           // (banco fica como está)
            CarregarListaLixo();             // compat (txtDropList)
            CarregarListaComer();            // compat (txtAutoEat)
            CarregarListaComerStatus();      // compat (txtAutoEatStatus)
            CarregarTextboxesSelecionadas(); // preferencial (sobrescreve se existir)
            CarregarDashboardProfileV1();    // consolidado v1 (sobrescreve legados se existir)

            // 1. INICIALIZAÇÃO DE DADOS E REDE (Primeiro)
            statsJogador = new PlayerStats();
            enviadorUDP = new UdpClient();
            rede = new NetworkService(_portaEscutaDashboard);

            // 2. INICIALIZAÇÃO DAS CLASSES DE LÓGICA (Crítico: Antes de ligar eventos)
            botMovimento = new BotMovement();
            gravadorRota = new RouteRecorder();
            botColeta = new BotHarvest();
            botMontaria = new BotMount();
            botCaçador = new BotHunter();
            SincronizarConfiguracoesCarregadasComBots();

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
                if (botCaçador != null) botCaçador.IsAtivo = chkAtivarHunt.Checked;
                if (_isLoadingUiText) return;
                LogarMensagem(string.Format(
                    Properties.Resources.Form1LogHunterStatusFormat,
                    botCaçador.IsAtivo ? "ON" : "OFF"));
            };

            if (chkUseMount != null) chkUseMount.CheckedChanged += ChkUseMount_CheckedChanged;


            if (chkHealFollowTopTarget != null)
            {
                chkHealFollowTopTarget.CheckedChanged += chkHealFollowTopTarget_CheckedChanged;
                UpdateHealFollowControlsState();
            }



            if (btnSaveSafe != null) btnSaveSafe.Click += BtnSaveSafe_Click;
            if (btnLoadSafe != null) btnLoadSafe.Click += BtnLoadSafe_Click;


            InitializeTamingModule();
            InitializeTrainingModule();
            InitializeInspectModule();


        }


        private static void ResolverPortasDashboard(int portaEscutaSolicitada, int portaEnvioSolicitada, out int portaEscutaResolvida, out int portaEnvioResolvida)
        {
            if (portaEscutaSolicitada > 0 && portaEnvioSolicitada > 0)
            {
                portaEscutaResolvida = portaEscutaSolicitada;
                portaEnvioResolvida = portaEnvioSolicitada;
                return;
            }

            foreach (var par in EnumerarParesPortaDashboard())


            {
                if (PortaUdpDisponivel(par[0]))
                {
                    portaEscutaResolvida = par[0];
                    portaEnvioResolvida = par[1];
                    return;
                }
            }

            portaEscutaResolvida = UDP_BASE_PORT;
            portaEnvioResolvida = UDP_BASE_PORT + 1;
        }

        private static IEnumerable<int[]> EnumerarParesPortaDashboard()
        {
            for (int indiceInstancia = 0; indiceInstancia < UDP_MAX_INSTANCES; indiceInstancia++)
            {
                int portaEscuta = UDP_BASE_PORT + (indiceInstancia * 2);
                int portaEnvio = portaEscuta + 1;
                yield return new[] { portaEscuta, portaEnvio };
            }


        }

        private static bool PortaUdpDisponivel(int porta)
        {
            UdpClient teste = null;
            try
            {
                teste = new UdpClient(porta);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (teste != null)
                    teste.Close();
            }
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
                if (chkAtivarHunt != null) dict["chkAtivarHunt"] = chkAtivarHunt.Checked ? "true" : "false";
                if (txtAutoEat != null) dict["txtAutoEat"] = txtAutoEat.Text ?? "";
                var txtAutoEatStatus = GetAutoEatStatusTextBox();
                if (txtAutoEatStatus != null) dict["txtAutoEatStatus"] = txtAutoEatStatus.Text ?? "";
                if (numEatThreshold != null) dict["numEatThreshold"] = numEatThreshold.Value.ToString();
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
                if (chkAtivarHunt != null && dict.TryGetValue("chkAtivarHunt", out var v5b))
                {
                    if (bool.TryParse(v5b, out var huntEnabled))
                        chkAtivarHunt.Checked = huntEnabled;
                }
                if (txtAutoEat != null && dict.TryGetValue("txtAutoEat", out var v6)) txtAutoEat.Text = v6 ?? "";
                var txtAutoEatStatus = GetAutoEatStatusTextBox();
                if (txtAutoEatStatus != null && dict.TryGetValue("txtAutoEatStatus", out var v6b)) txtAutoEatStatus.Text = v6b ?? "";
                if (numEatThreshold != null && dict.TryGetValue("numEatThreshold", out var v6c))
                {
                    if (decimal.TryParse(v6c, out var eatThreshold))
                    {
                        if (eatThreshold < numEatThreshold.Minimum) eatThreshold = numEatThreshold.Minimum;
                        if (eatThreshold > numEatThreshold.Maximum) eatThreshold = numEatThreshold.Maximum;
                        numEatThreshold.Value = eatThreshold;
                    }
                }
                if (txtRodName != null && dict.TryGetValue("txtRodName", out var v7)) txtRodName.Text = v7 ?? "";
                if (txtBaitName != null && dict.TryGetValue("txtBaitName", out var v8)) txtBaitName.Text = v8 ?? "";
            }
            catch { }
            finally
            {
                _isLoadingUiText = false;
            }
        }

        private void SalvarDashboardProfileV1()
        {
            try
            {
                var profile = new DashboardProfileV1
                {
                    Version = 1,
                    SavedAtUtc = DateTime.UtcNow.ToString("o"),
                    General = new DashboardGeneralSection
                    {
                        WeaponName = txtWeaponName != null ? (txtWeaponName.Text ?? "") : "",
                        EnableHunt = chkAtivarHunt != null && chkAtivarHunt.Checked,
                        UseMount = chkUseMount != null && chkUseMount.Checked
                    },
                    Bank = new DashboardBankSection
                    {
                        X = txtBankX != null ? (txtBankX.Text ?? "") : "",
                        Z = txtBankZ != null ? (txtBankZ.Text ?? "") : "",
                        Name = txtBankName != null ? (txtBankName.Text ?? "") : ""
                    },
                    Harvest = new DashboardHarvestSection
                    {
                        Items = txtListaColeta != null ? (txtListaColeta.Text ?? "") : "",
                        DropList = txtDropList != null ? (txtDropList.Text ?? "") : "",
                        SafeList = txtSafeList != null ? (txtSafeList.Text ?? "") : "",
                        Enabled = chkAtivarColeta != null && chkAtivarColeta.Checked
                    },
                    Hunt = new DashboardHuntSection
                    {
                        Mobs = txtListaMobs != null ? (txtListaMobs.Text ?? "") : "",
                        Enabled = chkAtivarHunt != null && chkAtivarHunt.Checked
                    },
                    Food = new DashboardFoodSection
                    {
                        EatList = txtAutoEat != null ? (txtAutoEat.Text ?? "") : "",
                        EatStatusList = GetAutoEatStatusTextBox() != null ? (GetAutoEatStatusTextBox().Text ?? "") : "",
                        Threshold = numEatThreshold != null ? Decimal.ToInt32(numEatThreshold.Value) : 0
                    },
                    Fishing = new DashboardFishingSection
                    {
                        RodName = txtRodName != null ? (txtRodName.Text ?? "") : "",
                        BaitName = txtBaitName != null ? (txtBaitName.Text ?? "") : ""
                    }
                };

                var ser = new JavaScriptSerializer();
                var json = ser.Serialize(profile);
                File.WriteAllText(_dashboardProfileFile, json, Encoding.UTF8);
            }
            catch { }
        }

        private void CarregarDashboardProfileV1()
        {
            try
            {
                if (!File.Exists(_dashboardProfileFile)) return;

                var json = File.ReadAllText(_dashboardProfileFile, Encoding.UTF8);
                var ser = new JavaScriptSerializer();
                var profile = ser.Deserialize<DashboardProfileV1>(json);
                if (profile == null) return;
                if (profile.Version != 1) return;

                _isLoadingUiText = true;

                if (profile.General != null)
                {
                    if (txtWeaponName != null) txtWeaponName.Text = profile.General.WeaponName ?? "";
                    if (chkAtivarHunt != null) chkAtivarHunt.Checked = profile.General.EnableHunt;
                    if (chkUseMount != null) chkUseMount.Checked = profile.General.UseMount;
                }

                if (profile.Bank != null)
                {
                    if (txtBankX != null) txtBankX.Text = profile.Bank.X ?? "";
                    if (txtBankZ != null) txtBankZ.Text = profile.Bank.Z ?? "";
                    if (txtBankName != null) txtBankName.Text = profile.Bank.Name ?? "";
                }

                if (profile.Harvest != null)
                {
                    if (txtListaColeta != null) txtListaColeta.Text = profile.Harvest.Items ?? "";
                    if (txtDropList != null) txtDropList.Text = profile.Harvest.DropList ?? "";
                    if (txtSafeList != null) txtSafeList.Text = profile.Harvest.SafeList ?? "";
                    if (chkAtivarColeta != null) chkAtivarColeta.Checked = profile.Harvest.Enabled;
                }

                if (profile.Hunt != null)
                {
                    if (txtListaMobs != null) txtListaMobs.Text = profile.Hunt.Mobs ?? "";
                    if (chkAtivarHunt != null) chkAtivarHunt.Checked = profile.Hunt.Enabled;
                }

                if (profile.Food != null)
                {
                    if (txtAutoEat != null) txtAutoEat.Text = profile.Food.EatList ?? "";
                    var txtAutoEatStatus = GetAutoEatStatusTextBox();
                    if (txtAutoEatStatus != null) txtAutoEatStatus.Text = profile.Food.EatStatusList ?? "";
                    if (numEatThreshold != null)
                    {
                        decimal threshold = profile.Food.Threshold;
                        if (threshold < numEatThreshold.Minimum) threshold = numEatThreshold.Minimum;
                        if (threshold > numEatThreshold.Maximum) threshold = numEatThreshold.Maximum;
                        numEatThreshold.Value = threshold;
                    }
                }

                if (profile.Fishing != null)
                {
                    if (txtRodName != null) txtRodName.Text = profile.Fishing.RodName ?? "";
                    if (txtBaitName != null) txtBaitName.Text = profile.Fishing.BaitName ?? "";
                }
            }
            catch { }
            finally
            {
                _isLoadingUiText = false;
            }
        }

        private void SincronizarConfiguracoesCarregadasComBots()
        {
            if (botColeta != null && txtListaColeta != null)
                botColeta.DefinirLista(txtListaColeta.Text);

            if (botCaçador != null && txtListaMobs != null)
                botCaçador.DefinirLista(txtListaMobs.Text);

            if (botColeta != null && chkAtivarColeta != null)
                botColeta.IsAtivo = chkAtivarColeta.Checked;

            if (botCaçador != null && chkAtivarHunt != null)
                botCaçador.IsAtivo = chkAtivarHunt.Checked;

            if (botMontaria != null && chkUseMount != null)
                botMontaria.AtualizarConfig(chkUseMount.Checked);
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

        private void SalvarListaComerStatus()
        {
            try
            {
                var txt = GetAutoEatStatusTextBox();
                if (txt != null) File.WriteAllText("lista_comer_status.txt", txt.Text ?? "");
            }
            catch { }
        }

        private void CarregarListaComerStatus()
        {
            try
            {
                var txt = GetAutoEatStatusTextBox();
                if (txt != null && File.Exists("lista_comer_status.txt"))
                    txt.Text = File.ReadAllText("lista_comer_status.txt");
            }
            catch { }
        }

        // --- BOTÕES DE LISTAS ---
        private void BtnSaveSafe_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = Properties.Resources.Form1DialogFilterSafeList;
            sfd.FileName = "lista_segura.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(sfd.FileName, txtSafeList.Text);
                    MessageBox.Show(Properties.Resources.Form1MessageSaved);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Properties.Resources.Form1MessageErrorFormat, ex.Message));
                }
            }
        }

        private void BtnLoadSafe_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = Properties.Resources.Form1DialogFilterSafeList;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtSafeList.Text = File.ReadAllText(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Properties.Resources.Form1MessageErrorFormat, ex.Message));
                }
            }
        }

        private void TxtListaColeta_TextChanged(object sender, EventArgs e)
        {
            if (_isLoadingUiText) return;
            if (botColeta != null)
            {
                botColeta.DefinirLista(txtListaColeta.Text);
                // Quando a pesca roda com o BOT principal desligado, o BotTimer não executa.
                // Então a mensagem HARVEST_LIST não é enviada automaticamente para o jogo.
                // Se estivermos pescando, sincronize imediatamente a lista de coleta.
                try
                {
                    if (isFishingRunning)
                    {
                        string atual = txtListaColeta.Text ?? "";
                        if (atual != _ultimaListaColetaEnviada)
                        {
                            string payload = BuildListPayload(atual);
                            EnviarComandoJogo("HARVEST_LIST;" + payload);
                            _ultimaListaColetaEnviada = atual;
                            LogarMensagem(string.Format(
                                Properties.Resources.Form1LogFishingHarvestListSyncedFormat,
                                botColeta.ContarItensLista(atual)));


                        }

                    }

                }
                catch { }

            }

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
                LogarMensagem(string.Format(
                    Properties.Resources.Form1LogHarvestStatusFormat,
                    botColeta.IsAtivo ? "ON" : "OFF"));
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

            // 2b. Envia Lista de Comida de Status
            SyncAutoEatStatusList();

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
                        LogarMensagem(Properties.Resources.Form1LogReachedCabinStartingDeposit);
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
            if (HasActiveHarvestLease())
            {
                return;
            }

            HarvestDecision decisaoColeta = botColeta.VerificarRadar(entidadesRadar);
            if (decisaoColeta != null)
            {
                string fullHarvestCmd = $"HARVEST;{decisaoColeta.DesiredName};{nomeArma};{decisaoColeta.Entity?.WorldId ?? 0}";

                if (_lastHarvestCmdSent != null &&
                    string.Equals(_lastHarvestCmdSent, fullHarvestCmd, StringComparison.OrdinalIgnoreCase) &&
                    (DateTime.Now - _lastHarvestSentTime).TotalMilliseconds < HARVEST_SEND_COOLDOWN_MS)
                {
                    return;
                }

                _lastHarvestCmdSent = fullHarvestCmd;
                _lastHarvestSentTime = DateTime.Now;
                BeginHarvestLease(decisaoColeta.Entity?.WorldId ?? 0, decisaoColeta.DesiredName);

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
                btnStartBot.Text = Properties.Resources.Form1ButtonStartBot;
                btnStartBot.BackColor = Color.Green;
            }

            botTimer.Stop();
            if (watchdogTimer != null) watchdogTimer.Stop();

            ClearHarvestLease();
            EnviarComandoJogo("BOT_STATUS;OFF");
            LogarMensagem(Properties.Resources.Form1LogBotStopped);

            if (btnStartFishing != null) btnStartFishing.Enabled = true;
        }

        private void IniciarBotUI()
        {
            botMovimento.Iniciar();
            if (!botMovimento.IsRodando) return;

            if (btnStartBot != null)
            {
                btnStartBot.Text = Properties.Resources.Form1ButtonStopBot;
                btnStartBot.BackColor = Color.Red;
            }

            botTimer.Start();
            if (watchdogTimer != null) watchdogTimer.Start();

            ClearHarvestLease();
            _ultimoEstadoMountEnviado = !botMontaria.IsAtivo;
            EnviarComandoJogo("BOT_STATUS;ON");
            LogarMensagem(Properties.Resources.Form1LogBotStarted);

            if (btnStartFishing != null) btnStartFishing.Enabled = false;

            // reseta watchdog
            ResetWatchdogMovement();
            _lastMoveCmdTime = DateTime.Now;
            _lastWorkCmdTime = DateTime.Now;
            _lastProgressCmdTime = DateTime.Now;
        }

        private void btnStartBot_Click(object sender, EventArgs e)
        {
            if (IsTrainingModeActive) { MessageBox.Show(Properties.Resources.Form1MessageStopTrainingBeforeMainBot); return; }
            if (isFishingRunning) { MessageBox.Show(Properties.Resources.Form1MessageStopFishingBeforeMainBot); return; }
            if (gravadorRota.IsGravando) { MessageBox.Show(Properties.Resources.Form1MessageStopRecordingBeforeMainBot); return; }

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
                btnRecordRoute.Text = Properties.Resources.Form1ButtonRecordRoute;
                btnRecordRoute.BackColor = Color.Orange;
                btnRecordRoute.ForeColor = Color.Black;
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = Properties.Resources.Form1DialogFilterRouteFile;
                sfd.FileName = "nova_rota.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    gravadorRota.IniciarGravacao(sfd.FileName);
                    recordTimer.Start();
                    btnRecordRoute.Text = Properties.Resources.Form1ButtonStopRecording;
                    btnRecordRoute.BackColor = Color.Red;
                    btnRecordRoute.ForeColor = Color.White;
                }
            }
        }

        private void btnLoadRoute_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = Properties.Resources.Form1DialogFilterTextFiles;
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
            sfd.Filter = Properties.Resources.Form1DialogFilterTextFiles;
            sfd.FileName = "lista_coleta.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(sfd.FileName, txtListaColeta.Text);
                    MessageBox.Show(Properties.Resources.Form1MessageSaved);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Properties.Resources.Form1MessageErrorFormat, ex.Message));
                }
            }
        }

        private void btnLoadList_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = Properties.Resources.Form1DialogFilterTextFiles;
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
                    MessageBox.Show(string.Format(Properties.Resources.Form1MessageErrorFormat, ex.Message));
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

        private TextBox GetAutoEatStatusTextBox()
        {
            return FindControl<TextBox>("txtAutoEatStatus");
        }

        private void SyncAutoEatStatusList(bool force = false)
        {
            try
            {
                var txt = GetAutoEatStatusTextBox();
                if (txt == null) return;

                string atual = txt.Text ?? "";
                if (!force && string.Equals(atual, _ultimaListaComerStatusEnviada, StringComparison.Ordinal))
                    return;

                string payload = BuildListPayload(atual);
                EnviarComandoJogo("EAT_STATUS_LIST;" + payload);
                _ultimaListaComerStatusEnviada = atual;
            }
            catch { }
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
                    if (!comando.StartsWith("HARVEST;", StringComparison.OrdinalIgnoreCase))
                        _lastProgressCmdTime = DateTime.Now;
                }

                byte[] dados = Encoding.ASCII.GetBytes(comando);
                enviadorUDP.Send(dados, dados.Length, "127.0.0.1", _portaEnvioBot);

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

        private bool HasActiveHarvestLease()
        {
            if (!_harvestLeaseActive) return false;

            double ageSeconds = (DateTime.Now - _harvestLeaseLastSignalAt).TotalSeconds;
            if (ageSeconds <= HARVEST_LEASE_STALE_SECONDS) return true;

            LogarMensagem($"[HARVEST] Lease expirado por silêncio ({(int)ageSeconds}s). Liberando nova tentativa.");
            ClearHarvestLease();
            return false;
        }

        private void BeginHarvestLease(int worldId, string desiredName)
        {
            _harvestLeaseActive = true;
            _harvestLeaseWorldId = worldId;
            _harvestLeaseName = desiredName;
            _harvestLeaseStartedAt = DateTime.Now;
            _harvestLeaseLastSignalAt = DateTime.Now;
        }

        private void RefreshHarvestLease(int worldId, string desiredName = null)
        {
            if (!_harvestLeaseActive)
            {
                BeginHarvestLease(worldId, desiredName);
                return;
            }

            if (worldId > 0) _harvestLeaseWorldId = worldId;
            if (!string.IsNullOrWhiteSpace(desiredName)) _harvestLeaseName = desiredName;
            _harvestLeaseLastSignalAt = DateTime.Now;
        }

        private void ClearHarvestLease()
        {
            _harvestLeaseActive = false;
            _harvestLeaseWorldId = 0;
            _harvestLeaseName = null;
            _harvestLeaseStartedAt = DateTime.MinValue;
            _harvestLeaseLastSignalAt = DateTime.MinValue;
        }

        private void Rede_OnIniciado()
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (lstLog != null) lstLog.Items.Add(string.Format(Properties.Resources.Form1LogUdpStartedFormat, _portaEscutaDashboard, _portaEnvioBot));
                this.Text = string.Format(Properties.Resources.Form1WindowTitleWithPortsFormat, _portaEscutaDashboard, _portaEnvioBot);
            });
        }

        private void Rede_OnErro(string msg)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (lstLog != null) lstLog.Items.Add(string.Format(Properties.Resources.Form1LogErrorFormat, msg));
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
                        if (lblX != null) lblX.Text = string.Format(Properties.Resources.Form1CoordXFormat, statsJogador.X);
                        if (lblY != null) lblY.Text = string.Format(Properties.Resources.Form1CoordYFormat, statsJogador.Y);
                        if (lblZ != null) lblZ.Text = string.Format(Properties.Resources.Form1CoordZFormat, statsJogador.Z);
                        if (btnConnect != null && btnConnect.Text != Properties.Resources.Form1ButtonSynced) btnConnect.Text = Properties.Resources.Form1ButtonSynced;
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
                            ClearHarvestLease();
                            botColeta.AdicionarBlacklist(partes[2]);
                            LogarMensagem(string.Format(Properties.Resources.Form1LogMissingToolFormat, partes[2]));
                        }
                        break;

                    case "REQ_OK":
                        if (partes.Length >= 2)
                        {
                            LogarMensagem(string.Format(Properties.Resources.Form1LogRequirementMetFormat, partes[1]));
                            botColeta.LimparBlacklist();
                        }
                        break;

                    case "HARVEST_STATE":
                        {
                            string state = partes.Length >= 2 ? partes[1] : "?";
                            string nome = partes.Length >= 3 ? partes[2] : _harvestLeaseName;
                            int worldId = 0;
                            if (partes.Length >= 4) int.TryParse(partes[3], out worldId);
                            RefreshHarvestLease(worldId, nome);
                            _lastProgressCmdTime = DateTime.Now;
                            LogarMensagem($"[HARVEST] Estado={state} alvo={nome} worldId={worldId}");
                        }
                        break;

                    case "HARVEST_DONE":
                        {
                            string status = partes.Length >= 2 ? partes[1] : "?";
                            string reason = partes.Length >= 3 ? partes[2] : "Unknown";
                            string nome = partes.Length >= 4 ? partes[3] : _harvestLeaseName;
                            int worldId = 0;
                            if (partes.Length >= 5) int.TryParse(partes[4], out worldId);
                            _lastProgressCmdTime = DateTime.Now;
                            ClearHarvestLease();
                            LogarMensagem($"[HARVEST] Fim status={status} motivo={reason} alvo={nome} worldId={worldId}");
                            if (worldId > 0)
                            {
                                if (string.Equals(status, "Failed", StringComparison.OrdinalIgnoreCase))
                                    botColeta.AdicionarBlacklistWorldId(worldId, null, reason);
                                else if (string.Equals(status, "Success", StringComparison.OrdinalIgnoreCase))
                                    botColeta.AdicionarBlacklistWorldId(worldId, TimeSpan.FromSeconds(4), "RecentSuccess");
                            }
                        }
                        break;

                    case "BAG_FULL":
                        if (!indoParaBanco && !aguardandoDeposito && botMovimento.IsRodando)
                        {
                            ClearHarvestLease();
                            indoParaBanco = true;
                            LogarMensagem(Properties.Resources.Form1LogBagFull);
                            if (txtSafeList != null) EnviarComandoJogo("SAFE_LIST;" + txtSafeList.Text.Replace("\r\n", "~"));
                            botColeta.IsAtivo = false;
                            EnviarComandoJogo("RESET_MODES"); // libera montaria (reseta _modoColeta/_modoHunter no client)
                            if (txtBankX != null) EnviarComandoJogo($"MOVE;{txtBankX.Text};{txtBankZ.Text}");
                            ResetWatchdogMovement();
                        }
                        break;

                    case "BANK_FINISH":
                        ClearHarvestLease();
                        LogarMensagem(Properties.Resources.Form1LogDepositFinished);
                        aguardandoDeposito = false;
                        indoParaBanco = false;
                        botColeta.IsAtivo = true;
                        if (botMovimento.IsRodando)
                        {
                            string ret = botMovimento.ObterPontoRetomada(statsJogador.X, statsJogador.Z, 20.0f);
                            if (ret != null)
                            {
                                LogarMensagem(Properties.Resources.Form1LogNavigationMovingAway);
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

                    case "TAMING_LOG":
                        if (partes.Length >= 2)
                        {
                            string msg = string.Join(";", partes.Skip(1));
                            LogarMensagem(msg);
                        }
                        break;



                    case "INSPECT_BEGIN":
                        HandleInspectBegin(partes);
                        break;

                    case "INSPECT_CHUNK":
                        HandleInspectChunk(partes);
                        break;

                    case "INSPECT_END":
                        HandleInspectEnd(partes);
                        break;

                    case "INSPECT_ERROR":
                        HandleInspectError(partes);
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

            TryStopTamingForShutdown();
            TryStopTrainingForShutdown();

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
            SalvarListaComerStatus();
            SaveTrainingUi();
            SalvarDashboardProfileV1();

            base.OnFormClosing(e);
        }

        private class DashboardProfileV1
        {
            public int Version { get; set; } = 1;
            public string SavedAtUtc { get; set; } = "";
            public DashboardGeneralSection General { get; set; } = new DashboardGeneralSection();
            public DashboardBankSection Bank { get; set; } = new DashboardBankSection();
            public DashboardHarvestSection Harvest { get; set; } = new DashboardHarvestSection();
            public DashboardHuntSection Hunt { get; set; } = new DashboardHuntSection();
            public DashboardFoodSection Food { get; set; } = new DashboardFoodSection();
            public DashboardFishingSection Fishing { get; set; } = new DashboardFishingSection();
        }

        private class DashboardGeneralSection
        {
            public string WeaponName { get; set; } = "";
            public bool EnableHunt { get; set; }
            public bool UseMount { get; set; }
        }

        private class DashboardBankSection
        {
            public string X { get; set; } = "";
            public string Z { get; set; } = "";
            public string Name { get; set; } = "";
        }

        private class DashboardHarvestSection
        {
            public string Items { get; set; } = "";
            public string DropList { get; set; } = "";
            public string SafeList { get; set; } = "";
            public bool Enabled { get; set; }
        }

        private class DashboardHuntSection
        {
            public string Mobs { get; set; } = "";
            public bool Enabled { get; set; }
        }

        private class DashboardFoodSection
        {
            public string EatList { get; set; } = "";
            public string EatStatusList { get; set; } = "";
            public int Threshold { get; set; }
        }

        private class DashboardFishingSection
        {
            public string RodName { get; set; } = "";
            public string BaitName { get; set; } = "";
        }

        private void Form1_Load(object sender, EventArgs e) { }

        // --- BOTÃO DE TESTE DE MONTARIA ---
        private void btnTestMount_Click(object sender, EventArgs e)
        {
            LogarMensagem(Properties.Resources.Form1LogSendingManualMountCommand);
            EnviarComandoJogo("TEST_MOUNT");
        }

        // --- BOTÃO DE TESTE DE CASA ---
        private void btnTestHome_Click(object sender, EventArgs e)
        {
            string x = txtBankX.Text.Trim();
            string z = txtBankZ.Text.Trim();
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(z))
            {
                LogarMensagem(Properties.Resources.Form1LogFillXAndZ);
                MessageBox.Show(Properties.Resources.Form1MessageFillXAndZ);
                return;
            }
            LogarMensagem(string.Format(Properties.Resources.Form1LogReturningHomeFormat, x, z));
            EnviarComandoJogo($"RETURN_HOME;{x};{z}");
        }

        // --- BOTÃO DE PESCA ---
        private void btnStartFishing_Click(object sender, EventArgs e)
        {
            if (IsTrainingModeActive)
            {
                MessageBox.Show(Properties.Resources.Form1MessageStopTrainingBeforeFishing);
                return;
            }

            if (botMovimento.IsRodando)
            {
                MessageBox.Show(Properties.Resources.Form1MessageStopMainBotBeforeFishing);
                return;
            }

            if (isFishingRunning)
            {
                EnviarComandoJogo("FISHING;OFF");
                isFishingRunning = false;
                btnStartFishing.Text = Properties.Resources.Form1ButtonStartFishing;
                btnStartFishing.BackColor = Color.Navy;
                btnStartBot.Enabled = true;
                LogarMensagem(Properties.Resources.Form1LogFishingDisabled);
            }
            else
            {
                string vara = txtRodName.Text.Trim();
                string isca = txtBaitName.Text.Trim();

                string local = cmbFishingSpot.Text.Trim();
                if (string.IsNullOrEmpty(local)) local = "River";

                if (string.IsNullOrEmpty(vara) || string.IsNullOrEmpty(isca))
                {
                    MessageBox.Show(Properties.Resources.Form1MessageFillRodBaitAndLocation);
                    return;
                }

                string armaDef = (txtWeaponName != null) ? txtWeaponName.Text.Trim() : "";
                EnviarComandoJogo($"FISHING;ON;{vara};{isca};{local};{armaDef}");
                // Sincroniza imediatamente a lista de coleta no jogo (mesmo com BOT principal desligado).
                // Sem isso, o UDPRunner pode ficar com itensColeta vazio durante a pesca.
                try
                {

                    if (txtListaColeta != null)
                    {
                        string atual = txtListaColeta.Text ?? "";
                        if (atual != _ultimaListaColetaEnviada)
                        {
                            string payload = BuildListPayload(atual);
                            EnviarComandoJogo("HARVEST_LIST;" + payload);
                            _ultimaListaColetaEnviada = atual;
                            int n = (botColeta != null) ? botColeta.ContarItensLista(atual) : 0;
                            LogarMensagem(string.Format(Properties.Resources.Form1LogFishingHarvestListSentFormat, n));


                        }

                    }
                }

                catch { }

                isFishingRunning = true;
                btnStartFishing.Text = Properties.Resources.Form1ButtonStopFishing;
                btnStartFishing.BackColor = Color.Red;
                btnStartBot.Enabled = false;
                LogarMensagem(string.Format(Properties.Resources.Form1LogFishingStartingFormat, local, isca));
            }
        }

        // Placeholders
        private void txtWeaponName_TextChanged(object sender, EventArgs e) { }


        private void chkHealFollowTopTarget_CheckedChanged(object sender, EventArgs e)
        {
            UpdateHealFollowControlsState();
        }

        private void UpdateHealFollowControlsState()
        {
            bool enabled = chkHealFollowTopTarget != null && chkHealFollowTopTarget.Checked;

            if (lblHealFollowSkill != null) lblHealFollowSkill.Enabled = enabled;
            if (txtHealFollowSkill != null) txtHealFollowSkill.Enabled = enabled;

            if (lblHealFollowTargetHpPct != null) lblHealFollowTargetHpPct.Enabled = enabled;
            if (numHealFollowTargetHpPct != null) numHealFollowTargetHpPct.Enabled = enabled;

            if (lblHealFollowDistance != null) lblHealFollowDistance.Enabled = enabled;
            if (numHealFollowDistance != null) numHealFollowDistance.Enabled = enabled;

            if (lblHealSelfRecoveryItems != null) lblHealSelfRecoveryItems.Enabled = enabled;
            if (txtHealSelfRecoveryItems != null) txtHealSelfRecoveryItems.Enabled = enabled;

            if (lblHealSelfRecoveryHpPct != null) lblHealSelfRecoveryHpPct.Enabled = enabled;
            if (numHealSelfRecoveryHpPct != null) numHealSelfRecoveryHpPct.Enabled = enabled;

            if (lblHealSelfRecoveryResumeHpPct != null) lblHealSelfRecoveryResumeHpPct.Enabled = enabled;
            if (numHealSelfRecoveryResumeHpPct != null) numHealSelfRecoveryResumeHpPct.Enabled = enabled;
        }



        // ======================
        // ===== MODO CURA ======
        // ======================
        // Requer que você crie os controles no Designer com estes nomes:
        // txtHealWeaponName (TextBox)
        // cmbHealTargetMode (ComboBox) itens: PET, SELF, PLAYER_BY_NAME
        // numHealRadius (NumericUpDown)
        // txtHealSkills (TextBox Multiline) -> 1 skill por linha (prioridade fixa)
        // txtHealTargetNames (TextBox Multiline) -> 1 nome por linha (apenas para PLAYER_BY_NAME)

        // chkHealFollowTopTarget (CheckBox)
        // txtHealFollowSkill (TextBox)
        // numHealFollowTargetHpPct (NumericUpDown)
        // numHealFollowDistance (NumericUpDown)
        // txtHealSelfRecoveryItems (TextBox Multiline)
        // numHealSelfRecoveryHpPct (NumericUpDown)
        // numHealSelfRecoveryResumeHpPct (NumericUpDown)



        // btnHealTrain (Button)
        // lblHealStatus (Label) opcional
        private void btnHealTrain_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsTrainingModeActive)
                {
                    MessageBox.Show(Properties.Resources.Form1MessageStopTrainingBeforeHeal);
                    return;
                }

                if (!botCura.IsAtivo)
                {
                    // Modo independente (não rodar junto com bot/pesca)
                    ClearHarvestLease();
                    EnviarComandoJogo("BOT_STATUS;OFF");
                    EnviarComandoJogo("FISHING;OFF");

                    // IMPORTANTe: com BOT_STATUS=OFF o BotTimer pode não rodar (dependendo do seu fluxo),
                    // então a lista de AutoEat/threshold pode não ser enviada automaticamente.
                    // Sincronize imediatamente para que o treino de cura possa se auto-alimentar.
                    try
                    {
                        if (txtAutoEat != null)
                        {
                            string eatTxt = txtAutoEat.Text ?? "";
                            if (!string.IsNullOrWhiteSpace(eatTxt))
                            {
                                EnviarComandoJogo("EAT_LIST;" + eatTxt.Replace("\r\n", "~").Replace("\n", "~"));
                                _ultimaListaComerEnviada = eatTxt;
                                LogarMensagem(Properties.Resources.Form1LogHealAutoEatSynced);
                            }
                        }
                        SyncAutoEatStatusList(force: true);
                        if (numEatThreshold != null)
                        {
                            int val = (int)numEatThreshold.Value;
                            EnviarComandoJogo($"EAT_THRESHOLD;{val}");
                            _ultimoThresholdEnviado = val;
                        }
                    }
                    catch { }


                    string weapon = (txtHealWeaponName?.Text ?? "").Trim();
                    if (string.IsNullOrWhiteSpace(weapon))
                    {
                        if (lstLog != null) lstLog.Items.Add(Properties.Resources.Form1LogHealWeaponNameRequired);
                        return;
                    }

                    string mode = (cmbHealTargetMode?.Text ?? "PET").Trim();
                    int radius = 18;
                    try { radius = (int)(numHealRadius?.Value ?? 18); } catch { }


                    bool followHealEnabled = false;
                    try { followHealEnabled = chkHealFollowTopTarget != null && chkHealFollowTopTarget.Checked; } catch { }

                    string followSkill = (txtHealFollowSkill?.Text ?? "").Trim();
                    decimal followTargetHpPct = 75;
                    decimal followDistance = 4.5m;
                    string selfRecoveryItems = (txtHealSelfRecoveryItems?.Text ?? "");
                    decimal selfRecoveryHpPct = 40;
                    decimal selfRecoveryResumeHpPct = 55;

                    try { followTargetHpPct = numHealFollowTargetHpPct?.Value ?? 75; } catch { }
                    try { followDistance = numHealFollowDistance?.Value ?? 4.5m; } catch { }
                    try { selfRecoveryHpPct = numHealSelfRecoveryHpPct?.Value ?? 40; } catch { }
                    try { selfRecoveryResumeHpPct = numHealSelfRecoveryResumeHpPct?.Value ?? 55; } catch { }

                    if (followHealEnabled)
                    {
                        if (!string.Equals(mode, "PLAYER_BY_NAME", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show(Properties.Resources.Form1MessageFollowHealRequiresPlayerByName);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(txtHealTargetNames?.Text))
                        {
                            MessageBox.Show(Properties.Resources.Form1MessageFollowHealRequiresTargetName);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(followSkill))
                        {
                            MessageBox.Show(Properties.Resources.Form1MessageFollowHealRequiresMainSkill);
                            return;
                        }

                        if (selfRecoveryResumeHpPct <= selfRecoveryHpPct)
                        {
                            MessageBox.Show(Properties.Resources.Form1MessageHealResumeMustBeGreater);
                            return;
                        }
                    }




                    botCura.WeaponName = weapon;
                    botCura.TargetMode = string.IsNullOrWhiteSpace(mode) ? "PET" : mode;
                    botCura.TargetRadius = radius;
                    botCura.SkillsText = (txtHealSkills?.Text ?? "");
                    botCura.TargetNamesText = (txtHealTargetNames?.Text ?? "");
                    botCura.FollowTopTargetEnabled = followHealEnabled;
                    botCura.FollowSkillName = followSkill;
                    botCura.FollowTargetHpPct = (int)Math.Round(followTargetHpPct, MidpointRounding.AwayFromZero);
                    botCura.FollowDistance = followDistance;
                    botCura.SelfRecoveryItemsText = selfRecoveryItems;
                    botCura.SelfRecoveryHpPct = (int)Math.Round(selfRecoveryHpPct, MidpointRounding.AwayFromZero);
                    botCura.SelfRecoveryResumeHpPct = (int)Math.Round(selfRecoveryResumeHpPct, MidpointRounding.AwayFromZero);


                    string cmd = botCura.BuildOnCommand();
                    EnviarComandoJogo(cmd);
                    botCura.Start();

                    if (btnHealTrain != null) btnHealTrain.Text = Properties.Resources.Form1ButtonStopHeal;
                    if (lblHealStatus != null) lblHealStatus.Text = Properties.Resources.Form1HealStatusOn;
                    if (lstLog != null) lstLog.Items.Add(Properties.Resources.Form1LogHealEnabled);
                }
                else
                {
                    EnviarComandoJogo(botCura.BuildOffCommand());
                    botCura.Stop();
                    if (btnHealTrain != null) btnHealTrain.Text = Properties.Resources.Form1ButtonStartHeal;
                    if (lblHealStatus != null) lblHealStatus.Text = Properties.Resources.Form1HealStatusOff;
                    if (lstLog != null) lstLog.Items.Add(Properties.Resources.Form1LogHealDisabled);
                }
            }
            catch { }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void grpTameConfig_Enter(object sender, EventArgs e)
        {

        }

        private void btnInspectPlayer_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e) { }
    }
}
