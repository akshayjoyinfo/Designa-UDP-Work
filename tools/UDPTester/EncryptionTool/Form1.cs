using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionTool
{
    public partial class Form1 : Form
    {
        public const string PASSWORD = "M2YhcJu_xEpqJ*SqAd!cWShmG^bNcLN3*&b!4-Bj+N5dE?!z+WR8";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string encryptedstring = StringCipher.Encrypt(textBox1.Text);
            
            textBox1.Text=encryptedstring;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string decrypted = StringCipher.Decrypt(textBox1.Text);
            
            textBox1.Text = decrypted;
        }
    }
}
