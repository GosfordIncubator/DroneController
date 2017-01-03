using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DroneControl
{
    static class Field
    {
        private static BindingList<Drone> drones = new BindingList<Drone>();
        private static int fieldLengthX;
        private static int fieldLengthY;
        private static int maxHeight;

        private static int borderRadius = 20;

        private static int droneNumber = 0;

        public static void setupField(int x, int y, int z)
        {
            fieldLengthX = x;
            fieldLengthY = y;
            maxHeight = z;
        }

        public static void newDrone(int x, int y, double o)
        {
            droneNumber++;
            drones.Add(new Drone(droneNumber, x, y, o));
        }

        public static void removeDrone(Drone drone)
        {
            drone.stop();
            drone.land();
            drones.Remove(drone);
        }

        public static bool isOccupied(Drone user, Position position)
        {
            List<Drone> f = new List<Drone>();
            f.AddRange(drones);
            f.Remove(user);
            foreach (Drone drone in f)
            {
                if (Math.Pow((drone.getXPos() - position.getxPos()), 2) + Math.Pow((drone.getYPos() - position.getyPos()), 2) <= Math.Pow((2 * borderRadius), 2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool crossesPath(Drone user, Position position)
        {
            List<Drone> f = new List<Drone>();
            f.AddRange(drones);
            f.Remove(user);
            foreach (Drone drone in f)
            {
                foreach (Position p in drone.getFutPos())
                {
                    if (p != null)
                    {
                        if (Math.Pow((p.getxPos() - position.getxPos()), 2) + Math.Pow((p.getyPos() - position.getyPos()), 2) <= Math.Pow((2 * borderRadius), 2))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void disconnectDrone(int id)
        {
            Drone drone = getDrone(id);
            drone.setConnected(false);
            drone.land();
        } 

        public static Drone getUnconnectedDrone()
        {
            foreach (Drone drone in drones)
            {
                if (!drone.getConnected())
                {
                    return drone;
                }
            }
            return null;
        }

        public static void connectToDrone(int id)
        {
            getDrone(id).setConnected(true);
        }

        public static int getFieldLengthX()
        {
            return fieldLengthX;
        }

        public static int getFieldLengthY()
        {
            return fieldLengthY;
        }

        public static int getMaxHeight()
        {
            return maxHeight;
        }

        public static int getBorderRadius()
        {
            return borderRadius;
        }

        public static BindingList<Drone> getDrones()
        {
            return drones;
        }

        public static Drone getDrone(int id)
        {
            foreach (Drone drone in drones)
            {
                if (drone.getId() == id) return drone;
            }
            return null;
        }
    }
}

