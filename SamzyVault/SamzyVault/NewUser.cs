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
    public partial class NewUser : MetroSetForm
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void metroSetControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void SamzyVault_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void metroSetTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            string bruh = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            File.WriteAllText($@"{bruh}/9e5197fbc04c87701ce8d84a0b679c34", AES.Encryptshhh(metroSetTextBox1.Text));
            FileInfo fi = new FileInfo($@"{bruh}/9e5197fbc04c87701ce8d84a0b679c34");
            fi.Attributes = FileAttributes.Hidden;
            File.WriteAllText($@"{bruh}/b4ecfe41c8a7d0bdfcdfa530db818117", AES.Encryptshhh(AES.RandomString(32)));
            FileInfo fi2 = new FileInfo($@"{bruh}/b4ecfe41c8a7d0bdfcdfa530db818117");
            fi2.Attributes = FileAttributes.Hidden;
            MessageBox.Show("Set Password!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Viewer s = new Viewer();
            s.Show();
            base.Hide();
        }

        private void metroSetLabel1_Click(object sender, EventArgs e)
        {
            
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            metroSetLabel1.ForeColor = Color.Silver;
        }
    }
}
