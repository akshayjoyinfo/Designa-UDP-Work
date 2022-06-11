using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDisplayTester
{
    public partial class Form1 : Form
    {
        ////控制卡IP
        //public static byte[] ip = Encoding.ASCII.GetBytes("192.168.0.199");
        ////控制卡类型
        //public static int Type = 8536;
        ////控制卡宽度
        //public static int width = 64;
        ////控制卡高度
        //public static int height = 32;
        ////端口
        //public static ushort port = 80;
        ////用户名，密码
        //public static string str = "guest";
        ////是否执行
        public static bool BL = true;
        public static int err = 0;
        public Form1()
        {
            InitializeComponent();
        }
        [HandleProcessCorruptedStateExceptions]
        private void button1_Click(object sender, EventArgs e)
        {
            var IP = Encoding.ASCII.GetBytes(textBox2.Text);
            var WIDTH = Convert.ToInt32(textBox3.Text);
            var HEIGHT = Convert.ToInt32(textBox4.Text);
            var PORT = Convert.ToUInt16(textBox5.Text);
            var TYPE = Convert.ToInt32(textBox6.Text);
            var USER_NAME = textBox7.Text;
            var USER_PASSWORD = textBox8.Text;
            var FONT_SIZE = Convert.ToInt32(textBox11.Text);
            var X = Convert.ToInt32(textBox9.Text);
            var Y = Convert.ToInt32(textBox10.Text);
            var STAY_TIME = Convert.ToInt32(textBox12.Text);

            try
            {


                LedYNetSdk.init_sdk();
                int err = 0;
                
                //
                IntPtr playlist = LedYNetSdk.create_playlist(WIDTH, HEIGHT, TYPE);

                // main display text
                string name = "program_main";
                IntPtr program = LedYNetSdk.create_program(name, "0xff000000");
                
                // actual text to LED
                IntPtr area_tree = LedYNetSdk.create_rich_text();
                err = LedYNetSdk.add_rich_text_unit(area_tree, STAY_TIME, 16, "", "0xff000000", "<span foreground='red' font='10'>"+textBox1.Text+"</span>");
                err = LedYNetSdk.add_rich_text(program, area_tree, X, Y, WIDTH, HEIGHT, 100, 1, 0);
                LedYNetSdk.delete_rich_text(area_tree);

                // main display text
                string reset_name = "program_reset";
                IntPtr reset_program = LedYNetSdk.create_program(reset_name, "0xff000000");

                // actual text to LED
                IntPtr reset_area_tree = LedYNetSdk.create_rich_text();
                err = LedYNetSdk.add_rich_text_unit(reset_area_tree, 0, 16, "", "0xff000000", $"<span foreground='red' font='{FONT_SIZE}'>Reset Display done</span>");
                err = LedYNetSdk.add_rich_text(reset_program, reset_area_tree, X, Y, WIDTH, Height, 100, 1, 0);
                LedYNetSdk.delete_rich_text(reset_area_tree);


                string m_aging_start_time = "";
                string m_aging_stop_time = "";
                string m_period_ontime = "";
                string m_period_offtime = "";
                err = LedYNetSdk.add_program_in_playlist(playlist, program, 1, 10, m_aging_start_time, m_aging_stop_time, m_period_ontime, m_period_offtime, 127);
                err = LedYNetSdk.add_program_in_playlist(playlist, reset_program, 1, 10, m_aging_start_time, m_aging_stop_time, m_period_ontime, m_period_offtime, 127);
                int send_style = 0;
                var szLocalTempDir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                long free_size = 0; long total_size = 0;
                err = LedYNetSdk.send_program(IP, PORT, USER_NAME, USER_PASSWORD, szLocalTempDir, playlist, send_style, ref free_size, ref total_size);

                if (err == 0)
                {
                    MessageBox.Show("DONE");
                }
                else
                {
                    MessageBox.Show("ERROR : "+ Errer.GetError(err));
                }
                //Thread.Sleep(5000);
                //LedYNetSdk.delete_playlist(playlist);
            }
            catch (AccessViolationException ee)
            {
                MessageBox.Show("LEDSDK Error "+ee.ToString());
            }
            catch (Exception exp) { Console.Write(exp); }
            finally
            {
                //清除显示屏所有节目
                //LedYNetSdk.clear_all_program(IP, (ushort)PORT, USER_NAME, USER_PASSWORD);
                //清除显示所有动态区
                //LedYNetSdk.clear_dynamic(IP, (ushort)PORT, USER_NAME, USER_PASSWORD);
                //LedYNetSdk.download_file(ip, (ushort)port, str, str, "", "");


                LedYNetSdk.release_sdk();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
