using System.Collections.Generic;
using System.ComponentModel;

namespace DroneControl
{
    class Field
    {
        private BindingList<Drone> drones = new BindingList<Drone>();
        private int fieldLengthX;
        private int fieldLengthY;
        private int maxHeight;

        private int droneNumber = 0;

        public Field(int fieldLengthX, int fieldLengthY, int maxHeight)
        {
            this.fieldLengthX = fieldLengthX;
            this.fieldLengthY = fieldLengthY;
            this.maxHeight = maxHeight;
        }

        public Drone newDrone(int x, int y, int o, int ip)
        {
            droneNumber++;
            Drone drone = new Drone(droneNumber, x, y, o, fieldLengthX, fieldLengthY, maxHeight, this, ip);
            drones.Add(drone);
            return drone;
        }

        public bool ipExists(int ip)
        {
            foreach (Drone drone in drones)
            {
                if (drone.getIp() == ip) return true;
            }
            return false;
        }

        public void removeDrone(Drone drone)
        {
            drones.Remove(drone);
        }

        public void deleteDrone(Drone drone)
        {
            drones.Remove(drone);
        }

        public bool isOccupied(Position position)
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

        public bool crossesPath(Drone user, Position position)
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

        public int getFieldLengthX()
        {
            return fieldLengthX;
        }

        public int getFieldLengthY()
        {
            return fieldLengthY;
        }

        public int getMaxHeight()
        {
            return maxHeight;
        }

        public BindingList<Drone> getDrones()
        {
            return drones;
        }
    }
}
