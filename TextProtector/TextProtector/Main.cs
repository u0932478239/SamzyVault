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

namespace TextProtector
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) //Result
        {

        }

        private void button1_Click(object sender, EventArgs e) //Encrypt
        {
            try
            {
                string file = textBox1.Text.Replace("\"", "");
                string text = File.ReadAllText($@"{file}");
                string encrypted = AES.Encrypt(text);
                richTextBox1.Text = encrypted;
            }
            catch
            {
                MessageBox.Show("Error Encrypting Text! Make sure you have the right file!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //File
        {

        }

        private void button2_Click(object sender, EventArgs e) //Decrypt
        {
            try
            {
                string file = textBox1.Text.Replace("\"", "");
                string text = File.ReadAllText($@"{file}");
                string decrypted = AES.Decrypt(text);
                richTextBox1.Text = decrypted;
            }
            catch
            {
                MessageBox.Show("Error Decrypting Text! Make sure you have the right file!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            File.WriteAllText(textBox1.Text, richTextBox1.Text);
            MessageBox.Show("Saved!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                textBox1.Text = selectedFileName;
            }
        }

        private void textBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files != null && files.Any())
                textBox1.Text = files.First();
        }
    }
}
