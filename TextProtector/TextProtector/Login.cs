using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextProtector
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == AES.Decrypt("eyJpdiI6Ik1pVzFLYmpUOWRiYnFxZGVzNWwyOVE9PSIsInZhbHVlIjoiSGljdytwM0k4SFFKK1JBcnRjZ1JtQT09IiwibWFjIjoiYTQxMjBkMmUzYTU3OTkyYmRjY2ZiYjQ5OWE2ODdjZmNhZmJiNTY3NDkyYzgyM2I3N2ExMTI3NzMyOTM5OTBmNyJ9"))
            {
                MessageBox.Show("Logged In Successfully!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main f1 = new Main();
                this.Hide();
                f1.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Password!", "Text Protector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
