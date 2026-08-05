
namespace AI_StreamingAI
{
    partial class CalibrationResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationResult));
            this.lblCalibrationResult = new System.Windows.Forms.Label();
            this.btnCalibrationAccept = new System.Windows.Forms.Button();
            this.btnCalibrationReject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCalibrationResult
            // 
            this.lblCalibrationResult.AutoSize = true;
            this.lblCalibrationResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalibrationResult.Location = new System.Drawing.Point(30, 12);
            this.lblCalibrationResult.Name = "lblCalibrationResult";
            this.lblCalibrationResult.Size = new System.Drawing.Size(428, 36);
            this.lblCalibrationResult.TabIndex = 0;
            this.lblCalibrationResult.Text = "Please Select The Result for the Master Calibration.\r\nA temperary report to view " +
    "the calibration results has been made";
            this.lblCalibrationResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCalibrationAccept
            // 
            this.btnCalibrationAccept.BackColor = System.Drawing.Color.LimeGreen;
            this.btnCalibrationAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibrationAccept.Location = new System.Drawing.Point(12, 51);
            this.btnCalibrationAccept.Name = "btnCalibrationAccept";
            this.btnCalibrationAccept.Size = new System.Drawing.Size(225, 50);
            this.btnCalibrationAccept.TabIndex = 1;
            this.btnCalibrationAccept.Text = "ACCEPT";
            this.btnCalibrationAccept.UseVisualStyleBackColor = false;
            this.btnCalibrationAccept.Click += new System.EventHandler(this.btnCalibrationAccept_Click);
            // 
            // btnCalibrationReject
            // 
            this.btnCalibrationReject.BackColor = System.Drawing.Color.Red;
            this.btnCalibrationReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibrationReject.Location = new System.Drawing.Point(248, 51);
            this.btnCalibrationReject.Name = "btnCalibrationReject";
            this.btnCalibrationReject.Size = new System.Drawing.Size(225, 50);
            this.btnCalibrationReject.TabIndex = 2;
            this.btnCalibrationReject.Text = "REJECT";
            this.btnCalibrationReject.UseVisualStyleBackColor = false;
            this.btnCalibrationReject.Click += new System.EventHandler(this.btnCalibrationReject_Click);
            // 
            // CalibrationResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 116);
            this.Controls.Add(this.btnCalibrationReject);
            this.Controls.Add(this.btnCalibrationAccept);
            this.Controls.Add(this.lblCalibrationResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CalibrationResult";
            this.Text = "Calibration Results";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCalibrationResult;
        private System.Windows.Forms.Button btnCalibrationAccept;
        private System.Windows.Forms.Button btnCalibrationReject;
    }
}