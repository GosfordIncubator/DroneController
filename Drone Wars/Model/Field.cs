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

        private static int droneNumber = 0;

        public static void setupField(int x, int y, int z)
        {
            fieldLengthX = x;
            fieldLengthY = y;
            maxHeight = z;
        }

        public static Drone newDrone(int x, int y, int o, int ip)
        {
            droneNumber++;
            Drone drone = new Drone(droneNumber, x, y, o, fieldLengthX, fieldLengthY, maxHeight, ip);
            drones.Add(drone);
            return drone;
        }

        public static bool ipExists(int ip)
        {
            foreach (Drone drone in drones)
            {
                if (drone.getIp() == ip) return true;
            }
            return false;
        }

        public static void removeDrone(Drone drone)
        {
            drones.Remove(drone);
        }

        public static void deleteDrone(Drone drone)
        {
            drone.stop();
            drone.land();
            drones.Remove(drone);
        }

        public static bool isOccupied(Position position)
        {
            foreach (Drone drone in drones)
            {
                if (drone.getPosition().equals(position))
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
                        if (p.equals(position))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
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
