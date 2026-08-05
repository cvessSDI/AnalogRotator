namespace AI_StreamingAI
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.LblCompanyName = new System.Windows.Forms.Label();
            this.LblCustomerName = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LblLicensedto = new System.Windows.Forms.Label();
            this.LblAddress = new System.Windows.Forms.Label();
            this.LblTelephoneNumber = new System.Windows.Forms.Label();
            this.LblEmailAdress = new System.Windows.Forms.Label();
            this.LblWebsiteAddress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblCompanyName
            // 
            this.LblCompanyName.AutoSize = true;
            this.LblCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCompanyName.Location = new System.Drawing.Point(154, 9);
            this.LblCompanyName.Name = "LblCompanyName";
            this.LblCompanyName.Size = new System.Drawing.Size(134, 20);
            this.LblCompanyName.TabIndex = 0;
            this.LblCompanyName.Text = "Company Name";
            // 
            // LblCustomerName
            // 
            this.LblCustomerName.AutoSize = true;
            this.LblCustomerName.Location = new System.Drawing.Point(125, 162);
            this.LblCustomerName.Name = "LblCustomerName";
            this.LblCustomerName.Size = new System.Drawing.Size(82, 13);
            this.LblCustomerName.TabIndex = 1;
            this.LblCustomerName.Text = "Customer Name";
            // 
            // LblVersion
            // 
            this.LblVersion.AutoSize = true;
            this.LblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVersion.Location = new System.Drawing.Point(12, 125);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(152, 20);
            this.LblVersion.TabIndex = 2;
            this.LblVersion.Text = "Program Version: ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 121);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // LblLicensedto
            // 
            this.LblLicensedto.AutoSize = true;
            this.LblLicensedto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLicensedto.Location = new System.Drawing.Point(12, 157);
            this.LblLicensedto.Name = "LblLicensedto";
            this.LblLicensedto.Size = new System.Drawing.Size(107, 20);
            this.LblLicensedto.TabIndex = 4;
            this.LblLicensedto.Text = "Licensed to:";
            // 
            // LblAddress
            // 
            this.LblAddress.AutoSize = true;
            this.LblAddress.Location = new System.Drawing.Point(157, 29);
            this.LblAddress.Name = "LblAddress";
            this.LblAddress.Size = new System.Drawing.Size(133, 26);
            this.LblAddress.TabIndex = 5;
            this.LblAddress.Text = "650 Via Alondra\r\nCamarillo, CA 93012, USA.";
            // 
            // LblTelephoneNumber
            // 
            this.LblTelephoneNumber.AutoSize = true;
            this.LblTelephoneNumber.Location = new System.Drawing.Point(157, 64);
            this.LblTelephoneNumber.Name = "LblTelephoneNumber";
            this.LblTelephoneNumber.Size = new System.Drawing.Size(100, 13);
            this.LblTelephoneNumber.TabIndex = 6;
            this.LblTelephoneNumber.Text = "Tel: (805) 987 7755";
            // 
            // LblEmailAdress
            // 
            this.LblEmailAdress.AutoSize = true;
            this.LblEmailAdress.Location = new System.Drawing.Point(157, 77);
            this.LblEmailAdress.Name = "LblEmailAdress";
            this.LblEmailAdress.Size = new System.Drawing.Size(124, 13);
            this.LblEmailAdress.TabIndex = 7;
            this.LblEmailAdress.Text = "Email: sales@sdindt.com";
            // 
            // LblWebsiteAddress
            // 
            this.LblWebsiteAddress.AutoSize = true;
            this.LblWebsiteAddress.Location = new System.Drawing.Point(157, 90);
            this.LblWebsiteAddress.Name = "LblWebsiteAddress";
            this.LblWebsiteAddress.Size = new System.Drawing.Size(85, 13);
            this.LblWebsiteAddress.TabIndex = 8;
            this.LblWebsiteAddress.Text = "www.sdindt.com";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 199);
            this.Controls.Add(this.LblWebsiteAddress);
            this.Controls.Add(this.LblEmailAdress);
            this.Controls.Add(this.LblTelephoneNumber);
            this.Controls.Add(this.LblAddress);
            this.Controls.Add(this.LblLicensedto);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.LblCustomerName);
            this.Controls.Add(this.LblCompanyName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblCompanyName;
        private System.Windows.Forms.Label LblCustomerName;
        private System.Windows.Forms.Label LblVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LblLicensedto;
        private System.Windows.Forms.Label LblAddress;
        private System.Windows.Forms.Label LblTelephoneNumber;
        private System.Windows.Forms.Label LblEmailAdress;
        private System.Windows.Forms.Label LblWebsiteAddress;
    }
}