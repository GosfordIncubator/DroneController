using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using DroneControl;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Drone_Wars.Model
{
    static class Network
    {
        static NetworkStream stream = default(NetworkStream);
        static List<LiteDrone> drones = new List<LiteDrone>();

        static TcpClient server;
        static Task taskOpenEndpoint;

        private static void sendMessage(int id, int message, int x, int y, int z)
        { 
            byte[] outStream = new byte[5];
            outStream[0] = (byte)id;
            outStream[1] = (byte)message;
            outStream[2] = (byte)x;
            outStream[3] = (byte)y;
            outStream[4] = (byte)z;
            stream.Write(outStream, 0, outStream.Length);
            stream.Flush();
        }

        public static void sendTakeOff(int id)
        {
            sendMessage(id, 3, 0, 0, 0);
        }

        public static void sendLand(int id)
        {
            sendMessage(id, 4, 0, 0, 0);
        }

        public static void sendStop(int id)
        {
            sendMessage(id, 6, 0, 0, 0);
        }

        public static void moveTo(int id, Position p)
        {
            sendMessage(id, 5, p.getxPos(), p.getyPos(), p.getzPos());                            
        }

        public static void sendNewDrone(int id)
        {
            sendMessage(id, 1, 0, 0, 0);
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
                drones.Add(new LiteDrone(id, x, y, z));
            }
        }

        public static void connectServer()
        {
            bool active = true;
            server = new TcpClient();
            server.Connect("localhost", 8000);
            stream = server.GetStream();

            sendMessage(0,0,0,0,0);

            taskOpenEndpoint = Task.Factory.StartNew(() =>
            {
                while (active)
                {
                    try {
                        stream = server.GetStream();
                        byte[] message = new byte[4];
                        stream.Read(message, 0, 4);

                        setDrone(message[0], message[1], message[2], message[3]);
                    } catch (ObjectDisposedException)
                    {
                        active = false;
                    } catch (IOException)
                    {
                        active = false;
                    }
                }
            });
        }

        public static void closeServer()
        {
            server.Close();
        }

        public static void connectPhones()
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
    }
}
