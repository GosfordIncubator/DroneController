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

        public bool isInside(int x, int y, int z)
        {
            if (xPos < 0) return false;
            if (xPos > x-1) return false;
            if (yPos < 0) return false;
            if (yPos > y-1) return false;
            if (zPos < 0) return false;
            if (zPos > z) return false;
            return true;
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