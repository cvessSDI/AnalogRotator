using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Access.Dao;
using static AnalogRotator.StreamingAIForm;

namespace AnalogRotator
{
    public partial class NewPart : Form
    {
        StreamingAIForm mainForm;
        int partIndex = -1;
        bool viewOnly = false;
        OpenFileDialog openFileDialogPart;
        OpenFileDialog openFileDialogProbe;
        bool editPart = false;

        /// <summary>
        /// Initialization specifically for when a New part is being created / added
        /// </summary>
        /// <param name="originalForm"></param> Main form
        public NewPart(StreamingAIForm originalForm)
        {
            InitializeComponent();
            this.mainForm = originalForm;
        }

        /// <summary>
        /// Initialization for when the user has chosed to edit a selected part
        /// </summary>
        /// <param name="originalForm"></param> Main form
        /// <param name="index"></param> Index of the part in the main forms allparts list
        /// <param name="isViewOnly"></param> shhhhhhhh
        public NewPart(StreamingAIForm originalForm, int index, bool isViewOnly)
        {
            if (index == -1)
            {
                MessageBox.Show("There are no parts in the parts list available to edit.", "Part Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InitializeComponent();
            editPart = true;
            this.mainForm = originalForm;
            this.partIndex = index;

            btnDeletePart.Visible = true;
            StreamingAIForm.Parts part = mainForm.allParts[index];

            tbxPartNo.Text= part.partNo;
            tbxProbeNo.Text= part.probeNo;
            tbxNotch1.Text= part.notch1.ToString();
            tbxNotch2.Text= part.notch2.ToString();
            tbxNotch1V.Text= part.notch1Volt.ToString();
            tbxNotch2V.Text= part.notch2Volt.ToString();
            tbxScanPlanName.Text= part.scanPlanName;
            tbxUWFileName.Text= part.uniwestSetupName;
            tbxTechniqueFile.Text = (part.techniqueFile == "") ? "None" : part.techniqueFile; 

            if (part.scanPlanType == 1)
                cbxIsProfile.Checked = true;


            string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "\\Notes\\" + $"{part.partNo}" + "-Notes.txt";
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }


            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    tbxTechnique.Text = sr.ReadToEnd();
                }
            }

            fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{part.partNo}" + "-PartImage.png";
            fi = new FileInfo(fileName);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }

            if (File.Exists(fileName)) 
            {
                lblPartFile.Text = $"{part.partNo}" + "-PartImage.png";
            }

            fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{part.partNo}" + "-ProbeImage.png";
            fi = new FileInfo(fileName);

            if (File.Exists(fileName))
            {
                lblProbeFile.Text = $"{part.partNo}" + "-ProbeImage.png";
            }


            if (isViewOnly)
            {
                btnSubmit.Visible = false;
                btnSubmit.Enabled= false;
            }

        }

        /// <summary>
        /// Submit button clicked and info submitted to either create new part or alter a part
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbxScanPlanName.Text.Contains("."))
            {
                string tmp = "Scan Plan name should not include the file extension or a period. Please verify the plan name and try again";
                MessageBox.Show(tmp, "Scan Plan Name Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!editPart && mainForm.IsDuplicatePart(tbxPartNo.Text))
            {
                string tmp = "This part number is already in use. Please change to a new part number or edit the current part using the Edit Part Info button.";
                MessageBox.Show(tmp, "Duplicate Part Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (Double.Parse(tbxNotch1.Text) * 2 != Double.Parse(tbxNotch2.Text) || Double.Parse(tbxNotch1V.Text) * 2 != Double.Parse(tbxNotch2V.Text))
                {
                    MessageBox.Show("Voltage #2 and Notch #2 must be double of the Voltage #1 and Notch #1. Please check your values.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("There was an error reading the Notch values. Please check these and try again.", "New Part Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(tbxTechniqueFile.Text != "None" && tbxTechniqueFile.Text != "" && (openFileDialogPart != null || lblPartFile.Text != "None") && (openFileDialogProbe != null || lblProbeFile.Text != "None") && tbxPartNo.Text!="" && tbxNotch2V.Text != "" && tbxNotch1V.Text!="" && tbxScanPlanName.Text !="" && tbxUWFileName.Text != "" && tbxProbeNo.Text != "" && lblNotch1.Text != "" && lblNotch2.Text != "" && tbxNotch1.Text != "" && tbxNotch2.Text != "")
            {
                StreamingAIForm.Parts part = new StreamingAIForm.Parts();

                part.partNo = tbxPartNo.Text;
                part.probeNo = tbxProbeNo.Text;
                try
                {
                    part.notch1 = Double.Parse(tbxNotch1.Text);
                    part.notch2 = Double.Parse(tbxNotch2.Text);
                    part.notch1Volt = Double.Parse(tbxNotch1V.Text);
                    part.notch2Volt = Double.Parse(tbxNotch2V.Text);
                }
                catch
                {
                    MessageBox.Show("There was an error reading the Notch values. Please check these and try again.", "New Part Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                part.scanPlanName = tbxScanPlanName.Text;
                part.uniwestSetupName = tbxUWFileName.Text;
                part.techniqueFile = tbxTechniqueFile.Text;

                if (cbxIsProfile.Checked) {
                    part.scanPlanType = 1;
                }
                else
                {
                    part.scanPlanType = 0;
                }

                if(partIndex == -1) //Part index is inherently -1 so only when a part is being editted does this value get adjusted 
                {
                    mainForm.allParts.Add(part);
                    mainForm.partListReload();
                }
                else
                {
                    mainForm.allParts[partIndex] = part;
                }

                string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\" + "\\Notes\\" + $"{part.partNo}" + "-Notes.txt";
                FileInfo fi = new FileInfo(fileName);

                if (!fi.Directory.Exists)
                {
                    System.IO.Directory.CreateDirectory(fi.DirectoryName);
                }

                using (StreamWriter sw = new StreamWriter(fileName, false))
                {
                    sw.WriteLine(tbxTechnique.Text);
                }

                fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{part.partNo}" + "-PartImage.png";
                
                if(openFileDialogPart != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    System.IO.File.Copy(openFileDialogPart.FileName, fileName);
                }


                fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{part.partNo}" + "-ProbeImage.png";

                if (openFileDialogProbe != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    System.IO.File.Copy(openFileDialogProbe.FileName, fileName);

                }

                mainForm.donePressed(0, editPart);
                this.Close();
            }
            else
            {
                MessageBox.Show("One or more fields are missing. Please fill the entire form out before submitting.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Function so that only one new part form can be open at a time 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.newPartForm = null;
        }

        /// <summary>
        /// Load up the image file of part and its location. Copies image over into local folder 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPartImage_Click(object sender, EventArgs e)
        {
            string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{tbxPartNo.Text}" + "-PartImage.png";
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Image File (*.jpg)| *.jpg| All Files(*.*)| *.*";
            openFileDialog.FilterIndex = 1;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openFileDialogPart = openFileDialog;
                /*if (openFileDialog.CheckFileExists)
                {
                    try
                    {
                        System.IO.File.Copy(openFileDialog.FileName, fileName);
                    }
                    catch
                    {
                        System.IO.File.Delete(fileName);
                        System.IO.File.Copy(openFileDialog.FileName, fileName);
                    }
                }*/
                lblPartFile.Text = openFileDialog.SafeFileName;
            }

            
        }

        /// <summary>
        /// Load up the image file of probe and its location. Copies image over into local folder 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProbeImage_Click(object sender, EventArgs e)
        {
            string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{tbxPartNo.Text}" + "-ProbeImage.png";
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Image File (*.jpg)| *.jpg| All Files(*.*)| *.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openFileDialogProbe = openFileDialog;
                /*if (openFileDialog.CheckFileExists)
                {
                    try
                    {
                        System.IO.File.Copy(openFileDialog.FileName, fileName);
                    }
                    catch
                    {
                        System.IO.File.Delete(fileName);
                        System.IO.File.Copy(openFileDialog.FileName, fileName);
                    }
                    
                }*/
                lblProbeFile.Text = openFileDialog.SafeFileName;
            }

            
        }
        /// <summary>
        /// Automatically doubles the value of Notch 1 and places it into the Notch 2 fill in area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxNotch2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxNotch1.Text != "")
                {
                    tbxNotch2.Text = (2 * Double.Parse(tbxNotch1.Text)).ToString();
                }
            }
            catch
            {
                MessageBox.Show("Notch 1 value was invalid. Please check and try again.", "New Part Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Automatically doubles the value of Voltage 1 and places it into the Voltage 2 fill in area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxNotch2V_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxNotch1V.Text != "")
                {
                    tbxNotch2V.Text = (2 * Double.Parse(tbxNotch1V.Text)).ToString();
                }
            }
            catch
            {
                MessageBox.Show("Notch 1 Voltage value was invalid. Please check and try again.", "New Part Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Records the location of the PDF file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTechniqueFile_Click(object sender, EventArgs e)
        {
            string fileName = tbxTechniqueFile.Text;
            /*FileInfo fi = new FileInfo(fileName);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }*/

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = (fileName != "" || fileName != "None") ? fileName : "C:\\";
            openFileDialog.Filter = "PDF File (*.pdf)| *.pdf| All Files(*.*)| *.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbxTechniqueFile.Text = openFileDialog.FileName;
            }

            
        }

        /// <summary>
        /// Deletes the images and calls to delete the part from the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            StreamingAIForm.Parts part = mainForm.allParts[partIndex];

            mainForm.deletePart();
            string fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{part.partNo}" + "-PartImage.png";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Images\\" + $"{part.partNo}" + "-ProbeImage.png";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            fileName = System.Windows.Forms.Application.StartupPath + "\\PartSettings\\Notes\\" + $"{part.partNo}" + "-Notes.png";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            this.Close();
        }
    }
}
