using ChatLib;
using LogLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatUi
{
    public partial class ChatForm : Form
    {
        Log log = new Log();
        Client client = new Client();
        Thread listeningThread;
        bool connected = false;
        public ChatForm()
        {
            client.MessageReceived += new MessageReceivedEventHandler(Client_MessageReceived);
            InitializeComponent();
        }

        public void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (ConversationTextBox.InvokeRequired)
            {
                //lets threads talk to each other
                MethodInvoker invoker = new MethodInvoker(delegate () {
                    log.LogMessage("server: "+e.Message);
                    ConversationTextBox.AppendText("\r\nReceived: " + e.Message);

                });
                ConversationTextBox.BeginInvoke(invoker);

            }
            else
            {
                //update the Conversation
                ConversationTextBox.AppendText("\r\nReceived: " + e.Message);

            }
            
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                try
                {
                    log.LogMessage("client: " + MessageTextBox.Text);
                    ConversationTextBox.AppendText("\r\n" + MessageTextBox.Text);
                    client.Send(MessageTextBox.Text);
                    MessageTextBox.Text = "";
                }
                catch(Exception ex)
                {
                    DisplayErrorMessage("Connection lost");
                }
            }
            else
            {
                DisplayErrorMessage("Not connected to the server");
            }
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            
            
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                connected = client.Connect();
            }
            else
            {
                DisplayErrorMessage("Already connected...");
            }
            if (connected)
            {
                client.OpenStream();
                listeningThread = new Thread(client.Receive);
                listeningThread.Start();
                ConversationTextBox.Text="Connected";
            }
            else
            {
                DisplayErrorMessage("Could not connect to the server");
            }
        }
        private void DisplayErrorMessage(string message)
        {
            MessageBox.Show(this,
                message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                //TODO close the listening thread before terminating the connection
                connected = client.Close();
                ConversationTextBox.Text = "Connection terminated";
            }
            else
            {
                DisplayErrorMessage("Nothing to Disconnect from");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                connected = client.Close();
            }
            Environment.Exit(0);
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connected)
            {
                connected = client.Close();
            }
        }
    }
}
