namespace AnalogRotator
{
   partial class StreamingAIForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamingAIForm));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.trackBar_div = new System.Windows.Forms.TrackBar();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_pause = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label_YCoordinateMin = new System.Windows.Forms.Label();
            this.label_YCoordinateMax = new System.Windows.Forms.Label();
            this.label_YCoordinateMiddle = new System.Windows.Forms.Label();
            this.label_XCoordinateMin = new System.Windows.Forms.Label();
            this.label_XCoordinateMax = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.waveformAiCtrl1 = new Automation.BDaq.WaveformAiCtrl(this.components);
            this.lblLowPass = new System.Windows.Forms.Label();
            this.tbxLowPass = new System.Windows.Forms.TextBox();
            this.tbxHighPass = new System.Windows.Forms.TextBox();
            this.lblHighPass = new System.Windows.Forms.Label();
            this.pnlVoltageRange = new System.Windows.Forms.Panel();
            this.radV1 = new System.Windows.Forms.RadioButton();
            this.radV2 = new System.Windows.Forms.RadioButton();
            this.radV5 = new System.Windows.Forms.RadioButton();
            this.radV10 = new System.Windows.Forms.RadioButton();
            this.lblVoltageRange = new System.Windows.Forms.Label();
            this.chkGenReport = new System.Windows.Forms.CheckBox();
            this.pnlPartInfo = new System.Windows.Forms.Panel();
            this.btnLoadReportVals = new System.Windows.Forms.Button();
            this.btnSaveReportVals = new System.Windows.Forms.Button();
            this.cmbxPartNo = new System.Windows.Forms.ComboBox();
            this.chkMasterPart = new System.Windows.Forms.CheckBox();
            this.lblETLvl1 = new System.Windows.Forms.Label();
            this.lblETLvl2 = new System.Windows.Forms.Label();
            this.lblETTSNo = new System.Windows.Forms.Label();
            this.lblMasterSN = new System.Windows.Forms.Label();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.lblPtrNo = new System.Windows.Forms.Label();
            this.lblSN = new System.Windows.Forms.Label();
            this.lblPN = new System.Windows.Forms.Label();
            this.txtETLev1 = new System.Windows.Forms.TextBox();
            this.txtETLev2 = new System.Windows.Forms.TextBox();
            this.txtETTSNo = new System.Windows.Forms.TextBox();
            this.txtMasterSN = new System.Windows.Forms.TextBox();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtPTRNo = new System.Windows.Forms.TextBox();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.txtPN = new System.Windows.Forms.TextBox();
            this.btnPreviewFileName = new System.Windows.Forms.Button();
            this.lblReportFilenameDisplay = new System.Windows.Forms.Label();
            this.lblReportFileName = new System.Windows.Forms.Label();
            this.txtGain = new System.Windows.Forms.TextBox();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.lblGain = new System.Windows.Forms.Label();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblAngle = new System.Windows.Forms.Label();
            this.llbProbeDrive = new System.Windows.Forms.Label();
            this.lblProbeType = new System.Windows.Forms.Label();
            this.lblStopOnFail = new System.Windows.Forms.Label();
            this.btnGetUniWestVals = new System.Windows.Forms.Button();
            this.tbcOptionPanel = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmbProbeDrive = new System.Windows.Forms.TextBox();
            this.btnHPSet = new System.Windows.Forms.Button();
            this.cmbProbeType = new System.Windows.Forms.TextBox();
            this.btnLPSet = new System.Windows.Forms.Button();
            this.btnGainSet = new System.Windows.Forms.Button();
            this.btnFreqSet = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnPartInfo = new System.Windows.Forms.Button();
            this.btnEditPartInfo = new System.Windows.Forms.Button();
            this.txt4Tolerance = new System.Windows.Forms.TextBox();
            this.lbl4Tolerance = new System.Windows.Forms.Label();
            this.btnUpdateMasterCal = new System.Windows.Forms.Button();
            this.lblMinUntilMaster = new System.Windows.Forms.Label();
            this.lblPartsUntilMaster = new System.Windows.Forms.Label();
            this.lblMuM = new System.Windows.Forms.Label();
            this.lblPuM = new System.Windows.Forms.Label();
            this.txtMinutesPerMaster = new System.Windows.Forms.TextBox();
            this.txtPartsPerMaster = new System.Windows.Forms.TextBox();
            this.lblMinutesPerMaster = new System.Windows.Forms.Label();
            this.lblPartsPerMaster = new System.Windows.Forms.Label();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.btnDoneEditing = new System.Windows.Forms.Button();
            this.btnPasswordSubmit = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtOptionsPassword = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grpSaveVoltage = new System.Windows.Forms.GroupBox();
            this.cbxSortVoltageByVal = new System.Windows.Forms.CheckBox();
            this.cbxVoltages = new System.Windows.Forms.CheckBox();
            this.cbxVoltageD = new System.Windows.Forms.CheckBox();
            this.cbxVoltageV = new System.Windows.Forms.CheckBox();
            this.cbxVoltageH = new System.Windows.Forms.CheckBox();
            this.grpPrimaryGraph = new System.Windows.Forms.GroupBox();
            this.radPrimaryVecSumT = new System.Windows.Forms.RadioButton();
            this.radPrimaryHorizT = new System.Windows.Forms.RadioButton();
            this.radPrimaryVertT = new System.Windows.Forms.RadioButton();
            this.chkTripleGraph = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxHorizontal = new System.Windows.Forms.CheckBox();
            this.cbxVertical = new System.Windows.Forms.CheckBox();
            this.cbxDistance = new System.Windows.Forms.CheckBox();
            this.grpDisplayedGraphs = new System.Windows.Forms.GroupBox();
            this.radDiffOpt = new System.Windows.Forms.RadioButton();
            this.radVerticalOpt = new System.Windows.Forms.RadioButton();
            this.radHorizOpt = new System.Windows.Forms.RadioButton();
            this.btnAbt = new System.Windows.Forms.Button();
            this.cbxAmpInch = new System.Windows.Forms.CheckBox();
            this.label_XCoordinateMin2 = new System.Windows.Forms.Label();
            this.label_XCoordinateMax2 = new System.Windows.Forms.Label();
            this.label_YCoordinateMin2 = new System.Windows.Forms.Label();
            this.label_YCoordinateMax2 = new System.Windows.Forms.Label();
            this.label_YCoordinateMiddle2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_XCoordinateMin3 = new System.Windows.Forms.Label();
            this.label_XCoordinateMax3 = new System.Windows.Forms.Label();
            this.label_YCoordinateMin3 = new System.Windows.Forms.Label();
            this.label_YCoordinateMax3 = new System.Windows.Forms.Label();
            this.label_YCoordinateMiddle3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tmrStartFileChecking = new System.Windows.Forms.Timer(this.components);
            this.tmrEndFileChecking = new System.Windows.Forms.Timer(this.components);
            this.btnAddPart = new System.Windows.Forms.Button();
            this.grpMainControls = new System.Windows.Forms.GroupBox();
            this.cbxLoadUniwest = new System.Windows.Forms.CheckBox();
            this.chkLoadMSPlan = new System.Windows.Forms.CheckBox();
            this.BtnReconnectUniWest = new System.Windows.Forms.Button();
            this.lblBiPolarOrUniPolar = new System.Windows.Forms.Label();
            this.BtnClearGraphs = new System.Windows.Forms.Button();
            this.BtnNullUniWest = new System.Windows.Forms.Button();
            this.lblTopGraph = new System.Windows.Forms.Label();
            this.lblMiddleGraph = new System.Windows.Forms.Label();
            this.lblBottomGraph = new System.Windows.Forms.Label();
            this.grpMasterCalibResults = new System.Windows.Forms.GroupBox();
            this.btnCalibrationReject = new System.Windows.Forms.Button();
            this.btnCalibrationAccept = new System.Windows.Forms.Button();
            this.lblCalibrationResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_div)).BeginInit();
            this.pnlVoltageRange.SuspendLayout();
            this.pnlPartInfo.SuspendLayout();
            this.tbcOptionPanel.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grpSaveVoltage.SuspendLayout();
            this.grpPrimaryGraph.SuspendLayout();
            this.grpDisplayedGraphs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.grpMainControls.SuspendLayout();
            this.grpMasterCalibResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Black;
            this.pictureBox.Location = new System.Drawing.Point(56, 33);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(2300, 312);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // trackBar_div
            // 
            this.trackBar_div.AutoSize = false;
            this.trackBar_div.BackColor = System.Drawing.Color.PapayaWhip;
            this.trackBar_div.Location = new System.Drawing.Point(527, 18);
            this.trackBar_div.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackBar_div.Maximum = 1000;
            this.trackBar_div.Minimum = 10;
            this.trackBar_div.Name = "trackBar_div";
            this.trackBar_div.Size = new System.Drawing.Size(154, 23);
            this.trackBar_div.TabIndex = 15;
            this.trackBar_div.TickFrequency = 100;
            this.trackBar_div.Value = 100;
            this.trackBar_div.Scroll += new System.EventHandler(this.trackBar_div_Scroll);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(646, 64);
            this.button_stop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(76, 26);
            this.button_stop.TabIndex = 6;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_pause
            // 
            this.button_pause.Location = new System.Drawing.Point(562, 64);
            this.button_pause.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_pause.Name = "button_pause";
            this.button_pause.Size = new System.Drawing.Size(76, 26);
            this.button_pause.TabIndex = 5;
            this.button_pause.Text = "Pause";
            this.button_pause.UseVisualStyleBackColor = true;
            this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(479, 64);
            this.button_start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(76, 26);
            this.button_start.TabIndex = 4;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // listView
            // 
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(129, 10);
            this.listView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(352, 37);
            this.listView.TabIndex = 20;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Color of channels:";
            // 
            // label_YCoordinateMin
            // 
            this.label_YCoordinateMin.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMin.Location = new System.Drawing.Point(-3, 310);
            this.label_YCoordinateMin.Name = "label_YCoordinateMin";
            this.label_YCoordinateMin.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMin.TabIndex = 27;
            this.label_YCoordinateMin.Text = "0V";
            this.label_YCoordinateMin.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMin.Visible = false;
            // 
            // label_YCoordinateMax
            // 
            this.label_YCoordinateMax.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMax.Location = new System.Drawing.Point(-3, 33);
            this.label_YCoordinateMax.Name = "label_YCoordinateMax";
            this.label_YCoordinateMax.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMax.TabIndex = 26;
            this.label_YCoordinateMax.Text = "5V";
            this.label_YCoordinateMax.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMax.Visible = false;
            // 
            // label_YCoordinateMiddle
            // 
            this.label_YCoordinateMiddle.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMiddle.Location = new System.Drawing.Point(-3, 172);
            this.label_YCoordinateMiddle.Name = "label_YCoordinateMiddle";
            this.label_YCoordinateMiddle.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMiddle.TabIndex = 28;
            this.label_YCoordinateMiddle.Text = "0V";
            this.label_YCoordinateMiddle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMiddle.Visible = false;
            // 
            // label_XCoordinateMin
            // 
            this.label_XCoordinateMin.AutoSize = true;
            this.label_XCoordinateMin.BackColor = System.Drawing.Color.Transparent;
            this.label_XCoordinateMin.Location = new System.Drawing.Point(50, 330);
            this.label_XCoordinateMin.Name = "label_XCoordinateMin";
            this.label_XCoordinateMin.Size = new System.Drawing.Size(38, 15);
            this.label_XCoordinateMin.TabIndex = 31;
            this.label_XCoordinateMin.Text = "0 Sec";
            this.label_XCoordinateMin.Visible = false;
            // 
            // label_XCoordinateMax
            // 
            this.label_XCoordinateMax.AutoSize = true;
            this.label_XCoordinateMax.BackColor = System.Drawing.Color.Transparent;
            this.label_XCoordinateMax.Location = new System.Drawing.Point(2311, 332);
            this.label_XCoordinateMax.Name = "label_XCoordinateMax";
            this.label_XCoordinateMax.Size = new System.Drawing.Size(45, 15);
            this.label_XCoordinateMax.TabIndex = 30;
            this.label_XCoordinateMax.Text = "12 Sec";
            this.label_XCoordinateMax.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(495, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
            this.label2.TabIndex = 32;
            this.label2.Text = "Div:";
            // 
            // waveformAiCtrl1
            // 
            this.waveformAiCtrl1._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("waveformAiCtrl1._StateStream")));
            this.waveformAiCtrl1.DataReady += new System.EventHandler<Automation.BDaq.BfdAiEventArgs>(this.waveformAiCtrl1_DataReady);
            this.waveformAiCtrl1.Overrun += new System.EventHandler<Automation.BDaq.BfdAiEventArgs>(this.waveformAiCtrl1_Overrun);
            this.waveformAiCtrl1.CacheOverflow += new System.EventHandler<Automation.BDaq.BfdAiEventArgs>(this.waveformAiCtrl1_CacheOverflow);
            // 
            // lblLowPass
            // 
            this.lblLowPass.AutoSize = true;
            this.lblLowPass.Location = new System.Drawing.Point(445, 14);
            this.lblLowPass.Name = "lblLowPass";
            this.lblLowPass.Size = new System.Drawing.Size(90, 15);
            this.lblLowPass.TabIndex = 33;
            this.lblLowPass.Text = "Low Pass Filter";
            // 
            // tbxLowPass
            // 
            this.tbxLowPass.Location = new System.Drawing.Point(541, 14);
            this.tbxLowPass.Name = "tbxLowPass";
            this.tbxLowPass.Size = new System.Drawing.Size(68, 21);
            this.tbxLowPass.TabIndex = 7;
            // 
            // tbxHighPass
            // 
            this.tbxHighPass.Location = new System.Drawing.Point(541, 43);
            this.tbxHighPass.Name = "tbxHighPass";
            this.tbxHighPass.Size = new System.Drawing.Size(68, 21);
            this.tbxHighPass.TabIndex = 9;
            // 
            // lblHighPass
            // 
            this.lblHighPass.AutoSize = true;
            this.lblHighPass.Location = new System.Drawing.Point(442, 49);
            this.lblHighPass.Name = "lblHighPass";
            this.lblHighPass.Size = new System.Drawing.Size(93, 15);
            this.lblHighPass.TabIndex = 35;
            this.lblHighPass.Text = "High Pass Filter";
            // 
            // pnlVoltageRange
            // 
            this.pnlVoltageRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVoltageRange.Controls.Add(this.radV1);
            this.pnlVoltageRange.Controls.Add(this.radV2);
            this.pnlVoltageRange.Controls.Add(this.radV5);
            this.pnlVoltageRange.Controls.Add(this.radV10);
            this.pnlVoltageRange.Controls.Add(this.lblVoltageRange);
            this.pnlVoltageRange.Location = new System.Drawing.Point(21, 80);
            this.pnlVoltageRange.Name = "pnlVoltageRange";
            this.pnlVoltageRange.Size = new System.Drawing.Size(65, 146);
            this.pnlVoltageRange.TabIndex = 37;
            // 
            // radV1
            // 
            this.radV1.AutoSize = true;
            this.radV1.Location = new System.Drawing.Point(6, 123);
            this.radV1.Name = "radV1";
            this.radV1.Size = new System.Drawing.Size(39, 19);
            this.radV1.TabIndex = 4;
            this.radV1.Text = "1V";
            this.radV1.UseVisualStyleBackColor = true;
            this.radV1.CheckedChanged += new System.EventHandler(this.ChangeVoltageRange);
            // 
            // radV2
            // 
            this.radV2.AutoSize = true;
            this.radV2.Location = new System.Drawing.Point(6, 98);
            this.radV2.Name = "radV2";
            this.radV2.Size = new System.Drawing.Size(49, 19);
            this.radV2.TabIndex = 3;
            this.radV2.Text = "2.5V";
            this.radV2.UseVisualStyleBackColor = true;
            this.radV2.CheckedChanged += new System.EventHandler(this.ChangeVoltageRange);
            // 
            // radV5
            // 
            this.radV5.AutoSize = true;
            this.radV5.Location = new System.Drawing.Point(6, 73);
            this.radV5.Name = "radV5";
            this.radV5.Size = new System.Drawing.Size(39, 19);
            this.radV5.TabIndex = 2;
            this.radV5.Text = "5V";
            this.radV5.UseVisualStyleBackColor = true;
            this.radV5.CheckedChanged += new System.EventHandler(this.ChangeVoltageRange);
            // 
            // radV10
            // 
            this.radV10.AutoSize = true;
            this.radV10.Checked = true;
            this.radV10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radV10.Location = new System.Drawing.Point(5, 48);
            this.radV10.Name = "radV10";
            this.radV10.Size = new System.Drawing.Size(46, 19);
            this.radV10.TabIndex = 1;
            this.radV10.TabStop = true;
            this.radV10.Text = "10V";
            this.radV10.UseVisualStyleBackColor = true;
            this.radV10.CheckedChanged += new System.EventHandler(this.ChangeVoltageRange);
            // 
            // lblVoltageRange
            // 
            this.lblVoltageRange.AutoSize = true;
            this.lblVoltageRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoltageRange.Location = new System.Drawing.Point(3, 6);
            this.lblVoltageRange.Name = "lblVoltageRange";
            this.lblVoltageRange.Size = new System.Drawing.Size(57, 36);
            this.lblVoltageRange.TabIndex = 0;
            this.lblVoltageRange.Text = "Voltage\r\nRange:";
            // 
            // chkGenReport
            // 
            this.chkGenReport.AutoSize = true;
            this.chkGenReport.Location = new System.Drawing.Point(360, 61);
            this.chkGenReport.Name = "chkGenReport";
            this.chkGenReport.Size = new System.Drawing.Size(106, 34);
            this.chkGenReport.TabIndex = 3;
            this.chkGenReport.Text = "Create Report\r\nAfter Scan End";
            this.chkGenReport.UseVisualStyleBackColor = true;
            this.chkGenReport.CheckedChanged += new System.EventHandler(this.chkGenReport_CheckedChanged);
            // 
            // pnlPartInfo
            // 
            this.pnlPartInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPartInfo.Controls.Add(this.btnLoadReportVals);
            this.pnlPartInfo.Controls.Add(this.btnSaveReportVals);
            this.pnlPartInfo.Controls.Add(this.cmbxPartNo);
            this.pnlPartInfo.Controls.Add(this.chkMasterPart);
            this.pnlPartInfo.Controls.Add(this.lblETLvl1);
            this.pnlPartInfo.Controls.Add(this.lblETLvl2);
            this.pnlPartInfo.Controls.Add(this.lblETTSNo);
            this.pnlPartInfo.Controls.Add(this.lblMasterSN);
            this.pnlPartInfo.Controls.Add(this.lblBatchNo);
            this.pnlPartInfo.Controls.Add(this.lblPtrNo);
            this.pnlPartInfo.Controls.Add(this.lblSN);
            this.pnlPartInfo.Controls.Add(this.lblPN);
            this.pnlPartInfo.Controls.Add(this.txtETLev1);
            this.pnlPartInfo.Controls.Add(this.txtETLev2);
            this.pnlPartInfo.Controls.Add(this.txtETTSNo);
            this.pnlPartInfo.Controls.Add(this.txtMasterSN);
            this.pnlPartInfo.Controls.Add(this.txtBatchNo);
            this.pnlPartInfo.Controls.Add(this.txtPTRNo);
            this.pnlPartInfo.Controls.Add(this.txtSN);
            this.pnlPartInfo.Controls.Add(this.txtPN);
            this.pnlPartInfo.Location = new System.Drawing.Point(104, 113);
            this.pnlPartInfo.Name = "pnlPartInfo";
            this.pnlPartInfo.Size = new System.Drawing.Size(652, 113);
            this.pnlPartInfo.TabIndex = 39;
            // 
            // btnLoadReportVals
            // 
            this.btnLoadReportVals.Location = new System.Drawing.Point(562, 78);
            this.btnLoadReportVals.Name = "btnLoadReportVals";
            this.btnLoadReportVals.Size = new System.Drawing.Size(63, 24);
            this.btnLoadReportVals.TabIndex = 10;
            this.btnLoadReportVals.Text = "Load";
            this.btnLoadReportVals.UseVisualStyleBackColor = true;
            this.btnLoadReportVals.Click += new System.EventHandler(this.btnLoadReportFile_Click);
            // 
            // btnSaveReportVals
            // 
            this.btnSaveReportVals.Location = new System.Drawing.Point(487, 78);
            this.btnSaveReportVals.Name = "btnSaveReportVals";
            this.btnSaveReportVals.Size = new System.Drawing.Size(63, 24);
            this.btnSaveReportVals.TabIndex = 9;
            this.btnSaveReportVals.Text = "Save";
            this.btnSaveReportVals.UseVisualStyleBackColor = true;
            this.btnSaveReportVals.Click += new System.EventHandler(this.btnSaveReportFile_Click);
            // 
            // cmbxPartNo
            // 
            this.cmbxPartNo.FormattingEnabled = true;
            this.cmbxPartNo.Location = new System.Drawing.Point(62, 14);
            this.cmbxPartNo.Name = "cmbxPartNo";
            this.cmbxPartNo.Size = new System.Drawing.Size(100, 23);
            this.cmbxPartNo.TabIndex = 0;
            this.cmbxPartNo.SelectedValueChanged += new System.EventHandler(this.cmbxPartType_SelectedValueChanged);
            // 
            // chkMasterPart
            // 
            this.chkMasterPart.AutoSize = true;
            this.chkMasterPart.Location = new System.Drawing.Point(380, 82);
            this.chkMasterPart.Name = "chkMasterPart";
            this.chkMasterPart.Size = new System.Drawing.Size(101, 19);
            this.chkMasterPart.TabIndex = 8;
            this.chkMasterPart.Text = "Is Master Part";
            this.chkMasterPart.UseVisualStyleBackColor = true;
            this.chkMasterPart.CheckedChanged += new System.EventHandler(this.chkMasterPart_CheckedChanged);
            // 
            // lblETLvl1
            // 
            this.lblETLvl1.AutoSize = true;
            this.lblETLvl1.Location = new System.Drawing.Point(377, 51);
            this.lblETLvl1.Name = "lblETLvl1";
            this.lblETLvl1.Size = new System.Drawing.Size(75, 15);
            this.lblETLvl1.TabIndex = 15;
            this.lblETLvl1.Text = "ET LEVEL 1:";
            // 
            // lblETLvl2
            // 
            this.lblETLvl2.AutoSize = true;
            this.lblETLvl2.Location = new System.Drawing.Point(377, 20);
            this.lblETLvl2.Name = "lblETLvl2";
            this.lblETLvl2.Size = new System.Drawing.Size(75, 15);
            this.lblETLvl2.TabIndex = 14;
            this.lblETLvl2.Text = "ET LEVEL 2:";
            // 
            // lblETTSNo
            // 
            this.lblETTSNo.AutoSize = true;
            this.lblETTSNo.Location = new System.Drawing.Point(190, 83);
            this.lblETTSNo.Name = "lblETTSNo";
            this.lblETTSNo.Size = new System.Drawing.Size(59, 15);
            this.lblETTSNo.TabIndex = 13;
            this.lblETTSNo.Text = "ETTS No:";
            // 
            // lblMasterSN
            // 
            this.lblMasterSN.AutoSize = true;
            this.lblMasterSN.Location = new System.Drawing.Point(178, 51);
            this.lblMasterSN.Name = "lblMasterSN";
            this.lblMasterSN.Size = new System.Drawing.Size(71, 15);
            this.lblMasterSN.TabIndex = 12;
            this.lblMasterSN.Text = "Master S/N:";
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Location = new System.Drawing.Point(189, 20);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(60, 15);
            this.lblBatchNo.TabIndex = 11;
            this.lblBatchNo.Text = "Batch No:";
            // 
            // lblPtrNo
            // 
            this.lblPtrNo.AutoSize = true;
            this.lblPtrNo.Location = new System.Drawing.Point(3, 83);
            this.lblPtrNo.Name = "lblPtrNo";
            this.lblPtrNo.Size = new System.Drawing.Size(53, 15);
            this.lblPtrNo.TabIndex = 10;
            this.lblPtrNo.Text = "PTR No:";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(26, 53);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(30, 15);
            this.lblSN.TabIndex = 9;
            this.lblSN.Text = "S/N:";
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(26, 20);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(30, 15);
            this.lblPN.TabIndex = 8;
            this.lblPN.Text = "P/N:";
            // 
            // txtETLev1
            // 
            this.txtETLev1.Location = new System.Drawing.Point(458, 48);
            this.txtETLev1.Name = "txtETLev1";
            this.txtETLev1.Size = new System.Drawing.Size(167, 21);
            this.txtETLev1.TabIndex = 7;
            // 
            // txtETLev2
            // 
            this.txtETLev2.Location = new System.Drawing.Point(458, 17);
            this.txtETLev2.Name = "txtETLev2";
            this.txtETLev2.Size = new System.Drawing.Size(167, 21);
            this.txtETLev2.TabIndex = 6;
            // 
            // txtETTSNo
            // 
            this.txtETTSNo.Location = new System.Drawing.Point(255, 80);
            this.txtETTSNo.Name = "txtETTSNo";
            this.txtETTSNo.Size = new System.Drawing.Size(100, 21);
            this.txtETTSNo.TabIndex = 5;
            // 
            // txtMasterSN
            // 
            this.txtMasterSN.Location = new System.Drawing.Point(255, 48);
            this.txtMasterSN.Name = "txtMasterSN";
            this.txtMasterSN.Size = new System.Drawing.Size(100, 21);
            this.txtMasterSN.TabIndex = 4;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(255, 17);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(100, 21);
            this.txtBatchNo.TabIndex = 3;
            // 
            // txtPTRNo
            // 
            this.txtPTRNo.Location = new System.Drawing.Point(62, 80);
            this.txtPTRNo.Name = "txtPTRNo";
            this.txtPTRNo.Size = new System.Drawing.Size(100, 21);
            this.txtPTRNo.TabIndex = 2;
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(62, 50);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(100, 21);
            this.txtSN.TabIndex = 1;
            // 
            // txtPN
            // 
            this.txtPN.Location = new System.Drawing.Point(62, 17);
            this.txtPN.Name = "txtPN";
            this.txtPN.Size = new System.Drawing.Size(100, 21);
            this.txtPN.TabIndex = 0;
            this.txtPN.Visible = false;
            // 
            // btnPreviewFileName
            // 
            this.btnPreviewFileName.Location = new System.Drawing.Point(573, 79);
            this.btnPreviewFileName.Name = "btnPreviewFileName";
            this.btnPreviewFileName.Size = new System.Drawing.Size(63, 28);
            this.btnPreviewFileName.TabIndex = 11;
            this.btnPreviewFileName.Text = "Preview";
            this.btnPreviewFileName.UseVisualStyleBackColor = true;
            this.btnPreviewFileName.Click += new System.EventHandler(this.btnPreviewFileName_Click);
            // 
            // lblReportFilenameDisplay
            // 
            this.lblReportFilenameDisplay.BackColor = System.Drawing.SystemColors.Window;
            this.lblReportFilenameDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReportFilenameDisplay.Location = new System.Drawing.Point(337, 83);
            this.lblReportFilenameDisplay.Name = "lblReportFilenameDisplay";
            this.lblReportFilenameDisplay.Size = new System.Drawing.Size(239, 22);
            this.lblReportFilenameDisplay.TabIndex = 38;
            // 
            // lblReportFileName
            // 
            this.lblReportFileName.AutoSize = true;
            this.lblReportFileName.Location = new System.Drawing.Point(267, 76);
            this.lblReportFileName.Name = "lblReportFileName";
            this.lblReportFileName.Size = new System.Drawing.Size(62, 30);
            this.lblReportFileName.TabIndex = 37;
            this.lblReportFileName.Text = "Report\r\nFilename:";
            this.lblReportFileName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtGain
            // 
            this.txtGain.Location = new System.Drawing.Point(310, 43);
            this.txtGain.Name = "txtGain";
            this.txtGain.Size = new System.Drawing.Size(68, 21);
            this.txtGain.TabIndex = 5;
            // 
            // txtFrequency
            // 
            this.txtFrequency.Location = new System.Drawing.Point(310, 12);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(68, 21);
            this.txtFrequency.TabIndex = 3;
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(78, 75);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(154, 21);
            this.txtAngle.TabIndex = 2;
            // 
            // lblGain
            // 
            this.lblGain.AutoSize = true;
            this.lblGain.Location = new System.Drawing.Point(268, 46);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(36, 15);
            this.lblGain.TabIndex = 4;
            this.lblGain.Text = "Gain:";
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(237, 15);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(67, 15);
            this.lblFrequency.TabIndex = 3;
            this.lblFrequency.Text = "Frequency:";
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(31, 77);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(41, 15);
            this.lblAngle.TabIndex = 2;
            this.lblAngle.Text = "Angle:";
            // 
            // llbProbeDrive
            // 
            this.llbProbeDrive.AutoSize = true;
            this.llbProbeDrive.Location = new System.Drawing.Point(-2, 48);
            this.llbProbeDrive.Name = "llbProbeDrive";
            this.llbProbeDrive.Size = new System.Drawing.Size(74, 15);
            this.llbProbeDrive.TabIndex = 1;
            this.llbProbeDrive.Text = "Probe Drive:";
            // 
            // lblProbeType
            // 
            this.lblProbeType.AutoSize = true;
            this.lblProbeType.Location = new System.Drawing.Point(0, 15);
            this.lblProbeType.Name = "lblProbeType";
            this.lblProbeType.Size = new System.Drawing.Size(72, 15);
            this.lblProbeType.TabIndex = 0;
            this.lblProbeType.Text = "Probe Type:";
            // 
            // lblStopOnFail
            // 
            this.lblStopOnFail.BackColor = System.Drawing.Color.LimeGreen;
            this.lblStopOnFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStopOnFail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStopOnFail.Location = new System.Drawing.Point(705, 13);
            this.lblStopOnFail.Name = "lblStopOnFail";
            this.lblStopOnFail.Size = new System.Drawing.Size(195, 28);
            this.lblStopOnFail.TabIndex = 41;
            this.lblStopOnFail.Text = "CONTINUE ON FAIL";
            this.lblStopOnFail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStopOnFail.DoubleClick += new System.EventHandler(this.lblStopOnFail_DoubleClick);
            // 
            // btnGetUniWestVals
            // 
            this.btnGetUniWestVals.Location = new System.Drawing.Point(1347, 69);
            this.btnGetUniWestVals.Name = "btnGetUniWestVals";
            this.btnGetUniWestVals.Size = new System.Drawing.Size(131, 26);
            this.btnGetUniWestVals.TabIndex = 7;
            this.btnGetUniWestVals.Text = "Get UniWest Values";
            this.btnGetUniWestVals.UseVisualStyleBackColor = true;
            this.btnGetUniWestVals.Click += new System.EventHandler(this.btnGetUniVals_Click);
            // 
            // tbcOptionPanel
            // 
            this.tbcOptionPanel.Controls.Add(this.tabPage1);
            this.tbcOptionPanel.Controls.Add(this.tabPage2);
            this.tbcOptionPanel.Controls.Add(this.tabPage3);
            this.tbcOptionPanel.Location = new System.Drawing.Point(805, 90);
            this.tbcOptionPanel.Name = "tbcOptionPanel";
            this.tbcOptionPanel.SelectedIndex = 0;
            this.tbcOptionPanel.Size = new System.Drawing.Size(677, 143);
            this.tbcOptionPanel.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.cmbProbeDrive);
            this.tabPage1.Controls.Add(this.btnHPSet);
            this.tabPage1.Controls.Add(this.cmbProbeType);
            this.tabPage1.Controls.Add(this.btnLPSet);
            this.tabPage1.Controls.Add(this.btnGainSet);
            this.tabPage1.Controls.Add(this.btnFreqSet);
            this.tabPage1.Controls.Add(this.btnPreviewFileName);
            this.tabPage1.Controls.Add(this.lblReportFilenameDisplay);
            this.tabPage1.Controls.Add(this.lblProbeType);
            this.tabPage1.Controls.Add(this.lblReportFileName);
            this.tabPage1.Controls.Add(this.llbProbeDrive);
            this.tabPage1.Controls.Add(this.lblAngle);
            this.tabPage1.Controls.Add(this.lblFrequency);
            this.tabPage1.Controls.Add(this.txtGain);
            this.tabPage1.Controls.Add(this.lblLowPass);
            this.tabPage1.Controls.Add(this.txtFrequency);
            this.tabPage1.Controls.Add(this.lblGain);
            this.tabPage1.Controls.Add(this.tbxHighPass);
            this.tabPage1.Controls.Add(this.tbxLowPass);
            this.tabPage1.Controls.Add(this.lblHighPass);
            this.tabPage1.Controls.Add(this.txtAngle);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(669, 115);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "UniWest";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmbProbeDrive
            // 
            this.cmbProbeDrive.Location = new System.Drawing.Point(77, 43);
            this.cmbProbeDrive.Name = "cmbProbeDrive";
            this.cmbProbeDrive.Size = new System.Drawing.Size(154, 21);
            this.cmbProbeDrive.TabIndex = 1;
            // 
            // btnHPSet
            // 
            this.btnHPSet.Location = new System.Drawing.Point(615, 42);
            this.btnHPSet.Name = "btnHPSet";
            this.btnHPSet.Size = new System.Drawing.Size(49, 21);
            this.btnHPSet.TabIndex = 10;
            this.btnHPSet.Text = "Set";
            this.btnHPSet.UseVisualStyleBackColor = true;
            this.btnHPSet.Click += new System.EventHandler(this.btnHPSet_Click);
            // 
            // cmbProbeType
            // 
            this.cmbProbeType.Location = new System.Drawing.Point(77, 12);
            this.cmbProbeType.Name = "cmbProbeType";
            this.cmbProbeType.Size = new System.Drawing.Size(154, 21);
            this.cmbProbeType.TabIndex = 0;
            // 
            // btnLPSet
            // 
            this.btnLPSet.Location = new System.Drawing.Point(615, 14);
            this.btnLPSet.Name = "btnLPSet";
            this.btnLPSet.Size = new System.Drawing.Size(49, 21);
            this.btnLPSet.TabIndex = 8;
            this.btnLPSet.Text = "Set";
            this.btnLPSet.UseVisualStyleBackColor = true;
            this.btnLPSet.Click += new System.EventHandler(this.btnLPSet_Click);
            // 
            // btnGainSet
            // 
            this.btnGainSet.Location = new System.Drawing.Point(384, 43);
            this.btnGainSet.Name = "btnGainSet";
            this.btnGainSet.Size = new System.Drawing.Size(49, 21);
            this.btnGainSet.TabIndex = 6;
            this.btnGainSet.Text = "Set";
            this.btnGainSet.UseVisualStyleBackColor = true;
            this.btnGainSet.Click += new System.EventHandler(this.btnGainSet_Click);
            // 
            // btnFreqSet
            // 
            this.btnFreqSet.Location = new System.Drawing.Point(384, 12);
            this.btnFreqSet.Name = "btnFreqSet";
            this.btnFreqSet.Size = new System.Drawing.Size(49, 21);
            this.btnFreqSet.TabIndex = 4;
            this.btnFreqSet.Text = "Set";
            this.btnFreqSet.UseVisualStyleBackColor = true;
            this.btnFreqSet.Click += new System.EventHandler(this.btnFreqSet_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.btnPartInfo);
            this.tabPage2.Controls.Add(this.btnEditPartInfo);
            this.tabPage2.Controls.Add(this.txt4Tolerance);
            this.tabPage2.Controls.Add(this.lbl4Tolerance);
            this.tabPage2.Controls.Add(this.btnUpdateMasterCal);
            this.tabPage2.Controls.Add(this.lblMinUntilMaster);
            this.tabPage2.Controls.Add(this.lblPartsUntilMaster);
            this.tabPage2.Controls.Add(this.lblMuM);
            this.tabPage2.Controls.Add(this.lblPuM);
            this.tabPage2.Controls.Add(this.txtMinutesPerMaster);
            this.tabPage2.Controls.Add(this.txtPartsPerMaster);
            this.tabPage2.Controls.Add(this.lblMinutesPerMaster);
            this.tabPage2.Controls.Add(this.lblPartsPerMaster);
            this.tabPage2.Controls.Add(this.txtTolerance);
            this.tabPage2.Controls.Add(this.lblTolerance);
            this.tabPage2.Controls.Add(this.btnDoneEditing);
            this.tabPage2.Controls.Add(this.btnPasswordSubmit);
            this.tabPage2.Controls.Add(this.lblPassword);
            this.tabPage2.Controls.Add(this.txtOptionsPassword);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(669, 115);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Options";
            // 
            // btnPartInfo
            // 
            this.btnPartInfo.BackColor = System.Drawing.Color.White;
            this.btnPartInfo.Location = new System.Drawing.Point(565, 30);
            this.btnPartInfo.Name = "btnPartInfo";
            this.btnPartInfo.Size = new System.Drawing.Size(96, 27);
            this.btnPartInfo.TabIndex = 3;
            this.btnPartInfo.Text = "Part Info";
            this.btnPartInfo.UseVisualStyleBackColor = false;
            this.btnPartInfo.Click += new System.EventHandler(this.btnPartInfo_Click);
            // 
            // btnEditPartInfo
            // 
            this.btnEditPartInfo.BackColor = System.Drawing.Color.White;
            this.btnEditPartInfo.Enabled = false;
            this.btnEditPartInfo.Location = new System.Drawing.Point(565, 60);
            this.btnEditPartInfo.Name = "btnEditPartInfo";
            this.btnEditPartInfo.Size = new System.Drawing.Size(96, 50);
            this.btnEditPartInfo.TabIndex = 2;
            this.btnEditPartInfo.Text = "Edit Part Info";
            this.btnEditPartInfo.UseVisualStyleBackColor = false;
            this.btnEditPartInfo.Click += new System.EventHandler(this.btnEditPartInfo_Click);
            // 
            // txt4Tolerance
            // 
            this.txt4Tolerance.Enabled = false;
            this.txt4Tolerance.Location = new System.Drawing.Point(349, 36);
            this.txt4Tolerance.Name = "txt4Tolerance";
            this.txt4Tolerance.Size = new System.Drawing.Size(97, 21);
            this.txt4Tolerance.TabIndex = 9;
            // 
            // lbl4Tolerance
            // 
            this.lbl4Tolerance.Location = new System.Drawing.Point(261, 39);
            this.lbl4Tolerance.Name = "lbl4Tolerance";
            this.lbl4Tolerance.Size = new System.Drawing.Size(82, 14);
            this.lbl4Tolerance.TabIndex = 0;
            this.lbl4Tolerance.Text = "                (#2):";
            // 
            // btnUpdateMasterCal
            // 
            this.btnUpdateMasterCal.Location = new System.Drawing.Point(452, 60);
            this.btnUpdateMasterCal.Name = "btnUpdateMasterCal";
            this.btnUpdateMasterCal.Size = new System.Drawing.Size(96, 50);
            this.btnUpdateMasterCal.TabIndex = 5;
            this.btnUpdateMasterCal.Text = "Update Master\r\nCal. Status\r\n";
            this.btnUpdateMasterCal.UseVisualStyleBackColor = true;
            this.btnUpdateMasterCal.Click += new System.EventHandler(this.btnUpdateMasterCal_Click);
            // 
            // lblMinUntilMaster
            // 
            this.lblMinUntilMaster.BackColor = System.Drawing.SystemColors.Window;
            this.lblMinUntilMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMinUntilMaster.Location = new System.Drawing.Point(349, 89);
            this.lblMinUntilMaster.Name = "lblMinUntilMaster";
            this.lblMinUntilMaster.Size = new System.Drawing.Size(97, 22);
            this.lblMinUntilMaster.TabIndex = 11;
            // 
            // lblPartsUntilMaster
            // 
            this.lblPartsUntilMaster.BackColor = System.Drawing.SystemColors.Window;
            this.lblPartsUntilMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPartsUntilMaster.Location = new System.Drawing.Point(349, 60);
            this.lblPartsUntilMaster.Name = "lblPartsUntilMaster";
            this.lblPartsUntilMaster.Size = new System.Drawing.Size(97, 22);
            this.lblPartsUntilMaster.TabIndex = 10;
            // 
            // lblMuM
            // 
            this.lblMuM.AutoSize = true;
            this.lblMuM.Location = new System.Drawing.Point(222, 93);
            this.lblMuM.Name = "lblMuM";
            this.lblMuM.Size = new System.Drawing.Size(121, 15);
            this.lblMuM.TabIndex = 58;
            this.lblMuM.Text = "Minutes until Master:";
            // 
            // lblPuM
            // 
            this.lblPuM.AutoSize = true;
            this.lblPuM.Location = new System.Drawing.Point(238, 64);
            this.lblPuM.Name = "lblPuM";
            this.lblPuM.Size = new System.Drawing.Size(105, 15);
            this.lblPuM.TabIndex = 57;
            this.lblPuM.Text = "Parts until Master:";
            // 
            // txtMinutesPerMaster
            // 
            this.txtMinutesPerMaster.Enabled = false;
            this.txtMinutesPerMaster.Location = new System.Drawing.Point(145, 89);
            this.txtMinutesPerMaster.Name = "txtMinutesPerMaster";
            this.txtMinutesPerMaster.Size = new System.Drawing.Size(74, 21);
            this.txtMinutesPerMaster.TabIndex = 8;
            // 
            // txtPartsPerMaster
            // 
            this.txtPartsPerMaster.Enabled = false;
            this.txtPartsPerMaster.Location = new System.Drawing.Point(145, 58);
            this.txtPartsPerMaster.Name = "txtPartsPerMaster";
            this.txtPartsPerMaster.Size = new System.Drawing.Size(74, 21);
            this.txtPartsPerMaster.TabIndex = 7;
            // 
            // lblMinutesPerMaster
            // 
            this.lblMinutesPerMaster.AutoSize = true;
            this.lblMinutesPerMaster.Location = new System.Drawing.Point(22, 90);
            this.lblMinutesPerMaster.Name = "lblMinutesPerMaster";
            this.lblMinutesPerMaster.Size = new System.Drawing.Size(117, 15);
            this.lblMinutesPerMaster.TabIndex = 7;
            this.lblMinutesPerMaster.Text = "Minutes Per Master:";
            // 
            // lblPartsPerMaster
            // 
            this.lblPartsPerMaster.AutoSize = true;
            this.lblPartsPerMaster.Location = new System.Drawing.Point(38, 64);
            this.lblPartsPerMaster.Name = "lblPartsPerMaster";
            this.lblPartsPerMaster.Size = new System.Drawing.Size(101, 15);
            this.lblPartsPerMaster.TabIndex = 6;
            this.lblPartsPerMaster.Text = "Parts Per Master:";
            // 
            // txtTolerance
            // 
            this.txtTolerance.Enabled = false;
            this.txtTolerance.Location = new System.Drawing.Point(145, 32);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(74, 21);
            this.txtTolerance.TabIndex = 6;
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Location = new System.Drawing.Point(4, 38);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(135, 15);
            this.lblTolerance.TabIndex = 4;
            this.lblTolerance.Text = "Tolerance:               (#1):";
            // 
            // btnDoneEditing
            // 
            this.btnDoneEditing.Enabled = false;
            this.btnDoneEditing.Location = new System.Drawing.Point(565, 2);
            this.btnDoneEditing.Name = "btnDoneEditing";
            this.btnDoneEditing.Size = new System.Drawing.Size(96, 25);
            this.btnDoneEditing.TabIndex = 4;
            this.btnDoneEditing.Text = "Done";
            this.btnDoneEditing.UseVisualStyleBackColor = true;
            this.btnDoneEditing.Click += new System.EventHandler(this.btnDoneEditing_Click);
            // 
            // btnPasswordSubmit
            // 
            this.btnPasswordSubmit.Location = new System.Drawing.Point(245, 3);
            this.btnPasswordSubmit.Name = "btnPasswordSubmit";
            this.btnPasswordSubmit.Size = new System.Drawing.Size(55, 21);
            this.btnPasswordSubmit.TabIndex = 1;
            this.btnPasswordSubmit.Text = "Submit";
            this.btnPasswordSubmit.UseVisualStyleBackColor = true;
            this.btnPasswordSubmit.Click += new System.EventHandler(this.btnPasswordSubmit_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 6);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 15);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password:";
            // 
            // txtOptionsPassword
            // 
            this.txtOptionsPassword.Location = new System.Drawing.Point(73, 3);
            this.txtOptionsPassword.Name = "txtOptionsPassword";
            this.txtOptionsPassword.PasswordChar = '*';
            this.txtOptionsPassword.Size = new System.Drawing.Size(175, 21);
            this.txtOptionsPassword.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grpSaveVoltage);
            this.tabPage3.Controls.Add(this.grpPrimaryGraph);
            this.tabPage3.Controls.Add(this.chkTripleGraph);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.cbxHorizontal);
            this.tabPage3.Controls.Add(this.cbxVertical);
            this.tabPage3.Controls.Add(this.cbxDistance);
            this.tabPage3.Controls.Add(this.grpDisplayedGraphs);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(669, 115);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Graph Options";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // grpSaveVoltage
            // 
            this.grpSaveVoltage.Controls.Add(this.cbxSortVoltageByVal);
            this.grpSaveVoltage.Controls.Add(this.cbxVoltages);
            this.grpSaveVoltage.Controls.Add(this.cbxVoltageD);
            this.grpSaveVoltage.Controls.Add(this.cbxVoltageV);
            this.grpSaveVoltage.Controls.Add(this.cbxVoltageH);
            this.grpSaveVoltage.Location = new System.Drawing.Point(99, -1);
            this.grpSaveVoltage.Name = "grpSaveVoltage";
            this.grpSaveVoltage.Size = new System.Drawing.Size(103, 113);
            this.grpSaveVoltage.TabIndex = 64;
            this.grpSaveVoltage.TabStop = false;
            // 
            // cbxSortVoltageByVal
            // 
            this.cbxSortVoltageByVal.AutoSize = true;
            this.cbxSortVoltageByVal.Location = new System.Drawing.Point(5, 91);
            this.cbxSortVoltageByVal.Name = "cbxSortVoltageByVal";
            this.cbxSortVoltageByVal.Size = new System.Drawing.Size(98, 19);
            this.cbxSortVoltageByVal.TabIndex = 4;
            this.cbxSortVoltageByVal.Text = "Sort By Value";
            this.cbxSortVoltageByVal.UseVisualStyleBackColor = true;
            // 
            // cbxVoltages
            // 
            this.cbxVoltages.AutoSize = true;
            this.cbxVoltages.Location = new System.Drawing.Point(5, 4);
            this.cbxVoltages.Name = "cbxVoltages";
            this.cbxVoltages.Size = new System.Drawing.Size(98, 34);
            this.cbxVoltages.TabIndex = 0;
            this.cbxVoltages.Text = "Save Voltage\r\nList In Report";
            this.cbxVoltages.UseVisualStyleBackColor = true;
            this.cbxVoltages.CheckedChanged += new System.EventHandler(this.cbxVoltages_CheckedChanged);
            // 
            // cbxVoltageD
            // 
            this.cbxVoltageD.AutoSize = true;
            this.cbxVoltageD.Location = new System.Drawing.Point(5, 73);
            this.cbxVoltageD.Name = "cbxVoltageD";
            this.cbxVoltageD.Size = new System.Drawing.Size(89, 19);
            this.cbxVoltageD.TabIndex = 3;
            this.cbxVoltageD.Text = "Vector Sum";
            this.cbxVoltageD.UseVisualStyleBackColor = true;
            // 
            // cbxVoltageV
            // 
            this.cbxVoltageV.AutoSize = true;
            this.cbxVoltageV.Checked = true;
            this.cbxVoltageV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxVoltageV.Location = new System.Drawing.Point(6, 37);
            this.cbxVoltageV.Name = "cbxVoltageV";
            this.cbxVoltageV.Size = new System.Drawing.Size(66, 19);
            this.cbxVoltageV.TabIndex = 1;
            this.cbxVoltageV.Text = "Vertical";
            this.cbxVoltageV.UseVisualStyleBackColor = true;
            // 
            // cbxVoltageH
            // 
            this.cbxVoltageH.AutoSize = true;
            this.cbxVoltageH.Location = new System.Drawing.Point(5, 55);
            this.cbxVoltageH.Name = "cbxVoltageH";
            this.cbxVoltageH.Size = new System.Drawing.Size(82, 19);
            this.cbxVoltageH.TabIndex = 2;
            this.cbxVoltageH.Text = "Horizontal";
            this.cbxVoltageH.UseVisualStyleBackColor = true;
            // 
            // grpPrimaryGraph
            // 
            this.grpPrimaryGraph.Controls.Add(this.radPrimaryVecSumT);
            this.grpPrimaryGraph.Controls.Add(this.radPrimaryHorizT);
            this.grpPrimaryGraph.Controls.Add(this.radPrimaryVertT);
            this.grpPrimaryGraph.Location = new System.Drawing.Point(239, 25);
            this.grpPrimaryGraph.Name = "grpPrimaryGraph";
            this.grpPrimaryGraph.Size = new System.Drawing.Size(113, 87);
            this.grpPrimaryGraph.TabIndex = 63;
            this.grpPrimaryGraph.TabStop = false;
            this.grpPrimaryGraph.Text = "Primary Graph";
            // 
            // radPrimaryVecSumT
            // 
            this.radPrimaryVecSumT.AutoSize = true;
            this.radPrimaryVecSumT.Location = new System.Drawing.Point(6, 65);
            this.radPrimaryVecSumT.Name = "radPrimaryVecSumT";
            this.radPrimaryVecSumT.Size = new System.Drawing.Size(88, 19);
            this.radPrimaryVecSumT.TabIndex = 2;
            this.radPrimaryVecSumT.Text = "Vector Sum";
            this.radPrimaryVecSumT.UseVisualStyleBackColor = true;
            this.radPrimaryVecSumT.CheckedChanged += new System.EventHandler(this.ChangePrimaryGraph);
            // 
            // radPrimaryHorizT
            // 
            this.radPrimaryHorizT.AutoSize = true;
            this.radPrimaryHorizT.Location = new System.Drawing.Point(6, 41);
            this.radPrimaryHorizT.Name = "radPrimaryHorizT";
            this.radPrimaryHorizT.Size = new System.Drawing.Size(81, 19);
            this.radPrimaryHorizT.TabIndex = 1;
            this.radPrimaryHorizT.Text = "Horizontal";
            this.radPrimaryHorizT.UseVisualStyleBackColor = true;
            this.radPrimaryHorizT.CheckedChanged += new System.EventHandler(this.ChangePrimaryGraph);
            // 
            // radPrimaryVertT
            // 
            this.radPrimaryVertT.AutoSize = true;
            this.radPrimaryVertT.Checked = true;
            this.radPrimaryVertT.Location = new System.Drawing.Point(6, 20);
            this.radPrimaryVertT.Name = "radPrimaryVertT";
            this.radPrimaryVertT.Size = new System.Drawing.Size(65, 19);
            this.radPrimaryVertT.TabIndex = 0;
            this.radPrimaryVertT.TabStop = true;
            this.radPrimaryVertT.Text = "Vertical";
            this.radPrimaryVertT.UseVisualStyleBackColor = true;
            this.radPrimaryVertT.CheckedChanged += new System.EventHandler(this.ChangePrimaryGraph);
            // 
            // chkTripleGraph
            // 
            this.chkTripleGraph.AutoSize = true;
            this.chkTripleGraph.Checked = true;
            this.chkTripleGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTripleGraph.Location = new System.Drawing.Point(239, 7);
            this.chkTripleGraph.Name = "chkTripleGraph";
            this.chkTripleGraph.Size = new System.Drawing.Size(101, 19);
            this.chkTripleGraph.TabIndex = 3;
            this.chkTripleGraph.Text = "Three Graphs";
            this.chkTripleGraph.UseVisualStyleBackColor = true;
            this.chkTripleGraph.CheckedChanged += new System.EventHandler(this.chkTripleGraph_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Graphs to Save\r\nIn the Report:";
            // 
            // cbxHorizontal
            // 
            this.cbxHorizontal.AutoSize = true;
            this.cbxHorizontal.Location = new System.Drawing.Point(9, 66);
            this.cbxHorizontal.Name = "cbxHorizontal";
            this.cbxHorizontal.Size = new System.Drawing.Size(82, 19);
            this.cbxHorizontal.TabIndex = 1;
            this.cbxHorizontal.Text = "Horizontal";
            this.cbxHorizontal.UseVisualStyleBackColor = true;
            // 
            // cbxVertical
            // 
            this.cbxVertical.AutoSize = true;
            this.cbxVertical.Checked = true;
            this.cbxVertical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxVertical.Location = new System.Drawing.Point(9, 47);
            this.cbxVertical.Name = "cbxVertical";
            this.cbxVertical.Size = new System.Drawing.Size(66, 19);
            this.cbxVertical.TabIndex = 0;
            this.cbxVertical.Text = "Vertical";
            this.cbxVertical.UseVisualStyleBackColor = true;
            // 
            // cbxDistance
            // 
            this.cbxDistance.AutoSize = true;
            this.cbxDistance.Location = new System.Drawing.Point(9, 87);
            this.cbxDistance.Name = "cbxDistance";
            this.cbxDistance.Size = new System.Drawing.Size(89, 19);
            this.cbxDistance.TabIndex = 2;
            this.cbxDistance.Text = "Vector Sum";
            this.cbxDistance.UseVisualStyleBackColor = true;
            // 
            // grpDisplayedGraphs
            // 
            this.grpDisplayedGraphs.Controls.Add(this.radDiffOpt);
            this.grpDisplayedGraphs.Controls.Add(this.radVerticalOpt);
            this.grpDisplayedGraphs.Controls.Add(this.radHorizOpt);
            this.grpDisplayedGraphs.Location = new System.Drawing.Point(239, 25);
            this.grpDisplayedGraphs.Name = "grpDisplayedGraphs";
            this.grpDisplayedGraphs.Size = new System.Drawing.Size(113, 87);
            this.grpDisplayedGraphs.TabIndex = 65;
            this.grpDisplayedGraphs.TabStop = false;
            this.grpDisplayedGraphs.Text = "Displayed Graph";
            this.grpDisplayedGraphs.Visible = false;
            // 
            // radDiffOpt
            // 
            this.radDiffOpt.AutoSize = true;
            this.radDiffOpt.Location = new System.Drawing.Point(6, 65);
            this.radDiffOpt.Name = "radDiffOpt";
            this.radDiffOpt.Size = new System.Drawing.Size(88, 19);
            this.radDiffOpt.TabIndex = 7;
            this.radDiffOpt.Text = "Vector Sum";
            this.radDiffOpt.UseVisualStyleBackColor = true;
            this.radDiffOpt.Visible = false;
            this.radDiffOpt.CheckedChanged += new System.EventHandler(this.radDiffOpt_CheckedChanged);
            // 
            // radVerticalOpt
            // 
            this.radVerticalOpt.AutoSize = true;
            this.radVerticalOpt.Checked = true;
            this.radVerticalOpt.Location = new System.Drawing.Point(6, 20);
            this.radVerticalOpt.Name = "radVerticalOpt";
            this.radVerticalOpt.Size = new System.Drawing.Size(65, 19);
            this.radVerticalOpt.TabIndex = 5;
            this.radVerticalOpt.TabStop = true;
            this.radVerticalOpt.Text = "Vertical";
            this.radVerticalOpt.UseVisualStyleBackColor = true;
            this.radVerticalOpt.Visible = false;
            this.radVerticalOpt.CheckedChanged += new System.EventHandler(this.radVerticalOpt_CheckedChanged);
            // 
            // radHorizOpt
            // 
            this.radHorizOpt.AutoSize = true;
            this.radHorizOpt.Location = new System.Drawing.Point(6, 41);
            this.radHorizOpt.Name = "radHorizOpt";
            this.radHorizOpt.Size = new System.Drawing.Size(81, 19);
            this.radHorizOpt.TabIndex = 6;
            this.radHorizOpt.Text = "Horizontal";
            this.radHorizOpt.UseVisualStyleBackColor = true;
            this.radHorizOpt.Visible = false;
            this.radHorizOpt.CheckedChanged += new System.EventHandler(this.radHorizOpt_CheckedChanged);
            // 
            // btnAbt
            // 
            this.btnAbt.BackColor = System.Drawing.Color.White;
            this.btnAbt.Location = new System.Drawing.Point(2260, 2);
            this.btnAbt.Name = "btnAbt";
            this.btnAbt.Size = new System.Drawing.Size(96, 27);
            this.btnAbt.TabIndex = 64;
            this.btnAbt.Text = "About";
            this.btnAbt.UseVisualStyleBackColor = false;
            this.btnAbt.Click += new System.EventHandler(this.btnAbt_Click);
            // 
            // cbxAmpInch
            // 
            this.cbxAmpInch.AutoSize = true;
            this.cbxAmpInch.Location = new System.Drawing.Point(104, 61);
            this.cbxAmpInch.Name = "cbxAmpInch";
            this.cbxAmpInch.Size = new System.Drawing.Size(127, 34);
            this.cbxAmpInch.TabIndex = 1;
            this.cbxAmpInch.Text = "Change Y Axis\r\nto Crack Depth (in)";
            this.cbxAmpInch.UseVisualStyleBackColor = true;
            this.cbxAmpInch.CheckedChanged += new System.EventHandler(this.cbxAmpInch_CheckedChanged);
            // 
            // label_XCoordinateMin2
            // 
            this.label_XCoordinateMin2.AutoSize = true;
            this.label_XCoordinateMin2.BackColor = System.Drawing.Color.Transparent;
            this.label_XCoordinateMin2.Location = new System.Drawing.Point(50, 663);
            this.label_XCoordinateMin2.Name = "label_XCoordinateMin2";
            this.label_XCoordinateMin2.Size = new System.Drawing.Size(38, 15);
            this.label_XCoordinateMin2.TabIndex = 50;
            this.label_XCoordinateMin2.Text = "0 Sec";
            this.label_XCoordinateMin2.Visible = false;
            // 
            // label_XCoordinateMax2
            // 
            this.label_XCoordinateMax2.AutoSize = true;
            this.label_XCoordinateMax2.BackColor = System.Drawing.Color.Transparent;
            this.label_XCoordinateMax2.Location = new System.Drawing.Point(2311, 665);
            this.label_XCoordinateMax2.Name = "label_XCoordinateMax2";
            this.label_XCoordinateMax2.Size = new System.Drawing.Size(45, 15);
            this.label_XCoordinateMax2.TabIndex = 49;
            this.label_XCoordinateMax2.Text = "12 Sec";
            this.label_XCoordinateMax2.Visible = false;
            // 
            // label_YCoordinateMin2
            // 
            this.label_YCoordinateMin2.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMin2.Location = new System.Drawing.Point(-3, 643);
            this.label_YCoordinateMin2.Name = "label_YCoordinateMin2";
            this.label_YCoordinateMin2.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMin2.TabIndex = 47;
            this.label_YCoordinateMin2.Text = "0V";
            this.label_YCoordinateMin2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMin2.Visible = false;
            // 
            // label_YCoordinateMax2
            // 
            this.label_YCoordinateMax2.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMax2.Location = new System.Drawing.Point(-3, 366);
            this.label_YCoordinateMax2.Name = "label_YCoordinateMax2";
            this.label_YCoordinateMax2.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMax2.TabIndex = 46;
            this.label_YCoordinateMax2.Text = "5V";
            this.label_YCoordinateMax2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMax2.Visible = false;
            // 
            // label_YCoordinateMiddle2
            // 
            this.label_YCoordinateMiddle2.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMiddle2.Location = new System.Drawing.Point(-3, 505);
            this.label_YCoordinateMiddle2.Name = "label_YCoordinateMiddle2";
            this.label_YCoordinateMiddle2.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMiddle2.TabIndex = 48;
            this.label_YCoordinateMiddle2.Text = "0V";
            this.label_YCoordinateMiddle2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMiddle2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox1.Location = new System.Drawing.Point(56, 366);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2300, 312);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 45;
            this.pictureBox1.TabStop = false;
            // 
            // label_XCoordinateMin3
            // 
            this.label_XCoordinateMin3.AutoSize = true;
            this.label_XCoordinateMin3.BackColor = System.Drawing.Color.Transparent;
            this.label_XCoordinateMin3.Location = new System.Drawing.Point(50, 997);
            this.label_XCoordinateMin3.Name = "label_XCoordinateMin3";
            this.label_XCoordinateMin3.Size = new System.Drawing.Size(38, 15);
            this.label_XCoordinateMin3.TabIndex = 56;
            this.label_XCoordinateMin3.Text = "0 Sec";
            this.label_XCoordinateMin3.Visible = false;
            // 
            // label_XCoordinateMax3
            // 
            this.label_XCoordinateMax3.AutoSize = true;
            this.label_XCoordinateMax3.BackColor = System.Drawing.Color.Transparent;
            this.label_XCoordinateMax3.Location = new System.Drawing.Point(2311, 999);
            this.label_XCoordinateMax3.Name = "label_XCoordinateMax3";
            this.label_XCoordinateMax3.Size = new System.Drawing.Size(45, 15);
            this.label_XCoordinateMax3.TabIndex = 55;
            this.label_XCoordinateMax3.Text = "12 Sec";
            this.label_XCoordinateMax3.Visible = false;
            // 
            // label_YCoordinateMin3
            // 
            this.label_YCoordinateMin3.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMin3.Location = new System.Drawing.Point(-3, 977);
            this.label_YCoordinateMin3.Name = "label_YCoordinateMin3";
            this.label_YCoordinateMin3.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMin3.TabIndex = 53;
            this.label_YCoordinateMin3.Text = "0V";
            this.label_YCoordinateMin3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMin3.Visible = false;
            // 
            // label_YCoordinateMax3
            // 
            this.label_YCoordinateMax3.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMax3.Location = new System.Drawing.Point(-3, 700);
            this.label_YCoordinateMax3.Name = "label_YCoordinateMax3";
            this.label_YCoordinateMax3.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMax3.TabIndex = 52;
            this.label_YCoordinateMax3.Text = "5V";
            this.label_YCoordinateMax3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMax3.Visible = false;
            // 
            // label_YCoordinateMiddle3
            // 
            this.label_YCoordinateMiddle3.BackColor = System.Drawing.Color.Transparent;
            this.label_YCoordinateMiddle3.Location = new System.Drawing.Point(-3, 839);
            this.label_YCoordinateMiddle3.Name = "label_YCoordinateMiddle3";
            this.label_YCoordinateMiddle3.Size = new System.Drawing.Size(52, 15);
            this.label_YCoordinateMiddle3.TabIndex = 54;
            this.label_YCoordinateMiddle3.Text = "0V";
            this.label_YCoordinateMiddle3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_YCoordinateMiddle3.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox2.Location = new System.Drawing.Point(56, 700);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(2300, 312);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 51;
            this.pictureBox2.TabStop = false;
            // 
            // tmrStartFileChecking
            // 
            this.tmrStartFileChecking.Enabled = true;
            this.tmrStartFileChecking.Interval = 250;
            this.tmrStartFileChecking.Tick += new System.EventHandler(this.tmrStartFileChecking_Tick);
            // 
            // tmrEndFileChecking
            // 
            this.tmrEndFileChecking.Interval = 1000;
            this.tmrEndFileChecking.Tick += new System.EventHandler(this.tmrEndFileChecking_Tick);
            // 
            // btnAddPart
            // 
            this.btnAddPart.Location = new System.Drawing.Point(1494, 101);
            this.btnAddPart.Name = "btnAddPart";
            this.btnAddPart.Size = new System.Drawing.Size(83, 40);
            this.btnAddPart.TabIndex = 9;
            this.btnAddPart.Text = "Add New Part";
            this.btnAddPart.UseVisualStyleBackColor = true;
            this.btnAddPart.Click += new System.EventHandler(this.btnAddPart_Click);
            // 
            // grpMainControls
            // 
            this.grpMainControls.Controls.Add(this.cbxLoadUniwest);
            this.grpMainControls.Controls.Add(this.chkGenReport);
            this.grpMainControls.Controls.Add(this.chkLoadMSPlan);
            this.grpMainControls.Controls.Add(this.BtnReconnectUniWest);
            this.grpMainControls.Controls.Add(this.lblBiPolarOrUniPolar);
            this.grpMainControls.Controls.Add(this.BtnClearGraphs);
            this.grpMainControls.Controls.Add(this.BtnNullUniWest);
            this.grpMainControls.Controls.Add(this.btnAddPart);
            this.grpMainControls.Controls.Add(this.cbxAmpInch);
            this.grpMainControls.Controls.Add(this.lblStopOnFail);
            this.grpMainControls.Controls.Add(this.pnlPartInfo);
            this.grpMainControls.Controls.Add(this.pnlVoltageRange);
            this.grpMainControls.Controls.Add(this.label2);
            this.grpMainControls.Controls.Add(this.label1);
            this.grpMainControls.Controls.Add(this.btnGetUniWestVals);
            this.grpMainControls.Controls.Add(this.listView);
            this.grpMainControls.Controls.Add(this.button_stop);
            this.grpMainControls.Controls.Add(this.button_pause);
            this.grpMainControls.Controls.Add(this.button_start);
            this.grpMainControls.Controls.Add(this.trackBar_div);
            this.grpMainControls.Controls.Add(this.tbcOptionPanel);
            this.grpMainControls.Location = new System.Drawing.Point(35, 1014);
            this.grpMainControls.Name = "grpMainControls";
            this.grpMainControls.Size = new System.Drawing.Size(1599, 239);
            this.grpMainControls.TabIndex = 59;
            this.grpMainControls.TabStop = false;
            // 
            // cbxLoadUniwest
            // 
            this.cbxLoadUniwest.AutoSize = true;
            this.cbxLoadUniwest.Location = new System.Drawing.Point(17, 40);
            this.cbxLoadUniwest.Name = "cbxLoadUniwest";
            this.cbxLoadUniwest.Size = new System.Drawing.Size(77, 34);
            this.cbxLoadUniwest.TabIndex = 0;
            this.cbxLoadUniwest.Text = "Uniwest \r\nFile Load";
            this.cbxLoadUniwest.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cbxLoadUniwest.UseVisualStyleBackColor = true;
            // 
            // chkLoadMSPlan
            // 
            this.chkLoadMSPlan.AutoSize = true;
            this.chkLoadMSPlan.Checked = true;
            this.chkLoadMSPlan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoadMSPlan.Location = new System.Drawing.Point(230, 54);
            this.chkLoadMSPlan.Name = "chkLoadMSPlan";
            this.chkLoadMSPlan.Size = new System.Drawing.Size(123, 49);
            this.chkLoadMSPlan.TabIndex = 2;
            this.chkLoadMSPlan.Text = "Load MasterScan\r\nScan Plan on\r\nPart Selection";
            this.chkLoadMSPlan.UseVisualStyleBackColor = true;
            // 
            // BtnReconnectUniWest
            // 
            this.BtnReconnectUniWest.Location = new System.Drawing.Point(1494, 58);
            this.BtnReconnectUniWest.Name = "BtnReconnectUniWest";
            this.BtnReconnectUniWest.Size = new System.Drawing.Size(83, 38);
            this.BtnReconnectUniWest.TabIndex = 8;
            this.BtnReconnectUniWest.Text = "Reconnect\r\nUniWest";
            this.BtnReconnectUniWest.UseVisualStyleBackColor = true;
            this.BtnReconnectUniWest.Click += new System.EventHandler(this.BtnReconnectUniWest_Click);
            // 
            // lblBiPolarOrUniPolar
            // 
            this.lblBiPolarOrUniPolar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.lblBiPolarOrUniPolar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBiPolarOrUniPolar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBiPolarOrUniPolar.Location = new System.Drawing.Point(1398, 21);
            this.lblBiPolarOrUniPolar.Name = "lblBiPolarOrUniPolar";
            this.lblBiPolarOrUniPolar.Size = new System.Drawing.Size(195, 28);
            this.lblBiPolarOrUniPolar.TabIndex = 61;
            this.lblBiPolarOrUniPolar.Text = "Data: Bipolar";
            this.lblBiPolarOrUniPolar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBiPolarOrUniPolar.DoubleClick += new System.EventHandler(this.lblBiPolarOrUniPolar_DoubleClick);
            // 
            // BtnClearGraphs
            // 
            this.BtnClearGraphs.Location = new System.Drawing.Point(1494, 145);
            this.BtnClearGraphs.Name = "BtnClearGraphs";
            this.BtnClearGraphs.Size = new System.Drawing.Size(83, 40);
            this.BtnClearGraphs.TabIndex = 10;
            this.BtnClearGraphs.Text = "Clear Graphs";
            this.BtnClearGraphs.UseVisualStyleBackColor = true;
            this.BtnClearGraphs.Click += new System.EventHandler(this.BtnClearGraphs_Click);
            // 
            // BtnNullUniWest
            // 
            this.BtnNullUniWest.Location = new System.Drawing.Point(1494, 191);
            this.BtnNullUniWest.Name = "BtnNullUniWest";
            this.BtnNullUniWest.Size = new System.Drawing.Size(83, 38);
            this.BtnNullUniWest.TabIndex = 11;
            this.BtnNullUniWest.Text = "Null\r\nUniWest";
            this.BtnNullUniWest.UseVisualStyleBackColor = true;
            this.BtnNullUniWest.Click += new System.EventHandler(this.BtnNullUniWest_Click);
            // 
            // lblTopGraph
            // 
            this.lblTopGraph.AutoSize = true;
            this.lblTopGraph.Location = new System.Drawing.Point(55, 14);
            this.lblTopGraph.Name = "lblTopGraph";
            this.lblTopGraph.Size = new System.Drawing.Size(47, 15);
            this.lblTopGraph.TabIndex = 60;
            this.lblTopGraph.Text = "Vertical";
            // 
            // lblMiddleGraph
            // 
            this.lblMiddleGraph.AutoSize = true;
            this.lblMiddleGraph.Location = new System.Drawing.Point(55, 347);
            this.lblMiddleGraph.Name = "lblMiddleGraph";
            this.lblMiddleGraph.Size = new System.Drawing.Size(63, 15);
            this.lblMiddleGraph.TabIndex = 61;
            this.lblMiddleGraph.Text = "Horizontal";
            // 
            // lblBottomGraph
            // 
            this.lblBottomGraph.AutoSize = true;
            this.lblBottomGraph.Location = new System.Drawing.Point(58, 682);
            this.lblBottomGraph.Name = "lblBottomGraph";
            this.lblBottomGraph.Size = new System.Drawing.Size(70, 15);
            this.lblBottomGraph.TabIndex = 62;
            this.lblBottomGraph.Text = "Vector Sum";
            // 
            // grpMasterCalibResults
            // 
            this.grpMasterCalibResults.BackColor = System.Drawing.SystemColors.Control;
            this.grpMasterCalibResults.Controls.Add(this.btnCalibrationReject);
            this.grpMasterCalibResults.Controls.Add(this.btnCalibrationAccept);
            this.grpMasterCalibResults.Controls.Add(this.lblCalibrationResult);
            this.grpMasterCalibResults.Location = new System.Drawing.Point(1770, 1113);
            this.grpMasterCalibResults.Name = "grpMasterCalibResults";
            this.grpMasterCalibResults.Size = new System.Drawing.Size(500, 134);
            this.grpMasterCalibResults.TabIndex = 63;
            this.grpMasterCalibResults.TabStop = false;
            this.grpMasterCalibResults.Text = "Calibration Results";
            this.grpMasterCalibResults.Visible = false;
            // 
            // btnCalibrationReject
            // 
            this.btnCalibrationReject.BackColor = System.Drawing.Color.Red;
            this.btnCalibrationReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibrationReject.Location = new System.Drawing.Point(256, 72);
            this.btnCalibrationReject.Name = "btnCalibrationReject";
            this.btnCalibrationReject.Size = new System.Drawing.Size(225, 50);
            this.btnCalibrationReject.TabIndex = 5;
            this.btnCalibrationReject.Text = "REJECT";
            this.btnCalibrationReject.UseVisualStyleBackColor = false;
            this.btnCalibrationReject.Click += new System.EventHandler(this.btnCalibrationReject_Click);
            // 
            // btnCalibrationAccept
            // 
            this.btnCalibrationAccept.BackColor = System.Drawing.Color.LimeGreen;
            this.btnCalibrationAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibrationAccept.Location = new System.Drawing.Point(20, 72);
            this.btnCalibrationAccept.Name = "btnCalibrationAccept";
            this.btnCalibrationAccept.Size = new System.Drawing.Size(225, 50);
            this.btnCalibrationAccept.TabIndex = 4;
            this.btnCalibrationAccept.Text = "ACCEPT";
            this.btnCalibrationAccept.UseVisualStyleBackColor = false;
            this.btnCalibrationAccept.Click += new System.EventHandler(this.btnCalibrationAccept_Click);
            // 
            // lblCalibrationResult
            // 
            this.lblCalibrationResult.AutoSize = true;
            this.lblCalibrationResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalibrationResult.Location = new System.Drawing.Point(38, 20);
            this.lblCalibrationResult.Name = "lblCalibrationResult";
            this.lblCalibrationResult.Size = new System.Drawing.Size(428, 36);
            this.lblCalibrationResult.TabIndex = 3;
            this.lblCalibrationResult.Text = "Please Select The Result for the Master Calibration.\r\nA temperary report to view " +
    "the calibration results has been made";
            this.lblCalibrationResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // StreamingAIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(2380, 1261);
            this.Controls.Add(this.btnAbt);
            this.Controls.Add(this.grpMasterCalibResults);
            this.Controls.Add(this.lblBottomGraph);
            this.Controls.Add(this.lblMiddleGraph);
            this.Controls.Add(this.lblTopGraph);
            this.Controls.Add(this.label_XCoordinateMin3);
            this.Controls.Add(this.label_XCoordinateMax3);
            this.Controls.Add(this.label_YCoordinateMin3);
            this.Controls.Add(this.label_YCoordinateMax3);
            this.Controls.Add(this.label_YCoordinateMiddle3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label_XCoordinateMin2);
            this.Controls.Add(this.label_XCoordinateMax2);
            this.Controls.Add(this.label_YCoordinateMin2);
            this.Controls.Add(this.label_YCoordinateMax2);
            this.Controls.Add(this.label_YCoordinateMiddle2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_XCoordinateMin);
            this.Controls.Add(this.label_XCoordinateMax);
            this.Controls.Add(this.label_YCoordinateMin);
            this.Controls.Add(this.label_YCoordinateMax);
            this.Controls.Add(this.label_YCoordinateMiddle);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.grpMainControls);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StreamingAIForm";
            this.Text = "Chart Recorder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StreamingAIForm_FormClosing);
            this.Load += new System.EventHandler(this.StreamingBufferedAiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_div)).EndInit();
            this.pnlVoltageRange.ResumeLayout(false);
            this.pnlVoltageRange.PerformLayout();
            this.pnlPartInfo.ResumeLayout(false);
            this.pnlPartInfo.PerformLayout();
            this.tbcOptionPanel.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.grpSaveVoltage.ResumeLayout(false);
            this.grpSaveVoltage.PerformLayout();
            this.grpPrimaryGraph.ResumeLayout(false);
            this.grpPrimaryGraph.PerformLayout();
            this.grpDisplayedGraphs.ResumeLayout(false);
            this.grpDisplayedGraphs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.grpMainControls.ResumeLayout(false);
            this.grpMainControls.PerformLayout();
            this.grpMasterCalibResults.ResumeLayout(false);
            this.grpMasterCalibResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.PictureBox pictureBox;
      private System.Windows.Forms.TrackBar trackBar_div;
      private System.Windows.Forms.Button button_stop;
      private System.Windows.Forms.Button button_pause;
      private System.Windows.Forms.Button button_start;
      private System.Windows.Forms.ListView listView;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label_YCoordinateMin;
      private System.Windows.Forms.Label label_YCoordinateMax;
      private System.Windows.Forms.Label label_YCoordinateMiddle;
      private System.Windows.Forms.Label label_XCoordinateMin;
		private System.Windows.Forms.Label label_XCoordinateMax;
      private System.Windows.Forms.Label label2;
		private Automation.BDaq.WaveformAiCtrl waveformAiCtrl1;
        private System.Windows.Forms.Label lblLowPass;
        private System.Windows.Forms.TextBox tbxLowPass;
        private System.Windows.Forms.TextBox tbxHighPass;
        private System.Windows.Forms.Label lblHighPass;
        private System.Windows.Forms.Panel pnlVoltageRange;
        private System.Windows.Forms.RadioButton radV1;
        private System.Windows.Forms.RadioButton radV2;
        private System.Windows.Forms.RadioButton radV5;
        private System.Windows.Forms.RadioButton radV10;
        private System.Windows.Forms.Label lblVoltageRange;
        private System.Windows.Forms.CheckBox chkGenReport;
        private System.Windows.Forms.Panel pnlPartInfo;
        private System.Windows.Forms.Label lblETLvl1;
        private System.Windows.Forms.Label lblETLvl2;
        private System.Windows.Forms.Label lblETTSNo;
        private System.Windows.Forms.Label lblMasterSN;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.Label lblPtrNo;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.Label lblPN;
        private System.Windows.Forms.TextBox txtETLev1;
        private System.Windows.Forms.TextBox txtETLev2;
        private System.Windows.Forms.TextBox txtETTSNo;
        private System.Windows.Forms.TextBox txtMasterSN;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.TextBox txtPTRNo;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.TextBox txtPN;
        private System.Windows.Forms.Label lblGain;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label llbProbeDrive;
        private System.Windows.Forms.Label lblProbeType;
        private System.Windows.Forms.TextBox txtGain;
        private System.Windows.Forms.TextBox txtFrequency;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.Label lblStopOnFail;
        private System.Windows.Forms.Button btnPreviewFileName;
        private System.Windows.Forms.Label lblReportFilenameDisplay;
        private System.Windows.Forms.Label lblReportFileName;
        private System.Windows.Forms.Button btnGetUniWestVals;
        private System.Windows.Forms.TabControl tbcOptionPanel;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnPasswordSubmit;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtOptionsPassword;
        private System.Windows.Forms.Button btnDoneEditing;
        private System.Windows.Forms.TextBox txtMinutesPerMaster;
        private System.Windows.Forms.TextBox txtPartsPerMaster;
        private System.Windows.Forms.Label lblMinutesPerMaster;
        private System.Windows.Forms.Label lblPartsPerMaster;
        private System.Windows.Forms.TextBox txtTolerance;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.CheckBox cbxAmpInch;
        private System.Windows.Forms.Label label_XCoordinateMin2;
        private System.Windows.Forms.Label label_XCoordinateMax2;
        private System.Windows.Forms.Label label_YCoordinateMin2;
        private System.Windows.Forms.Label label_YCoordinateMax2;
        private System.Windows.Forms.Label label_YCoordinateMiddle2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_XCoordinateMin3;
        private System.Windows.Forms.Label label_XCoordinateMax3;
        private System.Windows.Forms.Label label_YCoordinateMin3;
        private System.Windows.Forms.Label label_YCoordinateMax3;
        private System.Windows.Forms.Label label_YCoordinateMiddle3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnUpdateMasterCal;
        private System.Windows.Forms.Label lblMinUntilMaster;
        private System.Windows.Forms.Label lblPartsUntilMaster;
        private System.Windows.Forms.Label lblMuM;
        private System.Windows.Forms.Label lblPuM;
        private System.Windows.Forms.Button btnHPSet;
        private System.Windows.Forms.Button btnLPSet;
        private System.Windows.Forms.Button btnGainSet;
        private System.Windows.Forms.Button btnFreqSet;
        private System.Windows.Forms.CheckBox chkMasterPart;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.CheckBox cbxVertical;
        private System.Windows.Forms.CheckBox cbxDistance;
        private System.Windows.Forms.CheckBox cbxHorizontal;
        private System.Windows.Forms.Timer tmrStartFileChecking;
        private System.Windows.Forms.Timer tmrEndFileChecking;
        private System.Windows.Forms.ComboBox cmbxPartNo;
        private System.Windows.Forms.Button btnAddPart;
        private System.Windows.Forms.CheckBox chkTripleGraph;
        private System.Windows.Forms.GroupBox grpMainControls;
        private System.Windows.Forms.Button BtnNullUniWest;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton radDiffOpt;
        private System.Windows.Forms.RadioButton radHorizOpt;
        private System.Windows.Forms.RadioButton radVerticalOpt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbxVoltages;
        private System.Windows.Forms.CheckBox cbxVoltageH;
        private System.Windows.Forms.CheckBox cbxVoltageV;
        private System.Windows.Forms.CheckBox cbxVoltageD;
        private System.Windows.Forms.Button BtnClearGraphs;
        private System.Windows.Forms.Label lblBiPolarOrUniPolar;
        private System.Windows.Forms.Button BtnReconnectUniWest;
        private System.Windows.Forms.CheckBox chkLoadMSPlan;
        private System.Windows.Forms.GroupBox grpSaveVoltage;
        private System.Windows.Forms.GroupBox grpPrimaryGraph;
        private System.Windows.Forms.RadioButton radPrimaryVecSumT;
        private System.Windows.Forms.RadioButton radPrimaryHorizT;
        private System.Windows.Forms.RadioButton radPrimaryVertT;
        private System.Windows.Forms.GroupBox grpDisplayedGraphs;
        private System.Windows.Forms.Label lblTopGraph;
        private System.Windows.Forms.Label lblMiddleGraph;
        private System.Windows.Forms.Label lblBottomGraph;
        private System.Windows.Forms.TextBox txt4Tolerance;
        private System.Windows.Forms.Label lbl4Tolerance;
        private System.Windows.Forms.GroupBox grpMasterCalibResults;
        private System.Windows.Forms.Button btnCalibrationReject;
        private System.Windows.Forms.Button btnCalibrationAccept;
        private System.Windows.Forms.Label lblCalibrationResult;
        private System.Windows.Forms.Button btnEditPartInfo;
        private System.Windows.Forms.CheckBox cbxSortVoltageByVal;
        private System.Windows.Forms.Button btnAbt;
        private System.Windows.Forms.Button btnLoadReportVals;
        private System.Windows.Forms.Button btnSaveReportVals;
        private System.Windows.Forms.TextBox cmbProbeDrive;
        private System.Windows.Forms.TextBox cmbProbeType;
        private System.Windows.Forms.Button btnPartInfo;
        private System.Windows.Forms.CheckBox cbxLoadUniwest;
    }
}

