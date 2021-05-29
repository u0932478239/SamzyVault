using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public void Reload()
        {
            listBox1.Enabled = true;
            string bruh = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            listBox1.Items.Clear();
            string[] fileEntries = Directory.GetFiles($@"{bruh}\SamzyDev");
            ArrayList USStates = new ArrayList();
            foreach (string fileName in fileEntries)
            {
                USStates.Add(new USState(fileName, Path.GetFileName(fileName)));
            }
            listBox1.DataSource = USStates;
            listBox1.DisplayMember = "ShortName";
            listBox1.ValueMember = "LongName";
            if (listBox1.Items.Count == 0)
            {
                listBox1.Items.Add("No Files Found");
                listBox1.Enabled = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string file = ((USState)listBox1.SelectedItem).LongName;
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
                else if (text == "")
                {
                    richTextBox1.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Unable to Open File!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                File.Delete(((USState)listBox1.SelectedItem).LongName);
                listBox1.Items.Remove(listBox1.SelectedItem);
                Reload();
            }
            catch
            {
                MessageBox.Show("No File Selected!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroSetButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string file = ((USState)listBox1.SelectedItem).LongName;
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
            catch
            {
                MessageBox.Show("Unable to Open File!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroSetButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox1.Text);
                MessageBox.Show("Copied Text!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Unable to Copy Text!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroSetButton5_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(((USState)listBox1.SelectedItem).LongName, AES.Encrypt(richTextBox1.Text));
                MessageBox.Show("Saved!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Unable to Save File!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroSetButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently Broken!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Import yeah = new Import();
            yeah.ShowDialog();
            Reload();
        }

        private void metroSetButton7_Click(object sender, EventArgs e)
        {
            try
            {
                string bruh = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string Key2 = AES.shhhh(File.ReadAllText($@"{bruh}/b4ecfe41c8a7d0bdfcdfa530db818117"));
                Clipboard.SetText(Key2);
                MessageBox.Show("Copied Key!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Unable to Copy Key!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroSetButton8_Click(object sender, EventArgs e)
        {
            try
            {
                string file = ((USState)listBox1.SelectedItem).LongName;
                string text = File.ReadAllText($@"{file}");

                if (text != "")
                {
                    Clipboard.SetText(text);
                    MessageBox.Show("Copied Encrypted Text!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Unable to Copy Encrypted Text!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroSetButton7_Click_1(object sender, EventArgs e)
        {
            try
            {
                string file = ((USState)listBox1.SelectedItem).LongName;
                string text = File.ReadAllText($@"{file}");

                if (text != "")
                {
                    try
                    {
                        string decrypted = AES.Decrypt(text);
                        MessageBox.Show(decrypted);
                        string encrypted = AES.Encryptshhh(decrypted);
                        Clipboard.SetText(encrypted);
                        MessageBox.Show("Copied Encrypted Text!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error Encrypting Text: " + ex, "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unable to Open File!", "SamzyVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string target = e.LinkText;
            Process.Start(target);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Viewer_TextChanged(object sender, EventArgs e)
        {
            string str = richTextBox1.Text;
            Regex re = new Regex(@"^((ht|f)tp(s?)\:\/\/|~/|/)?([\w]+:\w+@)?([a-zA-Z]{1}([\w\-]+\.)+([\w]{2,5}))(:[\d]{1,5})?((/?\w+/)+|/?)(\w+\.[\w]{3,4})?((\?\w+=\w+)?(&\w+=\w+)*)?", RegexOptions.None);

            MatchCollection mc = re.Matches(str);

            foreach (Match ma in mc)

            {

                richTextBox1.Select(ma.Index, ma.Length);

                richTextBox1.SelectionColor = Color.Red;

            }
        }
    }
    public class USState
    {
        private string myShortName;
        private string myLongName;

        public USState(string strLongName, string strShortName)
        {

            this.myShortName = strShortName;
            this.myLongName = strLongName;
        }

        public string ShortName
        {
            get
            {
                return myShortName;
            }
        }

        public string LongName
        {

            get
            {
                return myLongName;
            }
        }
    }
}
