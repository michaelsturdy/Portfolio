using ChatLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatUi
{
    public partial class ChatForm : Form
    {
        Client client = new Client();
        public ChatForm()
        {
            client.MessageReceived += new MessageReceivedEventHandler(Client_MessageReceived);
            InitializeComponent();
        }

        public void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            ConversationTextBox.AppendText("\n" + e.Message);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {

        }

        
    }
}
