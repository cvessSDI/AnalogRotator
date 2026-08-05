using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Access.Dao;

namespace AnalogRotator
{
    public partial class ViewPart : Form
    {
        StreamingAIForm mainForm;

        /// <summary>
        /// Initiialization with a part selected.
        /// </summary>
        /// <param name="mainForm"></param>
        /// <param name="index"></param>
        public ViewPart(StreamingAIForm mainForm, int index)
        {
            InitializeComponent();
            this.mainForm = mainForm;


            StreamingAIForm.Parts part = mainForm.allParts[index];

            lblPartNum.Text = "Part Num: "+part.partNo;
            lblProbeNum.Text = "Probe Num: " + part.probeNo;
            lblNotch1T.Text = part.notch1.ToString();
            lblNotch2T.Text = part.notch2.ToString();
            lblNotch1VT.Text = part.notch1Volt.ToString();
            lblNotch2VT.Text = part.notch2Volt.ToString();
            lblScanPlanNameT.Text = part.scanPlanName;
            lblUWFileNameT.Text = part.uniwestSetupName;
            tbxTechniqueFile.Text = part.techniqueFile;
            if (part.scanPlanType == 0)
            {
                lblIsProfileT.Text = "No";
            }
            else
            {
                lblIsProfileT.Text = "Yes";
            }


            string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "\\Notes\\" + $"{part.partNo}" + "-Notes.txt";
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }

            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        tbxTechnique.Text = sr.ReadToEnd();
                    }
                }
                else
                {
                    MessageBox.Show("The notes file is missing or has been relabled.", "Notes Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("The notes file is either corrupted or missing.", "Notes Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

            fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{part.partNo}" + "-PartImage.png";
            fi = new FileInfo(fileName);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }

            try 
            {
                if (File.Exists(fileName))
                {
                    var ms = new MemoryStream(File.ReadAllBytes(fileName));
                    System.Drawing.Image img = Image.FromStream(ms);//System.Drawing.Image.FromFile(fileName);
                    Bitmap b = new Bitmap(img);
                    System.Drawing.Image i = resizeImage(b, new Size(500, 300));
                    pbxPartImage.Image = i;
                    ms.Close();
                }
                else
                {
                    MessageBox.Show("The part image is missing or has been relabeled.", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{part.partNo}" + "-ProbeImage.png";
                fi = new FileInfo(fileName);

                if (File.Exists(fileName))
                {
                    var ms = new MemoryStream(File.ReadAllBytes(fileName));
                    System.Drawing.Image img = Image.FromStream(ms);//System.Drawing.Image.FromFile(fileName);
                    Bitmap b = new Bitmap(img);
                    System.Drawing.Image i = resizeImage(b, new Size(500, 300));
                    pbxProbeImage.Image = i;
                    ms.Close();
                }
                else
                {
                    MessageBox.Show("The probe image is missing or has been relabeled.", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch 
            {
                MessageBox.Show("Something has gone wrong with the image files. Please check if they have been corrupted and remove them if so.", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// This function is to grab the image which can be of whatever size the user has it at and resizes it to fit withing the alotted picturebox
        /// </summary>
        /// <param name="imgToResize"></param> The image
        /// <param name="size"></param> The size 
        /// <returns></returns>
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width    
            int sourceWidth = imgToResize.Width;
            //Get the image current height    
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size    
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size    
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width    
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height    
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height    
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        /// <summary>
        /// Dispose the images when closing 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(pbxPartImage.Image != null)
            {
                pbxPartImage.Image.Dispose();
                pbxPartImage.Image = null;
            }
            if (pbxProbeImage.Image != null)
            {
                pbxProbeImage.Image.Dispose();
                pbxProbeImage.Image = null;
            }

            
        }

        private void ViewPart_Load(object sender, EventArgs e)
        {

        }
    }
}
