﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LogLib;

namespace ChatLib
{
    public class Client
    {
        Log log = new Log();
        public event MessageReceivedEventHandler MessageReceived;
        public bool isListening;
        public TcpClient client;//Property to hold the TcpClient object
        public NetworkStream Stream;//Property to hold the NetworkStream object
        Byte[] Data = new Byte[256];//Byte array property to send or receive data from the stream



        /// <summary>
        /// Connects a client to a server
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            Int32 port = 13000;
            string ip = "127.0.0.1";
            try
            {
                client = new TcpClient(ip, port);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool Close()
        {
            try
            {
                isListening = false;
                client.Close();
                return false;
            }
            catch
            {
                return true;
            }
        }

        public void OpenStream()
        {
            Stream = client.GetStream();
        }


        /// <summary>
        /// Sends data through the stream
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public void Send(string message)
        {
            log.LogMessage("Client: " + message);
            Data = System.Text.Encoding.ASCII.GetBytes(message);
                Stream.Write(Data, 0, Data.Length);
        }


        /// <summary>
        /// Listens for incoming data from the stream
        /// </summary>
        /// <returns>Data received as a string</returns>
        public void Receive()
        {
            isListening = true;
            while (isListening)
            {
                Data = new Byte[256];
                String responseData = String.Empty;//Empty string to hold the response Data


                if (Stream.DataAvailable)
                {
                    Int32 bytes = Stream.Read(Data, 0, Data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(Data, 0, bytes);
                    log.LogMessage("Server: " + responseData);
                    MessageReceived(this, new MessageReceivedEventArgs(responseData));

                }

            }
        }


    }//end class
}
