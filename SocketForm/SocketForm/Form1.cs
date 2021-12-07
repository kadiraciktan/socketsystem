using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace SocketForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        WebSocket webSocket = new WebSocket("ws://localhost:3000");

        private void Form1_Load(object sender, EventArgs e)
        {
            webSocket.OnMessage += WebSocket_OnMessage;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (webSocket.IsAlive)
            {
                webSocket.Send(textBox2.Text);
            }
            else
            {
                webSocket.Connect();

                MessageBox.Show(webSocket.IsAlive.ToString());
                //webSocket.Send(textBox2.Text);
            }


        }

        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Data;
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    txtAllMessages.Text = message;
                }));
            }
          //  MessageBox.Show(message, "Bir Mesaj Alındı");
        }

    }
}
