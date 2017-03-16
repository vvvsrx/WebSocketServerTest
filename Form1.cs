using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSocket.SocketBase;
using SuperSocket.WebSocket;
using Newtonsoft.Json;
using SuperSocket.SocketBase.Config;

namespace WebSocketNoticeServer
{
    public partial class Form1 : Form
    {
        WebSocketServer wsServer = null;


        public Form1()
        {
            InitializeComponent();

            this.btnStart.BackColor = Color.Green;
            this.btnStop.BackColor = Color.Red;
            this.btnStop.Enabled = false;
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;

            wsServer = new WebSocketServer();

            if (!wsServer.Setup(new ServerConfig {
                Ip = "Any",
                Port = 2017,
                Mode = SocketMode.Tcp
            }))
            {
                MessageBox.Show("Failed to setup!");
                Application.Exit();
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var session in wsServer.GetAllSessions())
            {
                session.Send(JsonConvert.SerializeObject(new MessageModel { Command = 1, Data = new { Text = "您有1个订单正在配送中" } }));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var session in wsServer.GetAllSessions())
            {
                session.Send(JsonConvert.SerializeObject(new MessageModel { Command = 2, Data = new { Text = "您最近有999个订单" } }));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var session in wsServer.GetAllSessions())
            {
                session.Send(JsonConvert.SerializeObject(new MessageModel { Command = 3, Data = new { Text = "订单已收货" } }));
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnStart.BackColor = Color.Green;
            this.btnStop.BackColor = Color.Red;
            this.btnStop.Enabled = false;
            this.btnStart.Enabled = true;
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            wsServer.Stop();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!wsServer.Start())
            {
                MessageBox.Show("Failed to start!");
                return;
            }
            this.btnStart.BackColor = Color.Red;
            this.btnStop.BackColor =  Color.Green;
            this.btnStop.Enabled = true;
            this.btnStart.Enabled = false;
            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.button3.Enabled = true;
        }
    }

    public class MessageModel
    {
        public int Command { get; set; }

        public object Data { get; set; }
    }
}
