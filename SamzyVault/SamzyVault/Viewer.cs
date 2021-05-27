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
    public partial class Viewer : MetroSetForm //WORK ON DISPLAY MEMBERS HERE https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.listcontrol.displaymember?view=net-5.0
    {
        public Viewer()
        {
            InitializeComponent();
        }

        private void Viewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
        private void Reload()
        {
            listBox1.Enabled = true;
            string bruh = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            listBox1.Items.Clear();
            string[] fileEntries = Directory.GetFiles($@"{bruh}\SamzyDev");
            foreach (string fileName in fileEntries)
            {
                listBox1.Items.Add(fileName);
            }
            if (listBox1.Items.Count == 0)
            {
                listBox1.Items.Add("No Files Found");
                listBox1.Enabled = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            string bruh = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists($@"{bruh}\SamzyDev"))
            {
                Directory.CreateDirectory($@"{bruh}\SamzyDev");
            }
            Reload();
        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                string bruh = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                saveFileDialog1.InitialDirectory = $@"{bruh}/SamzyDev";
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myStream.Close();
                    }
                }
                Reload();
            }
            catch
            {
                MessageBox.Show("No File Selected!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroSetButton3_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(listBox1.SelectedItem.ToString());
                listBox1.Items.Remove(listBox1.SelectedItem.ToString());
                Reload();
            }
            catch
            {
                MessageBox.Show("No File Selected!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroSetButton2_Click(object sender, EventArgs e)
        {
            string file = listBox1.SelectedItem.ToString();
            string text = File.ReadAllText($@"{file}");

            if (text != "")
            {
                try
                {
                    richTextBox1.Text = AES.Decrypt(text);
                }
                catch
                {
                    MessageBox.Show("Error Decrypting!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void metroSetButton4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void metroSetButton5_Click(object sender, EventArgs e)
        {
            File.WriteAllText(listBox1.SelectedItem.ToString(), AES.Encrypt(richTextBox1.Text));
            MessageBox.Show("Saved!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void metroSetButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature is a work in progress!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
