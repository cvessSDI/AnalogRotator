using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogRotator
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            LoadAboutInfo();
        }
        private void LoadAboutInfo()
        {
            try
            {
                /*// Path to the file that contains the company and customer name
                string filePath = "about.txt"; // Ensure this file exists in the correct path
                if (!File.Exists(filePath))
                {
                    string[] defaultLines = { "Structural Diagnostics Inc.", "Customer Name" };
                    File.WriteAllLines(filePath, defaultLines);
                }
                // Read the file lines
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length >= 2)
                {
                    LblCompanyName.Text = lines[0]; // First line - Company Name
                    LblCustomerName.Text = lines[1]; // Second line - Customer Name
                }*/


                LblCompanyName.Text = "Structural Diagnostics Inc.";
                LblCustomerName.Text = "RBC Bearings Inc."; // Second line - Customer Name

                // Get and display the assembly version
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                LblVersion.Text = $"Version: {version}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading about information: {ex.Message}");
            }
        }
    }
}
