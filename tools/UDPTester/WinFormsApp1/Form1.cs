using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress serverAddr = IPAddress.Parse(textBox2.Text);
            IPEndPoint endPoint = new IPEndPoint(serverAddr, Convert.ToInt32(textBox3.Text));


            byte[] send_buffer = Encoding.ASCII.GetBytes(textBox1.Text);
            var res = sock.SendTo(send_buffer, endPoint);
        }
    }
}
