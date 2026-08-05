using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Automation.BDaq;
using System.Diagnostics;
using AI_StreamingAI;

enum TimeUnit { Nanosecond, Microsecond, Millisecond, Second };
enum FrequencyUnit { Hz, KHz, MHz };
enum OverOneScreenMode { BeginScreen, EndScreen }

class SimpleGraph
{
    #region fields
    public Bitmap m_bitmap;
    Graphics m_graphics;
    Size m_size;
    Control m_control;

    Double m_YCordRangeMin;
    Double m_YCordRangeMax;

    Double m_xIncByTime;
    Double[] m_drawDataBuffer;
    PointF[][] m_dataPointBuffer;

    Double m_XCordDividedRate;
    Double m_XCordTimeDiv;
    Double m_XCordTimeOffset;

    Double m_shiftCount;
    int dataPointsPerPixel = 30; //If Changed here, also change the variable with the same name at the top of StreamingAI
    bool m_recordEndFlag;
    bool m_drawRecordEnd;
    int m_recordEndPointX;
    int m_xPosition;
    public Double ShiftCount
    {
        get { return m_shiftCount; }
        set { m_shiftCount = value; }
    }
    int m_plotCount;
    int m_dataCountCachePerPlot;
    int m_mapDataIndexPerPlot = 0;
    int m_pointCountPerScreen;
    int m_dataCountPerPlot = 0;

    OverOneScreenMode m_overOneScreenMode;
    static Pen[] m_pens = new Pen[]{
System.Drawing.Pens.MidnightBlue/*Red*/,        System.Drawing.Pens.MediumVioletRed/*DarkViolet*/,     System.Drawing.Pens.Maroon/*GreenYellow*/,  System.Drawing.Pens.Salmon,
System.Drawing.Pens.SkyBlue,    System.Drawing.Pens.SteelBlue,      System.Drawing.Pens.DarkSeaGreen, System.Drawing.Pens.LightGreen,
System.Drawing.Pens.NavajoWhite,System.Drawing.Pens.OrangeRed,      System.Drawing.Pens.DeepPink,     System.Drawing.Pens.MediumOrchid,
System.Drawing.Pens.LightGray,  System.Drawing.Pens.MediumVioletRed,System.Drawing.Pens.MistyRose,    System.Drawing.Pens.PowderBlue};
    object LockObject = new object();

    //New fields
    public Double highPass;
    public Double lowPass;

    int channelNum;

    public Double tolerance2;
    public Double tolerance4;

    public List<Double> VoltagesHigh;
    public List<Double> VoltagesMedium;
    public List<Double> Times;

    public bool isManual;

    public Double fourThough;
    public Double twoThough;

    public PausableTimer timer;

    StreamingAIForm mainForm;

    public TestResult result = TestResult.NA; 

    public bool isUnipolar;

    public float Voltage;

    public int lineCount = 0;
    int setCount = 0;
    bool isMoving = false;
    double pixelMove = 0;

    public Double notch1Loc = 0;
    public Double notch2Loc = 0;

    public List<Tuple<Double, Double>> VoltTimeHigh;
    public List<Tuple<Double, Double>> VoltTimeMed;

    public int sectionCount;
    public enum TestResult
    {
        ACCEPT,
        REJECT,
        RETEST,
        NA
    }

    #endregion

    #region ctor and dtor
    /// <summary>
    /// Initialization of the simplegraph
    /// </summary>
    /// <param name="bmSize"></param> Size of the graph (thats actually moving and displaying the voltage values
    /// <param name="control"></param> Picture box to display to
    /// <param name="channelNum"></param> Channel number of voltage reading from the advantech board
    /// <param name="mainForm"></param> The mainform
    public SimpleGraph(Size bmSize, Control control, int channelNum, StreamingAIForm mainForm)
    {
        m_control = control;
        m_size = bmSize;
        m_bitmap = new Bitmap(bmSize.Width+40, bmSize.Height+17);
        m_overOneScreenMode = OverOneScreenMode.BeginScreen;
        m_control.Paint += new PaintEventHandler(ControlOnPaint);
        this.channelNum = channelNum;
        this.mainForm = mainForm;

    }

    #endregion

    #region properties

    public Double XCordTimeDiv
    {
        get
        {
            return m_XCordTimeDiv;
        }
        set
        {
            m_XCordTimeDiv = value;
            Div(m_XCordTimeDiv);
        }
    }

    public Double XCordTimeOffset
    {
        get
        {
            return m_XCordTimeOffset;
        }
        set
        {
            m_XCordTimeOffset = value;
            Shift(m_XCordTimeOffset);
        }
    }

    public Double YCordRangeMin
    {
        get
        {
            return m_YCordRangeMin;
        }
        set
        {
            m_YCordRangeMin = value;
        }
    }

    public Double YCordRangeMax
    {
        get
        {
            return m_YCordRangeMax;
        }
        set
        {
            m_YCordRangeMax = value;
        }
    }

    public Pen[] Pens
    {
        get
        {
            return m_pens;
        }
    }

    public OverOneScreenMode OverOneScreenMode
    {
        get
        {
            return m_overOneScreenMode;
        }
        set
        {
            m_overOneScreenMode = value;
        }
    }
    #endregion

    #region methods

    /// <summary>
    /// Clears the graphs visually and any unseen variables that need to be reset before next scan 
    /// </summary>
    public void Clear()
    {
        m_xIncByTime = 0;
        m_plotCount = 0;
        m_mapDataIndexPerPlot = 0;
        m_pointCountPerScreen = 0;
        m_dataCountPerPlot = 0;
        m_dataCountCachePerPlot = 0;
        m_shiftCount = 0;
        m_recordEndFlag = false;
        m_xPosition = 0;
        m_drawRecordEnd = false;
        m_control.Invalidate();
        lineCount = 0;
        setCount = 0;
        isMoving = false;
    }

    /// <summary>
    /// Changes the time offset 
    /// </summary>
    /// <param name="shiftTime"></param>
    public void Shift(double shiftTime)
    {
        m_mapDataIndexPerPlot = 0;
        m_XCordTimeOffset = shiftTime;
        Draw();
    }

    /// <summary>
    /// Changes the div value based on what the use has set the time to
    /// </summary>
    /// <param name="DivValue"></param>
    public void Div(double DivValue)
    {
        m_XCordTimeDiv = DivValue;
        Draw();
    }

    /// <summary>
    /// Redraws graph with updated values 
    /// </summary>
    private void Draw()
    {
        CalcDrawParams(m_xIncByTime);
        MapDataPoints();
        m_control.Invalidate();
    }

    /// <summary>
    /// Initial Chart call from the main form
    /// </summary>
    /// <param name="data"></param> The data of all of the channels in the order [x1,y1,z1,x2,y2,z2...]
    /// <param name="plotCount"></param> The channel this data is for 
    /// <param name="dataCountPerPlot"></param> Total data/ total channel count
    /// <param name="xIncBySec"></param> 1.0 / clockrate
    public void Chart(Double[] data, int plotCount, int dataCountPerPlot, double xIncBySec)
    {
        Chart(data, plotCount, dataCountPerPlot, xIncBySec, false);
    }

    //The function Chart previously has four parameters changing to five parameters now.  
    //The fifth parameter recordEndFlag used to determine whether or not draw a mark line when the returned value being WarningRecordEnd.
    //Meanwhile, the previous version four parameters function is still available. 
    public void Chart(Double[] data, int plotCount, int dataCountPerPlot, double xIncBySec, bool recordEndFlag)
    {
        m_recordEndFlag = recordEndFlag;
        m_xIncByTime = xIncBySec;
        m_dataCountPerPlot = dataCountPerPlot;
        //to be fit for variational for plotCount
        if (null == m_drawDataBuffer || plotCount != m_plotCount)
        {
            m_drawDataBuffer = new double[plotCount * (m_size.Width * dataPointsPerPixel + 1)];///HERE
            m_dataPointBuffer = new PointF[plotCount][];
            for (int i = 0; i < plotCount; i++)
            {
                m_dataPointBuffer[i] = new PointF[m_size.Width * dataPointsPerPixel + 1]; ///HERE
            }
            m_dataCountCachePerPlot = 0;
            m_plotCount = plotCount;
        }

        CalcDrawParams(xIncBySec);
        SaveData(data, m_plotCount, dataCountPerPlot);
        MapDataPoints();
        m_control.Invalidate(); // this is the command that causes the picturebox to be repainted
    }

    /// <summary>
    /// Calculates the rest of the parameters so the graph will be drawn properly
    /// </summary>
    /// <param name="XIncBySec"></param> 1.0/clockrate
    private void CalcDrawParams(double XIncBySec)
    {
        if (XIncBySec < double.Epsilon)
        {
            return;
        }

        lock (LockObject)
        {
            m_shiftCount = (int)(m_XCordTimeOffset * 1.0 / (XIncBySec * 1000));
            Double XcoordinateDivBase = m_size.Width * XIncBySec * 100.0;//ms

            while (XIncBySec * 10 * 1000 <= 1)
            {
                m_shiftCount = (int)(m_shiftCount / 1000);
                XcoordinateDivBase = XcoordinateDivBase * 1000.0;
                XIncBySec *= 1000;
            }

            m_XCordDividedRate = XcoordinateDivBase / m_XCordTimeDiv;
            m_pointCountPerScreen = (int)Math.Ceiling(m_size.Width * m_XCordTimeDiv / XcoordinateDivBase) + 1;
        }
    }
    
    /// <summary>
    /// Saves previously drawn data so that it can be used and shifted over later when new data appears
    /// </summary>
    /// <param name="data"></param> Array of the data just added
    /// <param name="plotCount"></param> Number of total channels 
    /// <param name="dataCountPerPlot"></param> data.count/ num of channels
    private void SaveData(Double[] data, int plotCount, int dataCountPerPlot)
    {
        if (dataCountPerPlot * m_plotCount > m_drawDataBuffer.Length)
        {
            m_drawDataBuffer = new Double[(dataCountPerPlot + 1) * m_plotCount];
        }

        int offset = 0;

        if (dataCountPerPlot >= m_pointCountPerScreen)
        {
            m_mapDataIndexPerPlot = m_dataCountPerPlot - m_pointCountPerScreen - 1;
            Array.Copy(data, 0, m_drawDataBuffer, 0, plotCount * dataCountPerPlot);
            m_dataCountCachePerPlot = dataCountPerPlot;
            offset = m_dataCountPerPlot;
        }
        else
        {
            if (m_dataCountCachePerPlot + dataCountPerPlot <= m_pointCountPerScreen)
            {
                Array.Copy(data, 0, m_drawDataBuffer, m_dataCountCachePerPlot * plotCount, plotCount * dataCountPerPlot);
                m_dataCountCachePerPlot += dataCountPerPlot;
                offset = 0;
            }
            else
            {
                int overflowCount = plotCount * (m_dataCountCachePerPlot + dataCountPerPlot - m_pointCountPerScreen);
                Array.Copy(m_drawDataBuffer, overflowCount, m_drawDataBuffer, 0, plotCount * m_dataCountCachePerPlot - overflowCount);
                Array.Copy(data, 0, m_drawDataBuffer, plotCount * m_dataCountCachePerPlot - overflowCount, plotCount * dataCountPerPlot);
                m_dataCountCachePerPlot = m_pointCountPerScreen;
                m_mapDataIndexPerPlot = 0;
                offset = m_dataCountCachePerPlot + dataCountPerPlot - m_pointCountPerScreen;
            }
        }

        int xPos = m_dataCountCachePerPlot - m_mapDataIndexPerPlot - 1;
        if (m_recordEndFlag == true)
        {
            m_xPosition = xPos;
            m_drawRecordEnd = true;
        }
        else
        {
            m_xPosition -= offset;
            m_drawRecordEnd = m_xPosition > 0 && m_xPosition < m_pointCountPerScreen;
        }
    }

    /// <summary>
    /// Physcial drawing of all of the points. Logic also contains
    ///     - Pass/Retest/Fail logic and signaling
    ///     - Converts the voltage numebrs to on screen pixel locations
    ///     - Accounts for bi-polar vs uni-polar data
    /// </summary>
    private void MapDataPoints()
    {
        Double YCordDividedRate = 1.0 * (m_size.Height - 1) / (m_YCordRangeMax - m_YCordRangeMin);
        int count = (int)(m_dataCountCachePerPlot - m_shiftCount - m_mapDataIndexPerPlot);
        int drawPoint = count > m_pointCountPerScreen ? m_pointCountPerScreen : count;

        if (drawPoint < 1)
        {
            return;
        }

        for (int index = 0; index < drawPoint; ++index)
        {
            for (int i = 0; i < m_plotCount; i++) // was originally m_plotCount
            {
                Double value=0;
                if (channelNum == 3 && i == 2)
                {
                    Double valueH = m_drawDataBuffer[(int)(m_plotCount * (index + m_mapDataIndexPerPlot + m_shiftCount) + 0)];
                    Double valueV = m_drawDataBuffer[(int)(m_plotCount * (index + m_mapDataIndexPerPlot + m_shiftCount) + 1)];

                    value = Math.Sqrt(Math.Pow(valueH, 2)+Math.Pow(valueV, 2));
                }
                else
                {
                    value = m_drawDataBuffer[(int)(m_plotCount * (index + m_mapDataIndexPerPlot + m_shiftCount) + i)];
                }

                if (isUnipolar || channelNum ==3)
                {
                   value = 2*Math.Abs(value);
                }

                if (isUnipolar || channelNum ==3)
                {
                    m_dataPointBuffer[i][index].Y = (((float)Math.Ceiling(YCordDividedRate *
                                    (m_YCordRangeMax - value))) +m_size.Height / 2);
    }
                else
                {
                    m_dataPointBuffer[i][index].Y = (float)Math.Ceiling(YCordDividedRate *
                                    (m_YCordRangeMax - value));
                }
                
                m_dataPointBuffer[i][index].X = (float)Math.Round((index) * m_XCordDividedRate) +40; ///EDIT

                if(isUnipolar || channelNum == 3)
                {
                    value = value / 2; //it seems that the x2 is specifically for the drawing of the value but the real value then gets skewed and compared incorrectly to the tolerance
                }

                if (((value >= tolerance4 || value <= -(tolerance4)) && /*channelNum !=3 && i !=2 &&*/ channelNum-1 == i) && index > drawPoint -sectionCount)
                {
                    result = TestResult.REJECT;
                    VoltagesHigh.Add(value);
                    Times.Add(mainForm.getTimer());
                    Tuple<Double,Double> values = new Tuple<Double,Double>(value, mainForm.getTimer());
                    VoltTimeHigh.Add(values);
                    //Debug.Print("High pass or Low pass triggered!");
                }
                else if(((value >= tolerance2 || value <= -(tolerance2)) && /*channelNum != 3 && i != 2 &&*/ channelNum - 1 == i)&& index  >drawPoint- sectionCount)
                {
                    if (result != TestResult.REJECT) 
                    {
                        result = TestResult.RETEST;
                    }
                    VoltagesMedium.Add(value);
                    Times.Add(mainForm.getTimer());
                    Tuple<Double, Double> values = new Tuple<Double, Double>(value, mainForm.getTimer());
                    VoltTimeMed.Add(values);
                }
            }
        }

        if (m_drawRecordEnd)
        {
            m_recordEndPointX = (int)(m_xPosition * m_XCordDividedRate);
        }
    }

    /// <summary>
    /// Paints the data !
    /// </summary>
    /// <param name="g"></param>
    private void PaintTo(Graphics g)
    {
        g.Clear(Color.White);//Black);
        float counter =0;
        float increment= 0;
        
        /// This sets up the voltage marking for all ofthe channels according tothe differen voltages selected and whether
        /// the graph i unipolar or bipoler
        if(isUnipolar || channelNum ==3) 
        {
            switch (Voltage)
            {
                case 10:
                    counter = 9;
                    increment = 1;
                    break;

                case 5:
                    counter = 5;
                    increment = 0.5F;
                    break;

                case 2.5F:
                    counter = 2.25F;
                    increment = 0.25F;
                    break;

                case 1:
                    counter = 0.90F;
                    increment = 0.10F;
                    break;

            }
        }
        else
        {
            switch (Voltage)
            {
                case 10:
                    counter = 8;
                    increment = 2;
                    break;

                case 5:
                    counter = 4;
                    increment = 1;
                    break;

                case 2.5F:
                    counter = 2.00F;
                    increment = 0.50F;
                    break;

                case 1:
                    counter = 0.80F;
                    increment = 0.20F;
                    break;
            }
        }



        //The left most and bottom most lines to seperate values from graph
        g.DrawLine(System.Drawing.Pens.LightGray,//DarkGreen,
            new Point((int)(40), 0),
            new Point((int)(40), (m_size.Height+40)));

        g.DrawLine(System.Drawing.Pens.LightGray,//DarkGreen,
            new Point((int)(0), m_size.Height),
            new Point((int)(m_size.Width +40), (m_size.Height)));


        int countLine = 11;
        for (int i = 0; i < countLine; i++)
        {

            //ORIGINAL
            ///If graph is not moving then draw the vertical lines as per normal
            if (!isMoving)
            {
                g.DrawLine(System.Drawing.Pens.LightGray,//DarkGreen,
                new Point((int)(1.0 * i * (m_size.Width) / 10) + 40, 0),
                new Point((int)(1.0 * i * (m_size.Width) / 10) + 40, (m_size.Height)));
                using (Font myFont = new Font("Arial", 11))
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    if (i == 0)
                    {
                        stringFormat.Alignment = StringAlignment.Near;
                    }
                    else if(i == 10)
                    {
                        stringFormat.Alignment = StringAlignment.Far;
                    }
                    g.DrawString(decimal.Round((decimal)(i * (XCordTimeDiv/1000)),2).ToString(), myFont, Brushes.Green, new Point((int)(1.0 * i * (m_size.Width) / 10) + 40, (int)m_size.Height + 10),stringFormat); ////HARDCODING TIME FOR NOW
                }
            }
            else // When graph is moving setcount will be defined and the equation below calculates the offset the graph should be moved by
            {
                int currCount = lineCount - setCount;
                int pixelCount = (int)Math.Round((currCount * m_XCordDividedRate * sectionCount) -pixelMove);//73;
                int xLoc = (int)(1.0 * i * (m_size.Width) / 10) + 40 - pixelCount;

                if(xLoc < 40)
                {
                    countLine++;
                    continue;
                }

                g.DrawLine(System.Drawing.Pens.LightGray,//DarkGreen,
                new Point(xLoc, 0),
                new Point(xLoc, (m_size.Height)));
                using (Font myFont = new Font("Arial", 11))
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    if (xLoc>40 && xLoc <50)
                    {
                        stringFormat.Alignment = StringAlignment.Near;
                    }
                    else if (xLoc > m_size.Width+40-10 && xLoc < m_size.Width+40)
                    {
                        stringFormat.Alignment = StringAlignment.Far;
                    }
                    decimal div = 0;
                    if(countLine >= setCount)
                    {
                        div = (decimal)countLine / (decimal)setCount;
                        div -= Math.Floor((decimal)countLine / (decimal)setCount);
                    }
                    else
                    {
                        div = (decimal)countLine / (decimal)setCount;
                    }
                    string temp = (decimal.Round((decimal)((int)Math.Floor(div) + i * (XCordTimeDiv / 1000.0)), 2)).ToString();
                    g.DrawString(temp, myFont, Brushes.Green, new Point(xLoc, (int)m_size.Height+10),stringFormat); ////HARDCODING TIME FOR NOW
                }
            }
        }

        ///This draws the horizontal lines when its just the regular voltage values 
        for (int k = 1; k < 10; k++, counter -= increment)
        {
            if (!isManual)
            {
                g.DrawLine(System.Drawing.Pens.LightGray,//DarkGreen,
                new Point(0+40, (int)(1.0 * k * (m_size.Height) / 10)), ///EDIT
                new Point(m_size.Width+40, (int)(1.0 * k * m_size.Height / 10))); ///EDIT

                var pixelLoc = k * m_size.Height / 10;


                using (Font myFont = new Font("Arial", 11))
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    if (pixelLoc > m_size.Height / 2)
                    {
                        g.DrawString(counter.ToString(), myFont, Brushes.Green, new Point(40, (int)pixelLoc),stringFormat);
                    }
                    else
                    {
                        g.DrawString(counter.ToString(), myFont, Brushes.Green, new Point(40, (int)pixelLoc),stringFormat);
                    }

                }
            }
        }
        lineCount++;

        ///This area draws the horizontal lines when the graph isManual is true and also adds the labels of the .001 inch locations + the red gates 
        if (isManual)
        {
            //TEST-CAT

            /*double slope = fourThough - twoThough;
            double otherSlope = slope - twoThough;*/
            int temp = 1;

            double slope = (notch2Loc - notch1Loc) /.001;
            slope = (fourThough - twoThough) / slope;
            if (channelNum == 3 || isUnipolar)
            {
                for (Double i = m_size.Height , j = 0; i > 0; i -= slope, j += .5)
                {
                        drawLine(m_size, Math.Ceiling(i), g, (j * .001));
                    temp++;
                }
            }
            else
            {
                for (Double i = m_size.Height / 2, j = 0; i > 0; i -= slope, j += 1)
                {
                    if (j == 0)
                    {
                        drawLine(m_size, (m_size.Height / 2), g, (j * .001));
                    }
                    else
                    {
                        drawLine(m_size, Math.Ceiling(i), g, (j * .001));
                    }
                    temp++;
                }
                temp = 1;
                for (Double i = m_size.Height / 2, j = 0; i < m_size.Height; i += slope, j -= 1)
                {
                    if (j == 0)
                    {
                        drawLine(m_size, (m_size.Height / 2), g, (j * .001));
                    }
                    else
                    {
                        drawLine(m_size, Math.Floor(i), g, (j * .001));
                    }
                    temp++;
                }
            }
        }

        if (m_drawRecordEnd)
        {
            g.DrawLine(System.Drawing.Pens.Yellow,
            new Point(m_recordEndPointX, 0),
            new Point(m_recordEndPointX, m_size.Height));
        }

        //draw sample data on bitmap.
        if (m_dataCountCachePerPlot > 0)
        {
            Pen plotPen;
            int count = (int)(m_dataCountCachePerPlot - m_shiftCount - m_mapDataIndexPerPlot);
            int countDrawnPerPlot = count > m_pointCountPerScreen ? m_pointCountPerScreen : count;
            if (1 > countDrawnPerPlot)
            {
                return;
            }
            PointF[] drawData = new PointF[countDrawnPerPlot];
            for (int plotNumber = channelNum-1; plotNumber < channelNum; plotNumber++) // every channel is painted to its own pictrebox w the appropriate channel
            {
                plotPen = (plotNumber >= 0 && plotNumber < 16) ? m_pens[plotNumber] : System.Drawing.Pens.Black;
                Array.Copy(m_dataPointBuffer[plotNumber], drawData, countDrawnPerPlot);
                if (countDrawnPerPlot > 1)
                {
                    g.DrawLines(plotPen, drawData);
                }
                else
                {
                    g.DrawLine(plotPen, drawData[0], drawData[0]);
                }
            }
            if((countDrawnPerPlot + sectionCount >= m_pointCountPerScreen) && !isMoving)
            {
                setCount = lineCount-1;
                isMoving = true;

                pixelMove = (m_pointCountPerScreen - countDrawnPerPlot) * m_XCordDividedRate;
            }
        }
    }

    /// <summary>
    /// Paint handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ControlOnPaint(object sender, PaintEventArgs e)
    {
        if (m_graphics == null)
        {
            m_graphics = Graphics.FromImage(m_bitmap);
        }
        PaintTo(m_graphics);

        e.Graphics.DrawImageUnscaled(m_bitmap, new Point(0, 0));
    }

    /// <summary>
    /// Draws the Horizontal lines when the useer selects the crack depth setting 
    /// </summary>
    /// <param name="m_size"></param> Size of the actual graph (no the total picturebox)
    /// <param name="pixelLoc"></param> Where on the y-axis should it be drawn
    /// <param name="g"></param> grpahics
    /// <param name="number"></param> Crack depth lable for the lines 
    private void drawLine(Size m_size, Double pixelLoc, Graphics g, Double number)
    {
        Pen pen;

        if (number == notch1Loc || number == -notch1Loc || number == notch2Loc || number == -notch2Loc)
        {
            pen = new Pen(Color.Red);
        }
        else
        {
            pen = new Pen(Color.LightGray);
        }


        g.DrawLine(pen,//DarkGreen,
            new Point(0+40, (int)(pixelLoc )), ///EDIT
            new Point(m_size.Width +40, (int)(pixelLoc))); ///EDIT

        using (Font myFont = new Font("Arial", 9))
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Center;

            if (pixelLoc > m_size.Height/2)
            {
                g.DrawString(number.ToString(), myFont, Brushes.Green, new Point(40, (int)pixelLoc),stringFormat);
            }
            else
            {
                g.DrawString(number.ToString(), myFont, Brushes.Green, new Point(40, (int)pixelLoc),stringFormat);
            }
            
        }
    }

    #endregion
}

static class Helpers
{
    public static void GetXCordRangeLabels(string[] ranges, Double rangeMax, Double rangeMin, TimeUnit unit)
    {
        string[] tUnit = { "ns", "us", "ms", "Sec" };
        int i;
        for (i = (int)unit; i < (int)TimeUnit.Second && rangeMax > 1000; ++i)
        {
            rangeMin /= 1000;
            rangeMax /= 1000;
        }
        ranges[0] = rangeMax.ToString() + " " + tUnit[i];
        ranges[1] = rangeMin.ToString() + " " + tUnit[i];
    }

    public static (Double time, TimeUnit unit) GetTimeSettings(Double rangeMax, TimeUnit unit)
    {
        string[] tUnit = { "ns", "us", "ms", "Sec" };
        int i;
        for (i = (int)unit; i < (int)TimeUnit.Second && rangeMax > 1000; ++i)
        {
            rangeMax /= 1000;
            unit++;
        }

        return (rangeMax, unit);
    }

    public static void GetYCordRangeLabels(string[] ranges, Double rangeMax, Double rangeMin, ValueUnit unit)
    {
        string[] sUnit = { "kV", "V", "mV", "uV", "KA", "A", "mA", "uA", "C", "" };
        int index = (int)unit;
        if (-1 == index)//No unit
        {
            index = sUnit.Length - 1;
        }
        ranges[0] = rangeMax.ToString() + sUnit[index];
        ranges[1] = rangeMin.ToString() + sUnit[index];
        ranges[2] = (rangeMax == -rangeMin) ? "0" : "";
    }

    public static void GetYCordRangeLabels(string[] ranges, Double rangeMax, Double rangeMin, FrequencyUnit unit)
    {
        string[] sUnit = { "Hz", "k", "M", "" };
        int index = (int)unit;
        if (-1 == index)//No unit
        {
            index = sUnit.Length - 1;
        }
        ranges[0] = rangeMax.ToString() + sUnit[index];
        ranges[1] = rangeMin.ToString() + sUnit[index];
        ranges[2] = (rangeMax == -rangeMin) ? "0" : "";
    }

}