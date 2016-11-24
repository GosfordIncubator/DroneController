using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

namespace Drone_Wars.Model
{
    static class Network
    {
        static TcpClient tcpClient = new TcpClient();
        static NetworkStream serverStream = default(NetworkStream);
        static string readData = string.Empty;

        private static void sendMessage(string message)
        {
            byte[] outStream = Encoding.ASCII.GetBytes(message);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        public static void sendTakeOff()
        {
            sendMessage("1");
        }

        public static void sendLand()
        {
            Console.WriteLine("Landing");
            sendMessage("2");
        }

        public static void sendStop()
        {
            sendMessage("3");
        }

        public static void sendStopX()
        {
            sendMessage("31");
        }

        public static void sendStopY()
        {
            sendMessage("32");
        }
        
        public static void sendStopZ()
        {
            sendMessage("33");
        }

        public static void sendForward()
        {
            sendMessage("4");
        }

        public static void sendBackward()
        {
            sendMessage("5");
        }

        public static void sendLeft()
        {
            sendMessage("6");
        }

        public static void sendRight()
        {
            sendMessage("7");
        }

        public static void sendUp()
        {
            sendMessage("8");
        }

        public static void sendDown()
        {
            sendMessage("9");
        }

        public static void connect()
        {
            tcpClient.Connect("127.0.0.1", 8000);
            serverStream = tcpClient.GetStream();

            sendMessage("0");

            // upload as javascript blob
            Task taskOpenEndpoint = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    // Read bytes
                    serverStream = tcpClient.GetStream();
                    byte[] message = new byte[4096];
                    int bytesRead;
                    bytesRead = 0;

                    try
                    {
                        // Read up to 4096 bytes
                        bytesRead = serverStream.Read(message, 0, 4096);
                    }
                    catch
                    {
                        /*a socket error has occured*/
                    }

                    //We have read the message.
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    Thread.Sleep(500);
                }
            });
        }
    }
}
