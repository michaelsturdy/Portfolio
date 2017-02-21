using ChatLib;

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

        Client client;// = new Client();
        Thread listeningThread;
        bool connected = false;
        public ChatForm(Client client)
        {
            this.client = client;
            client.MessageReceived += new MessageReceivedEventHandler(Client_MessageReceived);
            InitializeComponent();
        }

        public void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (ConversationTextBox.InvokeRequired)
            {
                //lets threads talk to each other
                MethodInvoker invoker = new MethodInvoker(delegate () {
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
                    client.Send(MessageTextBox.Text);
                    ConversationTextBox.AppendText("\r\n" + MessageTextBox.Text);
                    MessageTextBox.Text = "";
                }
                catch(Exception ex)
                {
                    connected = client.Close();
                    DisplayErrorMessage("Connection lost");
                    connectToolStripMenuItem.Enabled = true;
                    disconnectToolStripMenuItem.Enabled = false;
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
                ConversationTextBox.Text = "Connected";
                connectToolStripMenuItem.Enabled = false;
                disconnectToolStripMenuItem.Enabled = true;
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
                listeningThread.Join();
                ConversationTextBox.Text = "Connection terminated";
                connectToolStripMenuItem.Enabled = true;
                disconnectToolStripMenuItem.Enabled = false;
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
                listeningThread.Join();
            }
            Environment.Exit(0);
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connected)
            {
                connected = client.Close();
                listeningThread.Join();
            }
        }
    }
}
