using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WildTerraDashboard
{
    public partial class Form1
    {
        private BotTrainingMode botTraining;
        private readonly string _trainingUiFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "training_ui.json");
        private Button btnTrainingToggleRef;
        private Label lblTrainingStatusRef;
        private Label lblTrainingInfoRef;

        private bool IsTrainingModeActive
        {
            get { return botTraining != null && botTraining.IsAtivo; }
        }

        private void InitializeTrainingModule()
        {
            botTraining = new BotTrainingMode();

            btnTrainingToggleRef = FindControl<Button>("btnTrainingToggle");
            lblTrainingStatusRef = FindControl<Label>("lblTrainingStatus");
            lblTrainingInfoRef = FindControl<Label>("lblTrainingInfo");

            if (btnTrainingToggleRef != null)
            {
                btnTrainingToggleRef.Click -= BtnTrainingToggle_Click;
                btnTrainingToggleRef.Click += BtnTrainingToggle_Click;
            }

            if (lblTrainingInfoRef != null)
                lblTrainingInfoRef.Text = Properties.Resources.Form1TrainingInfoExclusiveMode;

            LoadTrainingUi();
            AtualizarEstadoTreinamento(false);
        }

        private void BtnTrainingToggle_Click(object sender, EventArgs e)
        {
            if (IsTrainingModeActive)
            {
                PararTreinamentoUI();
                return;
            }

            IniciarTreinamentoUI();
        }

        private void IniciarTreinamentoUI()
        {
            if (gravadorRota != null && gravadorRota.IsGravando)
            {
                MessageBox.Show(Properties.Resources.Form1TrainingMessageStopRouteRecordingBeforeTraining);
                return;
            }

            var cfg = ReadTrainingConfigFromUi();
            if (!cfg.Validate(out string error))
            {
                MessageBox.Show(error);
                return;
            }

            StopConflictingModesForTraining();
            SyncAutoEatForTraining();

            botTraining.Start(cfg);
            EnviarComandoJogo(botTraining.BuildOnCommand());
            AtualizarEstadoTreinamento(true);
            LogarMensagem(Properties.Resources.Form1TrainingLogActivatedPrefix + cfg.BuildSummary());
        }

        private void PararTreinamentoUI()
        {
            if (botTraining != null)
            {
                EnviarComandoJogo(botTraining.BuildOffCommand());

                botTraining.Stop();


            }


            AtualizarEstadoTreinamento(false);
            LogarMensagem(Properties.Resources.Form1TrainingLogDeactivated);
        }

        private void TryStopTrainingForShutdown()
        {
            try
            {
                if (botTraining != null && botTraining.IsAtivo)

                {


                    try { EnviarComandoJogo(botTraining.BuildOffCommand()); } catch { }

                    botTraining.Stop();



                }

            }
            catch { }

            try { ApplyTrainingExclusiveUiState(false); } catch { }
            try { AtualizarEstadoTreinamento(false); } catch { }
        }

        private TrainingModeConfig ReadTrainingConfigFromUi()
        {
            return new TrainingModeConfig
            {
                EnableSkills = GetChecked("chkTrainingSkills"),
                EnableBuffItems = GetChecked("chkTrainingBuffItems"),
                EnableRecovery = GetChecked("chkTrainingRecovery"),
                EnableAutoAttack = GetChecked("chkTrainingAutoAttack"),
                SkillsText = GetText("txtTrainingSkills"),
                BuffItemsText = GetText("txtTrainingBuffItems"),
                RecoveryItemsText = GetText("txtTrainingRecoveryItems"),
                AutoAttackTargetName = GetText("txtTrainingAutoAttackTarget"),
                BuffRefreshSeconds = GetNumericValue("numTrainingBuffRefreshSeconds", 1),
                HpThreshold = GetNumericValue("numTrainingHpThreshold", 50),
                SpThreshold = GetNumericValue("numTrainingSpThreshold", 50)
            };
        }

        private void AtualizarEstadoTreinamento(bool ativo)
        {
            if (btnTrainingToggleRef == null)
                btnTrainingToggleRef = FindControl<Button>("btnTrainingToggle");
            if (lblTrainingStatusRef == null)
                lblTrainingStatusRef = FindControl<Label>("lblTrainingStatus");

            if (btnTrainingToggleRef != null)
            {
                btnTrainingToggleRef.Text = ativo
                    ? Properties.Resources.Form1TrainingButtonStop
                    : Properties.Resources.Form1TrainingButtonStart;
                btnTrainingToggleRef.BackColor = ativo ? Color.Red : Color.Green;
                btnTrainingToggleRef.ForeColor = Color.White;
            }

            if (lblTrainingStatusRef != null)
                lblTrainingStatusRef.Text = ativo
                    ? Properties.Resources.Form1TrainingStatusOn
                    : Properties.Resources.Form1TrainingStatusOff;

            ApplyTrainingExclusiveUiState(ativo);
        }

        private void ApplyTrainingExclusiveUiState(bool trainingActive)
        {
            var btnMain = FindControl<Button>("btnStartBot");
            var btnFishing = FindControl<Button>("btnStartFishing");
            var btnHeal = FindControl<Button>("btnHealTrain");
            var btnTaming = FindControl<Button>("btnStartTaming");

            if (btnMain != null) btnMain.Enabled = !trainingActive;
            if (btnFishing != null) btnFishing.Enabled = !trainingActive;
            if (btnHeal != null) btnHeal.Enabled = !trainingActive;
            if (btnTaming != null) btnTaming.Enabled = !trainingActive;
        }

        private void SyncAutoEatForTraining()
        {
            try
            {
                if (txtAutoEat != null)
                {
                    string eatTxt = txtAutoEat.Text ?? "";
                    if (!string.IsNullOrWhiteSpace(eatTxt))
                    {
                        EnviarComandoJogo("EAT_LIST;" + eatTxt.Replace("\r\n", "~").Replace("\n", "~"));

                        _ultimaListaComerEnviada = eatTxt;
                        LogarMensagem(Properties.Resources.Form1TrainingLogAutoEatSynced);
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
        }







        private void StopConflictingModesForTraining()
        {
            if (botMovimento != null && botMovimento.IsRodando)
                PararBotUI();

            if (isFishingRunning)
            {
                EnviarComandoJogo("FISHING;OFF");
                isFishingRunning = false;

                var btnFishing = FindControl<Button>("btnStartFishing");
                if (btnFishing != null)
                {
                    btnFishing.Text = Properties.Resources.Form1ButtonStartFishing;
                    btnFishing.BackColor = Color.Navy;
                    btnFishing.ForeColor = Color.White;
                }

                LogarMensagem(Properties.Resources.Form1TrainingLogFishingDisabledForTraining);
            }

            if (botCura != null && botCura.IsAtivo)
            {
                EnviarComandoJogo(botCura.BuildOffCommand());
                botCura.Stop();

                var btnHeal = FindControl<Button>("btnHealTrain");
                if (btnHeal != null)
                    btnHeal.Text = Properties.Resources.Form1ButtonStartHeal;

                var lblHeal = FindControl<Label>("lblHealStatus");
                if (lblHeal != null)
                    lblHeal.Text = Properties.Resources.Form1HealStatusOff;

                LogarMensagem(Properties.Resources.Form1TrainingLogHealDisabledForTraining);
            }

            if (botTaming != null && botTaming.IsAtivo)
            {
                PararTamingUI();
                LogarMensagem(Properties.Resources.Form1TrainingLogTamingDisabledForTraining);
            }
        }

        private bool GetChecked(string controlName)
        {
            var chk = FindControl<CheckBox>(controlName);
            return chk != null && chk.Checked;
        }

        private string GetText(string controlName)
        {
            var txt = FindControl<TextBox>(controlName);
            return txt != null ? (txt.Text ?? "") : "";
        }

        private int GetNumericValue(string controlName, int defaultValue)
        {
            var num = FindControl<NumericUpDown>(controlName);
            if (num == null) return defaultValue;
            try { return Decimal.ToInt32(num.Value); }
            catch { return defaultValue; }
        }

        public void SaveTrainingUi()
        {
            try
            {
                var state = new TrainingUiState
                {
                    SkillsEnabled = GetChecked("chkTrainingSkills"),
                    BuffItemsEnabled = GetChecked("chkTrainingBuffItems"),
                    RecoveryEnabled = GetChecked("chkTrainingRecovery"),
                    AutoAttackEnabled = GetChecked("chkTrainingAutoAttack"),
                    SkillsText = GetText("txtTrainingSkills"),
                    BuffItemsText = GetText("txtTrainingBuffItems"),
                    RecoveryItemsText = GetText("txtTrainingRecoveryItems"),
                    AutoAttackTargetName = GetText("txtTrainingAutoAttackTarget"),
                    BuffRefreshSeconds = GetNumericValue("numTrainingBuffRefreshSeconds", 1),
                    HpThreshold = GetNumericValue("numTrainingHpThreshold", 50),
                    SpThreshold = GetNumericValue("numTrainingSpThreshold", 50)
                };

                var ser = new JavaScriptSerializer();
                var json = ser.Serialize(state);
                File.WriteAllText(_trainingUiFile, json, Encoding.UTF8);
            }
            catch { }
        }

        public void LoadTrainingUi()
        {
            try
            {
                if (!File.Exists(_trainingUiFile)) return;

                var json = File.ReadAllText(_trainingUiFile, Encoding.UTF8);
                var ser = new JavaScriptSerializer();
                var state = ser.Deserialize<TrainingUiState>(json);
                if (state == null) return;

                SetChecked("chkTrainingSkills", state.SkillsEnabled);
                SetChecked("chkTrainingBuffItems", state.BuffItemsEnabled);
                SetChecked("chkTrainingRecovery", state.RecoveryEnabled);
                SetChecked("chkTrainingAutoAttack", state.AutoAttackEnabled);
                SetText("txtTrainingSkills", state.SkillsText);
                SetText("txtTrainingBuffItems", state.BuffItemsText);
                SetText("txtTrainingRecoveryItems", state.RecoveryItemsText);
                SetText("txtTrainingAutoAttackTarget", state.AutoAttackTargetName);
                SetNumericValue("numTrainingBuffRefreshSeconds", state.BuffRefreshSeconds, 1);
                SetNumericValue("numTrainingHpThreshold", state.HpThreshold, 50);
                SetNumericValue("numTrainingSpThreshold", state.SpThreshold, 50);
            }
            catch { }
        }

        private void SetChecked(string controlName, bool value)
        {
            var chk = FindControl<CheckBox>(controlName);
            if (chk != null) chk.Checked = value;
        }

        private void SetText(string controlName, string value)
        {
            var txt = FindControl<TextBox>(controlName);
            if (txt != null) txt.Text = value ?? "";
        }

        private void SetNumericValue(string controlName, int value, int fallback)
        {
            var num = FindControl<NumericUpDown>(controlName);
            if (num == null) return;

            decimal finalValue = fallback;
            try { finalValue = value; } catch { }

            if (finalValue < num.Minimum) finalValue = num.Minimum;
            if (finalValue > num.Maximum) finalValue = num.Maximum;
            num.Value = finalValue;
        }

        private void grpTrainingSkills_Enter(object sender, EventArgs e)
        {
            // Handler vazio apenas para manter compatibilidade com o Designer atual.
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            // Handler vazio apenas para manter compatibilidade com o Designer atual.
        }





        private class TrainingUiState
        {
            public bool SkillsEnabled { get; set; }
            public bool BuffItemsEnabled { get; set; }
            public bool RecoveryEnabled { get; set; }
            public bool AutoAttackEnabled { get; set; }
            public string SkillsText { get; set; } = "";
            public string BuffItemsText { get; set; } = "";
            public string RecoveryItemsText { get; set; } = "";
            public string AutoAttackTargetName { get; set; } = "";
            public int BuffRefreshSeconds { get; set; } = 1;
            public int HpThreshold { get; set; } = 50;
            public int SpThreshold { get; set; } = 50;
        }
    }
}
