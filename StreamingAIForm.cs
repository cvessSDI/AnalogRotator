using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Automation.BDaq;
using System.IO;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using static System.Net.WebRequestMethods;
using System.Security.Policy;
using System.Diagnostics.Eventing.Reader;
using File = System.IO.File;

namespace AnalogRotator
{
    public partial class StreamingAIForm : Form
    {
        #region fields  
        SimpleGraph m_simpleGraph1;
        SimpleGraph m_simpleGraph2;
        SimpleGraph m_simpleGraph3;
        TimeUnit m_timeUnit;
        double[] m_dataScaled;
        double m_divideValue;
        string deviceDescription = "PCI-1747U,BID#0"; //Calling to the PCL-818 COMMENT THIS OUT FOR DEMO
        bool m_isFirstOverRun = true;
        double m_xInc;

        //Newly Added Variables
        double voltageType = 10;
        bool stopOnFail = false;
        double frequency;
        double magPerm;
        double conductivity;
        int lastVal;
        bool isPaused;
        NetworkStream stream;
        StreamWriter sw;
        StreamReader sr;
        TcpClient client;
        bool DEMOMODE = true; // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        double finalTmpTime;
        int minorRevCounter = 0;
        int imagesToDelete = 0;
        double mainTimeInterval;
        PausableTimer timer;
        string startFile = "\\FileToStartTheChartRecorderProgramPleaseIgnoreAndDontTouchThankYou.txt";
        string endFile = "\\FileToEndTheChartRecorderProgramPleaseIgnoreAndDontTouchThankYou.txt";
        public NewPart newPartForm;
        //CalibrationResult newCalibrationResultForm;
        public List<Parts> allParts;
        bool uniPolarData = false;
        public int dataPointsPerPixel = 30;  //If Changed here, also change the variable with the same name at the top of simpleGraphs
        public bool secondMasterPass = false;
        public int masterUserChoice = -1; //-1 for unanswered, 0 for reject, 1 for accept
        bool hasCycled = false;
        bool exitEarly = false;
        int finalFileToDelete = -1;
        string lastPendingFileNameThatWasSaved = "";
        string lastSelectedPart = "";
        bool secondPartNoPass = false;
        string lastSavedReportLocation = null;
        ViewPart partForm;
        public class Parts
        {
            public string partNo;
            public string probeNo;
            public Double notch1;
            public Double notch1Volt;
            public Double notch2;
            public Double notch2Volt;
            public string scanPlanName;
            public int scanPlanType; //0 for TT Scan, 1 for Profile Scan
            public string uniwestSetupName;
            public string techniqueFile;
            //public Image referenceStandardImage;
            ///// IMAGES SHOULD JUST BE IN THE FOLDER WHERE THE BINARY FILE IS AND UNDER THE NAME OF THE PART
            //pdf ???
        }
        delegate void pauseButton();
        #endregion

        public StreamingAIForm()
        {
            InitializeComponent();
            try
            {
                if (!DEMOMODE)
                    waveformAiCtrl1.SelectedDevice = new DeviceInformation(deviceDescription);
            }
            catch
            {
                MessageBox.Show("Failed to connect to the advantech board. Please reload and try again.", "Failed To Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }
            
        }

        /// <summary>
        /// Loads the form and calls the setup functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StreamingBufferedAiForm_Load(object sender, EventArgs e)
        {
            //The default device of project is demo device, users can choose other devices according to their needs. 
            if (!waveformAiCtrl1.Initialized)
            {
                MessageBox.Show("No device be selected or device open failed!", "StreamingAI");
                this.Close();
                return;
            }
            DeleteOldStartStopFiles();
            getPartdata();
            //initialize a graph with a picture box control to draw Ai data. 
            Size newSize = new Size(pictureBox.Width-40,pictureBox.Height-17);
            m_simpleGraph1 = new SimpleGraph(newSize, pictureBox,1,this);
            m_simpleGraph2 = new SimpleGraph(newSize, pictureBox1,2,this);
            m_simpleGraph3 = new SimpleGraph(newSize, pictureBox2,3, this);

            int chanCount = waveformAiCtrl1.Conversion.ChannelCount;
            int sectionLength = waveformAiCtrl1.Record.SectionLength;
            m_dataScaled = new double[chanCount * sectionLength];

            //this.Text = "Streaming AI(" + waveformAiCtrl1.SelectedDevice.Description + ")";
            tmrStartFileChecking.Enabled = true;
            tmrStartFileChecking.Start();
            button_start.Enabled = true;
            button_stop.Enabled = false;
            button_pause.Enabled = false;
            if(!DEMOMODE)
                PortSetup();
            LoadBinaryPrivateOptions();
            if (!DEMOMODE)
                GetRemainingCalibrationReqs();
            ConfigureGraph();
            InitListView();
            voltagesCheckChanged();
            threeGraphSwitch();

            try
            {
                //cmbxPartNo.SelectedIndex = 0;
            }
            catch
            {
                DialogResult res = MessageBox.Show("Something has gone wrong with the partData file and the parts have not been loaded. The file may be empty. \n\nPlease select OK if you would like the partData file to be re-created with default settings. \n\nThere may be a backup file available for you to copy old data from, please check the PartSettings folder.", "partData Load Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                if (res == DialogResult.OK)
                {
                    string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "partData.data";

                    try
                    {
                        if (System.IO.File.Exists(fileName))
                        {
                            System.IO.File.Delete(fileName);
                            getPartdata();
                        }
                    }
                    catch
                    {
                        MessageBox.Show($"The system has failed to delete the file. Please manually delete {fileName}.", "partData Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            
        }

        /// <summary>
        /// Formats the graphs, so the axis are correctly scales and are displaying properly
        /// </summary>
        private void ConfigureGraph()
        {
            m_timeUnit = TimeUnit.Millisecond;
            double conversionRate = waveformAiCtrl1.Conversion.ClockRate;
            double timeInterval = 100.0 * (pictureBox.Size.Width-40) / conversionRate;
            while (conversionRate >= 10 * 1000)
            {
                timeInterval *= 1000;
                conversionRate /= 1000;
                --m_timeUnit;
            }

            m_simpleGraph1.sectionCount = waveformAiCtrl1.Record.SectionLength;
            m_simpleGraph2.sectionCount = waveformAiCtrl1.Record.SectionLength;
            m_simpleGraph3.sectionCount = waveformAiCtrl1.Record.SectionLength;

            m_divideValue = timeInterval;
            int divValue = (int)Math.Floor(timeInterval);
            trackBar_div.Maximum = dataPointsPerPixel * divValue; // 1 pixel to 4 data points
            trackBar_div.Minimum = 100;//(int)Math.Ceiling(1.0 * divValue / 10);
            if(lastVal == 0)
            {
                trackBar_div.Value = trackBar_div.Maximum;//divValue;
            }
            else
            {
                trackBar_div.Value = lastVal;
            }
            

            m_simpleGraph1.XCordTimeDiv = trackBar_div.Value;
            m_simpleGraph1.XCordTimeOffset = 0;

            m_simpleGraph2.XCordTimeDiv = trackBar_div.Value;
            m_simpleGraph2.XCordTimeOffset = 0;

            m_simpleGraph3.XCordTimeDiv = trackBar_div.Value;
            m_simpleGraph3.XCordTimeOffset = 0;

            SetXCordRangeLabels();

            ValueUnit unit = (ValueUnit)(-1); // Don't show unit in the label.
            string[] Y_CordLables = new string[3];
            Helpers.GetYCordRangeLabels(Y_CordLables, voltageType, (voltageType * -1), unit);
            label_YCoordinateMax.Text = Y_CordLables[0];
            label_YCoordinateMin.Text = Y_CordLables[1];
            label_YCoordinateMiddle.Text = Y_CordLables[2];

            label_YCoordinateMax2.Text = Y_CordLables[0];
            label_YCoordinateMin2.Text = Y_CordLables[1];
            label_YCoordinateMiddle2.Text = Y_CordLables[2];

            m_simpleGraph1.YCordRangeMax = voltageType;
            m_simpleGraph1.YCordRangeMin = (voltageType * -1);
            m_simpleGraph1.Clear();

            m_simpleGraph2.YCordRangeMax = voltageType;
            m_simpleGraph2.YCordRangeMin = (voltageType * -1);
            m_simpleGraph2.Clear();

            (Double time, TimeUnit unitTime) = Helpers.GetTimeSettings(((int)(m_simpleGraph1.XCordTimeDiv)) * 10, m_timeUnit);

            if(unitTime == TimeUnit.Second)
            {
                if (timer == null)
                {
                    timer = new PausableTimer(time * 1000);
                    timer.Interval = time * 1000;
                    mainTimeInterval = (time);
                    timer.Elapsed += Timer_Elapsed;
                }
                else
                {
                    timer.Interval = time * 1000;
                    mainTimeInterval = (time);
                }

            }
            else if(unitTime == TimeUnit.Millisecond)
            {
                if(timer == null)
                {
                    timer = new PausableTimer(time);
                    timer.Interval = time;
                    mainTimeInterval = time / 1000;
                    timer.Elapsed += Timer_Elapsed;
                }
                else
                {
                    timer.Interval = time;
                    mainTimeInterval = time / 1000;
                }


            }
            
           


            int range = (int)Math.Ceiling(Math.Sqrt(Math.Pow(voltageType, 2) + Math.Pow(voltageType, 2)));

            label_YCoordinateMax3.Text = Y_CordLables[0];//range.ToString();
            label_YCoordinateMin3.Text = Y_CordLables[1];//(-range).ToString();
            label_YCoordinateMiddle3.Text = Y_CordLables[2];//0.ToString(); 

            m_simpleGraph3.YCordRangeMax = voltageType; ;// range;
            m_simpleGraph3.YCordRangeMin = (voltageType * -1); //(range * -1);
            m_simpleGraph3.Clear();


            m_simpleGraph1.Voltage = (float)voltageType;
            m_simpleGraph2.Voltage = (float)voltageType;
            m_simpleGraph3.Voltage = (float)voltageType;


            try
            {
                m_simpleGraph1.tolerance2 = Double.Parse(txtTolerance.Text);
                m_simpleGraph2.tolerance2 = Double.Parse(txtTolerance.Text);
                m_simpleGraph3.tolerance2 = Double.Parse(txtTolerance.Text);

                m_simpleGraph1.tolerance4 = Double.Parse(txt4Tolerance.Text);
                m_simpleGraph2.tolerance4 = Double.Parse(txt4Tolerance.Text);
                m_simpleGraph3.tolerance4 = Double.Parse(txt4Tolerance.Text);

                m_simpleGraph1.lowPass = Double.Parse(tbxLowPass.Text);
                m_simpleGraph1.highPass = Double.Parse(tbxHighPass.Text);

                m_simpleGraph2.lowPass = Double.Parse(tbxLowPass.Text);
                m_simpleGraph2.highPass = Double.Parse(tbxHighPass.Text);

                m_simpleGraph3.lowPass = Double.Parse(tbxLowPass.Text);
                m_simpleGraph3.highPass = Double.Parse(tbxHighPass.Text);


            }
            catch
            {
                Debug.Print("High or Low Pass filter values not valid"); // make this into a message box or smth ALSO maybe move this higher up lol
            }


        }

        /// <summary>
        /// Takes screenshots of each graph on a fixed interval for use later in the report creation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (chkGenReport.Checked)
            {
                saveSelectedImages();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitListView()
        {
            // listview control ,one grid indicates a channel which specials with color.
            listView.Clear();
            listView.FullRowSelect = false;
            listView.Width = 352;
            listView.Height = 43;
            listView.View = View.Details;// Set the view to show details.
            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.GridLines = true;
            // there are 8 columns for every item.
            for (int i = 0; i < 8; i++)
            {
                listView.Columns.Add("", 43);
            }

            // modify the grid's height with image Indirectly.
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 13);//width and height.
            listView.SmallImageList = imgList; //use imgList to modify the height of listView grids.

            // create two ListViewItem objects,so there are 16 grids for listView.
            ListViewItem firstItem;
            ListViewItem secondItem;

            firstItem = new ListViewItem();
            firstItem.UseItemStyleForSubItems = false;
            firstItem.SubItems.Clear();

            secondItem = new ListViewItem();
            secondItem.UseItemStyleForSubItems = false;
            secondItem.SubItems.Clear();

            // format every grid for output.
            firstItem.SubItems[0].Text = "";
            firstItem.SubItems[0].BackColor = m_simpleGraph1.Pens[0].Color;
            firstItem.SubItems[0].BackColor = m_simpleGraph2.Pens[0].Color;
            firstItem.SubItems[0].BackColor = m_simpleGraph3.Pens[0].Color;
            for (int i = 1; i < 8; i++)
            {

                if (i < waveformAiCtrl1.Conversion.ChannelCount)
                {
                    firstItem.SubItems.Add((""), Color.Black, Color.Honeydew, new System.Drawing.Font("SimSun", 10));
                    firstItem.SubItems[i].BackColor = m_simpleGraph1.Pens[i].Color;
                    firstItem.SubItems[i].BackColor = m_simpleGraph2.Pens[i].Color;
                    firstItem.SubItems[i].BackColor = m_simpleGraph3.Pens[i].Color;
                }
                else
                {

                    firstItem.SubItems.Add("");
                    firstItem.SubItems[i].BackColor = Color.White;
                }
            }

            if (8 < waveformAiCtrl1.Conversion.ChannelCount)
            {
                secondItem.SubItems[0].Text = "";
                secondItem.SubItems[0].BackColor = m_simpleGraph1.Pens[8].Color;
                secondItem.SubItems[0].BackColor = m_simpleGraph2.Pens[8].Color;
                secondItem.SubItems[0].BackColor = m_simpleGraph3.Pens[8].Color;
            }
            else
            {
                secondItem.SubItems[0].Text = "";
                secondItem.SubItems[0].BackColor = Color.White;
            }
            for (int i = 9; i < 16; i++)
            {
                if (i < waveformAiCtrl1.Conversion.ChannelCount)
                {
                    secondItem.SubItems.Add((""), Color.Black, Color.Honeydew, new System.Drawing.Font("SimSun", 10));
                    secondItem.SubItems[i - 8].BackColor = m_simpleGraph1.Pens[i].Color;
                    secondItem.SubItems[i - 8].BackColor = m_simpleGraph2.Pens[i].Color;
                    secondItem.SubItems[i - 8].BackColor = m_simpleGraph3.Pens[i].Color;
                }
                else
                {
                    secondItem.SubItems.Add("");
                    secondItem.SubItems[i - 8].BackColor = Color.White;
                }
            }

            ListViewItem[] list = new ListViewItem[] { firstItem, secondItem };
            listView.Items.AddRange(list);
        }

        /// <summary>
        /// Displays a message box with a description of the current error
        /// </summary>
        /// <param name="err"> The error code to describe</param>
        private void HandleError(ErrorCode err)
        {
            if ((err >= ErrorCode.ErrorHandleNotValid) && (err != ErrorCode.Success))
            {
                MessageBox.Show("Sorry ! Some errors happened, the error code is: " + err.ToString(), "StreamingAI");
            }
        }

        /// <summary>
        /// Starts the chart recorder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_start_Click(object sender, EventArgs e)
        {
            if (cmbxPartNo.SelectedIndex == -1)
            {
                MessageBox.Show("No Part Selected");
                return;
            }
            btnUpdateMasterCal_Click(sender, e);
            if(lblPartsUntilMaster.BackColor == Color.LightCoral && !chkMasterPart.Checked)
            {
                MessageBox.Show("There has been too many scans since the last Master Calibration. Please run a mechanical calibration and try again", "Scan Start Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(lblMinUntilMaster.BackColor == Color.LightCoral && !chkMasterPart.Checked)
            {
                MessageBox.Show("It has been too long since the last Master Calibration. Please run a mechanical calibration and try again", "Scan Start Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if((((cmbxPartNo.Text == "") || (txtSN.Text == "" && !chkMasterPart.Checked)) || txtBatchNo.Text == "") && chkGenReport.Checked)
            {
                MessageBox.Show("The appropriate text boxes were not filled out\n. Please fill out P/N, S/N, and the Batch Number and try again.", "Preview File name error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(chkGenReport.Checked && (!cbxVertical.Checked && !cbxHorizontal.Checked && !cbxDistance.Checked))
            {
                MessageBox.Show("There were no graphs selected to save in the report. Please select at least one and try again.", "No Graphs Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClearAllImageFilesInReports();
            chkMasterPart.Enabled = false;
            if (!isPaused) { 
                m_simpleGraph1.Clear();
                m_simpleGraph2.Clear();
                m_simpleGraph3.Clear();
            }
            hasCycled = false;
            finalFileToDelete = -1;
            ErrorCode err = ErrorCode.Success;
            minorRevCounter = 0;
            m_simpleGraph1.VoltagesHigh = new List<double>();
            m_simpleGraph2.VoltagesHigh = new List<double>();
            m_simpleGraph3.VoltagesHigh = new List<double>();

            m_simpleGraph1.VoltTimeHigh = new List<Tuple<Double,Double>>();
            m_simpleGraph2.VoltTimeHigh = new List<Tuple<Double, Double>>();
            m_simpleGraph3.VoltTimeHigh = new List<Tuple<Double, Double>>();

            m_simpleGraph1.VoltagesMedium = new List<double>();
            m_simpleGraph2.VoltagesMedium = new List<double>();
            m_simpleGraph3.VoltagesMedium = new List<double>();

            m_simpleGraph1.VoltTimeMed = new List<Tuple<Double, Double>>();
            m_simpleGraph2.VoltTimeMed = new List<Tuple<Double, Double>>();
            m_simpleGraph3.VoltTimeMed = new List<Tuple<Double, Double>>();

            m_simpleGraph1.Times = new List<double>();
            m_simpleGraph2.Times = new List<double>();
            m_simpleGraph3.Times = new List<double>();

            m_simpleGraph1.result = SimpleGraph.TestResult.NA;
            m_simpleGraph2.result = SimpleGraph.TestResult.NA;
            m_simpleGraph3.result = SimpleGraph.TestResult.NA;

            m_simpleGraph1.timer = timer;
            m_simpleGraph2.timer = timer;
            m_simpleGraph3.timer = timer;

            err = waveformAiCtrl1.Prepare();
            m_xInc = 1.0 / waveformAiCtrl1.Conversion.ClockRate;
            if (!isPaused)
            {
                ConfigureGraph();
            }

            if (err == ErrorCode.Success)
            {
                err = waveformAiCtrl1.Start();
                if (!isPaused)
                {
                    timer.Start();
                }
                else
                {
                    timer.Resume();
                }
                

            }

            if (err != ErrorCode.Success)
            {
                HandleError(err);
                return;
            }

            button_start.Enabled = false;
            button_pause.Enabled = true;
            button_stop.Enabled = true;
            trackBar_div.Enabled = false;
            chkTripleGraph.Enabled = false;
            chkMasterPart.Enabled = false;
            txtPN.Enabled = false;
            tbcOptionPanel.Enabled = false;
            radV10.Enabled = false;
            radV5.Enabled = false;
            radV2.Enabled = false;
            radV1.Enabled = false;

            try
            {
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + startFile);
            }
            catch
            { }
            tmrEndFileChecking.Start();
        }

        /// <summary>
        /// Processes the incomming data and performed the check for Stop on Defect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void waveformAiCtrl1_DataReady(object sender, BfdAiEventArgs args)
        {
            try
            {
                //The WaveformAiCtrl has been disposed.
                if (waveformAiCtrl1.State == ControlState.Idle)
                {
                    return;
                }

                //TODO
                if (stopOnFail &&(m_simpleGraph1.result == SimpleGraph.TestResult.REJECT || m_simpleGraph2.result == SimpleGraph.TestResult.REJECT || m_simpleGraph3.result == SimpleGraph.TestResult.REJECT))
                {
                    pauseButtonClick();
                    return;
                }

                if (m_dataScaled.Length < args.Count)
                {
                    m_dataScaled = new double[args.Count];
                }

                ErrorCode err = ErrorCode.Success;
                int chanCount = waveformAiCtrl1.Conversion.ChannelCount;
                int sectionLength = waveformAiCtrl1.Record.SectionLength;
                err = waveformAiCtrl1.GetData(args.Count, m_dataScaled);
                if (err != ErrorCode.Success && err != ErrorCode.WarningRecordEnd)
                {
                    HandleError(err);
                    return;
                }
                System.Diagnostics.Debug.WriteLine(args.Count.ToString());
                m_simpleGraph1.Chart(m_dataScaled,
                                        chanCount,
                            args.Count / chanCount,
                            m_xInc);
                m_simpleGraph2.Chart(m_dataScaled,
                                        chanCount,
                            args.Count / chanCount,
                            m_xInc);
                m_simpleGraph3.Chart(m_dataScaled,
                                        chanCount,
                            args.Count / chanCount,
                            m_xInc);
            }
            catch (System.Exception) { }
        }

        /// <summary>
        /// Pauses the Scan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_pause_Click(object sender, EventArgs e)
        {
            pauseButtonClick();
        }

        /// <summary>
        /// Stops the scan and starts the report creation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_stop_Click(object sender, EventArgs e)
        {
            ErrorCode err = ErrorCode.Success;
            err = waveformAiCtrl1.Stop();
            timer.Pause();
            if (err != ErrorCode.Success)
            {
                HandleError(err);
                return;
            }
            if (isPaused) 
            {
                isPaused = false;
            }

            /*if(m_simpleGraph1.result==SimpleGraph.TestResult.REJECT || m_simpleGraph2.result == SimpleGraph.TestResult.REJECT || m_simpleGraph3.result == SimpleGraph.TestResult.REJECT)
            {
                m_simpleGraph1.result = SimpleGraph.TestResult.REJECT;
                m_simpleGraph2.result = SimpleGraph.TestResult.REJECT;
                m_simpleGraph3.result = SimpleGraph.TestResult.REJECT;
            }
            else if(m_simpleGraph1.result == SimpleGraph.TestResult.RETEST || m_simpleGraph2.result == SimpleGraph.TestResult.RETEST || m_simpleGraph3.result == SimpleGraph.TestResult.RETEST)
            {
                m_simpleGraph1.result = SimpleGraph.TestResult.RETEST;
                m_simpleGraph2.result = SimpleGraph.TestResult.RETEST;
                m_simpleGraph3.result = SimpleGraph.TestResult.RETEST;
            }
            else
            {
                m_simpleGraph1.result = SimpleGraph.TestResult.ACCEPT;
                m_simpleGraph2.result = SimpleGraph.TestResult.ACCEPT;
                m_simpleGraph3.result = SimpleGraph.TestResult.ACCEPT;
            }*/
            if(!chkMasterPart.Checked)
            {
                button_start.Enabled = true;
                chkMasterPart.Enabled = true;
            }
            button_pause.Enabled = false;
            button_stop.Enabled = false;
            trackBar_div.Enabled = true;
            chkTripleGraph.Enabled = true;
            txtPN.Enabled = true;
            tbcOptionPanel.Enabled = true;
            radV10.Enabled = true;
            radV5.Enabled = true;
            radV2.Enabled = true;
            radV1.Enabled = true;
                


            Array.Clear(m_dataScaled, 0, m_dataScaled.Length);
            //m_simpleGraph1.Clear();
            //m_simpleGraph2.Clear();
            //m_simpleGraph3.Clear();

            if(chkGenReport.Checked)
            {
                if(hasCycled)
                {
                    saveFinalSelectedImages();
                }
                else
                {
                    saveSelectedImages();
                }
            }
                
            finalTmpTime = mainTimeInterval*1000 - timer.RemainingAfterPause;
            Debug.Print(finalTmpTime.ToString());
            timer.Stop();

            if (chkMasterPart.Checked) { }
            //GenerateFreshMasterCalibration(); //Moved this to where the accept button is located instead
            else
                UpdateMasterCalibration();
            if (chkGenReport.Checked)ExportToExcel();

            //printVoltages();
            try
            {
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + endFile);
            }
            catch
            { }
            GetRemainingCalibrationReqs();
            tmrStartFileChecking.Start();
        }

        /// <summary>
        /// Used to change the length of time displayed on the graphs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_div_Scroll(object sender, EventArgs e)
        {
            m_simpleGraph1.Div(trackBar_div.Value);
            m_simpleGraph2.Div(trackBar_div.Value);
            m_simpleGraph3.Div(trackBar_div.Value);
            lastVal = trackBar_div.Value;
            SetXCordRangeLabels();
            ConfigureGraph();
        }

        /// <summary>
        /// Sets the time labels depending on the trackbar value
        /// </summary>
        private void SetXCordRangeLabels()
        {
            string[] X_rangeLabels = new string[2];
            Helpers.GetXCordRangeLabels(X_rangeLabels, ((int)(m_simpleGraph1.XCordTimeDiv)) * 10, 0, m_timeUnit);
            label_XCoordinateMax.Text = X_rangeLabels[0];
            label_XCoordinateMin.Text = X_rangeLabels[1];

            label_XCoordinateMax2.Text = X_rangeLabels[0];
            label_XCoordinateMin2.Text = X_rangeLabels[1];

            label_XCoordinateMax3.Text = X_rangeLabels[0];
            label_XCoordinateMin3.Text = X_rangeLabels[1];
        }

        private void waveformAiCtrl1_CacheOverflow(object sender, BfdAiEventArgs e)
        {
            MessageBox.Show("WaveformAiCacheOverflow");
        }

        private void waveformAiCtrl1_Overrun(object sender, BfdAiEventArgs e)
        {
            if (m_isFirstOverRun)
            {
                MessageBox.Show("WaveformAiOverrun");
                m_isFirstOverRun = false;
            }
        }

        /// <summary>
        /// Switches the graph range to 10, 5, or 2.5V
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeVoltageRange(object sender, EventArgs e)
        {
            if (radV10.Checked)
                voltageType = 10;
            else if (radV5.Checked)
                voltageType = 5;
            else if (radV2.Checked)
                voltageType = 2.5;
            else
                voltageType = 1;
            ConfigureGraph();
            if(cbxAmpInch.Checked)
            redrawManualGraph();
        }

        /// <summary>
        /// Toggles the Stop on Defect Feature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblStopOnFail_DoubleClick(object sender, EventArgs e)
        {
            if (stopOnFail)
            {
                lblStopOnFail.Text = "CONTINUE ON FAIL";
                lblStopOnFail.BackColor = Color.Lime;
                stopOnFail = false;
            }
            else
            {
                lblStopOnFail.Text = "STOP ON FAIL";
                lblStopOnFail.BackColor = Color.Red;
                stopOnFail = true;
            }
        }

        /// <summary>
        /// Checks the password the user inputted to change the password protected options against the password hardcoded in this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPasswordSubmit_Click(object sender, EventArgs e)
        {
            if (txtOptionsPassword.Text.Equals("Quality1"))
            {
                txtOptionsPassword.Text = "";
                TogglePasswordOptions(true);
            }
            else
            {
                MessageBox.Show("Password was Incorrect. Please try again.", "Options Password Incorrect", MessageBoxButtons.OK);
                txtOptionsPassword.Text = "";
            }
        }

        /// <summary>
        /// Toggles the ability to change the password protected options based off the status parameter
        /// </summary>
        /// <param name="status">True to enable the options, false to disable</param>
        private void TogglePasswordOptions(bool status)
        {
            txtTolerance.Enabled = status;
            txtPartsPerMaster.Enabled = status;
            txtMinutesPerMaster.Enabled = status;
            btnDoneEditing.Enabled = status;
            txt4Tolerance.Enabled = status;
            btnEditPartInfo.Enabled = status;

        }

        /// <summary>
        /// Used to lock the password protected options once they are changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoneEditing_Click(object sender, EventArgs e)
        {
            donePressed(1);
        }

        /// <summary>
        /// Pauses the scan without clearing the graphs or saved data
        /// </summary>
        private void pauseButtonClick()
        {

            ErrorCode err = ErrorCode.Success;
            err = waveformAiCtrl1.Stop();
            timer.Pause();

            if (err != ErrorCode.Success)
            {
                HandleError(err);
                return;
            }

            button_start.Invoke((System.Action)delegate
            {
                button_start.Enabled = true;
            });

            button_pause.Invoke((System.Action)delegate
            {
                button_pause.Enabled = false;
            });
            
            isPaused = true;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        #region UniWest
        /// <summary>
        /// Establishes connection to the uniwest
        /// </summary>
        private void PortSetup()
        {
            string uniIpAddr = "192.168.1.137";
            int uniPort = 55556;
            try
            {
                client = new TcpClient(uniIpAddr, uniPort);
                //client = new TcpClient(
                stream = client.GetStream();
                sw = new StreamWriter(stream);
                sr = new StreamReader(stream);
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error connecting to the Uniwest. " +
                "Please ensure all values are corrent and the IP on the uniwest system is set to \" " + uniIpAddr + "\".\n The error was as follows: \n" +
                e.ToString(), "Uniwest Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //While any of these methods are working, the UniWest System should not be physically altared.
        /// <summary>
        /// Retrieves the UniWest Values from the System.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetUniVals_Click(object sender, EventArgs e)
        {
            try
            {
                GetUniVals();
            }
            catch
            {
                BtnReconnectUniWest_Click(sender, e);
                try
                { 
                    GetUniVals();
                }
                catch
                {
                    MessageBox.Show("There was an error retrieving the UniWest values. Please ensure the UniWest is connected and that no menus are currently open before trying again", "UniWest Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Interfaces with the uniWest to retrieve the frequency and gain values.
        /// The Low and High Pass Filters are ignored since the uniwest only send a 1 or 0 if they are enabled or disabled
        /// </summary>
        private void GetUniVals()
        {
            //Gets the Frequency, Horizontal Gain (x, channel 0) and Vertical Gain (y, channel 2)
            string tmp;
            string[] responses = new string[4];
            string[] tmpParseVals = new string[4];
            double[] fVals = new double[4];
            try
            {
                //Gets all the Information, may need to change gain 1&2 to 0&1
                tmp = "-FREQ ?";
                SendCommand(stream, tmp);
                responses[0] = ReadResponse(stream);
                tmp = "-GAIN ?";
                SendCommand(stream, tmp);
                responses[1] = ReadResponse(stream);
                tmp = "-FILTER ? H B";
                SendCommand(stream, tmp);
                responses[2] = ReadResponse(stream);
                tmp = "-FILTER ? L B";
                SendCommand(stream, tmp);
                responses[3] = ReadResponse(stream);
                //tmp = "-GAIN ? 2";
                //SendCommand(stream, tmp);
                //responses[2] = ReadResponse(stream);

                //Parses and sorts the incoming data by retrieving all the numbers and decimals from the strings and putting them in an array of ints.
                for (int i = 0; i < 2; i++)
                {
                    tmpParseVals[i] = "";
                    for (int j = 0; j < responses[i].Length; j++)
                    {
                        if (Char.IsNumber(responses[i][j]) || responses[i][j] == '.')
                        {
                            tmpParseVals[i] += responses[i][j];
                        }
                    }
                    try
                    {
                        fVals[i] = Double.Parse(tmpParseVals[i]);
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("The system was not able to process values from the UniWest due to the following error: " + ex2.ToString(), "Uniwest connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Puts the newly parsed data into the appropriate text boxes
                txtFrequency.Text = (fVals[0]/1000000).ToString();
                //txtHorizGain.Text = fVals[1].ToString();
                //txtVertGain.Text = fVals[1].ToString();
                txtGain.Text = fVals[1].ToString();
                //tbxHighPass.Text = fVals[2].ToString();
                //tbxLowPass.Text = fVals[3].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The system was not able to connect with the UniWest due to the following error: " + ex.ToString(), "Uniwest connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="command"></param>
        private void SendCommand(NetworkStream stream, string command)
        {
            //byte[] data = Encoding.ASCII.GetBytes(command);
            //stream.Write(data, 0, data.Length);
            //stream.Flush();
            command += "\r";
            if(sw != null)
            {
                sw.Write(command);
                sw.Flush();
            }
        }

        /// <summary>
        /// Waits for and reads the response value provided by the UniWest
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private string ReadResponse(NetworkStream stream)
        {
            string tmp = "";
            char c;
            bool loopTime = true;
            int crCounter = 0;
            if (sr != null)
            {
                while (loopTime)
                {
                    c = (char)sr.Read();
                    if (crCounter == 1)
                        tmp += c;
                    if (c == '\r')
                    {
                        if (crCounter == 1)
                            loopTime = false;
                        else
                            crCounter++;
                    }
                }
            }
            return tmp;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsInRange(int value, int min=0, int max=10000) => (uint)(value - min) <= (uint)(max - min);

        /// <summary>
        /// Checks if the error is a failure and returns a bool if it is
        /// </summary>
        /// <param name="error">The error to check</param>
        /// <returns></returns>
        public static bool IsFail (string error)
        {
            return error.Contains("FAIL");
        }

        /// <summary>
        /// Sets the Low Pass Filter on the UniWest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLPSet_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(tbxLowPass.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                return;
            }

            if (!IsInRange(parsedValue))
            {
                MessageBox.Show("Number is out of range");
                return;
            }

            string tmp = $"-FILTERSET L B {parsedValue}"; // in HZ
            SendCommand(stream, tmp);
            string error = ReadResponse(stream);


            if (IsFail(error))
            {
                MessageBox.Show($"Error Occured:\n {error}");
                return;
            }
        }

        /// <summary>
        /// Sets the High Pass Filter on the UniWest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHPSet_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(tbxHighPass.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                return;
            }

            if (!IsInRange(parsedValue))
            {
                MessageBox.Show("Number is out of range");
                return;
            }


            string tmp = $"-FILTERSET H B {parsedValue}"; //in HZ
            SendCommand(stream, tmp);
            string error = ReadResponse(stream);

            if (IsFail(error))
            {
                MessageBox.Show($"Error Occured:\n {error}");
                return;
            }
        }

        /// <summary>
        /// Calls the Set Frequency method and if it fails, gets the current frequency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFreqSet_Click(object sender, EventArgs e)
        {
            try
            {
                SetFreq();
            }
            catch
            {
                BtnReconnectUniWest_Click(sender, e);
                try
                {
                    GetUniVals();
                }
                catch
                {
                    MessageBox.Show("There was an error retrieving the UniWest values. Please ensure the UniWest is connected and that no menus are currently open before trying again", "UniWest Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// attempts ot set the frequency on the UniWest
        /// </summary>
        private void SetFreq()
        { 
            Double tmpNum;
            try
            {
                tmpNum = Double.Parse(txtFrequency.Text)*1000000;
            }
            catch
            {
                MessageBox.Show("There was an issue setting the frequency. Please check the set value and try again.", "Set Frequency Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SendCommand(stream, "-FREQ " + tmpNum);
            string tmp = ReadResponse(stream);
            MessageBox.Show("Status: " + tmp, "Frequency Test", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Attempts to set the gain on the UniWest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGainSet_Click(object sender, EventArgs e)
        {
            int tmpNum;
            try
            {
                tmpNum = Int32.Parse(txtGain.Text);
            }
            catch
            {
                MessageBox.Show("There was an issue setting the gain. Please check the set value and try again.", "Set Gain Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SendCommand(stream, "-GAIN " + tmpNum);
            string tmp = ReadResponse(stream);
            MessageBox.Show("Status: " + tmp, "Gain Test", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Generates the filename to be used to save the report with the format [partNo]-[SN or "MASTER"] [date/time]
        /// </summary>
        /// <returns></returns>
        private string GenerateFileName()
        {
            string fname = "";
            if ((cmbxPartNo.SelectedIndex != -1) && (txtSN.Text != "" || chkMasterPart.Checked))
            {
                fname += allParts[cmbxPartNo.SelectedIndex].partNo + "-";
                if (chkMasterPart.Checked)
                    fname += "MASTER";
                else
                    fname += txtSN.Text;
                fname += " ";
                //Need to add date (yymmdd)
                fname += DateTime.Now.ToString("yyMMdd");
                fname += "d " + DateTime.Now.ToString("HHmm") + "t";
                //lblReportFilenameDisplay.Text = fname;
                return fname;
            }
            else
            {
                MessageBox.Show("The appropriate text boxes were not filled out\n. Please fill out P/N and S/N and try again.", "Preview File name error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "File name invalid";
            }
        }

        /// <summary>
        /// used to display a preview of what the fileName will be once a report is created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreviewFileName_Click(object sender, EventArgs e)
        {
            string fname = "";
            if ((cmbxPartNo.SelectedIndex != -1) && (txtSN.Text != "" || chkMasterPart.Checked))
            {
                fname += allParts[cmbxPartNo.SelectedIndex].partNo + "-";
                if (chkMasterPart.Checked)
                    fname += "MASTER";
                else
                    fname += txtSN.Text;
                fname += " ";
                //Need to add date (yymmdd)
                fname += DateTime.Now.ToString("yy/MM/dd");
                fname += "d " + DateTime.Now.ToString("HH/mm") + "t";
                lblReportFilenameDisplay.Text = fname;
            }
            else
                MessageBox.Show("The appropriate text boxes were not filled out\n. Please fill out P/N and S/N and try again.", "Preview File name error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Creates an excel and PDF file to display the report after a scan
        /// </summary>
        public void ExportToExcel()
        {
            //File Creation
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();
            Excel.Worksheet sheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = sheet.UsedRange;
            //Formatting
            sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
            sheet.Columns[1].ColumnWidth = 8;
            for (int i = 2; i <= 4; i++)
            {
                sheet.Columns[i].ColumnWidth = 35;
                for (int j = 2; j < 5; j++)
                {
                    sheet.Cells[i, j].Borders.Color = System.Drawing.Color.Black.ToArgb();
                    sheet.Cells[i + 5, j].Borders.Color = System.Drawing.Color.Black.ToArgb();
                }
            }
            for (int i = 2; i <= 4; i++)
            {
                sheet.Cells[5, i].Borders.Color = System.Drawing.Color.Black.ToArgb();
                sheet.Cells[10, i].Borders.Color = System.Drawing.Color.Black.ToArgb();
            }
            sheet.Cells[1, 3].Font.Bold = true;
            //Text Entry
            sheet.Cells[2, 2].Value = "P/N: " + allParts[cmbxPartNo.SelectedIndex].partNo;
            sheet.Cells[3, 2].Value = "S/N: " + txtSN.Text;
            sheet.Cells[4, 2].Value = "PTR No: " + txtPTRNo.Text;
            sheet.Cells[2, 3].Value = "Batch No: " + txtBatchNo.Text;
            if (chkMasterPart.Checked)
            {
                if (!secondMasterPass) //Displays Pending for the first set of results
                    sheet.Cells[3, 3].Value = "Status: PENDING";
                else if (masterUserChoice == 0) //Onto the Second Pass Options
                    sheet.Cells[3, 3].Value = "Status: REJECT";
                else if (masterUserChoice == 1)
                    sheet.Cells[3, 3].Value = "Status: ACCEPT";
                else //Should not get here, but just to make sure
                    sheet.Cells[3, 3].Value = "Status: ERROR - NO USER INPUT SELECTED";
            }
            else
            {
                if(radPrimaryVertT.Checked)
                    sheet.Cells[3, 3].Value = "Status: " + m_simpleGraph1.result.ToString();
                else if (radPrimaryHorizT.Checked)
                    sheet.Cells[3, 3].Value = "Status: " + m_simpleGraph2.result.ToString();
                else if (radPrimaryHorizT.Checked)
                    sheet.Cells[3, 3].Value = "Status: " + m_simpleGraph3.result.ToString();
            }
                
            sheet.Cells[4, 3].Value = "Master S/N: " + txtMasterSN.Text;
            sheet.Cells[5, 3].Value = "ETTS#: " + txtETTSNo.Text;
            sheet.Cells[2, 4].Value = "Date: " + DateTime.Today.ToString("MM/dd/yyyy") + " - " + DateTime.Now.ToString("h:mm:ss: tt");
            sheet.Cells[3, 4].Value = "Scan Duration: " + Decimal.Round((decimal)getTimer(),1).ToString() + " s"; 
            sheet.Cells[4, 4].Value = "ET Level 2: " + txtETLev2.Text;
            sheet.Cells[5, 4].Value = "ET Level 1: " + txtETLev1.Text;

            sheet.Cells[7, 2].Value = "EC Generator Settings";
            sheet.Cells[8, 2].Value = "Probe Type = " + cmbProbeType.Text;
            sheet.Cells[9, 2].Value = "Angle = " + txtAngle.Text + " deg";
            sheet.Cells[10, 2].Value = "Probe Drive = " + cmbProbeDrive.Text;
            sheet.Cells[8, 3].Value = "Frequency = " + txtFrequency.Text + " MHz";
            sheet.Cells[10, 3].Value = "L. Pass Filter = " + tbxLowPass.Text + " Hz";
            sheet.Cells[9, 3].Value = "Gain = " + txtGain.Text + " dB";
            sheet.Cells[10, 4].Value = "H. Pass Filter = " + tbxHighPass.Text + " Hz";
            xlRange.Cells[1, 3].Value = "          ET System Verification.";
            sheet.Cells[20, 3].Value = "Time (s)";
            sheet.Cells[20, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            sheet.Cells[20, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            //Image Saving
            imagesToDelete = minorRevCounter;
            minorRevCounter = 0;
            bool exit = false;
            int tmpXPos = 2;
            int tmpYPos = 13;
            int tmpVSpot = 33;
            int flip = 2;
            int numSheets = 2;
            string temp = "";
            if ((chkTripleGraph.Checked && radPrimaryVertT.Checked) || (!chkTripleGraph.Checked && radVerticalOpt.Checked))
            {
                temp = "_Vertical_";
            }
            else if (chkTripleGraph.Checked && radPrimaryHorizT.Checked || (!chkTripleGraph.Checked && radHorizOpt.Checked))
            {
                temp = "_Horizontal_";
            }
            else if (chkTripleGraph.Checked && radPrimaryVecSumT.Checked || (!chkTripleGraph.Checked && radDiffOpt.Checked))
            {
                temp = "_Distance_";
            }
            string tmp, savePath;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Report";
                sfd.FileName = GenerateFileName();
                if (lastSavedReportLocation != null)
                    sfd.InitialDirectory = lastSavedReportLocation;
                else
                    sfd.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    savePath = sfd.FileName;
                    lastSavedReportLocation = Path.GetDirectoryName(savePath);
                    tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + temp + "(" + minorRevCounter.ToString() + ")" + ".Jpg";

                    while (!exit)
                    {
                        if (minorRevCounter == 0)
                        {
                            try
                            {
                                sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos + 5, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, sheet.Cells[tmpYPos, 5].Left - 1 - sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos + 7, tmpXPos].Top - sheet.Cells[tmpYPos, tmpXPos].Top);//495, 255); //Full: 660, 340
                            }
                            catch
                            {
                                try
                                {
                                    sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos + 5, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, sheet.Cells[tmpYPos, 5].Left - 1 - sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos + 7, tmpXPos].Top - sheet.Cells[tmpYPos, tmpXPos].Top);
                                    exit = true;
                                    exitEarly = true;
                                }
                                catch
                                {
                                    MessageBox.Show("There was an error creating the report. Please check all variables and try again.", "Export Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            int tmpNum = minorRevCounter + 1;
                            tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + temp + "(" + tmpNum.ToString() + ")" + ".Jpg";
                            if (System.IO.File.Exists(tmp))
                            {
                                minorRevCounter++;
                                tmpYPos = 22;
                                sheet.Cells[29, 3].Value = "Time (s)";
                                sheet.Cells[29, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                sheet.Cells[29, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                try
                                {
                                    sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos + 5, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, sheet.Cells[tmpYPos, 5].Left - 1 - sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos + 7, tmpXPos].Top - sheet.Cells[tmpYPos, tmpXPos].Top);//495, 255); //Full: 660, 340
                                }
                                catch
                                {
                                    try
                                    {
                                        sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos + 5, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, sheet.Cells[tmpYPos, 5].Left - 1 - sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos + 7, tmpXPos].Top - sheet.Cells[tmpYPos, tmpXPos].Top);
                                        exit = true;
                                        exitEarly = true;
                                    }
                                    catch
                                    {
                                        exit = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, sheet.Cells[tmpYPos, 5].Left - 1 - sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos + 7, tmpXPos].Top - sheet.Cells[tmpYPos, tmpXPos].Top);//sheet.Cells[tmpVSpot - flip, tmpXPos].Top - sheet.Cells[tmpYPos, tmpXPos].Top);//429, 221);
                        }
                        minorRevCounter++;
                        tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + temp + "(" + minorRevCounter.ToString() + ")" + ".Jpg";
                        if (!System.IO.File.Exists(tmp))
                        {
                            exit = true;
                        }
                        if (minorRevCounter == 2)
                        {
                            tmpYPos = 34;
                        }
                        else
                        {
                            tmpYPos += 8;
                            if (minorRevCounter % 4 == 2)
                                tmpYPos++;
                        }
                        if (minorRevCounter != 0 && !exit)
                        {
                            sheet.Cells[tmpVSpot, 3].Value = "Time (s)";
                            sheet.Cells[tmpVSpot, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            sheet.Cells[tmpVSpot, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            tmpVSpot += minorRevCounter % 4 == 1 ? 9 : 8;
                            flip = minorRevCounter % 4 == 3 ? 8 : 11;
                        }
                    }
                    
                    if (minorRevCounter > 1 || hasCycled)
                    {
                        sheet.Cells[tmpVSpot, 3].Value = "Time (s)";
                        sheet.Cells[tmpVSpot, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        sheet.Cells[tmpVSpot, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        if (minorRevCounter > 2)
                        {
                            sheet.Cells[tmpVSpot + 8, 3].Value = "Time (s)";
                            sheet.Cells[tmpVSpot + 8, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            sheet.Cells[tmpVSpot + 8, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }
                    string tmpFinal = tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + temp + "(" + "1" + ") - Final" + ".Jpg";
                    if (System.IO.File.Exists(tmpFinal))
                    {
                        sheet.Cells[28, 3].Value = "Time (s)";
                        sheet.Cells[28, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        sheet.Cells[28, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    else
                    {
                        tmpFinal = tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + temp + "(" + "2" + ") - Final" + ".Jpg";
                        if (System.IO.File.Exists(tmpFinal))
                        {
                            sheet.Cells[41, 3].Value = "Time (s)";
                            sheet.Cells[41, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            sheet.Cells[41, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }
                    sheet.Cells[33, 3].Value = "";
                    double percentOfGraph = timer._stopwatch.Elapsed.TotalSeconds / GetScanLength();
                    tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + temp + "(" + minorRevCounter.ToString() + ") - Final" + ".Jpg";
                    if (!exitEarly && hasCycled)
                    {
                        try
                        {
                            int tempNum = (int)(sheet.Cells[33, tmpXPos].Top - sheet.Cells[26, tmpXPos].Top); // this is here because it refused to work in the equation like a good little girl -cat
                            sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, ((sheet.Cells[tmpYPos, 5].Left - 1 - sheet.Cells[tmpYPos, tmpXPos].Left) * percentOfGraph), tempNum);
                        }
                        catch
                        {
                            exit = true;
                        }
                    }
                    int subtract = minorRevCounter % 2 == 1 ? 11 : 8;
                    //}
                    if (cbxVertical.Checked && !radPrimaryVertT.Checked && chkTripleGraph.Checked)
                    {
                        addGraph(numSheets, xlWorkbook, "Vertical");
                        numSheets++;
                    }
                    if (cbxHorizontal.Checked && !radPrimaryHorizT.Checked && chkTripleGraph.Checked)
                    {
                        addGraph(numSheets, xlWorkbook, "Horizontal");
                        numSheets++;
                    }
                    if (cbxDistance.Checked && !radPrimaryVecSumT.Checked && chkTripleGraph.Checked)
                    {
                        addGraph(numSheets, xlWorkbook, "Distance");
                        numSheets++;
                    }
                    if (cbxVoltages.Checked)
                    {
                        if (cbxVoltageV.Checked)
                        {
                            addVoltages(numSheets, xlWorkbook, m_simpleGraph1);
                            numSheets++;
                        }
                        if (cbxVoltageH.Checked)
                        {
                            addVoltages(numSheets, xlWorkbook, m_simpleGraph2);
                            numSheets++;
                        }

                        if (cbxVoltageD.Checked)
                        {
                            addVoltages(numSheets, xlWorkbook, m_simpleGraph3);
                            numSheets++;
                        }
                    }
                    if (!secondMasterPass && chkMasterPart.Checked)
                    {
                        savePath += " - PENDING";
                        lastPendingFileNameThatWasSaved = savePath;
                    }
                    if (secondMasterPass || !chkMasterPart.Checked)
                    {
                        try
                        {
                            xlWorkbook.SaveAs(savePath);
                        }
                        catch { }
                    }
                    try
                    {
                        // Call Excel's native export function (valid in Office 2007 and Office 2010, AFAIK)
                        xlWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, savePath);
                    }
                    catch (System.Exception ex)
                    {
                        // Mark the export as failed for the return value...
                        //exportSuccessful = false;

                        // Do something with any exceptions here, if you wish...
                        // MessageBox.Show...        
                    }

                    if (chkMasterPart.Checked && !secondMasterPass)
                    {
                        grpMasterCalibResults.Visible = true;
                    }
                    if (chkGenReport.Checked)
                    {
                        if (chkMasterPart.Checked && !secondMasterPass)
                        {
                            return;
                        }
                        else
                        {
                            //Reenable the controls disabled above
                            secondMasterPass = false;
                            deleteSelectedImages();
                            string deletePendingFilePath = lastPendingFileNameThatWasSaved;
                            try
                            {
                                System.IO.File.Delete(deletePendingFilePath + ".pdf");
                                System.IO.File.Delete(deletePendingFilePath + ".xlsx");
                            }
                            catch { }
                        }
                    }
                }
            }


            xlWorkbook.Close(false);
            xlApp.Quit();
            //removing the process from the task manager
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

            GC.Collect();
        }

        /// <summary>
        /// Saves what is in the current text boxes to file, so later a form can be easily filled with the same data (See btnLoadReportFile_Click)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveReportFile_Click(object sender, EventArgs e)
        {
            if(cmbxPartNo.Text == "" || txtSN.Text == "" || txtPTRNo.Text == "" || txtBatchNo.Text == "" || txtETTSNo.Text == "" || txtMasterSN.Text == "" || 
                txtETLev2.Text == "" || txtETLev1.Text == "" || cmbProbeType.Text == "" || cmbProbeDrive.Text == "" || txtAngle.Text == "" || lblLowPass.Text == "" || lblHighPass.Text == "")
            {
                MessageBox.Show("There was an error saving the Report Values. Please make sure that all the approprite report text boxes are filled out before saving.", "Report Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tmp;
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "Text File|*.txt";
                saveFileDialog1.Title = "Save Report Values";
                saveFileDialog1.DefaultExt = "txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    tmp = "This is a file for quickly filling out the different text boxes used in automatically generating the report.";
                    System.IO.File.WriteAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "This file is editable, but must remain in the same format otherwise the data may be wrong or an error may occur";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "***************************************************************************************************************";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "P/N: " + allParts[cmbxPartNo.SelectedIndex].partNo + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "S/N: " + txtSN.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "PTR No: " + txtPTRNo.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "Batch No: " + txtBatchNo.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "Master S/N: " + txtMasterSN.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "ETTS No: " + txtETTSNo.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "ET Level 1: " + txtETLev1.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "ET Level 2: " + txtETLev2.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "Probe Type: " + cmbProbeType.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "Probe Drive: " + cmbProbeDrive.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "Angle: " + txtAngle.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "L Pass Filter: " + tbxLowPass.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, Environment.NewLine);
                    tmp = "H Pass Filter: " + tbxHighPass.Text + "";
                    System.IO.File.AppendAllText(saveFileDialog1.FileName, tmp);
                }
            }
        }

        /// <summary>
        /// Reads from a text file generated by btnSaveReportFile_Click and puts all the data in its respective text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadReportFile_Click(object sender, EventArgs e)
        {
            StreamReader myStream;
            string tmp;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File|*.txt";
                ofd.Title = "Load Report Values";
                ofd.DefaultExt = "txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    myStream = new StreamReader(ofd.FileName);
                    //Gets through the Descriptions of the file
                    myStream.ReadLine();
                    myStream.ReadLine();
                    myStream.ReadLine();
                    //Starts reading the actual data
                    tmp = TrimStringForFileLoad(myStream.ReadLine()); // P/N
                    for (int i= 0;  i < allParts.Count; i++) 
                    {
                        if(allParts[i].partNo == tmp)
                        {
                            //cmbxPartNo.SelectedIndex = i;
                        }
                    }
                    
                    tmp = myStream.ReadLine(); // S/N
                    txtSN.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //Part No.
                    txtPTRNo.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //Batch No.
                    txtBatchNo.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //Master S/N
                    txtMasterSN.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //ETTS No.
                    txtETTSNo.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //ET Level 1
                    txtETLev1.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //ET Level 2
                    txtETLev2.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //Probe Type
                    cmbProbeType.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //Probe Drive
                    cmbProbeDrive.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //Angle
                    txtAngle.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //L Pass Filter
                    tbxLowPass.Text = TrimStringForFileLoad(tmp);

                    tmp = myStream.ReadLine(); //H Pass Filter
                    tbxHighPass.Text = TrimStringForFileLoad(tmp);
                    myStream.Close();
                }
            }
        }

        /// <summary>
        /// Used when reading values in the report information file to remove the excess characters and only returns the string to be placed into the textbox
        /// </summary>
        /// <param name="strFromFile"></param>
        /// <returns></returns>
        private string TrimStringForFileLoad(string strFromFile)
        {
            string tmp = "";
            int i = 0;
            while (strFromFile[i] != ':')
            {
                i++;
            }
            i++;
            while (strFromFile[i] == ' ')
            {
                i++;
            }
            for (int j = i; j < strFromFile.Length; j++)
            {
                tmp += strFromFile[j];
            }
            return tmp;
        }

        /// <summary>
        /// Saves a single screenshot for use in the report
        /// </summary>
        /// <param name="b"></param>
        /// <param name="name"></param>
        private void SaveImage(Bitmap b, string name)
        {
            String tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text+$"_{name}_" + "(" + minorRevCounter.ToString() + ")" + ".Jpg";
            //minorRevCounter++;
            b.Save(tmp);
        }



        /// <summary>
        /// Saves the final image of the bitmap b and reduces the size of it to eleminate duplicate data in the previous graphs
        /// </summary>
        /// <param name="b"></param>
        /// <param name="name"></param>
        private void SaveFinalImage(Bitmap b, string name)
        {
            String tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + $"_{name}_" + "(" + minorRevCounter.ToString() + ") - Final" + ".Jpg";
            string tmpLabel = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + $"_{name}_" + "(" + minorRevCounter.ToString() + ") - FinalLabel" + ".Jpg";
            //System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
            //double timeRemaining = timer._stopwatch.Elapsed.TotalSeconds % GetScanLength(); 
            //double percentOfGraph = timeRemaining / GetScanLength();
            finalFileToDelete = minorRevCounter;
            double percentOfGraph = timer._stopwatch.Elapsed.TotalSeconds / GetScanLength();
            int newWidth, newHeight, widthStartingSpot, hieghtStartingSpot, labelWidth;
            //Bitmap tmpBitmap;
            //tmpBitmap = new Bitmap(b);
            newWidth = (int)(b.Width * percentOfGraph);
            newHeight = b.Height; //Height Should not change
            widthStartingSpot = b.Width - newWidth;
            hieghtStartingSpot = b.Height;
            labelWidth = 40;
            Bitmap CroppedImage = b.Clone(new System.Drawing.Rectangle(widthStartingSpot, 0, newWidth, newHeight), b.PixelFormat);
            CroppedImage.Save(tmp);
            Bitmap labelImage = b.Clone(new System.Drawing.Rectangle(0, 0, labelWidth, newHeight), b.PixelFormat);
            labelImage.Save(tmpLabel);
        }

        /// <summary>
        /// returns the length of the scan
        /// </summary>
        /// <returns></returns>
        private double GetScanLength()
        {
            double ret = timer._initialInterval/1000;
            return ret;
        }

        /// <summary>
        /// deletes all the Images associated with one bitmap
        /// </summary>
        /// <param name="name">used to deleniate the graph to delete the images for (Horizontal, Vertical, Distance)</param>
        private void deleteImages(string name)
        {
            for (int i = 0; i < imagesToDelete; i++)
            {
                try
                {
                    System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + $"_{name}_" + "(" + i.ToString() + ")" + ".Jpg");
                }
                catch
                {
                    MessageBox.Show("There was an error cleaning up some of the image files used to generate the report. You may delete them yourself if you wish.", "Image File Deletion Error", MessageBoxButtons.OK);
                }
            }
            if(!exitEarly && finalFileToDelete != -1)
            {
                try
                {
                    System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + $"_{name}_" + "(" + finalFileToDelete.ToString() + ") - final" + ".Jpg");
                    System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + $"_{name}_" + "(" + finalFileToDelete.ToString() + ") - finalLabel" + ".Jpg");
                }
                catch { }
            }
        }

        /// <summary>
        /// Saves the final image for each of the graphs selected to be saved
        /// </summary>
        private void saveFinalSelectedImages()
        {
            if (cbxHorizontal.Checked)
            {
                SaveFinalImage(m_simpleGraph2.m_bitmap, "Horizontal");
            }

            if (cbxVertical.Checked)
            {
                SaveFinalImage(m_simpleGraph1.m_bitmap, "Vertical");
            }

            if (cbxDistance.Checked)
            {
                SaveFinalImage(m_simpleGraph3.m_bitmap, "Distance");
            }

            minorRevCounter++;
        }

        /// <summary>
        /// Saves an image of each graph selected to be saved at the current position
        /// </summary>
        private void saveSelectedImages()
        {
            if(getTimer() > GetScanLength())
                hasCycled = true;
            if (cbxHorizontal.Checked)
            {
                SaveImage(m_simpleGraph2.m_bitmap, "Horizontal");
            }

            if (cbxVertical.Checked)
            {
                SaveImage(m_simpleGraph1.m_bitmap, "Vertical");
            }

            if (cbxDistance.Checked)
            {
                SaveImage(m_simpleGraph3.m_bitmap, "Distance");
            }
            minorRevCounter++;
        }

        /// <summary>
        /// Deletes all the images used in the report
        /// </summary>
        private void deleteSelectedImages()
        {
            if (cbxHorizontal.Checked)
            {
                deleteImages("Horizontal");
            }

            if (cbxVertical.Checked)
            {
                deleteImages("Vertical");
            }

            if (cbxDistance.Checked)
            {
                deleteImages("Distance");
            }

            imagesToDelete = 0;
        }

        /// <summary>
        /// Used after a master part calibration was accepted to reset the master calibration timers
        /// </summary>
        public void GenerateFreshMasterCalibration()
        {
            string fileLoc = System.Windows.Forms.Application.StartupPath + "/MCalInfo.txt";
            StreamWriter swt = new StreamWriter(fileLoc);
            swt.WriteLine(txtPartsPerMaster.Text);
            swt.WriteLine(DateTime.Now);
            swt.Close();
            if (lblPartsUntilMaster.BackColor == Color.LightCoral)
                lblPartsUntilMaster.BackColor = SystemColors.Window;
            if (lblMinUntilMaster.BackColor == Color.LightCoral)
                lblMinUntilMaster.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Used to create a master calibration file that requires the user to run a new calibration before they proceed
        /// </summary>
        public void GenerateEmptyMasterCalibration()
        {
            string fileLoc = System.Windows.Forms.Application.StartupPath + "/MCalInfo.txt";
            StreamWriter swt = new StreamWriter(fileLoc);
            swt.WriteLine("0");
            swt.WriteLine(DateTime.Now);
            swt.Close();
            lblPartsUntilMaster.BackColor = Color.LightCoral;
            GetRemainingCalibrationReqs();
        }

        /// <summary>
        /// Updates the master calibration file to decrement the scans remaining counter
        /// </summary>
        private void UpdateMasterCalibration()
        {
            string fileLoc = System.Windows.Forms.Application.StartupPath + "/MCalInfo.txt";
            string tmp;
            //Reads the previous values and decrements the parts remaining number
            StreamReader srt = new StreamReader(fileLoc);
            tmp = srt.ReadLine();
            int tmpPartsLeft = Int32.Parse(tmp);
            tmpPartsLeft--;
            tmp = srt.ReadLine();
            srt.Close();
            //writes the new values back to file (keeping the time the same as before)
            StreamWriter swt = new StreamWriter(fileLoc);
            swt.WriteLine(tmpPartsLeft.ToString());
            swt.WriteLine(tmp);
            swt.Close();
        }

        /// <summary>
        /// reads the master calibration file and updates the user on when 
        /// </summary>
        private void GetRemainingCalibrationReqs()
        {
            string fileLoc = System.Windows.Forms.Application.StartupPath + "/MCalInfo.txt";
            StreamReader srt = new StreamReader(fileLoc);
            string tmp = srt.ReadLine();
            int partsLeft = Int32.Parse(tmp);
            lblPartsUntilMaster.Text = tmp;
            if (partsLeft <= 0)
            {
                lblPartsUntilMaster.BackColor = Color.LightCoral;
            }
            tmp = srt.ReadLine();
            DateTime timeRemaining = DateTime.Parse(tmp);
            TimeSpan dur = DateTime.Now - timeRemaining;
            int timeLeft = (int)(((int)(dur.TotalHours) * 60) + ((int)(dur.Minutes)));
            if (Int32.Parse(txtMinutesPerMaster.Text) - timeLeft <= 0)
            {
                lblMinUntilMaster.Text = "0";
                lblMinUntilMaster.BackColor = Color.LightCoral;
            }
            else
            {
                lblMinUntilMaster.Text = (Int32.Parse(txtMinutesPerMaster.Text) - timeLeft).ToString();
            }
            srt.Close();
        }

        //Returns if a Master Calibration neeeds to be done
        /// <summary>
        /// updates the form fields to display the current number of scans and time before a new calibrations needs ot be done
        /// </summary>
        /// <returns></returns>
        private bool RetMasterCalibration()
        {
            bool exit = false;
            try
            {
                GetRemainingCalibrationReqs();
                if (lblPartsUntilMaster.BackColor == Color.LightCoral || lblMinUntilMaster.BackColor == Color.LightCoral)
                    exit = true;
            }
            catch
            {
                MessageBox.Show("There was an error getting the information for the master calibration requirements.", "Master Calibration Status Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return exit;
        }

        /// <summary>
        /// the timer to check if Bearingscan is ready for the chartRecorder program to start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrStartFileChecking_Tick(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + startFile))
            {
                try
                {
                    System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + endFile);
                }
                catch{}
                tmrStartFileChecking.Stop();
                try
                {
                    System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + startFile);
                }
                catch
                {
                    MessageBox.Show("There was an error deleting a temperary file to start chart recorder. You may delete this yourself if you wish.", "Starting File Deletion Error", MessageBoxButtons.OK);
                }
                tmrEndFileChecking.Start();
                button_start_Click(sender, e);
            }
        }

        /// <summary>
        /// the timer to check if Bearingscan is ready for the chartRecorder program to stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrEndFileChecking_Tick(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + endFile))
            {
                try
                {
                    System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + startFile);
                }
                catch { }
                tmrEndFileChecking.Stop();
                try
                {
                    System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + endFile);
                }
                catch
                {
                    MessageBox.Show("There was an error deleting a temperary file to end chart recorder. You may delete this yourself if you wish.", "Ending File Deletion Error", MessageBoxButtons.OK);
                }
                tmrStartFileChecking.Start();
                button_stop_Click(sender, e);
            }
        }

        /// <summary>
        /// Closes the Uniwest ports when the form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StreamingAIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DEMOMODE)
            {
                try
                {
                    PortClose();
                }
                catch {}
            }
        }

        /// <summary>
        /// Closes the uniwest ports
        /// </summary>
        private void PortClose()
        {
            sr.Close();
            sw.Close();
            stream.Close();
        }

        /// <summary>
        /// deletes and extra files to start/stop the chart recorder
        /// </summary>
        private void DeleteOldStartStopFiles()
        {
            try
            {
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + startFile);
            }
            catch { }
            try
            {
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + endFile);
            }
            catch { }
        }

        /// <summary>
        /// calls the method to retrieve the current values until a mastercalibration is required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateMasterCal_Click(object sender, EventArgs e)
        {
            GetRemainingCalibrationReqs();
        }

        /// <summary>
        /// saves the password protected options in a binary file
        /// </summary>
        private void SavePrivateOptionsAsBinary()
        {
            string fileLoc = System.Windows.Forms.Application.StartupPath + "\\DefaultOptions.dat";
            using (var stream = System.IO.File.Open(fileLoc, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    writer.Write(txtTolerance.Text);
                    writer.Write(txt4Tolerance.Text);
                    writer.Write(txtPartsPerMaster.Text);
                    writer.Write(txtMinutesPerMaster.Text);
                }
            }
        }

        /// <summary>
        /// loads the passowrd protected options saved in a binary file
        /// </summary>
        private void LoadBinaryPrivateOptions()
        {
            string fileLoc = System.Windows.Forms.Application.StartupPath + "\\DefaultOptions.dat";
            string tmp;
            if (System.IO.File.Exists(fileLoc))
            {
                using (var stream = System.IO.File.Open(fileLoc, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8))
                    {
                        tmp = reader.ReadString();
                        txtTolerance.Text = 0.ToString();//tmp;
                        tmp = reader.ReadString();
                        txt4Tolerance.Text = 0.ToString();//tmp;
                        tmp = reader.ReadString();
                        txtPartsPerMaster.Text = tmp;
                        tmp = reader.ReadString();
                        txtMinutesPerMaster.Text = tmp;
                    }
                }
            }
        }

        /// <summary>
        /// used in debugging to save a file of all the voltages in the scan (currently unused)
        /// </summary>
        private void printVoltages()
        {
            string myfile = $"{txtBatchNo.Text}_voltage.txt";
            if (System.IO.File.Exists(myfile))
            {
                System.IO.File.Delete(myfile);
            }
            // Appending the given texts
            using (StreamWriter sw = System.IO.File.AppendText(myfile))
            {
                sw.WriteLine("Vertical:");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("High:");
                for (int i = 0; i < m_simpleGraph1.VoltagesHigh.Count; i++)
                {
                    sw.WriteLine(m_simpleGraph1.VoltagesHigh[i].ToString() + "       " + m_simpleGraph1.Times[i].ToString());
                }
                sw.WriteLine();

                if (m_simpleGraph1.VoltagesHigh.Count != 0) 
                { 
                    for (int i = 0; i < m_simpleGraph1.VoltagesHigh.Count; i++)
                    {
                        m_simpleGraph1.VoltagesHigh[i] = Math.Abs(m_simpleGraph1.VoltagesHigh[i]);
                    }
                    sw.WriteLine("MAX: " + Enumerable.Max(m_simpleGraph1.VoltagesHigh));
                }


                sw.WriteLine("Vertical");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Medium:");

                for (int i = 0; i < m_simpleGraph1.VoltagesMedium.Count; i++)
                {
                    sw.WriteLine(m_simpleGraph1.VoltagesMedium[i].ToString() + "       " + m_simpleGraph1.Times[i].ToString());
                }
                sw.WriteLine();

                if (m_simpleGraph1.VoltagesMedium.Count != 0)
                {
                    for (int i = 0; i < m_simpleGraph1.VoltagesMedium.Count; i++)
                    {
                        m_simpleGraph1.VoltagesMedium[i] = Math.Abs(m_simpleGraph1.VoltagesMedium[i]);
                    }
                    sw.WriteLine("MAX: " + Enumerable.Max(m_simpleGraph1.VoltagesMedium));
                }

                ////////////////////////////////////////////////////////////

                sw.WriteLine("Horizontal:");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("High:");
                for (int i = 0; i < m_simpleGraph2.VoltagesHigh.Count; i++)
                {
                    sw.WriteLine(m_simpleGraph2.VoltagesHigh[i].ToString() + "       " + m_simpleGraph2.Times[i].ToString());
                }
                sw.WriteLine();

                if (m_simpleGraph2.VoltagesHigh.Count != 0)
                {
                    for (int i = 0; i < m_simpleGraph2.VoltagesHigh.Count; i++)
                    {
                        m_simpleGraph2.VoltagesHigh[i] = Math.Abs(m_simpleGraph2.VoltagesHigh[i]);
                    }
                    sw.WriteLine("MAX: " + Enumerable.Max(m_simpleGraph2.VoltagesHigh));
                }


                sw.WriteLine("Horizontal");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Medium:");

                for (int i = 0; i < m_simpleGraph2.VoltagesMedium.Count; i++)
                {
                    sw.WriteLine(m_simpleGraph2.VoltagesMedium[i].ToString() + "       " + m_simpleGraph2.Times[i].ToString());
                }
                sw.WriteLine();

                if (m_simpleGraph2.VoltagesMedium.Count != 0)
                {
                    for (int i = 0; i < m_simpleGraph2.VoltagesMedium.Count; i++)
                    {
                        m_simpleGraph2.VoltagesMedium[i] = Math.Abs(m_simpleGraph2.VoltagesMedium[i]);
                    }
                    sw.WriteLine("MAX: " + Enumerable.Max(m_simpleGraph2.VoltagesMedium));
                }


                ///////////////////////////////////

                sw.WriteLine("Vector Sum:");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("High:");
                for (int i = 0; i < m_simpleGraph3.VoltagesHigh.Count; i++)
                {
                    sw.WriteLine(m_simpleGraph3.VoltagesHigh[i].ToString() + "       " + m_simpleGraph3.Times[i].ToString());
                }
                sw.WriteLine();

                if (m_simpleGraph3.VoltagesHigh.Count != 0)
                {
                    for (int i = 0; i < m_simpleGraph3.VoltagesHigh.Count; i++)
                    {
                        m_simpleGraph3.VoltagesHigh[i] = Math.Abs(m_simpleGraph3.VoltagesHigh[i]);
                    }
                    sw.WriteLine("MAX: " + Enumerable.Max(m_simpleGraph3.VoltagesHigh));
                }


                sw.WriteLine("Vector Sum");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Medium:");

                for (int i = 0; i < m_simpleGraph3.VoltagesMedium.Count; i++)
                {
                    sw.WriteLine(m_simpleGraph3.VoltagesMedium[i].ToString() + "       " + m_simpleGraph3.Times[i].ToString());
                }
                sw.WriteLine();

                if (m_simpleGraph3.VoltagesMedium.Count != 0)
                {
                    for (int i = 0; i < m_simpleGraph3.VoltagesMedium.Count; i++)
                    {
                        m_simpleGraph3.VoltagesMedium[i] = Math.Abs(m_simpleGraph3.VoltagesMedium[i]);
                    }
                    sw.WriteLine("MAX: " + Enumerable.Max(m_simpleGraph3.VoltagesMedium));
                }




                sw.Close();
            }

            //Process.Start(System.Windows.Forms.Application.StartupPath + $"\\{myfile}");
        }

        /// <summary>
        /// Adds an extra page to the report for a different graph's screenshots
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="xlWorkbook"></param>
        /// <param name="name"></param>
        private void addGraph(int sheetNum, Excel.Workbook xlWorkbook, string name)
        {
            xlWorkbook.Worksheets.Add(After: xlWorkbook.Sheets[xlWorkbook.Sheets.Count]);
            Excel.Worksheet sheet = xlWorkbook.Sheets[sheetNum];
            Excel.Range xlRange = sheet.UsedRange;
            sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            for (int i = 2; i <= 4; i++)
            {
                sheet.Columns[i].ColumnWidth = 35;
            }

            minorRevCounter = 0;
            String tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + $"_{name}_" + "(" + minorRevCounter.ToString() + ")" + ".Jpg";
            bool exit = false;
            //int tmpXPos = 45;
            //float tmpYPos = 166.15f;
            int tmpXPos = 2;
            int tmpYPos = 1;
            int tmpVSpot = 8;
            int flip = 1;
            while (!exit)
            {
                if (tmpVSpot == 1)
                {
                    sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, sheet.Cells[tmpYPos, 5].Left-1 - sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos+7, tmpXPos].Top - sheet.Cells[tmpYPos, tmpXPos].Top);
                }
                else
                {
                    sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, sheet.Cells[tmpYPos, 5].Left-1 - sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos+7, tmpXPos].Top - sheet.Cells[tmpYPos, tmpXPos].Top);//429, 221);
                }
                minorRevCounter++;
                tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + $"_{name}_" + "(" + minorRevCounter.ToString() + ")" + ".Jpg";
                if (!System.IO.File.Exists(tmp))
                {
                    tmp = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + txtBatchNo.Text + $"_{name}_" + "(" + minorRevCounter.ToString() + ") - Final" + ".Jpg";
                    exit = true;
                }
                //tmpYPos += 255;
                //tmpYPos += minorRevCounter % 2 == 1 ? 15 : 18;
                tmpYPos += 8;
                if (minorRevCounter % 4 == 2)
                    tmpYPos++;

                double percentOfGraph = timer._stopwatch.Elapsed.TotalSeconds / GetScanLength();
                if (!exitEarly && hasCycled)
                {
                    int tempNum = (int)(sheet.Cells[33, tmpXPos].Top - sheet.Cells[26, tmpXPos].Top); // this is here because it refused to work in the equation like a good little girl -cat
                    sheet.Shapes.AddPicture(tmp, MsoTriState.msoFalse, MsoTriState.msoCTrue, sheet.Cells[tmpYPos, tmpXPos].Left, sheet.Cells[tmpYPos, tmpXPos].Top + 1, ((sheet.Cells[tmpYPos, 5].Left - 1 - sheet.Cells[tmpYPos, tmpXPos].Left) * percentOfGraph), tempNum);
                }

                //sheet.Cells[tmpVSpot, 1].Value = "        +" + voltageType.ToString() + "V";
                //sheet.Cells[tmpVSpot + 3, 1].Value = "          0V";
                //sheet.Cells[tmpVSpot + 6, 1].Value = "        -" + voltageType.ToString() + "V";
                //sheet.Cells[tmpVSpot + 7, 2].Value = (mainTimeInterval * (minorRevCounter-1)).ToString() + " s";
                sheet.Cells[tmpVSpot, 3].Value = "Time (s)";
                sheet.Cells[tmpVSpot, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //sheet.Cells[tmpVSpot + 7, 4].Value = (mainTimeInterval * (minorRevCounter)).ToString() + "s";
                sheet.Cells[tmpVSpot, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //tmpVSpot += 17;
                //tmpVSpot += minorRevCounter % 2 == 1 ? 15 : 18;
                tmpVSpot += 8;
                if (minorRevCounter % 4 == 1)
                    tmpVSpot++;
                flip = minorRevCounter % 2 == 1 ? 7 : 7;
            }
            if (minorRevCounter > 1 || hasCycled)
            {
                sheet.Cells[tmpVSpot, 3].Value = "Time (s)";
                sheet.Cells[tmpVSpot, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                sheet.Cells[tmpVSpot, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            }
            //Fixes the time of the final screenshot
            int subtract = minorRevCounter % 2 == 1 ? 8 : 11;
            //Is minorRev-1 because without it, the final graph's times are off by 10 seconds
            /*if (minorRevCounter > 1)
            {
                sheet.Cells[tmpVSpot - subtract, 2].Value = decimal.Round((decimal)(((mainTimeInterval * (minorRevCounter - 1) * 1000) - (mainTimeInterval * 1000 - finalTmpTime)) / 1000), 2).ToString() + " s";
                sheet.Cells[tmpVSpot - subtract, 4].Value = decimal.Round((decimal)(((mainTimeInterval * (minorRevCounter - 1) * 1000) + finalTmpTime) / 1000), 2).ToString() + " s";
            }*/

        }

        /// <summary>
        /// Adds the selected volatge lists to the end of the excel file
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="xlWorkbook"></param>
        /// <param name="graph"></param>
        private void addVoltages(int sheetNum, Excel.Workbook xlWorkbook, SimpleGraph graph)
        {
            xlWorkbook.Worksheets.Add(After: xlWorkbook.Sheets[xlWorkbook.Sheets.Count]);
            Excel.Worksheet sheet = xlWorkbook.Sheets[sheetNum];
            Excel.Range xlRange = sheet.UsedRange;
            sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
            int tmpVSpot = 1;

            sheet.Cells[tmpVSpot, 1].Value = "High Voltages:";
            tmpVSpot++;
            List<Tuple<Double, Double>> sortedHigh = new List<Tuple<Double, Double>>();
            List<Tuple<Double, Double>> sortedMed = new List<Tuple<Double, Double>>();
            if (cbxSortVoltageByVal.Checked)
            {
                sortedHigh = graph.VoltTimeHigh.OrderByDescending(i => Math.Abs(i.Item1)).ToList();
                sortedMed = graph.VoltTimeMed.OrderByDescending(i => Math.Abs(i.Item1)).ToList();
            }

            if (graph.VoltagesHigh.Count != 0)
            {
                if (!cbxSortVoltageByVal.Checked)
                {
                    for (int i = 0; i < graph.VoltagesHigh.Count; i++)
                    {
                        sheet.Cells[tmpVSpot, 1].Value = graph.VoltagesHigh[i].ToString();
                        sheet.Cells[tmpVSpot, 3].Value = graph.Times[i].ToString();

                        tmpVSpot++;

                        graph.VoltagesHigh[i] = Math.Abs(graph.VoltagesHigh[i]);
                    }

                    sheet.Cells[tmpVSpot, 1].Value = ("MAX High: " + Enumerable.Max(graph.VoltagesHigh));
                    tmpVSpot += 4;
                }
                else
                {
                    for (int i = 0; i < graph.VoltagesHigh.Count; i++)
                    {
                        sheet.Cells[tmpVSpot, 1].Value = sortedHigh[i].Item1.ToString();
                        sheet.Cells[tmpVSpot, 3].Value = sortedHigh[i].Item2.ToString();
                        tmpVSpot++;
                        //graph.VoltagesHigh[i] = Math.Abs(graph.VoltagesHigh[i]);
                    }
                    tmpVSpot += 4;
                }
            }
            else //No  High Voltages
            {
                sheet.Cells[tmpVSpot, 1].Value = "No Voltages above the max threshold range";
            }
            if(graph.VoltagesMedium.Count != 0)
            {
                sheet.Cells[tmpVSpot, 1].Value = "Medium Voltages:";
                tmpVSpot++;

                if (!cbxSortVoltageByVal.Checked)
                {
                    for (int i = 0; i < graph.VoltagesMedium.Count; i++)
                    {
                        sheet.Cells[tmpVSpot, 1].Value = graph.VoltagesMedium[i].ToString();
                        sheet.Cells[tmpVSpot, 3].Value = graph.Times[i].ToString();

                        tmpVSpot++;

                        graph.VoltagesMedium[i] = Math.Abs(graph.VoltagesMedium[i]);
                    }

                    sheet.Cells[tmpVSpot, 1].Value = ("MAX Medium: " + Enumerable.Max(graph.VoltagesMedium));
                }
                else
                {
                    for (int i = 0; i < graph.VoltagesMedium.Count; i++)
                    {
                        sheet.Cells[tmpVSpot, 1].Value = sortedMed[i].Item1.ToString();
                        sheet.Cells[tmpVSpot, 3].Value = sortedMed[i].Item2.ToString();

                        tmpVSpot++;

                        //graph.VoltagesMedium[i] = Math.Abs(graph.VoltagesMedium[i]);
                    }

                    //sheet.Cells[tmpVSpot, 1].Value = ("MAX Medium: " + Enumerable.Max(graph.VoltagesMedium));
                }


                
            }
            else
            {
                sheet.Cells[tmpVSpot, 1].Value = "No Voltages in the medium threshold range";
            }
        }

        /// <summary>
        /// changes the currently selected part and requires the user to do another master calibration.
        /// Also loads the bearingScan scan plan, part info page, and technique document for the part
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbxPartType_SelectedValueChanged(object sender, EventArgs e)
        {
            /*if(lastSelectedPart == "") //First Part Selected of the program being opened
            {
                lastSelectedPart = cmbxPartNo.Text;
            }
            else */if(lastSelectedPart == cmbxPartNo.Text) //The same part being selected
            {
                return;
            }
            else //A new part being selected
            {
                /*/string tmpMsg = "Warning. Changing Part Numbers will require running a new calibration before any parts can be scanned. Do you want to continue?";
                DialogResult result = MessageBox.Show(tmpMsg, "Changing Part Numbers", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    cmbxPartNo.Text = lastSelectedPart;
                    //secondPartNoPass = true;
                    return;
                }*/
                //else
                //{
                    lastSelectedPart = cmbxPartNo.Text;
                    GenerateEmptyMasterCalibration();
                //}
            }
            if(cbxAmpInch.Checked)
            redrawManualGraph();
            txt4Tolerance.Text = allParts[cmbxPartNo.SelectedIndex].notch2Volt.ToString();
            txtTolerance.Text = allParts[cmbxPartNo.SelectedIndex].notch1Volt.ToString();

            if (!DEMOMODE && cbxLoadUniwest.Checked)
            {
                try
                {
                    selectUWFile();
                }
                catch
                {
                    BtnReconnectUniWest_Click(sender, e);
                    try
                    {
                        selectUWFile();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error loading the UniWest configuration settings. Please ensure the UniWest is connected and that no menus are currently open before trying again", "UniWest Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if(chkLoadMSPlan.Checked)
                SendScanPlanInformation(allParts[cmbxPartNo.SelectedIndex].scanPlanName, allParts[cmbxPartNo.SelectedIndex].scanPlanType);

            showPart();
        }

        /// <summary>
        /// clears and resets the graphs, i think
        /// </summary>
        private void redrawManualGraph()
        {
            m_simpleGraph1.isManual = true;
            m_simpleGraph2.isManual = true;
            m_simpleGraph3.isManual = true;


            //var atFour = Double.Parse(txtTolerance.Text)*2;//allParts[cmbxPartNo.SelectedIndex].notch4;
            var notch2 = allParts[cmbxPartNo.SelectedIndex].notch2Volt;
            var notch1 = allParts[cmbxPartNo.SelectedIndex].notch1Volt;
            Double YCordDividedRate = 1.0 * (pictureBox.Height - 1) / (voltageType * 2);

            m_simpleGraph1.fourThough = notch2 * YCordDividedRate;
            m_simpleGraph2.fourThough = notch2 * YCordDividedRate;
            m_simpleGraph3.fourThough = notch2 * YCordDividedRate;

            m_simpleGraph1.notch2Loc = allParts[cmbxPartNo.SelectedIndex].notch2;
            m_simpleGraph2.notch2Loc = allParts[cmbxPartNo.SelectedIndex].notch2;
            m_simpleGraph3.notch2Loc = allParts[cmbxPartNo.SelectedIndex].notch2;

            m_simpleGraph1.twoThough = notch1 * YCordDividedRate;
            m_simpleGraph2.twoThough = notch1 * YCordDividedRate;
            m_simpleGraph3.twoThough = notch1 * YCordDividedRate;

            m_simpleGraph1.notch1Loc = allParts[cmbxPartNo.SelectedIndex].notch1;
            m_simpleGraph2.notch1Loc = allParts[cmbxPartNo.SelectedIndex].notch1;
            m_simpleGraph3.notch1Loc = allParts[cmbxPartNo.SelectedIndex].notch1;


            m_simpleGraph1.Clear();
            m_simpleGraph2.Clear();
            m_simpleGraph3.Clear();
        }

        /// <summary>
        /// Launches the add new part form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPart_Click(object sender, EventArgs e)
        {
            if (newPartForm == null)
            {
                newPartForm = new NewPart(this);
                newPartForm.ShowDialog();
            }
        }

        /// <summary>
        /// loads the part data file if it exists, otherwise creates a default file
        /// </summary>
        private void getPartdata()
        {
            string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "partData.data";

            string fileNameB = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "partData-BACKUP.data";

            if (!System.IO.File.Exists(fileName))
            {
                allParts = new List<Parts>();
                allParts.Add(new Parts
                {
                    partNo = "17984R0",
                    probeNo = "00000",
                    notch1 = .002,
                    notch1Volt = 5.7/2,
                    notch2 = .004,
                    notch2Volt = 5.7,
                    scanPlanName = "testScanPlan1",
                    scanPlanType = 1,
                    uniwestSetupName = "17984r0",
                    techniqueFile = ""
                    //partImage = null,
                    //referenceStandardImage = null
                });

                allParts.Add(new Parts
                {
                    partNo = "18800R01",
                    probeNo = "00000",
                    notch1 = .002,
                    notch1Volt = 5.7 / 2,
                    notch2 = .004,
                    notch2Volt = 5.7,
                    scanPlanName = "testScanPlan2",
                    scanPlanType = 1,
                    uniwestSetupName = "18800r01",
                    techniqueFile = ""
                    //partImage = null,
                    //referenceStandardImage = null
                });

                allParts.Add(new Parts
                {
                    partNo = "19965R11",
                    probeNo = "00000",
                    notch1 = .002,
                    notch1Volt = 5.7 / 2,
                    notch2 = .004,
                    notch2Volt = 5.7,
                    scanPlanName = "testScanPlan3",
                    scanPlanType = 1,
                    uniwestSetupName = "19965r11",
                    techniqueFile = ""
                    //partImage = null,
                    //referenceStandardImage = null
                });

                allParts.Add(new Parts
                {
                    partNo = "19842R01",
                    probeNo = "00000",
                    notch1 = .002,
                    notch1Volt = 5.7 / 2,
                    notch2 = .004,
                    notch2Volt = 5.7,
                    scanPlanName = "testScanPlan4",
                    scanPlanType = 1,
                    uniwestSetupName = "19842r01",
                    techniqueFile = ""
                    //partImage = null,
                    //referenceStandardImage = null
                });

                FileInfo fi = new FileInfo(fileName);
                if (!fi.Directory.Exists)
                {
                    System.IO.Directory.CreateDirectory(fi.DirectoryName);
                }

                using (StreamWriter sw = System.IO.File.AppendText(fileName))
                {
                    foreach (var part in allParts)
                    {
                        sw.WriteLine(part.partNo);
                        sw.WriteLine(part.probeNo);
                        sw.WriteLine(part.notch1);
                        sw.WriteLine(part.notch1Volt);
                        sw.WriteLine(part.notch2);
                        sw.WriteLine(part.notch2Volt);
                        sw.WriteLine(part.scanPlanName);
                        sw.WriteLine(part.scanPlanType);                     
                        sw.WriteLine(part.uniwestSetupName);
                        sw.WriteLine(part.techniqueFile);
                        sw.WriteLine();
                    }
                }
            }
            else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        allParts = new List<Parts>();
                        while (!sr.EndOfStream)
                        {
                            allParts.Add(new Parts
                            {
                                partNo = sr.ReadLine(),
                                probeNo = sr.ReadLine(),
                                notch1 = Double.Parse(sr.ReadLine()),
                                notch1Volt = Double.Parse(sr.ReadLine()),
                                notch2 = Double.Parse(sr.ReadLine()),
                                notch2Volt = Double.Parse(sr.ReadLine()),
                                scanPlanName = sr.ReadLine(),
                                scanPlanType = int.Parse(sr.ReadLine()),
                                uniwestSetupName = sr.ReadLine(),
                                techniqueFile = sr.ReadLine(),
                                //partImage = null,
                                //referenceStandardImage = null
                            });
                            sr.ReadLine();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("It seems that partData is either empty or not formatted properly. \n\nPlease check the file contents and delete the file if you would like to start from the template.", "Load Part Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                


            }

            foreach (var part in allParts)
            {
                cmbxPartNo.Items.Add(part.partNo);
            }
        }
		
        /// <summary>
        /// reloads the part list
        /// </summary>
		public void partListReload()
		{
            string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "partData.data";
            string fileNameB = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "partData-BACKUP.data";
            //This all needs to be tested on monday
            using (StreamWriter sw = System.IO.File.CreateText(fileName))
                {
                    foreach (var part in allParts)
                    {
                        sw.WriteLine(part.partNo);
                        sw.WriteLine(part.probeNo);
                        sw.WriteLine(part.notch1);
                        sw.WriteLine(part.notch1Volt);
                        sw.WriteLine(part.notch2);
                        sw.WriteLine(part.notch2Volt);
                        sw.WriteLine(part.scanPlanName);
                        sw.WriteLine(part.scanPlanType);
                        sw.WriteLine(part.uniwestSetupName);
                        sw.WriteLine(part.techniqueFile);
                    sw.WriteLine();
                    }
                }

            FileInfo fiB = new FileInfo(fileNameB);
            if (!fiB.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fiB.DirectoryName);
            }

            using (StreamWriter sw = System.IO.File.CreateText(fileNameB))
            {
                foreach (var part in allParts)
                {
                    sw.WriteLine(part.partNo);
                    sw.WriteLine(part.probeNo);
                    sw.WriteLine(part.notch1);
                    sw.WriteLine(part.notch1Volt);
                    sw.WriteLine(part.notch2);
                    sw.WriteLine(part.notch2Volt);
                    sw.WriteLine(part.scanPlanName);
                    sw.WriteLine(part.scanPlanType);
                    sw.WriteLine(part.uniwestSetupName);
                    sw.WriteLine(part.techniqueFile);
                    sw.WriteLine();
                }
            }

            cmbxPartNo.Items.Add(allParts[allParts.Count - 1].partNo);
            
		}

        /// <summary>
        /// when the voltage is changed in the password protected area, this is called to update them internally
        /// </summary>
        private void editVoltages()
        {
            allParts[cmbxPartNo.SelectedIndex].notch1Volt = Double.Parse(txtTolerance.Text);
            allParts[cmbxPartNo.SelectedIndex].notch2Volt = Double.Parse(txt4Tolerance.Text);
        }

        /// <summary>
        /// Used to rewrite the partList file with the most recent data
        /// </summary>
        private void partListRewrite()
        {
            string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "partData.data";
            string fileNameB = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "partData-BACKUP.data";

            FileInfo fi = new FileInfo(fileName);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }

            using (StreamWriter sw = System.IO.File.CreateText(fileName))
            {
                foreach (var part in allParts)
                {
                    sw.WriteLine(part.partNo);
                    sw.WriteLine(part.probeNo);
                    sw.WriteLine(part.notch1);
                    sw.WriteLine(part.notch1Volt);
                    sw.WriteLine(part.notch2);
                    sw.WriteLine(part.notch2Volt);
                    sw.WriteLine(part.scanPlanName);
                    sw.WriteLine(part.scanPlanType);                     
                    sw.WriteLine(part.uniwestSetupName);
                    sw.WriteLine(part.techniqueFile);
                    sw.WriteLine();
                }
            }

            FileInfo fiB = new FileInfo(fileNameB);
            if (!fiB.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fiB.DirectoryName);
            }

            using (StreamWriter sw = System.IO.File.CreateText(fileNameB))
            {
                foreach (var part in allParts)
                {
                    sw.WriteLine(part.partNo);
                    sw.WriteLine(part.probeNo);
                    sw.WriteLine(part.notch1);
                    sw.WriteLine(part.notch1Volt);
                    sw.WriteLine(part.notch2);
                    sw.WriteLine(part.notch2Volt);
                    sw.WriteLine(part.scanPlanName);
                    sw.WriteLine(part.scanPlanType);
                    sw.WriteLine(part.uniwestSetupName);
                    sw.WriteLine(part.techniqueFile);
                    sw.WriteLine();
                }
            }
        }

        /// <summary>
        /// Attempts to load the part profile on the UniWest
        /// </summary>
        private void selectUWFile()
        {
            string fileName;
            try
            {
                fileName = allParts[cmbxPartNo.SelectedIndex].uniwestSetupName;
            }
            catch
            {
                MessageBox.Show("There was an issue setting the gain. Please check the set value and try again.", "Set Gain Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SendCommand(stream, "-LOAD S " + fileName);
            string tmp = ReadResponse(stream);
            if(tmp =="")
            {
                MessageBox.Show("Status: Failed", "Loaded File", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Status: " + tmp, "Loaded File", MessageBoxButtons.OK);
            }
            
        }

        /// <summary>
        /// returns the amount of time has elapsed on the stopwatch
        /// </summary>
        /// <returns></returns>
        public Double getTimer()
        {
            return timer._stopwatch.Elapsed.TotalSeconds + (timer._initialInterval / 1000) * timer.laps;
        }

        /// <summary>
        /// Toggles the graphs to have the red lines for the tolerance values and redraws the graphs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAmpInch_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAmpInch.Checked)
            {
                if (cmbxPartNo.SelectedIndex != -1) 
                {
                    redrawManualGraph();
                }
            }
            else
            {
                m_simpleGraph1.isManual = false;
                m_simpleGraph2.isManual = false;
                m_simpleGraph3.isManual = false;

                m_simpleGraph1.fourThough = 0;
                m_simpleGraph2.fourThough = 0;
                m_simpleGraph3.fourThough = 0;
                m_simpleGraph1.twoThough = 0;
                m_simpleGraph2.twoThough = 0; 
                m_simpleGraph3.twoThough = 0;

                m_simpleGraph1.Clear();
                m_simpleGraph2.Clear();
                m_simpleGraph3.Clear();
            }
        }

        /// <summary>
        /// Toggles the form between showing all 3 graphs and only 1 selected graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTripleGraph_CheckedChanged(object sender, EventArgs e)
        {
            threeGraphSwitch();
        }

        private void threeGraphSwitch()
        {
            m_simpleGraph1.Clear();
            m_simpleGraph2.Clear();
            m_simpleGraph3.Clear();
            ShowAllThreeGraphs(chkTripleGraph.Checked);
            grpPrimaryGraph.Visible = chkTripleGraph.Checked;
            lblMiddleGraph.Visible = chkTripleGraph.Checked;
            lblBottomGraph.Visible = chkTripleGraph.Checked;
        }

        /// <summary>
        /// does a lot of the resizing and moving of the graphs
        /// </summary>
        /// <param name="state"></param>
        private void ShowAllThreeGraphs(bool state)
        {
            /*pictureBox2.Visible = state;
            pictureBox1.Visible = state;
            label_XCoordinateMax2.Visible = state;
            label_XCoordinateMax3.Visible = state;
            label_XCoordinateMin2.Visible = state;
            label_XCoordinateMin3.Visible = state;
            label_YCoordinateMax2.Visible = state;
            label_YCoordinateMax3.Visible = state;
            label_YCoordinateMiddle2.Visible = state;
            label_YCoordinateMiddle3.Visible = state;
            label_YCoordinateMin2.Visible = state;
            label_YCoordinateMin3.Visible = state;
            cbxDistance.Visible = false;
            cbxHorizontal.Visible = false;
            cbxVertical.Visible = false;
            if (state)
            {
                grpMainControls.Location = new System.Drawing.Point(35, 1014);
                this.Size = new Size(2396, 1300);
            }
            else
            {
                grpMainControls.Location = new System.Drawing.Point(35, 366);
                this.Size = new Size(2396, 675);
                cbxVertical.Checked = true;
                cbxHorizontal.Checked = false;
                cbxDistance.Checked = false;
            }*/

            //3 Graphs Shown
            pictureBox.Location = new System.Drawing.Point(56, 33);
            pictureBox1.Location = new System.Drawing.Point(56, 366);
            pictureBox2.Location = new System.Drawing.Point(56, 700);
            if (state)
            {
                pictureBox.Visible = state;
                pictureBox2.Visible = state;
                pictureBox1.Visible = state;
                //label_XCoordinateMax2.Visible = state;
                //label_XCoordinateMax3.Visible = state;
                //label_XCoordinateMin2.Visible = state;
                //label_XCoordinateMin3.Visible = state;
                //label_YCoordinateMax2.Visible = state;
                //label_YCoordinateMax3.Visible = state;
                //label_YCoordinateMiddle2.Visible = state;
                //label_YCoordinateMiddle3.Visible = state;
                //label_YCoordinateMin2.Visible = state;
                //label_YCoordinateMin3.Visible = state;
                lblTopGraph.Text = "Vertical";
                cbxDistance.Visible = true;
                cbxHorizontal.Visible = true;
                cbxVertical.Visible = true;
                radVerticalOpt.Visible = false;
                radHorizOpt.Visible = false;
                radDiffOpt.Visible = false;
                grpDisplayedGraphs.Visible = false;
                grpMainControls.Location = new System.Drawing.Point(35, 1014);
                grpMasterCalibResults.Location = new System.Drawing.Point(1770, 1113);
                this.Size = new Size(2396, 1300);
            }
            else //Only 1 Graph Displayed
            {
                radVerticalOpt.Visible = true;
                radHorizOpt.Visible = true;
                radDiffOpt.Visible = true;
                cbxDistance.Visible = false;
                cbxHorizontal.Visible = false;
                cbxVertical.Visible = false;
                grpDisplayedGraphs.Visible = true;
                grpMainControls.Location = new System.Drawing.Point(35, 366);
                grpMasterCalibResults.Location = new System.Drawing.Point(1770, 366);
                this.Size = new Size(2396, 675);
                if (radVerticalOpt.Checked) //Graph 1
                {
                    lblTopGraph.Text = "Vertical";
                    cbxVertical.Checked = true;
                    cbxHorizontal.Checked = false;
                    cbxDistance.Checked = false;
                    pictureBox.Visible = true;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                }
                else if(radHorizOpt.Checked) //Graph 2
                {
                    lblTopGraph.Text = "Horizontal";
                    cbxVertical.Checked = false;
                    cbxHorizontal.Checked = true;
                    cbxDistance.Checked = false;
                    pictureBox.Visible = false;
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                    pictureBox1.Location = new System.Drawing.Point(56, 33);
                }
                else //Differential - Graph 3
                {
                    lblTopGraph.Text = "Vector Sum";
                    cbxVertical.Checked = false;
                    cbxHorizontal.Checked = false;
                    cbxDistance.Checked = true;
                    pictureBox.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = true;
                    pictureBox2.Location = new System.Drawing.Point(56, 33);
                }
            }
        }

        private void BtnNullUniWest_Click(object sender, EventArgs e)
        {
            try
            {
                NullUni();
            }
            catch
            {
                BtnReconnectUniWest_Click(sender, e);
                try
                {
                    NullUni();
                }
                catch
                {
                    MessageBox.Show("There was an error retrieving the UniWest values. Please ensure the UniWest is connected and that no menus are currently open before trying again", "UniWest Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NullUni()
        {
            SendCommand(stream, "-NULL");
            string tmp = ReadResponse(stream);
        }

        private void chkMasterPart_CheckedChanged(object sender, EventArgs e)
        {
            txtSN.Enabled = !chkMasterPart.Checked;
            if(chkMasterPart.Checked)
            {
                chkGenReport.Checked = true;
            }
        }

        private void radVerticalOpt_CheckedChanged(object sender, EventArgs e)
        {
            ShowAllThreeGraphs(chkTripleGraph.Checked);
        }

        private void radHorizOpt_CheckedChanged(object sender, EventArgs e)
        {
            ShowAllThreeGraphs(chkTripleGraph.Checked);
        }

        private void radDiffOpt_CheckedChanged(object sender, EventArgs e)
        {
            ShowAllThreeGraphs(chkTripleGraph.Checked);
        }

        private void chkGenReport_CheckedChanged(object sender, EventArgs e)
        {
            if(chkMasterPart.Checked && !chkGenReport.Checked)
            {
                chkGenReport.Checked = true;
                MessageBox.Show("When running a master calibration part, a report must be generated. To disable the report, please untoggle the \"Is Master Part\".", "Master Part Calibration Error", MessageBoxButtons.OK);
                return;
            }

            if (chkGenReport.Checked) 
            { 
                cbxVoltages.Enabled = true;
                cbxVoltageV.Enabled = true;
                cbxVoltageH.Enabled = true;
                cbxVoltageD.Enabled = true;
                voltagesCheckChanged();
            }
            else
            {
                cbxVoltages.Enabled = false;
                cbxVoltageV.Enabled = false;
                cbxVoltageH.Enabled = false;
                cbxVoltageD.Enabled = false;
            }

        }

        private void cbxVoltages_CheckedChanged(object sender, EventArgs e)
        {
            voltagesCheckChanged();
        }

        private void voltagesCheckChanged()
        {
            if (cbxVoltages.Checked)
            {
                cbxVoltageV.Enabled = true;
                cbxVoltageH.Enabled = true;
                cbxVoltageD.Enabled = true;
            }
            else
            {
                cbxVoltageV.Enabled = false;
                cbxVoltageH.Enabled = false;
                cbxVoltageD.Enabled = false;
            }
        }

        private void BtnClearGraphs_Click(object sender, EventArgs e)
        {
            m_simpleGraph1.Clear();
            m_simpleGraph2.Clear();
            m_simpleGraph3.Clear();
        }

        private void lblBiPolarOrUniPolar_DoubleClick(object sender, EventArgs e)
        {
            if (uniPolarData)
            {
                uniPolarData = false;
                m_simpleGraph1.isUnipolar = false;
                m_simpleGraph2.isUnipolar = false;
                m_simpleGraph3.isUnipolar = false;
                lblBiPolarOrUniPolar.BackColor = Color.MediumSlateBlue;
                lblBiPolarOrUniPolar.Text = "Data: BiPolar";
            }
            else
            {
                uniPolarData = true;
                m_simpleGraph1.isUnipolar = true;
                m_simpleGraph2.isUnipolar = true;
                m_simpleGraph3.isUnipolar = true;
                lblBiPolarOrUniPolar.BackColor = Color.DarkOrange;
                lblBiPolarOrUniPolar.Text = "Data: UniPolar";
            }

            ConfigureGraph();
        }

        //Creates and sends a file for masterScan to view to get the scan plan name and type
        //ProfileScan = 1, TT Scan = 0
        private void SendScanPlanInformation(string planName, int planType)
        {
            //string fileName = System.Windows.Forms.Application.StartupPath + "\\"+ planName;        //"\\MyScanPlan.txt";
            string fileName = System.Windows.Forms.Application.StartupPath + "\\MyScanPlan.txt";
            if (planType == 1)
                planName += ".IPR";
            else
                planName += ".2TT";
            using (StreamWriter sw = System.IO.File.CreateText(fileName))
            {
                sw.WriteLine(planName);
                sw.WriteLine(planType.ToString());
                sw.Close();
            }
            //MessageBox.Show()
        }

        //TODO - automate this later
        private void BtnReconnectUniWest_Click(object sender, EventArgs e)
        {
            try
            {
                PortClose();
            }
            catch
            {

            }
            try
            {
                PortSetup();
                //selectUWFile();
            }
            catch
            {
                MessageBox.Show("There was an error reconnecting to the UniWest. Please check the connection and try again.","Uniwest Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangePrimaryGraph(object sender, EventArgs e)
        {
            if(radPrimaryVertT.Checked)
            {
                if (!cbxVertical.Checked)
                {
                    cbxVertical.Checked = true;
                }
            }
            else if(radPrimaryHorizT.Checked)
            {
                if (!cbxHorizontal.Checked)
                {
                    cbxHorizontal.Checked = true;
                }
            }
            else //Vector Sum
            {
                if (!cbxDistance.Checked)
                {
                    cbxDistance.Checked = true;
                }
            }
        }

        private void btnCalibrationAccept_Click(object sender, EventArgs e)
        {
            masterUserChoice = 1;
            secondMasterPass = true;
            GenerateFreshMasterCalibration();
            ExportToExcel();
            GetRemainingCalibrationReqs();
            button_start.Enabled = true;
            chkMasterPart.Enabled = true;
            grpMasterCalibResults.Visible = false;
        }

        private void btnCalibrationReject_Click(object sender, EventArgs e)
        {
            masterUserChoice = 0;
            secondMasterPass = true;
            ExportToExcel();
            button_start.Enabled = true;
            chkMasterPart.Enabled = true;
            grpMasterCalibResults.Visible = false;
        }

        private void btnEditPartInfo_Click(object sender, EventArgs e)
        {
            if(partForm !=null)
            {
                partForm.Close();
            }
            if (newPartForm == null)
            {
                newPartForm = new NewPart(this,cmbxPartNo.SelectedIndex, false);
                newPartForm.ShowDialog();
            }
        }

        public void donePressed(int caseType, bool partEdit = false)
        {
            TogglePasswordOptions(false);
            SavePrivateOptionsAsBinary();
            if(caseType == 1)
                editVoltages();
            if (cbxAmpInch.Checked)
                redrawManualGraph();
            partListRewrite();

            try
            {
                if (cmbxPartNo.SelectedIndex == -1 || !partEdit)
                {
                    cmbxPartNo.SelectedIndex = allParts.Count - 1;

                    txt4Tolerance.Text = allParts[cmbxPartNo.SelectedIndex].notch2Volt.ToString();
                    txtTolerance.Text = allParts[cmbxPartNo.SelectedIndex].notch1Volt.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Failed to reload to most recent part.", "Reload Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void btnAbt_Click(object sender, EventArgs e)
        {
            About abtForm = new About();
            abtForm.ShowDialog();
        }

        private void btnPartInfo_Click(object sender, EventArgs e)
        {
            showPart();
        }

        //Clears all the JPeg Images in the reports folder prior to a scan to hopefully help reduce errors
        private void ClearAllImageFilesInReports()
        {
            string reportsDir = System.Windows.Forms.Application.StartupPath + "\\Reports";
            string[] files = Directory.GetFiles(reportsDir);
            foreach (string file in files)
            {
                if(file.EndsWith(".Jpg"))
                {
                    System.IO.File.Delete(file);
                }
            }
        }

        private void showPart()
        {
            if(partForm != null)
            {
                partForm.Close();
            }

            partForm = new ViewPart(this, cmbxPartNo.SelectedIndex);
            partForm.Show();
            try
            {
                if (allParts[cmbxPartNo.SelectedIndex].techniqueFile != "" && allParts[cmbxPartNo.SelectedIndex].techniqueFile != "None")
                    System.Diagnostics.Process.Start(allParts[cmbxPartNo.SelectedIndex].techniqueFile);
            }
            catch
            {
                MessageBox.Show("There was an error loading the technique file. Please make sure it hasn't been moved or removed from the system.\n\n If the file has been moved, please update its location in the parts settings.","Technique Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void deletePart()
        {
            allParts.RemoveAt(cmbxPartNo.SelectedIndex);
            partListRewrite();
            cmbxPartNo.Items.Clear();
            foreach (var part in allParts)
            {
                cmbxPartNo.Items.Add(part.partNo);
            }
            try
            {
                cmbxPartNo.SelectedIndex = 0;
            }
            catch
            {
                cmbxPartNo.SelectedIndex = -1;
            }

        }

        public bool IsDuplicatePart(string partNo)
        {
            if(cmbxPartNo.FindStringExact(partNo) == -1)
                return false;
            return true;
        }

    }

}