using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using DroneControl;
using System.Net;
using System.Collections.Generic;
using System.IO;

namespace Drone_Wars.Model
{
    static class Network
    {
        static NetworkStream stream = default(NetworkStream);
        static List<LiteDrone> drones = new List<LiteDrone>();
        static List<PhoneListener> listeners = new List<PhoneListener>();

        static TcpClient server;
        static TcpClient phones;
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
            return new Position(drone.getXPos(), drone.getYPos(), drone.getZPos(), drone.getOrientation());
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

        private static void setDrone(int id, int x, int y, int z, double o)
        {
            try
            {
                LiteDrone d = getDrone(id);
                d.setXPos(x);
                d.setYPos(y);
                d.setZPos(z);
                d.setOrientation(o);
            } catch (NullReferenceException)
            {
                drones.Add(new LiteDrone(id, x, y, z, o));
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
                        byte[] message = new byte[5];
                        stream.Read(message, 0, message.Length);
                        Console.WriteLine("Node received");
                        setDrone(message[0], message[1], message[2], message[3], message[4]);
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
            try
            {
                server.Close();
                phones.Close();
            } catch (NullReferenceException)
            {
                Console.WriteLine("Server not found.");
            }
        }

        public static void connectPhones()
        {
            
            Task taskOpenEndpoint = Task.Factory.StartNew(() =>
            {
                int port = 1;
                TcpListener l = new TcpListener(IPAddress.Any, 8500);
                l.Start();

                while (true)
                {
                    Console.WriteLine("Waiting for a phone connection...");
                    phones = l.AcceptTcpClient();
                    Console.WriteLine("Phone found");
                    NetworkStream s = phones.GetStream();

                    try
                    {
                        port++;
                        Console.WriteLine(port);
                        int emptyId = Field.getUnconnectedDrone().getId();

                        byte[] outStream = new byte[2];
                        outStream[0] = (byte)emptyId;
                        outStream[1] = (byte)port;
                        s.Write(outStream, 0, 2);
                        s.Flush();

                        phones.Close();

                        Field.connectToDrone(emptyId);
                        listeners.Add(new PhoneListener(port + 8000));
                    } catch (NullReferenceException)
                    {
                        Console.WriteLine("Invalid drone");
                        byte[] msg = new byte[1];
                        s.Write(msg, 0, msg.Length);
                    }
                }
            });
        }

        public static void command(int id, int msg, double theta, int port)
        {
            Drone d = Field.getDrone(id);
            try
            {
                if (d != null)
                {
                    switch (msg)
                    {
                        case 1:
                            Console.WriteLine("Drone takeoff");
                            d.takeOff();
                            break;
                        case 2:
                            Console.WriteLine("Drone land");
                            d.land();
                            break;
                        case 3:
                            Console.WriteLine("Drone stop");
                            d.stop();
                            break;
                        case 4:
                            Console.WriteLine("Drone move");
                            d.command("move", 10, theta);
                            break;
                        case 5:
                            Console.WriteLine("Drone move up");
                            d.command("up", 1, theta);
                            break;
                        case 6:
                            Console.WriteLine("Drone move down");
                            d.command("down", 1, theta);
                            break;
                        case 7:
                            Console.WriteLine("Disconnected");
                            Field.disconnectDrone(id);
                            PhoneListener l = getPhoneListener(port);
                            l.setActive(false);
                            listeners.Remove(l);
                            break;
                    }
                }
            } catch (LandedException)
            {
                Console.WriteLine("Unable to move, drone landed.");
            } catch (NullReferenceException)
            {
                Console.WriteLine("Drone with id " + id + " does not exist.");
            }
        }

        private static PhoneListener getPhoneListener(int port)
        {
            foreach (PhoneListener listener in listeners)
            {
                if (listener.getPort() == port) return listener;
            }
            return null;
        }
    }
}
