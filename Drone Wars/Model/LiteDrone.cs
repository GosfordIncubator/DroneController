using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drone_Wars.Model
{
    class LiteDrone
    {
        private int id;
        private int xPos;
        private int yPos;
        private int zPos;
        private double orientation;
        public LiteDrone(int id, int xPos, int yPos, int zPos, double orientation)
        {
            this.id = id;
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            this.orientation = orientation;
        }

        public int getId()
        {
            return id;
        }

        public int getXPos()
        {
            return xPos;
        }

        public void setXPos(int xPos)
        {
            this.xPos = xPos;
        }

        public int getYPos()
        {
            return yPos;
        }

        public void setYPos(int yPos)
        {
            this.yPos = yPos;
        }

        public int getZPos()
        {
            return zPos;
        }

        public void setZPos(int zPos)
        {
            this.zPos = zPos;
        }

        public double getOrientation()
        {
            return orientation;
        }

        public void setOrientation(double orientation)
        {
            this.orientation = orientation;
        }
    }
}
