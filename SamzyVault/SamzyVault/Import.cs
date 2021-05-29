using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI;
using MetroSet_UI.Forms;

namespace SamzyVault
{
    public partial class Import : MetroSetForm
    {
        public Import()
        {
            InitializeComponent();
        }

        private void metroSetControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void SamzyVault_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void metroSetTextBox1_Click(object sender, EventArgs e) //Encrypted Text
        {

        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string bruh = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = $@"{bruh}\SamzyDev";
                string text = AES.Import(metroSetTextBox1.Text);
                string text2 = AES.Encrypt(text);
                File.WriteAllText(path + $@"\{metroSetTextBox3.Text}.txt", text2);
                MessageBox.Show("Imported Text!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Importing. Make a new Issue with this code: " + ex, "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void metroSetLabel1_Click(object sender, EventArgs e)
        {
            
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            metroSetLabel1.ForeColor = Color.Silver;
        }

        private void metroSetTextBox2_Click(object sender, EventArgs e) //Encryption Key
        {

        }

        private void metroSetTextBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
