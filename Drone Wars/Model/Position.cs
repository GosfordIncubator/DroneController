using Drone_Wars.Model;
using System;

namespace DroneControl
{
    public class Position
    {
        private int xPos;
        private int yPos;
        private int zPos;

        public Position(int xPos, int yPos, int zPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
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
            if (xPos < 0) return false;
            if (xPos > x-1) return false;
            if (yPos < 0) return false;
            if (yPos > y-1) return false;
            if (zPos < 0) return false;
            if (zPos > z) return false;
            return true;
        }

        public Movement getPath(Position origin)
        {
            int xDistance = xPos - origin.getxPos();
            int yDistance = yPos - origin.getyPos();
            int zDistance = zPos - origin.getzPos();

            string xDirection = "None";
            string yDirection = "None";
            string zDirection = "None";

            if (xDistance > 0) xDirection = "West"; else if (xDistance < 0) xDirection = "East";
            if (yDistance > 0) yDirection = "North"; else if (yDistance < 0) yDirection = "South";
            if (zDistance > 0) zDirection = "down"; else if (zDistance < 0) zDirection = "up";

            return new Movement(Math.Abs(xDistance), Math.Abs(yDistance), Math.Abs(zDistance), xDirection,yDirection,zDirection);
        }

        public void set(Position position)
        {
            xPos = position.getxPos();
            yPos = position.getyPos();
            zPos = position.getzPos();
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
    }
}