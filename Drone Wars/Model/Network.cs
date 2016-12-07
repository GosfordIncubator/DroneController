using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using DroneControl;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Drone_Wars.Model
{
    static class Network
    {
        static NetworkStream stream;
        static List<LiteDrone> drones = new List<LiteDrone>();

        private static void sendMessage(int message, int id, int ip)
        { 
            byte[] outStream = new byte[3];
            outStream[0] = (byte)message;
            outStream[1] = (byte)id;
            outStream[2] = (byte)ip;
            stream.Write(outStream, 0, outStream.Length);
            stream.Flush();
        }

        public static void sendTakeOff(int id)
        {
            sendMessage(1, id, 0);
        }

        public static void sendLand(int id)
        {
            sendMessage(2, id, 0);
        }

        public static void sendStop(int id)
        {
            sendMessage(3, id, 0);
        }

        public static void sendStopX(int id)
        {
            sendMessage(31, id, 0);
        }

        public static void sendStopY(int id)
        {
            sendMessage(32, id, 0);
        }
        
        public static void sendStopZ(int id)
        {
            sendMessage(33, id, 0);
        }

        public static void sendForward(int id)
        {
            sendMessage(4, id, 0);
        }

        public static void sendBackward(int id)
        {
            sendMessage(5, id, 0);
        }

        public static void sendLeft(int id)
        {
            sendMessage(6, id, 0);
        }

        public static void sendRight(int id)
        {
            sendMessage(7, id, 0);
        }

        public static void sendUp(int id)
        {
            sendMessage(8, id, 0);
        }

        public static void sendDown(int id)
        {
            sendMessage(9, id, 0);
        }

        public static void sendNewDrone(int id, int ip)
        {
            sendMessage(10, id, ip);
        }

        public static Position getDronePos(int id)
        {
            LiteDrone drone = getDrone(id);
            return new Position(drone.getXPos(),drone.getYPos(),drone.getZPos());
        }

        private static LiteDrone getDrone(int id)
        {
            foreach(LiteDrone drone in drones)
            {
                if (drone.getId() == id)
                {
                    return drone;
                }
            }
            throw new NullReferenceException();
        }

        private static void setDrone(int id, int x, int y, int z)
        {
            bool found = false;
            foreach (LiteDrone drone in drones)
            {
                if (drone.getId() == id)
                {
                    drone.setXPos(x);
                    drone.setYPos(y);
                    drone.setZPos(z);
                    found = true;
                }
            }
            if (!found)
            {
                drones.Add(new LiteDrone(id, x, y, z, null));
            }
        }

        public static void connect()
        {
            TcpClient tcp = new TcpClient();
            tcp.Connect("localhost", 8000);
            stream = tcp.GetStream();

            sendMessage(0,0,0);

            Task taskOpenEndpoint = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    byte[] message = new byte[4];

                    try
                    {
                        stream.Read(message, 0, 4);
                    }
                    catch
                    {
                        Console.WriteLine("Network error");
                    }
                    
                    setDrone(message[0], message[1], message[2], message[3]);
                }
            });
        }

        public static Movement getMovement(int id)
        {
            return getDrone(id).getM();
        }

        public static void setMovement(int id, Movement m)
        {
            getDrone(id).setM(m);
        }

        public static void connect2()
        {
            TcpClient phones;
            Task taskOpenEndpoint = Task.Factory.StartNew(() =>
            {
                TcpListener l = new TcpListener(IPAddress.Any, 8001);
                l.Start();

                byte[] message = new byte[2];

                while (true)
                {
                    Console.WriteLine("Waiting for a phone connection...");
                    phones = l.AcceptTcpClient();
                    Console.WriteLine("Phone found");

                    NetworkStream s = phones.GetStream();
                    s.Read(message, 0, 2);

                    int id = message[0];
                    int msg = message[1];
                    Drone d = Field.getDrone(id);

                    Console.WriteLine(id);
                    Console.WriteLine(msg);

                    if (msg == 0)
                    {
                        Console.WriteLine("Connected");
                    }

                    if (d != null)
                    {
                        if (msg == 1)
                        {
                            Console.WriteLine("Drone takeoff");
                            d.takeOff();
                        }
                        if (msg == 2)
                        {
                            Console.WriteLine("Drone land");
                            d.land();
                        }
                        if (msg == 3)
                        {
                            Console.WriteLine("Drone stop");
                            d.stop();
                        }
                        if (msg == 4)
                        {
                            Console.WriteLine("Drone move forward");
                            d.command("forward", 1);
                        }
                        if (msg == 5)
                        {
                            Console.WriteLine("Drone move backward");
                            d.command("backward", 1);
                        }
                        if (msg == 6)
                        {
                            Console.WriteLine("Drone move left");
                            d.command("left", 1);
                        }
                        if (msg == 7)
                        {
                            Console.WriteLine("Drone move right");
                            d.command("right", 1);
                        }
                        if (msg == 8)
                        {
                            Console.WriteLine("Drone move up");
                            d.command("up", 1);
                        }
                        if (msg == 9)
                        {
                            Console.WriteLine("Drone move down");
                            d.command("down", 1);
                        }
                    }
                    phones.Close();
                }
            });
        }

        static IPEndPoint serverAddress;
        static Socket clientSocket;
        static byte[] message;
        static int width;
        
        public static void connect3(int x, int y)
        {
            serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8002);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(serverAddress);
            message = new byte[x*y];

            width = x;
            byte[] toSendLenBytes = BitConverter.GetBytes(message.Length);
            message[0] = (byte)x;
            message[1] = (byte)y;
            clientSocket.Send(toSendLenBytes);
            clientSocket.Send(message);
        }

        public static void updatePos()
        {
            for (int j = 0; j < message.Length; j++)
            {
                message[j] = 0;
            }
            byte[] toSendLenBytes = BitConverter.GetBytes(message.Length);
            foreach (Drone d in Field.getDrones())
            {
                for (int i = 1; i < d.getFutPos().Length; i++)
                {
                    Position p = d.getFutPos()[i];
                    message[p.getyPos() * width + p.getxPos()] = 2;
                }
            }
            foreach (Drone d in Field.getDrones())
            {
                message[d.getYPos() * width + d.getXPos()] = 1;
            }
            try
            {
                clientSocket.Send(toSendLenBytes);
                clientSocket.Send(message);
            } catch (SocketException)
            {
                Application.Exit();
            }
        }
    }
}
