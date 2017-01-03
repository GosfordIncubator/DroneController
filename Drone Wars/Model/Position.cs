using Drone_Wars.Model;
using System;

namespace DroneControl
{
    public class Position
    {
        private int xPos;
        private int yPos;
        private int zPos;
        private double orientation;

        public Position(int xPos, int yPos, int zPos, double o)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            this.orientation = o;
        }

        public bool equals(Position position)
        {
            if (position != null) return (xPos == position.getxPos() && yPos == position.getyPos());
            return false;
        }

        public bool isInside()
        {
            int x = Field.getFieldLengthX();
            int y = Field.getFieldLengthY();
            int z = Field.getMaxHeight();
            int offset = Field.getBorderRadius();
            if (xPos < 0 + offset) return false;
            if (xPos > x - offset) return false;
            if (yPos < 0 + offset) return false;
            if (yPos > y - offset) return false;
            if (zPos < 0) return false;
            if (zPos > z) return false;
            return true;
        }

        public void set(Position position)
        {
            xPos = position.getxPos();
            yPos = position.getyPos();
            zPos = position.getzPos();
            orientation = position.getOrientation();
        }

        public int getxPos()
        {
            return xPos;
        }

        public void setxPos(int xPos)
        {
            this.xPos = xPos;
        }

        public int getyPos()
        {
            return yPos;
        }

        public void setyPos(int yPos)
        {
            this.yPos = yPos;
        }

        public int getzPos()
        {
            return zPos;
        }

        public void setzPos(int zPos)
        {
            this.zPos = zPos;
        }

        public double getOrientation()
        {
            return orientation;
        }

        public void setOrientation(double o)
        {
            this.orientation = o;
        }

        override
        public string ToString()
        {
            return "(" + xPos + "," + yPos + ")";
        }
    }
}