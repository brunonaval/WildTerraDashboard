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
            this.listViewBag = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.barSP = new WildTerraDashboard.ColoredProgressBar();
            this.barHP = new WildTerraDashboard.ColoredProgressBar();
            this.visualRadar1 = new WildTerraDashboard.VisualRadar();
            this.btnTestHome = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtRodName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBaitName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnStartFishing = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEatThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visualRadar1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Fuchsia;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(12, 86);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(130, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "🔌 Conectar";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(792, 13);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(35, 13);
            this.lblX.TabIndex = 3;
            this.lblX.Text = "label1";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(891, 13);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(35, 13);
            this.lblY.TabIndex = 4;
            this.lblY.Text = "label2";
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.Location = new System.Drawing.Point(1012, 13);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(35, 13);
            this.lblZ.TabIndex = 5;
            this.lblZ.Text = "label3";
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
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.columnHeader1.Text = "Tipo";
            this.columnHeader1.Width = 79;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Level";
            this.columnHeader6.Width = 64;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nome";
            this.columnHeader2.Width = 201;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Distância";
            this.columnHeader3.Width = 82;
            // 
            // listViewBag
            // 
            this.listViewBag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.listViewBag.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewBag.GridLines = true;
            this.listViewBag.HideSelection = false;
            this.listViewBag.Location = new System.Drawing.Point(490, 568);
            this.listViewBag.Name = "listViewBag";
            this.listViewBag.Size = new System.Drawing.Size(204, 397);
            this.listViewBag.TabIndex = 11;
            this.listViewBag.UseCompatibleStateImageBehavior = false;
            this.listViewBag.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Nome:";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Qtd:";
            this.columnHeader5.Width = 50;
            // 
            // btnLoadRoute
            // 
            this.btnLoadRoute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnLoadRoute.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLoadRoute.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadRoute.Location = new System.Drawing.Point(12, 144);
            this.btnLoadRoute.Name = "btnLoadRoute";
            this.btnLoadRoute.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLoadRoute.Size = new System.Drawing.Size(130, 23);
            this.btnLoadRoute.TabIndex = 12;
            this.btnLoadRoute.Text = "📂 Carregar Rota";
            this.btnLoadRoute.UseVisualStyleBackColor = false;
            this.btnLoadRoute.Click += new System.EventHandler(this.btnLoadRoute_Click);
            // 
            // btnStartBot
            // 
            this.btnStartBot.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnStartBot.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStartBot.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartBot.Location = new System.Drawing.Point(12, 57);
            this.btnStartBot.Name = "btnStartBot";
            this.btnStartBot.Size = new System.Drawing.Size(130, 23);
            this.btnStartBot.TabIndex = 13;
            this.btnStartBot.Text = "▶ Iniciar";
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
            this.btnRecordRoute.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecordRoute.Location = new System.Drawing.Point(12, 115);
            this.btnRecordRoute.Name = "btnRecordRoute";
            this.btnRecordRoute.Size = new System.Drawing.Size(130, 23);
            this.btnRecordRoute.TabIndex = 14;
            this.btnRecordRoute.Text = "⏺ Gravar Rota";
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
            this.chkAtivarColeta.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAtivarColeta.Location = new System.Drawing.Point(621, 6);
            this.chkAtivarColeta.Name = "chkAtivarColeta";
            this.chkAtivarColeta.Size = new System.Drawing.Size(93, 17);
            this.chkAtivarColeta.TabIndex = 16;
            this.chkAtivarColeta.Text = "Ativar Coleta";
            this.chkAtivarColeta.UseVisualStyleBackColor = true;
            // 
            // btnSaveList
            // 
            this.btnSaveList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSaveList.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveList.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSaveList.Location = new System.Drawing.Point(381, 80);
            this.btnSaveList.Name = "btnSaveList";
            this.btnSaveList.Size = new System.Drawing.Size(52, 23);
            this.btnSaveList.TabIndex = 17;
            this.btnSaveList.Text = "SALVAR";
            this.btnSaveList.UseVisualStyleBackColor = true;
            this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
            // 
            // btnLoadList
            // 
            this.btnLoadList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLoadList.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadList.Location = new System.Drawing.Point(381, 113);
            this.btnLoadList.Name = "btnLoadList";
            this.btnLoadList.Size = new System.Drawing.Size(52, 23);
            this.btnLoadList.TabIndex = 18;
            this.btnLoadList.Text = "ABRIR";
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
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Coor X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Coor Z";
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
            this.Estrutura.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Estrutura.Location = new System.Drawing.Point(3, 86);
            this.Estrutura.Name = "Estrutura";
            this.Estrutura.Size = new System.Drawing.Size(54, 13);
            this.Estrutura.TabIndex = 25;
            this.Estrutura.Text = "Estrutura";
            // 
            // btnSaveSafe
            // 
            this.btnSaveSafe.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSafe.Location = new System.Drawing.Point(65, 389);
            this.btnSaveSafe.Name = "btnSaveSafe";
            this.btnSaveSafe.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSafe.TabIndex = 26;
            this.btnSaveSafe.Text = "Save List";
            this.btnSaveSafe.UseVisualStyleBackColor = true;
            // 
            // btnLoadSafe
            // 
            this.btnLoadSafe.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(515, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "LISTA DE COLETA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1269, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "RADAR";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(701, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Coordenadas:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(218, 346);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "NÃO COLOCAR NO BAÚ";
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
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(559, 416);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "DROPAR";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tabPage1.Text = "Coleta";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnTestMount
            // 
            this.btnTestMount.Location = new System.Drawing.Point(190, 5);
            this.btnTestMount.Name = "btnTestMount";
            this.btnTestMount.Size = new System.Drawing.Size(75, 23);
            this.btnTestMount.TabIndex = 35;
            this.btnTestMount.Text = "Montar";
            this.btnTestMount.UseVisualStyleBackColor = true;
            this.btnTestMount.Click += new System.EventHandler(this.btnTestMount_Click);
            // 
            // chkUseMount
            // 
            this.chkUseMount.AutoSize = true;
            this.chkUseMount.Location = new System.Drawing.Point(63, 164);
            this.chkUseMount.Name = "chkUseMount";
            this.chkUseMount.Size = new System.Drawing.Size(141, 17);
            this.chkUseMount.TabIndex = 35;
            this.chkUseMount.Text = "Usar Montaria (>15m)";
            this.chkUseMount.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Localização da sua Casa";
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
            this.tabPage2.Text = "Combate";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Equipar a arma:";
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
            this.chkAtivarHunt.Location = new System.Drawing.Point(555, 55);
            this.chkAtivarHunt.Name = "chkAtivarHunt";
            this.chkAtivarHunt.Size = new System.Drawing.Size(84, 17);
            this.chkAtivarHunt.TabIndex = 1;
            this.chkAtivarHunt.Text = "Ativar Caça";
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
            this.tabPage3.Controls.Add(this.numEatThreshold);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.txtAutoEat);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(725, 649);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Comida";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // numEatThreshold
            // 
            this.numEatThreshold.Location = new System.Drawing.Point(554, 20);
            this.numEatThreshold.Name = "numEatThreshold";
            this.numEatThreshold.Size = new System.Drawing.Size(120, 22);
            this.numEatThreshold.TabIndex = 35;
            this.numEatThreshold.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(308, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Auto Eat (Lista)";
            // 
            // txtAutoEat
            // 
            this.txtAutoEat.Location = new System.Drawing.Point(190, 80);
            this.txtAutoEat.Multiline = true;
            this.txtAutoEat.Name = "txtAutoEat";
            this.txtAutoEat.Size = new System.Drawing.Size(341, 380);
            this.txtAutoEat.TabIndex = 0;
            // 
            // barSP
            // 
            this.barSP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.barSP.BarColor = System.Drawing.Color.Goldenrod;
            this.barSP.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.barHP.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.visualRadar1.Location = new System.Drawing.Point(1156, 42);
            this.visualRadar1.Name = "visualRadar1";
            this.visualRadar1.Size = new System.Drawing.Size(271, 259);
            this.visualRadar1.TabIndex = 8;
            this.visualRadar1.TabStop = false;
            // 
            // btnTestHome
            // 
            this.btnTestHome.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestHome.Location = new System.Drawing.Point(605, 322);
            this.btnTestHome.Name = "btnTestHome";
            this.btnTestHome.Size = new System.Drawing.Size(75, 23);
            this.btnTestHome.TabIndex = 35;
            this.btnTestHome.Text = "HOME";
            this.btnTestHome.UseVisualStyleBackColor = true;
            this.btnTestHome.Click += new System.EventHandler(this.btnTestHome_Click);
            // 
            // tabPage4
            // 
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
            this.tabPage4.Text = "Pesca";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtRodName
            // 
            this.txtRodName.Location = new System.Drawing.Point(95, 11);
            this.txtRodName.Name = "txtRodName";
            this.txtRodName.Size = new System.Drawing.Size(248, 22);
            this.txtRodName.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Nome da Vara:";
            // 
            // txtBaitName
            // 
            this.txtBaitName.Location = new System.Drawing.Point(95, 49);
            this.txtBaitName.Name = "txtBaitName";
            this.txtBaitName.Size = new System.Drawing.Size(248, 22);
            this.txtBaitName.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Nome da Isca:";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // btnStartFishing
            // 
            this.btnStartFishing.Location = new System.Drawing.Point(452, 20);
            this.btnStartFishing.Name = "btnStartFishing";
            this.btnStartFishing.Size = new System.Drawing.Size(75, 23);
            this.btnStartFishing.TabIndex = 4;
            this.btnStartFishing.Text = "INICIAR PESCA";
            this.btnStartFishing.UseVisualStyleBackColor = true;
            this.btnStartFishing.Click += new System.EventHandler(this.btnStartFishing_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1439, 966);
            this.Controls.Add(this.btnTestHome);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRecordRoute);
            this.Controls.Add(this.btnStartBot);
            this.Controls.Add(this.btnLoadRoute);
            this.Controls.Add(this.listViewBag);
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
            ((System.ComponentModel.ISupportInitialize)(this.visualRadar1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
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
        private System.Windows.Forms.ListView listViewBag;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
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
    }
}

