using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using DroneControl;
using System.Net;
using System.Collections.Generic;

namespace Drone_Wars.Model
{
    static class Network
    {
        static TcpClient tcp = new TcpClient();
        static NetworkStream stream = default(NetworkStream);
        static List<LiteDrone> drones = new List<LiteDrone>();

        private static void sendMessage(int message, int id)
        { 
            byte[] outStream = new byte[2];
            outStream[0] = (byte)message;
            outStream[1] = (byte)id;
            stream.Write(outStream, 0, outStream.Length);
            stream.Flush();
        }

        public static void sendTakeOff(int id)
        {
            sendMessage(1, id);
        }

        public static void sendLand(int id)
        {
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

        public static void connect()
        {
            tcp.Connect("127.0.0.1", 8000);
            stream = tcp.GetStream();

            sendMessage(0,0);

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

                    //We have read the message.
                    setDrone(message[0], message[1], message[2], message[3]);
                    Thread.Sleep(500);
                }
            });
        }
    }
}
