using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using DroneControl;

namespace Drone_Wars.Model
{
    static class Network
    {
        static TcpClient tcpClient = new TcpClient();
        static NetworkStream serverStream = default(NetworkStream);
        static string readData = string.Empty;

        private static void sendMessage(int message, int id)
        {
            string idS = "" + id;
            if (id < 10) idS = "0" + id;
            //byte[] outStream = Encoding.ASCII.GetBytes(message + idS);
            byte[] outStream = new byte[2];
            outStream[0] = (byte)message;
            outStream[1] = (byte)id;
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        public static void sendTakeOff(int id)
        {
            sendMessage(1, id);
        }

        public static void sendLand(int id)
        {
            Console.WriteLine("Landing");
            sendMessage(2, id);
        }

        public static void sendStop(int id)
        {
            sendMessage(3, id);
        }

        public static void sendStopX(int id)
        {
            sendMessage(31, id);
        }

        public static void sendStopY(int id)
        {
            sendMessage(32, id);
        }
        
        public static void sendStopZ(int id)
        {
            sendMessage(33, id);
        }

        public static void sendForward(int id)
        {
            sendMessage(4, id);
        }

        public static void sendBackward(int id)
        {
            sendMessage(5, id);
        }

        public static void sendLeft(int id)
        {
            sendMessage(6, id);
        }

        public static void sendRight(int id)
        {
            sendMessage(7, id);
        }

        public static void sendUp(int id)
        {
            sendMessage(8, id);
        }

        public static void sendDown(int id)
        {
            sendMessage(9, id);
        }

        public static Position getDronePos(int id)
        {
            return null;
        }

        public static void connect()
        {
            tcpClient.Connect("127.0.0.1", 8000);
            serverStream = tcpClient.GetStream();

            sendMessage(0,0);

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
