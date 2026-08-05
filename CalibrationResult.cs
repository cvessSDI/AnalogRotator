using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AI_StreamingAI
{
    public partial class CalibrationResult : Form
    {
        StreamingAIForm mainForm;
        public CalibrationResult(StreamingAIForm originalForm)
        {
            InitializeComponent();
            this.mainForm = originalForm;
        }

        private void btnCalibrationAccept_Click(object sender, EventArgs e)
        {
            //IDK HOW IM GOING TO GO ABOUT THIS PART YET, BUT IT SEEMED TO BE SHIFTED DOWN IN PRIORITY FOR NOW
            mainForm.masterUserChoice = 1;
            mainForm.secondMasterPass = true;
            mainForm.GenerateFreshMasterCalibration();
            mainForm.ExportToExcel();
            this.Close();
        }

        private void btnCalibrationReject_Click(object sender, EventArgs e)
        {
            //IDK HOW IM GOING TO GO ABOUT THIS PART YET, BUT IT SEEMED TO BE SHIFTED DOWN IN PRIORITY FOR NOW
            mainForm.masterUserChoice = 0;
            mainForm.secondMasterPass = true;
            mainForm.ExportToExcel();
            this.Close();
        }
    }
}
