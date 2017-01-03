using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Drone_Wars.Model {
    class PhoneListener {
        private int port;
        private bool active = true;
        TcpClient phones;

        public PhoneListener(int port)
        {
            this.port = port;
            connectPhones();
        }

        public void connectPhones()
        {
            
            NetworkStream s;
            Task taskOpenEndpoint = Task.Factory.StartNew(() =>
            {
                TcpListener l = new TcpListener(IPAddress.Any, port);
                l.Start();
                Console.WriteLine("PhoneListener active");
                phones = l.AcceptTcpClient();
                Console.WriteLine("PhoneListener connected");

                while (active)
                {
                    s = phones.GetStream();
                    byte[] message = new byte[10];
                    s.Read(message, 0, 10);
                    Console.WriteLine("Messaged received");
                    Network.command(message[0], message[1], BitConverter.ToDouble(message, 2), port);
                }

                phones.Close();
            });
        }

        public int getPort()
        {
            return port;
        }

        public void setActive(bool active)
        {
            this.active = active;
        }
    }
}
