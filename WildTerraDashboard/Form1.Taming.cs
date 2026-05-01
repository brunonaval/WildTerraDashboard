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
                MessageBox.Show(Properties.Resources.Form1TamingMessageStopTrainingBeforeTaming);
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
                MessageBox.Show(Properties.Resources.Form1TamingMessageStopMainBotBeforeTaming);
                return;
            }

            if (isFishingRunning)
            {
                MessageBox.Show(Properties.Resources.Form1TamingMessageStopFishingBeforeTaming);
                return;
            }

            if (gravadorRota != null && gravadorRota.IsGravando)
            {
                MessageBox.Show(Properties.Resources.Form1TamingMessageStopRouteRecordingBeforeTaming);
                return;
            }

            if (botMovimento == null || !botMovimento.HasRoute)
            {
                MessageBox.Show(Properties.Resources.Form1TamingMessageLoadRouteBeforeTaming);
                return;
            }

            string mode = GetTamingModeUi();
            string trapName = GetTamingTrapNameUi();
            string targetsPayload = GetTamingTargetsPayloadUi();
            string combatWeaponName = GetTamingCombatWeaponNameUi();

            if (string.IsNullOrWhiteSpace(trapName))
            {
                MessageBox.Show(Properties.Resources.Form1TamingMessageFillTrapName);
                return;
            }

            if (string.IsNullOrWhiteSpace(targetsPayload))
            {
                MessageBox.Show(Properties.Resources.Form1TamingMessageFillAtLeastOneTarget);
                return;
            }

            if (string.Equals(mode, "AGRESSIVO", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(combatWeaponName))
            {
                MessageBox.Show(Properties.Resources.Form1TamingMessageFillCombatWeaponForAggressiveMode);
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
            LogarMensagem(string.Format(
                Properties.Resources.Form1TamingLogStandaloneModeConfigSentFormat,
                mode,
                trapName,
                combatWeaponName));
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
                btnStartTamingRef.Text = Properties.Resources.Form1TamingButtonStop;
                btnStartTamingRef.BackColor = Color.Red;
                btnStartTamingRef.ForeColor = Color.White;
            }
            else
            {
                btnStartTamingRef.Text = Properties.Resources.Form1TamingButtonStart;
                btnStartTamingRef.BackColor = Color.Green;
                btnStartTamingRef.ForeColor = Color.White;
            }
        }
    }
}
