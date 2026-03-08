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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.btnTestHome = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.listViewBag = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            ((System.ComponentModel.ISupportInitialize)(this.numHealRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visualRadar1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Fuchsia;
            resources.ApplyResources(this.btnConnect, "btnConnect");
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblX
            // 
            resources.ApplyResources(this.lblX, "lblX");
            this.lblX.Name = "lblX";
            // 
            // lblY
            // 
            resources.ApplyResources(this.lblY, "lblY");
            this.lblY.Name = "lblY";
            // 
            // lblZ
            // 
            resources.ApplyResources(this.lblZ, "lblZ");
            this.lblZ.Name = "lblZ";
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            resources.ApplyResources(this.lstLog, "lstLog");
            this.lstLog.Name = "lstLog";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader3});
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // btnLoadRoute
            // 
            this.btnLoadRoute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            resources.ApplyResources(this.btnLoadRoute, "btnLoadRoute");
            this.btnLoadRoute.Name = "btnLoadRoute";
            this.btnLoadRoute.UseVisualStyleBackColor = false;
            this.btnLoadRoute.Click += new System.EventHandler(this.btnLoadRoute_Click);
            // 
            // btnStartBot
            // 
            this.btnStartBot.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.btnStartBot, "btnStartBot");
            this.btnStartBot.Name = "btnStartBot";
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
            resources.ApplyResources(this.btnRecordRoute, "btnRecordRoute");
            this.btnRecordRoute.Name = "btnRecordRoute";
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
            resources.ApplyResources(this.txtListaColeta, "txtListaColeta");
            this.txtListaColeta.Name = "txtListaColeta";
            // 
            // chkAtivarColeta
            // 
            resources.ApplyResources(this.chkAtivarColeta, "chkAtivarColeta");
            this.chkAtivarColeta.Name = "chkAtivarColeta";
            this.chkAtivarColeta.UseVisualStyleBackColor = true;
            // 
            // btnSaveList
            // 
            resources.ApplyResources(this.btnSaveList, "btnSaveList");
            this.btnSaveList.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSaveList.Name = "btnSaveList";
            this.btnSaveList.UseVisualStyleBackColor = true;
            this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
            // 
            // btnLoadList
            // 
            resources.ApplyResources(this.btnLoadList, "btnLoadList");
            this.btnLoadList.Name = "btnLoadList";
            this.btnLoadList.UseVisualStyleBackColor = true;
            this.btnLoadList.Click += new System.EventHandler(this.btnLoadList_Click);
            // 
            // txtSafeList
            // 
            resources.ApplyResources(this.txtSafeList, "txtSafeList");
            this.txtSafeList.Name = "txtSafeList";
            // 
            // txtBankX
            // 
            resources.ApplyResources(this.txtBankX, "txtBankX");
            this.txtBankX.Name = "txtBankX";
            // 
            // txtBankZ
            // 
            resources.ApplyResources(this.txtBankZ, "txtBankZ");
            this.txtBankZ.Name = "txtBankZ";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBankName
            // 
            resources.ApplyResources(this.txtBankName, "txtBankName");
            this.txtBankName.Name = "txtBankName";
            // 
            // Estrutura
            // 
            resources.ApplyResources(this.Estrutura, "Estrutura");
            this.Estrutura.Name = "Estrutura";
            // 
            // btnSaveSafe
            // 
            resources.ApplyResources(this.btnSaveSafe, "btnSaveSafe");
            this.btnSaveSafe.Name = "btnSaveSafe";
            this.btnSaveSafe.UseVisualStyleBackColor = true;
            // 
            // btnLoadSafe
            // 
            resources.ApplyResources(this.btnLoadSafe, "btnLoadSafe");
            this.btnLoadSafe.Name = "btnLoadSafe";
            this.btnLoadSafe.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtDropList
            // 
            resources.ApplyResources(this.txtDropList, "txtDropList");
            this.txtDropList.Name = "txtDropList";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabHeal);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
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
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnTestMount
            // 
            resources.ApplyResources(this.btnTestMount, "btnTestMount");
            this.btnTestMount.Name = "btnTestMount";
            this.btnTestMount.UseVisualStyleBackColor = true;
            this.btnTestMount.Click += new System.EventHandler(this.btnTestMount_Click);
            // 
            // chkUseMount
            // 
            resources.ApplyResources(this.chkUseMount, "chkUseMount");
            this.chkUseMount.Name = "chkUseMount";
            this.chkUseMount.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.txtWeaponName);
            this.tabPage2.Controls.Add(this.chkAtivarHunt);
            this.tabPage2.Controls.Add(this.txtListaMobs);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtWeaponName
            // 
            resources.ApplyResources(this.txtWeaponName, "txtWeaponName");
            this.txtWeaponName.Name = "txtWeaponName";
            this.txtWeaponName.TextChanged += new System.EventHandler(this.txtWeaponName_TextChanged);
            // 
            // chkAtivarHunt
            // 
            resources.ApplyResources(this.chkAtivarHunt, "chkAtivarHunt");
            this.chkAtivarHunt.Name = "chkAtivarHunt";
            this.chkAtivarHunt.UseVisualStyleBackColor = true;
            // 
            // txtListaMobs
            // 
            resources.ApplyResources(this.txtListaMobs, "txtListaMobs");
            this.txtListaMobs.Name = "txtListaMobs";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.numEatThreshold);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.txtAutoEat);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // numEatThreshold
            // 
            resources.ApplyResources(this.numEatThreshold, "numEatThreshold");
            this.numEatThreshold.Name = "numEatThreshold";
            this.numEatThreshold.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtAutoEat
            // 
            resources.ApplyResources(this.txtAutoEat, "txtAutoEat");
            this.txtAutoEat.Name = "txtAutoEat";
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
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // cmbFishingSpot
            // 
            this.cmbFishingSpot.FormattingEnabled = true;
            this.cmbFishingSpot.Items.AddRange(new object[] {
            resources.GetString("cmbFishingSpot.Items"),
            resources.GetString("cmbFishingSpot.Items1"),
            resources.GetString("cmbFishingSpot.Items2"),
            resources.GetString("cmbFishingSpot.Items3"),
            resources.GetString("cmbFishingSpot.Items4")});
            resources.ApplyResources(this.cmbFishingSpot, "cmbFishingSpot");
            this.cmbFishingSpot.Name = "cmbFishingSpot";
            // 
            // btnStartFishing
            // 
            resources.ApplyResources(this.btnStartFishing, "btnStartFishing");
            this.btnStartFishing.Name = "btnStartFishing";
            this.btnStartFishing.UseVisualStyleBackColor = true;
            this.btnStartFishing.Click += new System.EventHandler(this.btnStartFishing_Click);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // txtBaitName
            // 
            resources.ApplyResources(this.txtBaitName, "txtBaitName");
            this.txtBaitName.Name = "txtBaitName";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txtRodName
            // 
            resources.ApplyResources(this.txtRodName, "txtRodName");
            this.txtRodName.Name = "txtRodName";
            // 
            // tabHeal
            // 
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
            resources.ApplyResources(this.tabHeal, "tabHeal");
            this.tabHeal.Name = "tabHeal";
            this.tabHeal.UseVisualStyleBackColor = true;
            // 
            // lblHealStatus
            // 
            resources.ApplyResources(this.lblHealStatus, "lblHealStatus");
            this.lblHealStatus.Name = "lblHealStatus";
            // 
            // btnHealTrain
            // 
            resources.ApplyResources(this.btnHealTrain, "btnHealTrain");
            this.btnHealTrain.Name = "btnHealTrain";
            this.btnHealTrain.UseVisualStyleBackColor = true;
            this.btnHealTrain.Click += new System.EventHandler(this.btnHealTrain_Click);
            // 
            // txtHealTargetNames
            // 
            resources.ApplyResources(this.txtHealTargetNames, "txtHealTargetNames");
            this.txtHealTargetNames.Name = "txtHealTargetNames";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // txtHealSkills
            // 
            resources.ApplyResources(this.txtHealSkills, "txtHealSkills");
            this.txtHealSkills.Name = "txtHealSkills";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // numHealRadius
            // 
            resources.ApplyResources(this.numHealRadius, "numHealRadius");
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
            this.numHealRadius.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // cmbHealTargetMode
            // 
            this.cmbHealTargetMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHealTargetMode.FormattingEnabled = true;
            this.cmbHealTargetMode.Items.AddRange(new object[] {
            resources.GetString("cmbHealTargetMode.Items"),
            resources.GetString("cmbHealTargetMode.Items1"),
            resources.GetString("cmbHealTargetMode.Items2")});
            resources.ApplyResources(this.cmbHealTargetMode, "cmbHealTargetMode");
            this.cmbHealTargetMode.Name = "cmbHealTargetMode";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txtHealWeaponName
            // 
            resources.ApplyResources(this.txtHealWeaponName, "txtHealWeaponName");
            this.txtHealWeaponName.Name = "txtHealWeaponName";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // btnTestHome
            // 
            resources.ApplyResources(this.btnTestHome, "btnTestHome");
            this.btnTestHome.Name = "btnTestHome";
            this.btnTestHome.UseVisualStyleBackColor = true;
            this.btnTestHome.Click += new System.EventHandler(this.btnTestHome_Click);
            // 
            // listViewBag
            // 
            this.listViewBag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            resources.ApplyResources(this.listViewBag, "listViewBag");
            this.listViewBag.GridLines = true;
            this.listViewBag.HideSelection = false;
            this.listViewBag.Name = "listViewBag";
            this.listViewBag.UseCompatibleStateImageBehavior = false;
            this.listViewBag.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // barSP
            // 
            this.barSP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.barSP.BarColor = System.Drawing.Color.Goldenrod;
            resources.ApplyResources(this.barSP, "barSP");
            this.barSP.Maximum = 100;
            this.barSP.Name = "barSP";
            this.barSP.Value = 100;
            // 
            // barHP
            // 
            this.barHP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.barHP.BarColor = System.Drawing.Color.DarkRed;
            resources.ApplyResources(this.barHP, "barHP");
            this.barHP.Maximum = 100;
            this.barHP.Name = "barHP";
            this.barHP.Value = 100;
            // 
            // visualRadar1
            // 
            this.visualRadar1.BackColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.visualRadar1, "visualRadar1");
            this.visualRadar1.Name = "visualRadar1";
            this.visualRadar1.TabStop = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
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
            this.Name = "Form1";
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
            ((System.ComponentModel.ISupportInitialize)(this.numHealRadius)).EndInit();
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
        private VisualRadar visualRadar1;
        private ColoredProgressBar barHP;
        private ColoredProgressBar barSP;
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
    }
}

