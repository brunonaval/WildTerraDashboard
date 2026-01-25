using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
        private int _ultimoThresholdEnviado = -1;
        private bool _ultimoEstadoMountEnviado = false;
        private bool isFishingRunning = false; // Controle de Pesca

        public Form1()
        {
            InitializeComponent();

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

            // 5. TIMERS
            botTimer.Tick -= BotTimer_Tick; // Remove duplicatas por segurança
            recordTimer.Tick -= RecordTimer_Tick;

            botTimer.Tick += BotTimer_Tick;
            recordTimer.Tick += RecordTimer_Tick;

            botTimer.Interval = 150;
            recordTimer.Interval = 1000;

            // 6. EVENTOS DE UI (INTERFACE)
            // Usamos verificação de nulo (null check) para evitar erros no Designer
            if (txtListaColeta != null) txtListaColeta.TextChanged += TxtListaColeta_TextChanged;
            if (chkAtivarColeta != null) chkAtivarColeta.CheckedChanged += ChkAtivarColeta_CheckedChanged;

            if (txtListaMobs != null) txtListaMobs.TextChanged += (s, e) => botCaçador.DefinirLista(txtListaMobs.Text);
            if (chkAtivarHunt != null) chkAtivarHunt.CheckedChanged += (s, e) => {
                botCaçador.IsAtivo = chkAtivarHunt.Checked;
                LogarMensagem($"Caçador: {(botCaçador.IsAtivo ? "ON" : "OFF")}");
            };

            if (chkUseMount != null) chkUseMount.CheckedChanged += ChkUseMount_CheckedChanged;

            if (btnSaveSafe != null) btnSaveSafe.Click += BtnSaveSafe_Click;
            if (btnLoadSafe != null) btnLoadSafe.Click += BtnLoadSafe_Click;

            // 7. CARREGAR CONFIGURAÇÕES SALVAS
            CarregarConfigBanco();
            CarregarListaLixo();
            CarregarListaComer();
        }

        // --- MÉTODOS DE LOG E UI ---
        private void LogarMensagem(string msg)
        {
            if (lstLog == null || lstLog.IsDisposed) return;
            this.BeginInvoke((MethodInvoker)delegate {
                lstLog.Items.Add(msg);
                lstLog.TopIndex = lstLog.Items.Count - 1;
            });
        }

        private void ChkUseMount_CheckedChanged(object sender, EventArgs e)
        {
            if (botMontaria != null) botMontaria.AtualizarConfig(chkUseMount.Checked);
        }

        // --- CONFIGURAÇÃO E PERSISTÊNCIA ---
        private void SalvarConfigBanco() { try { string[] linhas = { txtBankX.Text, txtBankZ.Text, txtBankName.Text }; File.WriteAllLines("config_banco.txt", linhas); } catch { } }
        private void CarregarConfigBanco() { try { if (File.Exists("config_banco.txt")) { string[] linhas = File.ReadAllLines("config_banco.txt"); if (linhas.Length >= 1) txtBankX.Text = linhas[0]; if (linhas.Length >= 2) txtBankZ.Text = linhas[1]; if (linhas.Length >= 3) txtBankName.Text = linhas[2]; } } catch { } }
        private void SalvarListaLixo() { try { if (txtDropList != null) File.WriteAllText("lista_lixo.txt", txtDropList.Text); } catch { } }
        private void CarregarListaLixo() { try { if (txtDropList != null && File.Exists("lista_lixo.txt")) txtDropList.Text = File.ReadAllText("lista_lixo.txt"); } catch { } }
        private void SalvarListaComer() { try { if (txtAutoEat != null) File.WriteAllText("lista_comer.txt", txtAutoEat.Text); } catch { } }
        private void CarregarListaComer() { try { if (txtAutoEat != null && File.Exists("lista_comer.txt")) txtAutoEat.Text = File.ReadAllText("lista_comer.txt"); } catch { } }

        // --- BOTÕES DE LISTAS ---
        private void BtnSaveSafe_Click(object sender, EventArgs e) { SaveFileDialog sfd = new SaveFileDialog(); sfd.Filter = "Safe List|*.txt"; sfd.FileName = "lista_segura.txt"; if (sfd.ShowDialog() == DialogResult.OK) { try { File.WriteAllText(sfd.FileName, txtSafeList.Text); MessageBox.Show("Salvo!"); } catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); } } }
        private void BtnLoadSafe_Click(object sender, EventArgs e) { OpenFileDialog ofd = new OpenFileDialog(); ofd.Filter = "Safe List|*.txt"; if (ofd.ShowDialog() == DialogResult.OK) { try { txtSafeList.Text = File.ReadAllText(ofd.FileName); } catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); } } }
        private void TxtListaColeta_TextChanged(object sender, EventArgs e) { if (botColeta != null) botColeta.DefinirLista(txtListaColeta.Text); }
        private void ChkAtivarColeta_CheckedChanged(object sender, EventArgs e) { if (botColeta != null) { botColeta.IsAtivo = chkAtivarColeta.Checked; LogarMensagem($"Coleta: {(botColeta.IsAtivo ? "ON" : "OFF")}"); } }

        // --- LOOP PRINCIPAL DO BOT (TIMER) ---
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

            // PEGA O NOME DA ARMA (USADO TANTO PARA CAÇA QUANTO PARA DEFESA NA COLETA)
            string nomeArma = "";
            if (txtWeaponName != null) nomeArma = txtWeaponName.Text.Trim();

            // === PRIORIDADE 1: CAÇA ===
            string comandoHunt = botCaçador.VerificarRadar(entidadesRadar);
            if (comandoHunt != null)
            {
                // Envia: HUNT;TipoMob;NomeArma
                EnviarComandoJogo($"{comandoHunt};{nomeArma}");
                return;
            }

            // === PRIORIDADE 2: COLETA ===
            string comandoColeta = botColeta.VerificarRadar(entidadesRadar);
            if (comandoColeta != null)
            {
                // CORREÇÃO AQUI: Agora enviamos HARVEST;Alvo;NomeArma
                // O bot na DLL vai ler esse terceiro parâmetro e configurar a defesa
                EnviarComandoJogo($"{comandoColeta};{nomeArma}");
                return;
            }

            // === PRIORIDADE 3: ROTA ===
            string comandoMove = botMovimento.ProcessarLogica(statsJogador.X, statsJogador.Z, statsJogador.SP);
            if (comandoMove != null) { EnviarComandoJogo(comandoMove); }
        }

        // --- CÁLCULOS E ROTA ---
        private float CalcularDistancia(float x1, float z1, float x2, float z2) { return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(z1 - z2, 2)); }
        private void RecordTimer_Tick(object sender, EventArgs e) { gravadorRota.ProcessarPosicao(statsJogador.X, statsJogador.Z); }

        // --- BOTÃO PRINCIPAL (START BOT) ---
        private void btnStartBot_Click(object sender, EventArgs e)
        {
            if (isFishingRunning) { MessageBox.Show("Pare o Bot de Pesca antes!"); return; }
            if (gravadorRota.IsGravando) { MessageBox.Show("Pare a gravação antes!"); return; }

            if (botMovimento.IsRodando)
            {
                botMovimento.Parar();
                btnStartBot.Text = "INICIAR BOT";
                btnStartBot.BackColor = Color.Green;
                botTimer.Stop();
                EnviarComandoJogo("BOT_STATUS;OFF");
                LogarMensagem("[SISTEMA] Bot Parado.");
                btnStartFishing.Enabled = true; // Destrava Pesca
            }
            else
            {
                botMovimento.Iniciar();
                if (botMovimento.IsRodando)
                {
                    btnStartBot.Text = "PARAR BOT";
                    btnStartBot.BackColor = Color.Red;
                    botTimer.Start();
                    _ultimoEstadoMountEnviado = !botMontaria.IsAtivo;
                    EnviarComandoJogo("BOT_STATUS;ON");
                    LogarMensagem("[SISTEMA] Bot Iniciado.");
                    btnStartFishing.Enabled = false; // Trava Pesca
                }
            }
        }

        // --- BOTÃO GRAVAR ROTA ---
        private void btnRecordRoute_Click(object sender, EventArgs e) { if (gravadorRota.IsGravando) { gravadorRota.PararGravacao(); recordTimer.Stop(); btnRecordRoute.Text = "GRAVAR ROTA"; btnRecordRoute.BackColor = Color.Orange; btnRecordRoute.ForeColor = Color.Black; } else { SaveFileDialog sfd = new SaveFileDialog(); sfd.Filter = "Arquivo de Rota|*.txt"; sfd.FileName = "nova_rota.txt"; if (sfd.ShowDialog() == DialogResult.OK) { gravadorRota.IniciarGravacao(sfd.FileName); recordTimer.Start(); btnRecordRoute.Text = "PARAR GRAVAÇÃO"; btnRecordRoute.BackColor = Color.Red; btnRecordRoute.ForeColor = Color.White; } } }
        private void btnLoadRoute_Click(object sender, EventArgs e) { OpenFileDialog ofd = new OpenFileDialog(); ofd.Filter = "Arquivos de Texto|*.txt"; if (ofd.ShowDialog() == DialogResult.OK) { botMovimento.CarregarRota(ofd.FileName); } }

        // --- BOTÃO CONECTAR ---
        private void btnConnect_Click(object sender, EventArgs e) { btnConnect.Enabled = false; rede.Iniciar(); }

        // --- BOTÕES DE LISTA DE COLETA ---
        private void btnSaveList_Click(object sender, EventArgs e) { SaveFileDialog sfd = new SaveFileDialog(); sfd.Filter = "Arquivos de Texto|*.txt"; sfd.FileName = "lista_coleta.txt"; if (sfd.ShowDialog() == DialogResult.OK) { try { System.IO.File.WriteAllText(sfd.FileName, txtListaColeta.Text); MessageBox.Show("Salvo!"); } catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); } } }
        private void btnLoadList_Click(object sender, EventArgs e) { OpenFileDialog ofd = new OpenFileDialog(); ofd.Filter = "Arquivos de Texto|*.txt"; if (ofd.ShowDialog() == DialogResult.OK) { try { string txt = System.IO.File.ReadAllText(ofd.FileName); txtListaColeta.Text = txt; botColeta.DefinirLista(txt); } catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); } } }

        // --- REDE (UDP) ---
        private void EnviarComandoJogo(string comando) { try { byte[] dados = Encoding.ASCII.GetBytes(comando); enviadorUDP.Send(dados, dados.Length, "127.0.0.1", 8889); } catch { } }
        private void Rede_OnIniciado() { this.BeginInvoke((MethodInvoker)delegate { lstLog.Items.Add("UDP Iniciado."); btnConnect.Text = "Ouvindo..."; }); }
        private void Rede_OnErro(string msg) { this.BeginInvoke((MethodInvoker)delegate { lstLog.Items.Add("Erro: " + msg); btnConnect.Enabled = true; }); }
        private void Rede_OnPacoteRecebido(string mensagem) { this.BeginInvoke((MethodInvoker)delegate { InterpretarComando(mensagem); }); }

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
                        if (barHP != null) { barHP.Value = statsJogador.HP; barHP.Refresh(); }
                        if (barSP != null) { barSP.Value = statsJogador.SP; barSP.Refresh(); }
                        if (lblX != null) lblX.Text = "X: " + statsJogador.X;
                        if (lblY != null) lblY.Text = "Y: " + statsJogador.Y;
                        if (lblZ != null) lblZ.Text = "Z: " + statsJogador.Z;
                        if (btnConnect.Text != "Sincronizado") btnConnect.Text = "Sincronizado";
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
                            if (txtBankX != null) EnviarComandoJogo($"MOVE;{txtBankX.Text};{txtBankZ.Text}");
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
                            if (ret != null) { LogarMensagem("[NAVEGAÇÃO] Afastando-se..."); EnviarComandoJogo(ret); }
                        }
                        break;

                    case "COMBAT_FLAG":
                        if (partes.Length >= 2)
                        {
                            bool emCombate = (partes[1] == "ON");
                            botMovimento.IsEmCombate = emCombate;
                            if (emCombate && btnStartBot.BackColor != Color.OrangeRed) btnStartBot.BackColor = Color.OrangeRed;
                            else if (!emCombate && btnStartBot.BackColor == Color.OrangeRed) btnStartBot.BackColor = Color.Red;
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
            if (rede != null) rede.Parar();
            if (enviadorUDP != null) enviadorUDP.Close();
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

                // Pega o valor do ComboBox, se for nulo usa "River" como segurança
                string local = cmbFishingSpot.Text.Trim();
                if (string.IsNullOrEmpty(local)) local = "River";

                if (string.IsNullOrEmpty(vara) || string.IsNullOrEmpty(isca))
                {
                    MessageBox.Show("Preencha Vara, Isca e Local!");
                    return;
                }

                // Envia: COMANDO;ON;VARA;ISCA;LOCAL
                EnviarComandoJogo($"FISHING;ON;{vara};{isca};{local}");

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