using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using DroneControl;
using System.Net;
using System.Collections.Generic;

namespace Drone_Wars.Model
{
    static class Network
    {
        static NetworkStream stream = default(NetworkStream);
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
                    stream = tcp.GetStream();
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
