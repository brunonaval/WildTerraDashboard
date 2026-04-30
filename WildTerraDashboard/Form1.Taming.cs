using System;
using System.Drawing;
using System.Windows.Forms;

namespace WildTerraDashboard
{
    public partial class Form1
    {
        private BotTaming botTaming;
        private TamingSettings tamingSettings;
        private Timer tamingTimer;
        private Button btnStartTamingRef;

        private void InitializeTamingModule()
        {
            tamingSettings = new TamingSettings();
            botTaming = new BotTaming(botMovimento, tamingSettings);
            botTaming.OnLog += LogarMensagem;

            tamingTimer = new Timer();
            tamingTimer.Interval = tamingSettings.TimerIntervalMs;
            tamingTimer.Tick += TamingTimer_Tick;

            btnStartTamingRef = FindControl<Button>("btnStartTaming");
            if (btnStartTamingRef != null)
            {
                btnStartTamingRef.Click -= BtnStartTaming_Click;
                btnStartTamingRef.Click += BtnStartTaming_Click;
                AtualizarEstadoBotaoTaming(false);
            }
        }

        private T FindControl<T>(string controlName) where T : Control
        {
            Control[] encontrados = Controls.Find(controlName, true);
            if (encontrados == null || encontrados.Length == 0) return null;
            return encontrados[0] as T;
        }


        private string GetTamingModeUi()
        {
            var cmb = FindControl<ComboBox>("cmbTamingMode");
            string mode = (cmb != null ? (cmb.Text ?? "") : "").Trim();
            if (string.IsNullOrWhiteSpace(mode)) mode = "Pacifico";
            if (string.Equals(mode, "Agressivo", StringComparison.OrdinalIgnoreCase)) return "AGRESSIVO";
            return "PACIFICO";
        }

        private string GetTamingTrapNameUi()
        {
            var txt = FindControl<TextBox>("txtTamingTrapName");
            return (txt != null ? txt.Text : "").Trim();
        }

        private string GetTamingTargetsPayloadUi()
        {
            var txt = FindControl<TextBox>("txtTamingTargets");
            return BuildListPayload(txt != null ? txt.Text : "");
        }

        private string GetTamingCombatWeaponNameUi()
        {
            return (txtWeaponName != null ? txtWeaponName.Text : "").Trim();
        }

        private void BtnStartTaming_Click(object sender, EventArgs e)
        {
            if (IsTrainingModeActive)
            {
                MessageBox.Show("Pare o modo Treinamento antes de iniciar a Doma.");
                return;
            }

            if (botTaming != null && botTaming.IsAtivo)
            {
                PararTamingUI();
                return;
            }

            IniciarTamingUI();
        }

        private void IniciarTamingUI()
        {
            if (botTimer != null && botTimer.Enabled)
            {
                MessageBox.Show("Pare o Bot Principal antes de iniciar a Doma.");
                return;
            }

            if (isFishingRunning)
            {
                MessageBox.Show("Pare a Pesca antes de iniciar a Doma.");
                return;
            }

            if (gravadorRota != null && gravadorRota.IsGravando)
            {
                MessageBox.Show("Pare a gravação da rota antes de iniciar a Doma.");
                return;
            }

            if (botMovimento == null || !botMovimento.HasRoute)
            {
                MessageBox.Show("Carregue uma rota no btnLoadRoute antes de iniciar a Doma.");
                return;
            }

            string mode = GetTamingModeUi();
            string trapName = GetTamingTrapNameUi();
            string targetsPayload = GetTamingTargetsPayloadUi();
            string combatWeaponName = GetTamingCombatWeaponNameUi();

            if (string.IsNullOrWhiteSpace(trapName))
            {
                MessageBox.Show("Preencha o nome da armadilha em txtTamingTrapName.");
                return;
            }

            if (string.IsNullOrWhiteSpace(targetsPayload))
            {
                MessageBox.Show("Preencha ao menos um alvo em txtTamingTargets.");
                return;
            }

            if (string.Equals(mode, "AGRESSIVO", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(combatWeaponName))
            {
                MessageBox.Show("No modo Agressivo, preencha a arma de combate em txtWeaponName.");
                return;
            }

            EnviarComandoJogo($"MOUNT_CONFIG;{(botMontaria != null && botMontaria.IsAtivo ? "ON" : "OFF")}");
            EnviarComandoJogo("BOT_STATUS;ON");

            if (!botTaming.Iniciar())
            {
                EnviarComandoJogo("BOT_STATUS;OFF");
                return;
            }

            EnviarComandoJogo($"TAMING;ON;{mode};{trapName};{targetsPayload};{combatWeaponName}");

            if (btnStartBot != null)
                btnStartBot.Enabled = false;

            if (tamingTimer != null)
                tamingTimer.Start();

            AtualizarEstadoBotaoTaming(true);
            LogarMensagem($"[TAMING] Modo independente ativo. Config enviada. mode={mode} trap='{trapName}' weapon='{combatWeaponName}'");
        }

        private void PararTamingUI()
        {
            if (tamingTimer != null)
                tamingTimer.Stop();

            if (botTaming != null)
                botTaming.Parar();

            EnviarComandoJogo("TAMING;OFF");
            EnviarComandoJogo("BOT_STATUS;OFF");



            if (btnStartBot != null)
                btnStartBot.Enabled = true;

            AtualizarEstadoBotaoTaming(false);
        }

        private void TryStopTamingForShutdown()
        {
            try { if (tamingTimer != null) tamingTimer.Stop(); } catch { }
            try { if (botTaming != null && botTaming.IsAtivo) botTaming.Parar(); } catch { }
            try { EnviarComandoJogo("TAMING;OFF"); } catch { }
            try { EnviarComandoJogo("BOT_STATUS;OFF"); } catch { }
            try { if (btnStartBot != null) btnStartBot.Enabled = true; } catch { }
        }

        private void TamingTimer_Tick(object sender, EventArgs e)
        {
            if (botTaming == null || !botTaming.IsAtivo) return;

            string comando = botTaming.ProcessarTick(statsJogador);
            if (!string.IsNullOrWhiteSpace(comando))
            {
                EnviarComandoJogo(comando);
            }
        }

        private void AtualizarEstadoBotaoTaming(bool ativo)
        {
            if (btnStartTamingRef == null)
                btnStartTamingRef = FindControl<Button>("btnStartTaming");

            if (btnStartTamingRef == null) return;

            if (ativo)
            {
                btnStartTamingRef.Text = "PARAR DOMA";
                btnStartTamingRef.BackColor = Color.Red;
                btnStartTamingRef.ForeColor = Color.White;
            }
            else
            {
                btnStartTamingRef.Text = "INICIAR DOMA";
                btnStartTamingRef.BackColor = Color.Green;
                btnStartTamingRef.ForeColor = Color.White;
            }
        }
    }
}
