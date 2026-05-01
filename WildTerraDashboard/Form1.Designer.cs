namespace WildTerraDashboard
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblZ = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLoadRoute = new System.Windows.Forms.Button();
            this.btnStartBot = new System.Windows.Forms.Button();
            this.botTimer = new System.Windows.Forms.Timer(this.components);
            this.btnRecordRoute = new System.Windows.Forms.Button();
            this.recordTimer = new System.Windows.Forms.Timer(this.components);
            this.txtListaColeta = new System.Windows.Forms.TextBox();
            this.chkAtivarColeta = new System.Windows.Forms.CheckBox();
            this.btnSaveList = new System.Windows.Forms.Button();
            this.btnLoadList = new System.Windows.Forms.Button();
            this.txtSafeList = new System.Windows.Forms.TextBox();
            this.txtBankX = new System.Windows.Forms.TextBox();
            this.txtBankZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.Estrutura = new System.Windows.Forms.Label();
            this.btnSaveSafe = new System.Windows.Forms.Button();
            this.btnLoadSafe = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDropList = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnTestMount = new System.Windows.Forms.Button();
            this.chkUseMount = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWeaponName = new System.Windows.Forms.TextBox();
            this.chkAtivarHunt = new System.Windows.Forms.CheckBox();
            this.txtListaMobs = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.txtAutoEatStatus = new System.Windows.Forms.TextBox();
            this.numEatThreshold = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAutoEat = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbFishingSpot = new System.Windows.Forms.ComboBox();
            this.btnStartFishing = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBaitName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRodName = new System.Windows.Forms.TextBox();
            this.tabHeal = new System.Windows.Forms.TabPage();
            this.chkHealFollowTopTarget = new System.Windows.Forms.CheckBox();
            this.grpHealFollowMode = new System.Windows.Forms.GroupBox();
            this.numHealSelfRecoveryResumeHpPct = new System.Windows.Forms.NumericUpDown();
            this.numHealSelfRecoveryHpPct = new System.Windows.Forms.NumericUpDown();
            this.lblHealSelfRecoveryResumeHpPct = new System.Windows.Forms.Label();
            this.lblHealSelfRecoveryHpPct = new System.Windows.Forms.Label();
            this.txtHealSelfRecoveryItems = new System.Windows.Forms.TextBox();
            this.lblHealSelfRecoveryItems = new System.Windows.Forms.Label();
            this.numHealFollowDistance = new System.Windows.Forms.NumericUpDown();
            this.lblHealFollowDistance = new System.Windows.Forms.Label();
            this.numHealFollowTargetHpPct = new System.Windows.Forms.NumericUpDown();
            this.lblHealFollowTargetHpPct = new System.Windows.Forms.Label();
            this.txtHealFollowSkill = new System.Windows.Forms.TextBox();
            this.lblHealFollowSkill = new System.Windows.Forms.Label();
            this.lblHealStatus = new System.Windows.Forms.Label();
            this.btnHealTrain = new System.Windows.Forms.Button();
            this.txtHealTargetNames = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtHealSkills = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.numHealRadius = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbHealTargetMode = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtHealWeaponName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabTaming = new System.Windows.Forms.TabPage();
            this.lblTamingMode = new System.Windows.Forms.Label();
            this.lblTamingTrapName = new System.Windows.Forms.Label();
            this.lblTamingTargets = new System.Windows.Forms.Label();
            this.cmbTamingMode = new System.Windows.Forms.ComboBox();
            this.txtTamingTrapName = new System.Windows.Forms.TextBox();
            this.txtTamingTargets = new System.Windows.Forms.TextBox();
            this.btnStartTaming = new System.Windows.Forms.Button();
            this.tabTraining = new System.Windows.Forms.TabPage();
            this.grpTrainingAutoAttack = new System.Windows.Forms.GroupBox();
            this.txtTrainingAutoAttackTarget = new System.Windows.Forms.TextBox();
            this.lblTrainingAutoAttackTarget = new System.Windows.Forms.Label();
            this.chkTrainingAutoAttack = new System.Windows.Forms.CheckBox();
            this.grpTrainingRecovery = new System.Windows.Forms.GroupBox();
            this.lblTrainingSpThreshold = new System.Windows.Forms.Label();
            this.numTrainingSpThreshold = new System.Windows.Forms.NumericUpDown();
            this.lblTrainingHpThreshold = new System.Windows.Forms.Label();
            this.numTrainingHpThreshold = new System.Windows.Forms.NumericUpDown();
            this.lblTrainingRecoveryHint = new System.Windows.Forms.Label();
            this.txtTrainingRecoveryItems = new System.Windows.Forms.TextBox();
            this.chkTrainingRecovery = new System.Windows.Forms.CheckBox();
            this.grpTrainingBuffItems = new System.Windows.Forms.GroupBox();
            this.lblTrainingBuffRefreshSeconds = new System.Windows.Forms.Label();
            this.numTrainingBuffRefreshSeconds = new System.Windows.Forms.NumericUpDown();
            this.lblTrainingBuffItemsHint = new System.Windows.Forms.Label();
            this.txtTrainingBuffItems = new System.Windows.Forms.TextBox();
            this.chkTrainingBuffItems = new System.Windows.Forms.CheckBox();
            this.grpTrainingSkills = new System.Windows.Forms.GroupBox();
            this.txtTrainingSkills = new System.Windows.Forms.TextBox();
            this.lblTrainingSkillsHint = new System.Windows.Forms.Label();
            this.chkTrainingSkills = new System.Windows.Forms.CheckBox();
            this.lblTrainingInfo = new System.Windows.Forms.Label();
            this.lblTrainingStatus = new System.Windows.Forms.Label();
            this.btnTrainingToggle = new System.Windows.Forms.Button();
            this.btnTestHome = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.listViewBag = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.pageSetupDialog2 = new System.Windows.Forms.PageSetupDialog();
            this.pageSetupDialog3 = new System.Windows.Forms.PageSetupDialog();
            this.txtInspectPlayerName = new System.Windows.Forms.TextBox();
            this.btnInspectPlayer = new System.Windows.Forms.Button();
            this.barSP = new WildTerraDashboard.ColoredProgressBar();
            this.barHP = new WildTerraDashboard.ColoredProgressBar();
            this.visualRadar1 = new WildTerraDashboard.VisualRadar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEatThreshold)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabHeal.SuspendLayout();
            this.grpHealFollowMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHealSelfRecoveryResumeHpPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealSelfRecoveryHpPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealFollowDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealFollowTargetHpPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealRadius)).BeginInit();
            this.tabTaming.SuspendLayout();
            this.tabTraining.SuspendLayout();
            this.grpTrainingAutoAttack.SuspendLayout();
            this.grpTrainingRecovery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainingSpThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainingHpThreshold)).BeginInit();
            this.grpTrainingBuffItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainingBuffRefreshSeconds)).BeginInit();
            this.grpTrainingSkills.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.visualRadar1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Fuchsia;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnConnect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnect.Location = new System.Drawing.Point(12, 86);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(130, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "🔌 Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblX.Location = new System.Drawing.Point(792, 13);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 3;
            this.lblX.Text = "X";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblY.Location = new System.Drawing.Point(891, 13);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 13);
            this.lblY.TabIndex = 4;
            this.lblY.Text = "Y";
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblZ.Location = new System.Drawing.Point(1012, 13);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(14, 13);
            this.lblZ.TabIndex = 5;
            this.lblZ.Text = "Z";
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(12, 805);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(476, 160);
            this.lstLog.TabIndex = 6;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 568);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(476, 231);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 79;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Level";
            this.columnHeader6.Width = 64;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 201;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Distance";
            this.columnHeader3.Width = 82;
            // 
            // btnLoadRoute
            // 
            this.btnLoadRoute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnLoadRoute.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLoadRoute.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLoadRoute.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadRoute.Location = new System.Drawing.Point(12, 144);
            this.btnLoadRoute.Name = "btnLoadRoute";
            this.btnLoadRoute.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLoadRoute.Size = new System.Drawing.Size(130, 23);
            this.btnLoadRoute.TabIndex = 12;
            this.btnLoadRoute.Text = "📂 Load Route";
            this.btnLoadRoute.UseVisualStyleBackColor = false;
            this.btnLoadRoute.Click += new System.EventHandler(this.btnLoadRoute_Click);
            // 
            // btnStartBot
            // 
            this.btnStartBot.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnStartBot.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStartBot.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnStartBot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStartBot.Location = new System.Drawing.Point(12, 57);
            this.btnStartBot.Name = "btnStartBot";
            this.btnStartBot.Size = new System.Drawing.Size(130, 23);
            this.btnStartBot.TabIndex = 13;
            this.btnStartBot.Text = "▶ Start";
            this.btnStartBot.UseVisualStyleBackColor = false;
            this.btnStartBot.Click += new System.EventHandler(this.btnStartBot_Click);
            // 
            // botTimer
            // 
            this.botTimer.Interval = 150;
            // 
            // btnRecordRoute
            // 
            this.btnRecordRoute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRecordRoute.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRecordRoute.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRecordRoute.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRecordRoute.Location = new System.Drawing.Point(12, 115);
            this.btnRecordRoute.Name = "btnRecordRoute";
            this.btnRecordRoute.Size = new System.Drawing.Size(130, 23);
            this.btnRecordRoute.TabIndex = 14;
            this.btnRecordRoute.Text = "⏺ Record Route";
            this.btnRecordRoute.UseVisualStyleBackColor = false;
            this.btnRecordRoute.Click += new System.EventHandler(this.btnRecordRoute_Click);
            // 
            // recordTimer
            // 
            this.recordTimer.Interval = 1000;
            // 
            // txtListaColeta
            // 
            this.txtListaColeta.BackColor = System.Drawing.SystemColors.Window;
            this.txtListaColeta.Location = new System.Drawing.Point(439, 54);
            this.txtListaColeta.Multiline = true;
            this.txtListaColeta.Name = "txtListaColeta";
            this.txtListaColeta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtListaColeta.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtListaColeta.Size = new System.Drawing.Size(275, 305);
            this.txtListaColeta.TabIndex = 15;
            // 
            // chkAtivarColeta
            // 
            this.chkAtivarColeta.AutoSize = true;
            this.chkAtivarColeta.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chkAtivarColeta.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkAtivarColeta.Location = new System.Drawing.Point(621, 6);
            this.chkAtivarColeta.Name = "chkAtivarColeta";
            this.chkAtivarColeta.Size = new System.Drawing.Size(103, 17);
            this.chkAtivarColeta.TabIndex = 16;
            this.chkAtivarColeta.Text = "Enable Harvest";
            this.chkAtivarColeta.UseVisualStyleBackColor = true;
            // 
            // btnSaveList
            // 
            this.btnSaveList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSaveList.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveList.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSaveList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveList.Location = new System.Drawing.Point(381, 80);
            this.btnSaveList.Name = "btnSaveList";
            this.btnSaveList.Size = new System.Drawing.Size(52, 23);
            this.btnSaveList.TabIndex = 17;
            this.btnSaveList.Text = "SAVE";
            this.btnSaveList.UseVisualStyleBackColor = true;
            this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
            // 
            // btnLoadList
            // 
            this.btnLoadList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLoadList.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLoadList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadList.Location = new System.Drawing.Point(381, 113);
            this.btnLoadList.Name = "btnLoadList";
            this.btnLoadList.Size = new System.Drawing.Size(52, 23);
            this.btnLoadList.TabIndex = 18;
            this.btnLoadList.Text = "OPEN";
            this.btnLoadList.UseVisualStyleBackColor = true;
            this.btnLoadList.Click += new System.EventHandler(this.btnLoadList_Click);
            // 
            // txtSafeList
            // 
            this.txtSafeList.Location = new System.Drawing.Point(146, 374);
            this.txtSafeList.Multiline = true;
            this.txtSafeList.Name = "txtSafeList";
            this.txtSafeList.Size = new System.Drawing.Size(275, 264);
            this.txtSafeList.TabIndex = 19;
            // 
            // txtBankX
            // 
            this.txtBankX.Location = new System.Drawing.Point(63, 26);
            this.txtBankX.Name = "txtBankX";
            this.txtBankX.Size = new System.Drawing.Size(53, 22);
            this.txtBankX.TabIndex = 20;
            // 
            // txtBankZ
            // 
            this.txtBankZ.Location = new System.Drawing.Point(63, 51);
            this.txtBankZ.Name = "txtBankZ";
            this.txtBankZ.Size = new System.Drawing.Size(53, 22);
            this.txtBankZ.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Coord X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Coord Z";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(63, 81);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(100, 22);
            this.txtBankName.TabIndex = 24;
            // 
            // Estrutura
            // 
            this.Estrutura.AutoSize = true;
            this.Estrutura.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.Estrutura.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Estrutura.Location = new System.Drawing.Point(3, 86);
            this.Estrutura.Name = "Estrutura";
            this.Estrutura.Size = new System.Drawing.Size(54, 13);
            this.Estrutura.TabIndex = 25;
            this.Estrutura.Text = "Structure";
            // 
            // btnSaveSafe
            // 
            this.btnSaveSafe.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveSafe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveSafe.Location = new System.Drawing.Point(65, 389);
            this.btnSaveSafe.Name = "btnSaveSafe";
            this.btnSaveSafe.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSafe.TabIndex = 26;
            this.btnSaveSafe.Text = "Save List";
            this.btnSaveSafe.UseVisualStyleBackColor = true;
            // 
            // btnLoadSafe
            // 
            this.btnLoadSafe.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLoadSafe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadSafe.Location = new System.Drawing.Point(65, 418);
            this.btnLoadSafe.Name = "btnLoadSafe";
            this.btnLoadSafe.Size = new System.Drawing.Size(75, 23);
            this.btnLoadSafe.TabIndex = 27;
            this.btnLoadSafe.Text = "Open List";
            this.btnLoadSafe.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(515, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "HARVEST LIST";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(1269, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "RADAR";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(701, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Coordinates:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(218, 346);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "DO NOT DEPOSIT";
            // 
            // txtDropList
            // 
            this.txtDropList.Location = new System.Drawing.Point(444, 432);
            this.txtDropList.Multiline = true;
            this.txtDropList.Name = "txtDropList";
            this.txtDropList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDropList.Size = new System.Drawing.Size(270, 206);
            this.txtDropList.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(559, 416);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "DROP";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabHeal);
            this.tabControl1.Controls.Add(this.tabTaming);
            this.tabControl1.Controls.Add(this.tabTraining);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(700, 290);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 675);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnTestMount);
            this.tabPage1.Controls.Add(this.chkUseMount);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtDropList);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.Estrutura);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtBankName);
            this.tabPage1.Controls.Add(this.btnLoadSafe);
            this.tabPage1.Controls.Add(this.txtBankZ);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtBankX);
            this.tabPage1.Controls.Add(this.btnSaveSafe);
            this.tabPage1.Controls.Add(this.txtListaColeta);
            this.tabPage1.Controls.Add(this.btnSaveList);
            this.tabPage1.Controls.Add(this.btnLoadList);
            this.tabPage1.Controls.Add(this.chkAtivarColeta);
            this.tabPage1.Controls.Add(this.txtSafeList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(725, 649);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Harvest";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnTestMount
            // 
            this.btnTestMount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTestMount.Location = new System.Drawing.Point(190, 5);
            this.btnTestMount.Name = "btnTestMount";
            this.btnTestMount.Size = new System.Drawing.Size(75, 23);
            this.btnTestMount.TabIndex = 35;
            this.btnTestMount.Text = "Mount";
            this.btnTestMount.UseVisualStyleBackColor = true;
            this.btnTestMount.Click += new System.EventHandler(this.btnTestMount_Click);
            // 
            // chkUseMount
            // 
            this.chkUseMount.AutoSize = true;
            this.chkUseMount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkUseMount.Location = new System.Drawing.Point(63, 164);
            this.chkUseMount.Name = "chkUseMount";
            this.chkUseMount.Size = new System.Drawing.Size(125, 17);
            this.chkUseMount.TabIndex = 35;
            this.chkUseMount.Text = "Use Mount (>15m)";
            this.chkUseMount.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(26, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Your Home Location";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.txtWeaponName);
            this.tabPage2.Controls.Add(this.chkAtivarHunt);
            this.tabPage2.Controls.Add(this.txtListaMobs);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(725, 649);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Combat";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(18, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Equip weapon:";
            // 
            // txtWeaponName
            // 
            this.txtWeaponName.Location = new System.Drawing.Point(112, 41);
            this.txtWeaponName.Name = "txtWeaponName";
            this.txtWeaponName.Size = new System.Drawing.Size(100, 22);
            this.txtWeaponName.TabIndex = 2;
            this.txtWeaponName.TextChanged += new System.EventHandler(this.txtWeaponName_TextChanged);
            // 
            // chkAtivarHunt
            // 
            this.chkAtivarHunt.AutoSize = true;
            this.chkAtivarHunt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkAtivarHunt.Location = new System.Drawing.Point(555, 55);
            this.chkAtivarHunt.Name = "chkAtivarHunt";
            this.chkAtivarHunt.Size = new System.Drawing.Size(90, 17);
            this.chkAtivarHunt.TabIndex = 1;
            this.chkAtivarHunt.Text = "Enable Hunt";
            this.chkAtivarHunt.UseVisualStyleBackColor = true;
            // 
            // txtListaMobs
            // 
            this.txtListaMobs.Location = new System.Drawing.Point(215, 91);
            this.txtListaMobs.Multiline = true;
            this.txtListaMobs.Name = "txtListaMobs";
            this.txtListaMobs.Size = new System.Drawing.Size(317, 396);
            this.txtListaMobs.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.txtAutoEatStatus);
            this.tabPage3.Controls.Add(this.numEatThreshold);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.txtAutoEat);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(725, 649);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Food";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(488, 241);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(112, 13);
            this.label19.TabIndex = 37;
            this.label19.Text = "Comidas com Status";
            // 
            // txtAutoEatStatus
            // 
            this.txtAutoEatStatus.AcceptsReturn = true;
            this.txtAutoEatStatus.Location = new System.Drawing.Point(421, 273);
            this.txtAutoEatStatus.Multiline = true;
            this.txtAutoEatStatus.Name = "txtAutoEatStatus";
            this.txtAutoEatStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAutoEatStatus.Size = new System.Drawing.Size(253, 187);
            this.txtAutoEatStatus.TabIndex = 36;
            this.txtAutoEatStatus.WordWrap = false;
            // 
            // numEatThreshold
            // 
            this.numEatThreshold.Location = new System.Drawing.Point(554, 20);
            this.numEatThreshold.Name = "numEatThreshold";
            this.numEatThreshold.Size = new System.Drawing.Size(120, 22);
            this.numEatThreshold.TabIndex = 35;
            this.numEatThreshold.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(147, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Auto Eat (Lista)";
            // 
            // txtAutoEat
            // 
            this.txtAutoEat.Location = new System.Drawing.Point(30, 80);
            this.txtAutoEat.Multiline = true;
            this.txtAutoEat.Name = "txtAutoEat";
            this.txtAutoEat.Size = new System.Drawing.Size(341, 380);
            this.txtAutoEat.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.cmbFishingSpot);
            this.tabPage4.Controls.Add(this.btnStartFishing);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.txtBaitName);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.txtRodName);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(725, 649);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Fishing";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(6, 91);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Fishing spot:";
            // 
            // cmbFishingSpot
            // 
            this.cmbFishingSpot.FormattingEnabled = true;
            this.cmbFishingSpot.Items.AddRange(new object[] {
            "River",
            "Ocean",
            "Desert",
            "Withered Canyon",
            "Battle Field Island"});
            this.cmbFishingSpot.Location = new System.Drawing.Point(95, 88);
            this.cmbFishingSpot.Name = "cmbFishingSpot";
            this.cmbFishingSpot.Size = new System.Drawing.Size(248, 21);
            this.cmbFishingSpot.TabIndex = 5;
            // 
            // btnStartFishing
            // 
            this.btnStartFishing.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStartFishing.Location = new System.Drawing.Point(452, 20);
            this.btnStartFishing.Name = "btnStartFishing";
            this.btnStartFishing.Size = new System.Drawing.Size(107, 23);
            this.btnStartFishing.TabIndex = 4;
            this.btnStartFishing.Text = "START FISHING";
            this.btnStartFishing.UseVisualStyleBackColor = true;
            this.btnStartFishing.Click += new System.EventHandler(this.btnStartFishing_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(6, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Bait name:";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // txtBaitName
            // 
            this.txtBaitName.Location = new System.Drawing.Point(95, 49);
            this.txtBaitName.Name = "txtBaitName";
            this.txtBaitName.Size = new System.Drawing.Size(248, 22);
            this.txtBaitName.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(6, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Rod name:";
            // 
            // txtRodName
            // 
            this.txtRodName.Location = new System.Drawing.Point(95, 11);
            this.txtRodName.Name = "txtRodName";
            this.txtRodName.Size = new System.Drawing.Size(248, 22);
            this.txtRodName.TabIndex = 0;
            // 
            // tabHeal
            // 
            this.tabHeal.Controls.Add(this.chkHealFollowTopTarget);
            this.tabHeal.Controls.Add(this.grpHealFollowMode);
            this.tabHeal.Controls.Add(this.lblHealStatus);
            this.tabHeal.Controls.Add(this.btnHealTrain);
            this.tabHeal.Controls.Add(this.txtHealTargetNames);
            this.tabHeal.Controls.Add(this.label18);
            this.tabHeal.Controls.Add(this.txtHealSkills);
            this.tabHeal.Controls.Add(this.label17);
            this.tabHeal.Controls.Add(this.numHealRadius);
            this.tabHeal.Controls.Add(this.label16);
            this.tabHeal.Controls.Add(this.cmbHealTargetMode);
            this.tabHeal.Controls.Add(this.label15);
            this.tabHeal.Controls.Add(this.txtHealWeaponName);
            this.tabHeal.Controls.Add(this.label14);
            this.tabHeal.Location = new System.Drawing.Point(4, 22);
            this.tabHeal.Name = "tabHeal";
            this.tabHeal.Size = new System.Drawing.Size(725, 649);
            this.tabHeal.TabIndex = 4;
            this.tabHeal.Text = "Heal";
            this.tabHeal.UseVisualStyleBackColor = true;
            // 
            // chkHealFollowTopTarget
            // 
            this.chkHealFollowTopTarget.AutoSize = true;
            this.chkHealFollowTopTarget.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkHealFollowTopTarget.Location = new System.Drawing.Point(34, 190);
            this.chkHealFollowTopTarget.Name = "chkHealFollowTopTarget";
            this.chkHealFollowTopTarget.Size = new System.Drawing.Size(197, 17);
            this.chkHealFollowTopTarget.TabIndex = 13;
            this.chkHealFollowTopTarget.Text = "Ativar Follow Heal (topo da lista)";
            this.chkHealFollowTopTarget.UseVisualStyleBackColor = true;
            // 
            // grpHealFollowMode
            // 
            this.grpHealFollowMode.Controls.Add(this.numHealSelfRecoveryResumeHpPct);
            this.grpHealFollowMode.Controls.Add(this.numHealSelfRecoveryHpPct);
            this.grpHealFollowMode.Controls.Add(this.lblHealSelfRecoveryResumeHpPct);
            this.grpHealFollowMode.Controls.Add(this.lblHealSelfRecoveryHpPct);
            this.grpHealFollowMode.Controls.Add(this.txtHealSelfRecoveryItems);
            this.grpHealFollowMode.Controls.Add(this.lblHealSelfRecoveryItems);
            this.grpHealFollowMode.Controls.Add(this.numHealFollowDistance);
            this.grpHealFollowMode.Controls.Add(this.lblHealFollowDistance);
            this.grpHealFollowMode.Controls.Add(this.numHealFollowTargetHpPct);
            this.grpHealFollowMode.Controls.Add(this.lblHealFollowTargetHpPct);
            this.grpHealFollowMode.Controls.Add(this.txtHealFollowSkill);
            this.grpHealFollowMode.Controls.Add(this.lblHealFollowSkill);
            this.grpHealFollowMode.Location = new System.Drawing.Point(25, 213);
            this.grpHealFollowMode.Name = "grpHealFollowMode";
            this.grpHealFollowMode.Size = new System.Drawing.Size(678, 409);
            this.grpHealFollowMode.TabIndex = 12;
            this.grpHealFollowMode.TabStop = false;
            this.grpHealFollowMode.Text = "Follow Heal";
            // 
            // numHealSelfRecoveryResumeHpPct
            // 
            this.numHealSelfRecoveryResumeHpPct.Location = new System.Drawing.Point(331, 150);
            this.numHealSelfRecoveryResumeHpPct.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHealSelfRecoveryResumeHpPct.Name = "numHealSelfRecoveryResumeHpPct";
            this.numHealSelfRecoveryResumeHpPct.Size = new System.Drawing.Size(50, 22);
            this.numHealSelfRecoveryResumeHpPct.TabIndex = 26;
            this.numHealSelfRecoveryResumeHpPct.Value = new decimal(new int[] {
            55,
            0,
            0,
            0});
            // 
            // numHealSelfRecoveryHpPct
            // 
            this.numHealSelfRecoveryHpPct.Location = new System.Drawing.Point(331, 87);
            this.numHealSelfRecoveryHpPct.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHealSelfRecoveryHpPct.Name = "numHealSelfRecoveryHpPct";
            this.numHealSelfRecoveryHpPct.Size = new System.Drawing.Size(50, 22);
            this.numHealSelfRecoveryHpPct.TabIndex = 25;
            this.numHealSelfRecoveryHpPct.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // lblHealSelfRecoveryResumeHpPct
            // 
            this.lblHealSelfRecoveryResumeHpPct.AutoSize = true;
            this.lblHealSelfRecoveryResumeHpPct.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHealSelfRecoveryResumeHpPct.Location = new System.Drawing.Point(328, 122);
            this.lblHealSelfRecoveryResumeHpPct.Name = "lblHealSelfRecoveryResumeHpPct";
            this.lblHealSelfRecoveryResumeHpPct.Size = new System.Drawing.Size(146, 13);
            this.lblHealSelfRecoveryResumeHpPct.TabIndex = 24;
            this.lblHealSelfRecoveryResumeHpPct.Text = "Retomar cura acima de (%)";
            // 
            // lblHealSelfRecoveryHpPct
            // 
            this.lblHealSelfRecoveryHpPct.AutoSize = true;
            this.lblHealSelfRecoveryHpPct.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHealSelfRecoveryHpPct.Location = new System.Drawing.Point(325, 60);
            this.lblHealSelfRecoveryHpPct.Name = "lblHealSelfRecoveryHpPct";
            this.lblHealSelfRecoveryHpPct.Size = new System.Drawing.Size(154, 13);
            this.lblHealSelfRecoveryHpPct.TabIndex = 22;
            this.lblHealSelfRecoveryHpPct.Text = "Usar autocura abaixo de (%)";
            // 
            // txtHealSelfRecoveryItems
            // 
            this.txtHealSelfRecoveryItems.Location = new System.Drawing.Point(109, 51);
            this.txtHealSelfRecoveryItems.Multiline = true;
            this.txtHealSelfRecoveryItems.Name = "txtHealSelfRecoveryItems";
            this.txtHealSelfRecoveryItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHealSelfRecoveryItems.Size = new System.Drawing.Size(213, 154);
            this.txtHealSelfRecoveryItems.TabIndex = 21;
            this.txtHealSelfRecoveryItems.WordWrap = false;
            // 
            // lblHealSelfRecoveryItems
            // 
            this.lblHealSelfRecoveryItems.AutoSize = true;
            this.lblHealSelfRecoveryItems.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHealSelfRecoveryItems.Location = new System.Drawing.Point(6, 60);
            this.lblHealSelfRecoveryItems.Name = "lblHealSelfRecoveryItems";
            this.lblHealSelfRecoveryItems.Size = new System.Drawing.Size(97, 13);
            this.lblHealSelfRecoveryItems.TabIndex = 20;
            this.lblHealSelfRecoveryItems.Text = "Itens de autocura";
            // 
            // numHealFollowDistance
            // 
            this.numHealFollowDistance.DecimalPlaces = 1;
            this.numHealFollowDistance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numHealFollowDistance.Location = new System.Drawing.Point(482, 21);
            this.numHealFollowDistance.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numHealFollowDistance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHealFollowDistance.Name = "numHealFollowDistance";
            this.numHealFollowDistance.Size = new System.Drawing.Size(50, 22);
            this.numHealFollowDistance.TabIndex = 19;
            this.numHealFollowDistance.Value = new decimal(new int[] {
            45,
            0,
            0,
            65536});
            // 
            // lblHealFollowDistance
            // 
            this.lblHealFollowDistance.AutoSize = true;
            this.lblHealFollowDistance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHealFollowDistance.Location = new System.Drawing.Point(384, 30);
            this.lblHealFollowDistance.Name = "lblHealFollowDistance";
            this.lblHealFollowDistance.Size = new System.Drawing.Size(96, 13);
            this.lblHealFollowDistance.TabIndex = 18;
            this.lblHealFollowDistance.Text = "Distância do alvo";
            // 
            // numHealFollowTargetHpPct
            // 
            this.numHealFollowTargetHpPct.Location = new System.Drawing.Point(328, 21);
            this.numHealFollowTargetHpPct.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHealFollowTargetHpPct.Name = "numHealFollowTargetHpPct";
            this.numHealFollowTargetHpPct.Size = new System.Drawing.Size(50, 22);
            this.numHealFollowTargetHpPct.TabIndex = 17;
            this.numHealFollowTargetHpPct.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // lblHealFollowTargetHpPct
            // 
            this.lblHealFollowTargetHpPct.AutoSize = true;
            this.lblHealFollowTargetHpPct.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHealFollowTargetHpPct.Location = new System.Drawing.Point(187, 30);
            this.lblHealFollowTargetHpPct.Name = "lblHealFollowTargetHpPct";
            this.lblHealFollowTargetHpPct.Size = new System.Drawing.Size(135, 13);
            this.lblHealFollowTargetHpPct.TabIndex = 16;
            this.lblHealFollowTargetHpPct.Text = "Curar alvo abaixo de (%)";
            // 
            // txtHealFollowSkill
            // 
            this.txtHealFollowSkill.Location = new System.Drawing.Point(81, 21);
            this.txtHealFollowSkill.Name = "txtHealFollowSkill";
            this.txtHealFollowSkill.Size = new System.Drawing.Size(100, 22);
            this.txtHealFollowSkill.TabIndex = 15;
            this.txtHealFollowSkill.Text = "SacrificeHealth";
            // 
            // lblHealFollowSkill
            // 
            this.lblHealFollowSkill.AutoSize = true;
            this.lblHealFollowSkill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHealFollowSkill.Location = new System.Drawing.Point(4, 30);
            this.lblHealFollowSkill.Name = "lblHealFollowSkill";
            this.lblHealFollowSkill.Size = new System.Drawing.Size(69, 13);
            this.lblHealFollowSkill.TabIndex = 14;
            this.lblHealFollowSkill.Text = "Skill de cura";
            // 
            // lblHealStatus
            // 
            this.lblHealStatus.AutoSize = true;
            this.lblHealStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHealStatus.Location = new System.Drawing.Point(640, 10);
            this.lblHealStatus.Name = "lblHealStatus";
            this.lblHealStatus.Size = new System.Drawing.Size(63, 13);
            this.lblHealStatus.TabIndex = 11;
            this.lblHealStatus.Text = "CURA: OFF";
            // 
            // btnHealTrain
            // 
            this.btnHealTrain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHealTrain.Location = new System.Drawing.Point(517, 10);
            this.btnHealTrain.Name = "btnHealTrain";
            this.btnHealTrain.Size = new System.Drawing.Size(75, 23);
            this.btnHealTrain.TabIndex = 10;
            this.btnHealTrain.Text = "Iniciar Cura";
            this.btnHealTrain.UseVisualStyleBackColor = true;
            this.btnHealTrain.Click += new System.EventHandler(this.btnHealTrain_Click);
            // 
            // txtHealTargetNames
            // 
            this.txtHealTargetNames.Location = new System.Drawing.Point(308, 68);
            this.txtHealTargetNames.Multiline = true;
            this.txtHealTargetNames.Name = "txtHealTargetNames";
            this.txtHealTargetNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHealTargetNames.Size = new System.Drawing.Size(249, 120);
            this.txtHealTargetNames.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(305, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(145, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Alvos (PLAYER_BY_NAME)";
            // 
            // txtHealSkills
            // 
            this.txtHealSkills.Location = new System.Drawing.Point(25, 67);
            this.txtHealSkills.Multiline = true;
            this.txtHealSkills.Name = "txtHealSkills";
            this.txtHealSkills.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHealSkills.Size = new System.Drawing.Size(249, 120);
            this.txtHealSkills.TabIndex = 7;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(22, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(252, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "1 skill por linha (prioridade de cima para baixo)";
            // 
            // numHealRadius
            // 
            this.numHealRadius.Location = new System.Drawing.Point(308, 27);
            this.numHealRadius.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numHealRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHealRadius.Name = "numHealRadius";
            this.numHealRadius.Size = new System.Drawing.Size(120, 22);
            this.numHealRadius.TabIndex = 5;
            this.numHealRadius.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(251, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "Raio (m)";
            // 
            // cmbHealTargetMode
            // 
            this.cmbHealTargetMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHealTargetMode.FormattingEnabled = true;
            this.cmbHealTargetMode.Items.AddRange(new object[] {
            "PET",
            "SELF",
            "PLAYER_BY_NAME"});
            this.cmbHealTargetMode.Location = new System.Drawing.Point(124, 27);
            this.cmbHealTargetMode.Name = "cmbHealTargetMode";
            this.cmbHealTargetMode.Size = new System.Drawing.Size(121, 21);
            this.cmbHealTargetMode.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(22, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Modo alvo:";
            // 
            // txtHealWeaponName
            // 
            this.txtHealWeaponName.Location = new System.Drawing.Point(125, 3);
            this.txtHealWeaponName.Name = "txtHealWeaponName";
            this.txtHealWeaponName.Size = new System.Drawing.Size(250, 22);
            this.txtHealWeaponName.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(19, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Arma de cura:";
            // 
            // tabTaming
            // 
            this.tabTaming.Controls.Add(this.lblTamingMode);
            this.tabTaming.Controls.Add(this.lblTamingTrapName);
            this.tabTaming.Controls.Add(this.lblTamingTargets);
            this.tabTaming.Controls.Add(this.cmbTamingMode);
            this.tabTaming.Controls.Add(this.txtTamingTrapName);
            this.tabTaming.Controls.Add(this.txtTamingTargets);
            this.tabTaming.Controls.Add(this.btnStartTaming);
            this.tabTaming.Location = new System.Drawing.Point(4, 22);
            this.tabTaming.Name = "tabTaming";
            this.tabTaming.Padding = new System.Windows.Forms.Padding(3);
            this.tabTaming.Size = new System.Drawing.Size(725, 649);
            this.tabTaming.TabIndex = 5;
            this.tabTaming.Text = "Taming";
            this.tabTaming.UseVisualStyleBackColor = true;
            // 
            // lblTamingMode
            // 
            this.lblTamingMode.AutoSize = true;
            this.lblTamingMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTamingMode.Location = new System.Drawing.Point(97, 38);
            this.lblTamingMode.Name = "lblTamingMode";
            this.lblTamingMode.Size = new System.Drawing.Size(79, 13);
            this.lblTamingMode.TabIndex = 37;
            this.lblTamingMode.Text = "Tipo de doma";
            // 
            // lblTamingTrapName
            // 
            this.lblTamingTrapName.AutoSize = true;
            this.lblTamingTrapName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTamingTrapName.Location = new System.Drawing.Point(271, 126);
            this.lblTamingTrapName.Name = "lblTamingTrapName";
            this.lblTamingTrapName.Size = new System.Drawing.Size(110, 13);
            this.lblTamingTrapName.TabIndex = 36;
            this.lblTamingTrapName.Text = "Nome da armadilha";
            // 
            // lblTamingTargets
            // 
            this.lblTamingTargets.AutoSize = true;
            this.lblTamingTargets.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTamingTargets.Location = new System.Drawing.Point(80, 126);
            this.lblTamingTargets.Name = "lblTamingTargets";
            this.lblTamingTargets.Size = new System.Drawing.Size(96, 13);
            this.lblTamingTargets.TabIndex = 4;
            this.lblTamingTargets.Text = "Nomes dos alvos";
            // 
            // cmbTamingMode
            // 
            this.cmbTamingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTamingMode.FormattingEnabled = true;
            this.cmbTamingMode.Items.AddRange(new object[] {
            "Pacifico",
            "Agressivo"});
            this.cmbTamingMode.Location = new System.Drawing.Point(80, 54);
            this.cmbTamingMode.Name = "cmbTamingMode";
            this.cmbTamingMode.Size = new System.Drawing.Size(121, 21);
            this.cmbTamingMode.TabIndex = 3;
            // 
            // txtTamingTrapName
            // 
            this.txtTamingTrapName.Location = new System.Drawing.Point(274, 155);
            this.txtTamingTrapName.Name = "txtTamingTrapName";
            this.txtTamingTrapName.Size = new System.Drawing.Size(123, 22);
            this.txtTamingTrapName.TabIndex = 2;
            // 
            // txtTamingTargets
            // 
            this.txtTamingTargets.Location = new System.Drawing.Point(16, 145);
            this.txtTamingTargets.Multiline = true;
            this.txtTamingTargets.Name = "txtTamingTargets";
            this.txtTamingTargets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTamingTargets.Size = new System.Drawing.Size(224, 244);
            this.txtTamingTargets.TabIndex = 1;
            // 
            // btnStartTaming
            // 
            this.btnStartTaming.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStartTaming.Location = new System.Drawing.Point(535, 101);
            this.btnStartTaming.Name = "btnStartTaming";
            this.btnStartTaming.Size = new System.Drawing.Size(95, 23);
            this.btnStartTaming.TabIndex = 0;
            this.btnStartTaming.Text = "INICIAR DOMA";
            this.btnStartTaming.UseVisualStyleBackColor = true;
            // 
            // tabTraining
            // 
            this.tabTraining.Controls.Add(this.grpTrainingAutoAttack);
            this.tabTraining.Controls.Add(this.grpTrainingRecovery);
            this.tabTraining.Controls.Add(this.grpTrainingBuffItems);
            this.tabTraining.Controls.Add(this.grpTrainingSkills);
            this.tabTraining.Controls.Add(this.lblTrainingInfo);
            this.tabTraining.Controls.Add(this.lblTrainingStatus);
            this.tabTraining.Controls.Add(this.btnTrainingToggle);
            this.tabTraining.Location = new System.Drawing.Point(4, 22);
            this.tabTraining.Name = "tabTraining";
            this.tabTraining.Padding = new System.Windows.Forms.Padding(3);
            this.tabTraining.Size = new System.Drawing.Size(725, 649);
            this.tabTraining.TabIndex = 6;
            this.tabTraining.Text = "Treinamento";
            this.tabTraining.UseVisualStyleBackColor = true;
            // 
            // grpTrainingAutoAttack
            // 
            this.grpTrainingAutoAttack.Controls.Add(this.txtTrainingAutoAttackTarget);
            this.grpTrainingAutoAttack.Controls.Add(this.lblTrainingAutoAttackTarget);
            this.grpTrainingAutoAttack.Controls.Add(this.chkTrainingAutoAttack);
            this.grpTrainingAutoAttack.Location = new System.Drawing.Point(475, 427);
            this.grpTrainingAutoAttack.Name = "grpTrainingAutoAttack";
            this.grpTrainingAutoAttack.Size = new System.Drawing.Size(229, 215);
            this.grpTrainingAutoAttack.TabIndex = 6;
            this.grpTrainingAutoAttack.TabStop = false;
            this.grpTrainingAutoAttack.Text = "Auto Ataque (somente desmontado)";
            // 
            // txtTrainingAutoAttackTarget
            // 
            this.txtTrainingAutoAttackTarget.Location = new System.Drawing.Point(93, 63);
            this.txtTrainingAutoAttackTarget.Name = "txtTrainingAutoAttackTarget";
            this.txtTrainingAutoAttackTarget.Size = new System.Drawing.Size(130, 22);
            this.txtTrainingAutoAttackTarget.TabIndex = 6;
            this.txtTrainingAutoAttackTarget.Text = "Cavecrawler";
            // 
            // lblTrainingAutoAttackTarget
            // 
            this.lblTrainingAutoAttackTarget.AutoSize = true;
            this.lblTrainingAutoAttackTarget.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingAutoAttackTarget.Location = new System.Drawing.Point(3, 72);
            this.lblTrainingAutoAttackTarget.Name = "lblTrainingAutoAttackTarget";
            this.lblTrainingAutoAttackTarget.Size = new System.Drawing.Size(84, 13);
            this.lblTrainingAutoAttackTarget.TabIndex = 5;
            this.lblTrainingAutoAttackTarget.Text = "Nome do alvo:";
            // 
            // chkTrainingAutoAttack
            // 
            this.chkTrainingAutoAttack.AutoSize = true;
            this.chkTrainingAutoAttack.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkTrainingAutoAttack.Location = new System.Drawing.Point(6, 43);
            this.chkTrainingAutoAttack.Name = "chkTrainingAutoAttack";
            this.chkTrainingAutoAttack.Size = new System.Drawing.Size(222, 17);
            this.chkTrainingAutoAttack.TabIndex = 4;
            this.chkTrainingAutoAttack.Text = "Atacar sem parar no alvo configurado";
            this.chkTrainingAutoAttack.UseVisualStyleBackColor = true;
            // 
            // grpTrainingRecovery
            // 
            this.grpTrainingRecovery.Controls.Add(this.lblTrainingSpThreshold);
            this.grpTrainingRecovery.Controls.Add(this.numTrainingSpThreshold);
            this.grpTrainingRecovery.Controls.Add(this.lblTrainingHpThreshold);
            this.grpTrainingRecovery.Controls.Add(this.numTrainingHpThreshold);
            this.grpTrainingRecovery.Controls.Add(this.lblTrainingRecoveryHint);
            this.grpTrainingRecovery.Controls.Add(this.txtTrainingRecoveryItems);
            this.grpTrainingRecovery.Controls.Add(this.chkTrainingRecovery);
            this.grpTrainingRecovery.Location = new System.Drawing.Point(9, 415);
            this.grpTrainingRecovery.Name = "grpTrainingRecovery";
            this.grpTrainingRecovery.Size = new System.Drawing.Size(460, 227);
            this.grpTrainingRecovery.TabIndex = 5;
            this.grpTrainingRecovery.TabStop = false;
            this.grpTrainingRecovery.Text = "Recuperação HP / SP (somente desmontado)";
            // 
            // lblTrainingSpThreshold
            // 
            this.lblTrainingSpThreshold.AutoSize = true;
            this.lblTrainingSpThreshold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingSpThreshold.Location = new System.Drawing.Point(260, 119);
            this.lblTrainingSpThreshold.Name = "lblTrainingSpThreshold";
            this.lblTrainingSpThreshold.Size = new System.Drawing.Size(166, 13);
            this.lblTrainingSpThreshold.TabIndex = 41;
            this.lblTrainingSpThreshold.Text = "Usar item de SP abaixo de (%):";
            // 
            // numTrainingSpThreshold
            // 
            this.numTrainingSpThreshold.Location = new System.Drawing.Point(261, 138);
            this.numTrainingSpThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTrainingSpThreshold.Name = "numTrainingSpThreshold";
            this.numTrainingSpThreshold.Size = new System.Drawing.Size(120, 22);
            this.numTrainingSpThreshold.TabIndex = 40;
            this.numTrainingSpThreshold.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblTrainingHpThreshold
            // 
            this.lblTrainingHpThreshold.AutoSize = true;
            this.lblTrainingHpThreshold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingHpThreshold.Location = new System.Drawing.Point(260, 59);
            this.lblTrainingHpThreshold.Name = "lblTrainingHpThreshold";
            this.lblTrainingHpThreshold.Size = new System.Drawing.Size(168, 13);
            this.lblTrainingHpThreshold.TabIndex = 39;
            this.lblTrainingHpThreshold.Text = "Usar item de HP abaixo de (%):";
            // 
            // numTrainingHpThreshold
            // 
            this.numTrainingHpThreshold.Location = new System.Drawing.Point(260, 78);
            this.numTrainingHpThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTrainingHpThreshold.Name = "numTrainingHpThreshold";
            this.numTrainingHpThreshold.Size = new System.Drawing.Size(120, 22);
            this.numTrainingHpThreshold.TabIndex = 38;
            this.numTrainingHpThreshold.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblTrainingRecoveryHint
            // 
            this.lblTrainingRecoveryHint.AutoSize = true;
            this.lblTrainingRecoveryHint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingRecoveryHint.Location = new System.Drawing.Point(7, 32);
            this.lblTrainingRecoveryHint.Name = "lblTrainingRecoveryHint";
            this.lblTrainingRecoveryHint.Size = new System.Drawing.Size(390, 13);
            this.lblTrainingRecoveryHint.TabIndex = 4;
            this.lblTrainingRecoveryHint.Text = "Use uma linha por item no formato HP:NomeDoItem ou SP:NomeDoItem.";
            // 
            // txtTrainingRecoveryItems
            // 
            this.txtTrainingRecoveryItems.AcceptsReturn = true;
            this.txtTrainingRecoveryItems.Location = new System.Drawing.Point(10, 65);
            this.txtTrainingRecoveryItems.Multiline = true;
            this.txtTrainingRecoveryItems.Name = "txtTrainingRecoveryItems";
            this.txtTrainingRecoveryItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTrainingRecoveryItems.Size = new System.Drawing.Size(244, 143);
            this.txtTrainingRecoveryItems.TabIndex = 37;
            // 
            // chkTrainingRecovery
            // 
            this.chkTrainingRecovery.AutoSize = true;
            this.chkTrainingRecovery.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkTrainingRecovery.Location = new System.Drawing.Point(263, 12);
            this.chkTrainingRecovery.Name = "chkTrainingRecovery";
            this.chkTrainingRecovery.Size = new System.Drawing.Size(184, 17);
            this.chkTrainingRecovery.TabIndex = 36;
            this.chkTrainingRecovery.Text = "Ativar recuperação automática";
            this.chkTrainingRecovery.UseVisualStyleBackColor = true;
            // 
            // grpTrainingBuffItems
            // 
            this.grpTrainingBuffItems.Controls.Add(this.lblTrainingBuffRefreshSeconds);
            this.grpTrainingBuffItems.Controls.Add(this.numTrainingBuffRefreshSeconds);
            this.grpTrainingBuffItems.Controls.Add(this.lblTrainingBuffItemsHint);
            this.grpTrainingBuffItems.Controls.Add(this.txtTrainingBuffItems);
            this.grpTrainingBuffItems.Controls.Add(this.chkTrainingBuffItems);
            this.grpTrainingBuffItems.Location = new System.Drawing.Point(288, 51);
            this.grpTrainingBuffItems.Name = "grpTrainingBuffItems";
            this.grpTrainingBuffItems.Size = new System.Drawing.Size(416, 358);
            this.grpTrainingBuffItems.TabIndex = 4;
            this.grpTrainingBuffItems.TabStop = false;
            this.grpTrainingBuffItems.Text = "Consumíveis com Buff (somente desmontado)";
            // 
            // lblTrainingBuffRefreshSeconds
            // 
            this.lblTrainingBuffRefreshSeconds.AutoSize = true;
            this.lblTrainingBuffRefreshSeconds.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingBuffRefreshSeconds.Location = new System.Drawing.Point(237, 63);
            this.lblTrainingBuffRefreshSeconds.Name = "lblTrainingBuffRefreshSeconds";
            this.lblTrainingBuffRefreshSeconds.Size = new System.Drawing.Size(160, 13);
            this.lblTrainingBuffRefreshSeconds.TabIndex = 4;
            this.lblTrainingBuffRefreshSeconds.Text = "Reaplicar quando faltar (seg):";
            // 
            // numTrainingBuffRefreshSeconds
            // 
            this.numTrainingBuffRefreshSeconds.Location = new System.Drawing.Point(290, 83);
            this.numTrainingBuffRefreshSeconds.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numTrainingBuffRefreshSeconds.Name = "numTrainingBuffRefreshSeconds";
            this.numTrainingBuffRefreshSeconds.Size = new System.Drawing.Size(120, 22);
            this.numTrainingBuffRefreshSeconds.TabIndex = 3;
            this.numTrainingBuffRefreshSeconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTrainingBuffRefreshSeconds.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged_1);
            // 
            // lblTrainingBuffItemsHint
            // 
            this.lblTrainingBuffItemsHint.AutoSize = true;
            this.lblTrainingBuffItemsHint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingBuffItemsHint.Location = new System.Drawing.Point(6, 42);
            this.lblTrainingBuffItemsHint.Name = "lblTrainingBuffItemsHint";
            this.lblTrainingBuffItemsHint.Size = new System.Drawing.Size(377, 13);
            this.lblTrainingBuffItemsHint.TabIndex = 2;
            this.lblTrainingBuffItemsHint.Text = "Um item por linha. O bot tenta reutilizar quando o buff do item expirar.";
            // 
            // txtTrainingBuffItems
            // 
            this.txtTrainingBuffItems.AcceptsReturn = true;
            this.txtTrainingBuffItems.Location = new System.Drawing.Point(7, 83);
            this.txtTrainingBuffItems.Multiline = true;
            this.txtTrainingBuffItems.Name = "txtTrainingBuffItems";
            this.txtTrainingBuffItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTrainingBuffItems.Size = new System.Drawing.Size(268, 269);
            this.txtTrainingBuffItems.TabIndex = 1;
            // 
            // chkTrainingBuffItems
            // 
            this.chkTrainingBuffItems.AutoSize = true;
            this.chkTrainingBuffItems.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkTrainingBuffItems.Location = new System.Drawing.Point(7, 22);
            this.chkTrainingBuffItems.Name = "chkTrainingBuffItems";
            this.chkTrainingBuffItems.Size = new System.Drawing.Size(274, 17);
            this.chkTrainingBuffItems.TabIndex = 0;
            this.chkTrainingBuffItems.Text = "Ativar uso automático de consumíveis com buff";
            this.chkTrainingBuffItems.UseVisualStyleBackColor = true;
            // 
            // grpTrainingSkills
            // 
            this.grpTrainingSkills.Controls.Add(this.txtTrainingSkills);
            this.grpTrainingSkills.Controls.Add(this.lblTrainingSkillsHint);
            this.grpTrainingSkills.Controls.Add(this.chkTrainingSkills);
            this.grpTrainingSkills.Location = new System.Drawing.Point(9, 51);
            this.grpTrainingSkills.Name = "grpTrainingSkills";
            this.grpTrainingSkills.Size = new System.Drawing.Size(260, 358);
            this.grpTrainingSkills.TabIndex = 3;
            this.grpTrainingSkills.TabStop = false;
            this.grpTrainingSkills.Text = "Skills (somente desmontado)";
            this.grpTrainingSkills.Enter += new System.EventHandler(this.grpTrainingSkills_Enter);
            // 
            // txtTrainingSkills
            // 
            this.txtTrainingSkills.AcceptsReturn = true;
            this.txtTrainingSkills.Location = new System.Drawing.Point(10, 83);
            this.txtTrainingSkills.Multiline = true;
            this.txtTrainingSkills.Name = "txtTrainingSkills";
            this.txtTrainingSkills.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTrainingSkills.Size = new System.Drawing.Size(244, 269);
            this.txtTrainingSkills.TabIndex = 3;
            // 
            // lblTrainingSkillsHint
            // 
            this.lblTrainingSkillsHint.AutoSize = true;
            this.lblTrainingSkillsHint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingSkillsHint.Location = new System.Drawing.Point(7, 50);
            this.lblTrainingSkillsHint.Name = "lblTrainingSkillsHint";
            this.lblTrainingSkillsHint.Size = new System.Drawing.Size(255, 13);
            this.lblTrainingSkillsHint.TabIndex = 2;
            this.lblTrainingSkillsHint.Text = "Uma skill por linha. Use o nome interno da skill.";
            // 
            // chkTrainingSkills
            // 
            this.chkTrainingSkills.AutoSize = true;
            this.chkTrainingSkills.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkTrainingSkills.Location = new System.Drawing.Point(69, 21);
            this.chkTrainingSkills.Name = "chkTrainingSkills";
            this.chkTrainingSkills.Size = new System.Drawing.Size(185, 17);
            this.chkTrainingSkills.TabIndex = 0;
            this.chkTrainingSkills.Text = "Ativar uso automático de skills";
            this.chkTrainingSkills.UseVisualStyleBackColor = true;
            // 
            // lblTrainingInfo
            // 
            this.lblTrainingInfo.AutoSize = true;
            this.lblTrainingInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingInfo.Location = new System.Drawing.Point(6, 10);
            this.lblTrainingInfo.Name = "lblTrainingInfo";
            this.lblTrainingInfo.Size = new System.Drawing.Size(463, 13);
            this.lblTrainingInfo.TabIndex = 2;
            this.lblTrainingInfo.Text = "Modo exclusivo. Skills, consumíveis com buff e recuperação só funcionam desmontad" +
    "o.";
            // 
            // lblTrainingStatus
            // 
            this.lblTrainingStatus.AutoSize = true;
            this.lblTrainingStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrainingStatus.Location = new System.Drawing.Point(639, 6);
            this.lblTrainingStatus.Name = "lblTrainingStatus";
            this.lblTrainingStatus.Size = new System.Drawing.Size(65, 13);
            this.lblTrainingStatus.TabIndex = 1;
            this.lblTrainingStatus.Text = "Status: OFF";
            // 
            // btnTrainingToggle
            // 
            this.btnTrainingToggle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTrainingToggle.Location = new System.Drawing.Point(598, 22);
            this.btnTrainingToggle.Name = "btnTrainingToggle";
            this.btnTrainingToggle.Size = new System.Drawing.Size(121, 23);
            this.btnTrainingToggle.TabIndex = 0;
            this.btnTrainingToggle.Text = "Start Treinamento";
            this.btnTrainingToggle.UseVisualStyleBackColor = true;
            // 
            // btnTestHome
            // 
            this.btnTestHome.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnTestHome.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTestHome.Location = new System.Drawing.Point(605, 322);
            this.btnTestHome.Name = "btnTestHome";
            this.btnTestHome.Size = new System.Drawing.Size(75, 23);
            this.btnTestHome.TabIndex = 35;
            this.btnTestHome.Text = "HOME";
            this.btnTestHome.UseVisualStyleBackColor = true;
            this.btnTestHome.Click += new System.EventHandler(this.btnTestHome_Click);
            // 
            // listViewBag
            // 
            this.listViewBag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.listViewBag.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.listViewBag.GridLines = true;
            this.listViewBag.HideSelection = false;
            this.listViewBag.Location = new System.Drawing.Point(494, 568);
            this.listViewBag.Name = "listViewBag";
            this.listViewBag.Size = new System.Drawing.Size(204, 397);
            this.listViewBag.TabIndex = 11;
            this.listViewBag.UseCompatibleStateImageBehavior = false;
            this.listViewBag.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name:";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Qty:";
            this.columnHeader5.Width = 50;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtInspectPlayerName
            // 
            this.txtInspectPlayerName.Location = new System.Drawing.Point(882, 117);
            this.txtInspectPlayerName.Name = "txtInspectPlayerName";
            this.txtInspectPlayerName.Size = new System.Drawing.Size(118, 20);
            this.txtInspectPlayerName.TabIndex = 36;
            // 
            // btnInspectPlayer
            // 
            this.btnInspectPlayer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInspectPlayer.Location = new System.Drawing.Point(755, 115);
            this.btnInspectPlayer.Name = "btnInspectPlayer";
            this.btnInspectPlayer.Size = new System.Drawing.Size(112, 23);
            this.btnInspectPlayer.TabIndex = 37;
            this.btnInspectPlayer.Text = "Extrair Inspect";
            this.btnInspectPlayer.UseVisualStyleBackColor = true;
            this.btnInspectPlayer.Click += new System.EventHandler(this.btnInspectPlayer_Click);
            // 
            // barSP
            // 
            this.barSP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.barSP.BarColor = System.Drawing.Color.Goldenrod;
            this.barSP.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.barSP.Location = new System.Drawing.Point(356, 9);
            this.barSP.Maximum = 100;
            this.barSP.Name = "barSP";
            this.barSP.Size = new System.Drawing.Size(338, 30);
            this.barSP.TabIndex = 10;
            this.barSP.Text = "coloredProgressBar1";
            this.barSP.Value = 100;
            // 
            // barHP
            // 
            this.barHP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.barHP.BarColor = System.Drawing.Color.DarkRed;
            this.barHP.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.barHP.Location = new System.Drawing.Point(12, 9);
            this.barHP.Maximum = 100;
            this.barHP.Name = "barHP";
            this.barHP.Size = new System.Drawing.Size(338, 30);
            this.barHP.TabIndex = 9;
            this.barHP.Text = "coloredProgressBar1";
            this.barHP.Value = 100;
            // 
            // visualRadar1
            // 
            this.visualRadar1.BackColor = System.Drawing.SystemColors.WindowText;
            this.visualRadar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.visualRadar1.Location = new System.Drawing.Point(1156, 42);
            this.visualRadar1.Name = "visualRadar1";
            this.visualRadar1.Size = new System.Drawing.Size(271, 259);
            this.visualRadar1.TabIndex = 8;
            this.visualRadar1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1439, 966);
            this.Controls.Add(this.btnInspectPlayer);
            this.Controls.Add(this.txtInspectPlayerName);
            this.Controls.Add(this.listViewBag);
            this.Controls.Add(this.btnTestHome);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRecordRoute);
            this.Controls.Add(this.btnStartBot);
            this.Controls.Add(this.btnLoadRoute);
            this.Controls.Add(this.barSP);
            this.Controls.Add(this.barHP);
            this.Controls.Add(this.visualRadar1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.lblZ);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.btnConnect);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "Form1";
            this.Text = "Wild Terra Bot - M4rMil";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEatThreshold)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabHeal.ResumeLayout(false);
            this.tabHeal.PerformLayout();
            this.grpHealFollowMode.ResumeLayout(false);
            this.grpHealFollowMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHealSelfRecoveryResumeHpPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealSelfRecoveryHpPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealFollowDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealFollowTargetHpPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealRadius)).EndInit();
            this.tabTaming.ResumeLayout(false);
            this.tabTaming.PerformLayout();
            this.tabTraining.ResumeLayout(false);
            this.tabTraining.PerformLayout();
            this.grpTrainingAutoAttack.ResumeLayout(false);
            this.grpTrainingAutoAttack.PerformLayout();
            this.grpTrainingRecovery.ResumeLayout(false);
            this.grpTrainingRecovery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainingSpThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainingHpThreshold)).EndInit();
            this.grpTrainingBuffItems.ResumeLayout(false);
            this.grpTrainingBuffItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainingBuffRefreshSeconds)).EndInit();
            this.grpTrainingSkills.ResumeLayout(false);
            this.grpTrainingSkills.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.visualRadar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private WildTerraDashboard.VisualRadar visualRadar1;
        private WildTerraDashboard.ColoredProgressBar barHP;
        private WildTerraDashboard.ColoredProgressBar barSP;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnLoadRoute;
        private System.Windows.Forms.Button btnStartBot;
        private System.Windows.Forms.Timer botTimer;
        private System.Windows.Forms.Button btnRecordRoute;
        private System.Windows.Forms.Timer recordTimer;
        private System.Windows.Forms.TextBox txtListaColeta;
        private System.Windows.Forms.CheckBox chkAtivarColeta;
        private System.Windows.Forms.Button btnSaveList;
        private System.Windows.Forms.Button btnLoadList;
        private System.Windows.Forms.TextBox txtSafeList;
        private System.Windows.Forms.TextBox txtBankX;
        private System.Windows.Forms.TextBox txtBankZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label Estrutura;
        private System.Windows.Forms.Button btnSaveSafe;
        private System.Windows.Forms.Button btnLoadSafe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDropList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAutoEat;
        private System.Windows.Forms.NumericUpDown numEatThreshold;
        private System.Windows.Forms.CheckBox chkUseMount;
        private System.Windows.Forms.Button btnTestMount;
        private System.Windows.Forms.CheckBox chkAtivarHunt;
        private System.Windows.Forms.TextBox txtListaMobs;
        private System.Windows.Forms.TextBox txtWeaponName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnTestHome;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBaitName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRodName;
        private System.Windows.Forms.Button btnStartFishing;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbFishingSpot;
        private System.Windows.Forms.TabPage tabHeal;
        private System.Windows.Forms.TextBox txtHealWeaponName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbHealTargetMode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numHealRadius;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtHealTargetNames;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtHealSkills;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnHealTrain;
        private System.Windows.Forms.Label lblHealStatus;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ListView listViewBag;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TabPage tabTaming;
        private System.Windows.Forms.Button btnStartTaming;
        private System.Windows.Forms.Label lblTamingTrapName;
        private System.Windows.Forms.Label lblTamingTargets;
        private System.Windows.Forms.ComboBox cmbTamingMode;
        private System.Windows.Forms.TextBox txtTamingTrapName;
        private System.Windows.Forms.TextBox txtTamingTargets;
        private System.Windows.Forms.Label lblTamingMode;
        private System.Windows.Forms.TabPage tabTraining;
        private System.Windows.Forms.Label lblTrainingStatus;
        private System.Windows.Forms.Button btnTrainingToggle;
        private System.Windows.Forms.GroupBox grpTrainingSkills;
        private System.Windows.Forms.CheckBox chkTrainingSkills;
        private System.Windows.Forms.Label lblTrainingInfo;
        private System.Windows.Forms.Label lblTrainingSkillsHint;
        private System.Windows.Forms.GroupBox grpTrainingBuffItems;
        private System.Windows.Forms.Label lblTrainingBuffItemsHint;
        private System.Windows.Forms.TextBox txtTrainingBuffItems;
        private System.Windows.Forms.CheckBox chkTrainingBuffItems;
        private System.Windows.Forms.NumericUpDown numTrainingBuffRefreshSeconds;
        private System.Windows.Forms.Label lblTrainingBuffRefreshSeconds;
        private System.Windows.Forms.GroupBox grpTrainingRecovery;
        private System.Windows.Forms.CheckBox chkTrainingRecovery;
        private System.Windows.Forms.TextBox txtTrainingSkills;
        private System.Windows.Forms.Label lblTrainingHpThreshold;
        private System.Windows.Forms.NumericUpDown numTrainingHpThreshold;
        private System.Windows.Forms.Label lblTrainingRecoveryHint;
        private System.Windows.Forms.TextBox txtTrainingRecoveryItems;
        private System.Windows.Forms.Label lblTrainingSpThreshold;
        private System.Windows.Forms.NumericUpDown numTrainingSpThreshold;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog2;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog3;
        private System.Windows.Forms.GroupBox grpHealFollowMode;
        private System.Windows.Forms.TextBox txtHealFollowSkill;
        private System.Windows.Forms.Label lblHealFollowSkill;
        private System.Windows.Forms.Label lblHealSelfRecoveryItems;
        private System.Windows.Forms.NumericUpDown numHealFollowDistance;
        private System.Windows.Forms.Label lblHealFollowDistance;
        private System.Windows.Forms.NumericUpDown numHealFollowTargetHpPct;
        private System.Windows.Forms.Label lblHealFollowTargetHpPct;
        private System.Windows.Forms.Label lblHealSelfRecoveryHpPct;
        private System.Windows.Forms.TextBox txtHealSelfRecoveryItems;
        private System.Windows.Forms.Label lblHealSelfRecoveryResumeHpPct;
        private System.Windows.Forms.NumericUpDown numHealSelfRecoveryResumeHpPct;
        private System.Windows.Forms.NumericUpDown numHealSelfRecoveryHpPct;
        private System.Windows.Forms.CheckBox chkHealFollowTopTarget;
        private System.Windows.Forms.GroupBox grpTrainingAutoAttack;
        private System.Windows.Forms.CheckBox chkTrainingAutoAttack;
        private System.Windows.Forms.TextBox txtTrainingAutoAttackTarget;
        private System.Windows.Forms.Label lblTrainingAutoAttackTarget;
        private System.Windows.Forms.TextBox txtInspectPlayerName;
        private System.Windows.Forms.Button btnInspectPlayer;
        private System.Windows.Forms.TextBox txtAutoEatStatus;
        private System.Windows.Forms.Label label19;
    }
}

