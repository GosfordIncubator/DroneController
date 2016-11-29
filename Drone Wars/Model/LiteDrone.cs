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
        private int orientation;
        private Movement m;
        public LiteDrone(int id, int xPos, int yPos, int zPos, Movement m)
        {
            this.id = id;
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            //this.orientation = orientation;
            this.m = m;
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

        public int getOrientation()
        {
            return orientation;
        }

        public void setOrientation(int orientation)
        {
            this.orientation = orientation;
        }

        public Movement getM()
        {
            return m;
        }

        public void setM(Movement m)
        {
            this.m = m;
        }
    }
}
