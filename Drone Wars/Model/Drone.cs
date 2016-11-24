using Drone_Wars.Model;
using System;

namespace DroneControl
{
    class Drone
    {
        private int id;
        private Position position = new Position(0, 0, 0);
        private Position[] futPos = new Position[10];
        private bool landed;
        private string state;

        private float inputLag = 0;

        Field field;
        private int fieldLengthX;
        private int fieldLengthY;
        private int maxHeight;
        private int orientation;

        private string xAction = "None";
        private string yAction = "None";
        private string zAction = "None";

        private int xActionCount = 0;
        private int yActionCount = 0;
        private int zActionCount = 0;

        public Drone(int id, int xPos, int yPos, int orientation, int fieldLengthX, int fieldLengthY, int maxHeight, Field field)
        {
            this.id = id;
            setXPos(xPos);
            setYPos(yPos);
            setZPos(0);
            this.orientation = orientation;
            this.fieldLengthX = fieldLengthX;
            this.fieldLengthY = fieldLengthY;
            this.maxHeight = maxHeight;
            this.field = field;
            landed = true;
            state = "landed";
        }
        
        public void operate()
        {
            if (inputLag == 0)
            {
                if (yActionCount == 0 && !yAction.Equals("None"))
                {
                    stopY();
                }

                if (xActionCount == 0 && !xAction.Equals("None"))
                {
                    stopX();
                }

                if (zActionCount == 0 && !zAction.Equals("None"))
                {
                    stopZ();
                }

                if (yActionCount > 0)
                {
                    switch (yAction)
                    {
                        case "forward":
                            if (!isSafeForward(position))
                            {
                                stopY();
                            }
                            else position = moveForward(position);
                            break;
                        case "backward":
                            if (!isSafeBackward(position))
                            {
                                stopY();
                            }
                            else position = moveBackward(position);
                            break;
                    }
                    yActionCount--;
                }

                if (xActionCount > 0)
                {
                    switch (xAction)
                    {
                        case "left":
                            if (!isSafeLeft(position))
                            {
                                stopX();
                            }
                            else position = moveLeft(position);
                            break;
                        case "right":
                            if (!isSafeRight(position))
                            {
                                stopX();
                            }
                            else position = moveRight(position);
                            break;
                    }
                    xActionCount--;
                }

                if (zActionCount > 0)
                {
                    switch (zAction)
                    {
                        case "up":
                            if (!isSafeUp(position))
                            {
                                stopZ();
                            }
                            else position = moveUp(position);
                            break;
                        case "down":
                            if (!isSafeDown(position))
                            {
                                land();
                            }
                            else position = moveDown(position);
                            break;
                    }
                    zActionCount--;
                }
            }
            else inputLag--;

            
            for (int i = 0; i < futPos.Length; i++)
            {
                futPos[i] = getFuturePos(i);
            }
        }

        public void command(string action, int actionCount)
        {
            if (!landed)
            {
                switch(action)
                {
                    case "forward":
                        if (isSafeForward(position))
                        {
                            if (inputLag == 0) inputLag++;
                            Network.sendForward();
                            yAction = action;
                            yActionCount = actionCount;
                        }
                        break;
                    case "backward":
                        if (isSafeBackward(position))
                        {
                            if (inputLag == 0) inputLag++;
                            Network.sendBackward();
                            yAction = action;
                            yActionCount = actionCount;
                        }
                        break;
                    case "left":
                        if (isSafeLeft(position))
                        {
                            if (inputLag == 0) inputLag+=2;
                            Network.sendLeft();
                            xAction = action;
                            xActionCount = actionCount;
                        }
                        break;
                    case "right":
                        if (isSafeRight(position))
                        {
                            if (inputLag == 0) inputLag+=2;
                            Network.sendRight();
                            xAction = action;
                            xActionCount = actionCount;
                        }
                        break;
                    case "up":
                        if (isSafeUp(position))
                        {
                            if (inputLag == 0) inputLag++;
                            Network.sendUp();
                            zAction = action;
                            zActionCount = actionCount;
                        }
                        break;
                    case "down":
                        if (isSafeDown(position))
                        {
                            if (inputLag == 0) inputLag++;
                            Network.sendDown();
                            zAction = action;
                            zActionCount = actionCount;
                        }
                        else
                        {
                            land();
                        }
                        break;
                }
            }
            else throw new LandedException();
        }

        public void takeOff()
        {
            if (getZPos() == 0 && landed == true)
            {
                setZPos(1);
                landed = false;
                state = "flying";
                Network.sendTakeOff();
            }
        }

        public void land()
        {
            if (!landed)
            {
                Network.sendLand();
                setZPos(0);
                landed = true;
                state = "landed";
                stop();
            }
        }

        public void stop()
        {
            Network.sendStop();
            xAction = "None";
            xActionCount = 0;
            yAction = "None";
            yActionCount = 0;
            zAction = "None";
            zActionCount = 0;
        }

        private void stopX()
        {
            Network.sendStopX();
            xAction = "None";
            xActionCount = 0;
        }

        private void stopY()
        {
            Network.sendStopY();
            yAction = "None";
            yActionCount = 0;
        }

        private void stopZ()
        {
            Network.sendStopZ();
            zAction = "None";
            zActionCount = 0;
        }

        private Position moveForward(Position position)
        {
            switch (orientation)
            {
                //Facing North
                case 0:
                    //North
                    return new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                //Facing North-East
                case 1:
                    //North-East
                    return new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                //Facing East
                case 2:
                    //East
                    return new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                //Facing South-East
                case 3:
                    //South-East
                    return new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                //Facing South
                case 4:
                    //South
                    return new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                //Facing South-West
                case 5:
                    //South-West
                    return new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                //Facing West
                case 6:
                    //West
                    return new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                //Facing North-West
                case 7:
                    //North-West
                    return new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
            }
            return position;
        }

        private Position moveBackward(Position position)
        {
            switch (orientation)
            {
                //Facing North
                case 0:
                    //South
                    return new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                //Facing North-East
                case 1:
                    //South-West
                    return new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                //Facing East
                case 2:
                    //West
                    return new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                //Facing South-East
                case 3:
                    //North-West
                    return new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                //Facing South
                case 4:
                    //North
                    return new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                //Facing South-West
                case 5:
                    //North-East
                    return new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                //Facing West
                case 6:
                    //East
                    return new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                //Facing North-West
                case 7:
                    //South-East
                    return new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
            }
            return position;
        }

        private Position moveLeft(Position position)
        {
            switch (orientation)
            {
                //Facing North
                case 0:
                    //West
                    return new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                //Facing North-East
                case 1:
                    //North-West
                    return new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                //Facing East
                case 2:
                    //North
                    return new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                //Facing South-East
                case 3:
                    //North-East
                    return new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                //Facing South
                case 4:
                    //East
                    return new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                //Facing South-West
                case 5:
                    //South-East
                    return new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                //Facing West
                case 6:
                    //South
                    return new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                //Facing North-West
                case 7:
                    //South-West
                    return new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
            }
            return position;
        }

        private Position moveRight(Position position)
        {
            switch (orientation)
            {
                //Facing North
                case 0:
                    //East
                    return new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                //Facing North-East
                case 1:
                    //South-East
                    return new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                //Facing East
                case 2:
                    //South
                    return new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                //Facing South-East
                case 3:
                    //South-West
                    return new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                //Facing South
                case 4:
                    //West
                    return new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                //Facing South-West
                case 5:
                    //North-West
                    return new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                //Facing West
                case 6:
                    //North
                    return new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                //Facing North-West
                case 7:
                    //North-East
                    return new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
            }
            return position;
        }

        private Position moveUp(Position position)
        {
            return new Position(position.getxPos(), position.getyPos(), position.getzPos() + 1);
        }

        private Position moveDown(Position position)
        {
            return new Position(position.getxPos(), position.getyPos(), position.getzPos() - 1);
        }

        private bool isSafeForward(Position position)
        {
            Position p;
            switch (orientation)
            {
                //Facing North
                case 0:
                    //North
                    p = new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this,p);
                //Facing North-East
                case 1:
                    //North-East
                    p = new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing East
                case 2:
                    //East
                    p = new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South-East
                case 3:
                    //South-East
                    p = new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South
                case 4:
                    //South
                    p = new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South-West
                case 5:
                    //South-West
                    p = new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing West
                case 6:
                    //West
                    p = new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing North-West
                case 7:
                    //North-West
                    p = new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
            }
            return false;
        }

        private bool isSafeBackward(Position position)
        {
            Position p;
            switch (orientation)
            {
                //Facing North
                case 0:
                    //South
                    p = new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing North-East
                case 1:
                    //South-West
                    p = new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing East
                case 2:
                    //West
                    p = new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South-East
                case 3:
                    //North-West
                    p = new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South
                case 4:
                    //North
                    p = new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South-West
                case 5:
                    //North-East
                    p = new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing West
                case 6:
                    //East
                    p = new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing North-West
                case 7:
                    //South-East
                    p = new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
            }
            return false;
        }

        private bool isSafeLeft(Position position)
        {
            Position p;
            switch(orientation)
            {
                //Facing North
                case 0:
                    //West
                    p = new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing North-East
                case 1:
                    //North-West
                    p = new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing East
                case 2:
                    //North
                    p = new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South-East
                case 3:
                    //North-East
                    p = new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South
                case 4:
                    //East
                    p = new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South-West
                case 5:
                    //South-East
                    p = new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing West
                case 6:
                    //South
                    p = new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing North-West
                case 7:
                    //South-West
                    p = new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
            }
            return false;
        }

        private bool isSafeRight(Position position)
        {
            Position p;
            switch (orientation)
            {
                //Facing North
                case 0:
                    //East
                    p = new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing North-East
                case 1:
                    //South-East
                    p = new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing East
                case 2:
                    //South
                    p = new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South-East
                case 3:
                    //South-West
                    p = new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South
                case 4:
                    //West
                    p = new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing South-West
                case 5:
                    //North-West
                    p = new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing West
                case 6:
                    //North
                    p = new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
                //Facing North-West
                case 7:
                    //North-East
                    p = new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                    return !field.isOccupied(p) && p.isInside(fieldLengthX, fieldLengthY, maxHeight) && !field.crossesPath(this, p);
            }
            return false;
        }

        private bool isSafeUp(Position position)
        {
            return position.getzPos() + 1 < maxHeight;
        }

        private bool isSafeDown(Position position)
        {
            return position.getzPos() - 1 > 0;
        }

        public Position getFuturePos(int i)
        {
            Position position = new Position(0,0,0);
            position.set(this.position); ;

            string tempYAction = yAction;
            string tempXAction = xAction;
            string tempZAction = zAction;

            int tempYActionCount = yActionCount;
            int tempXActionCount = xActionCount;
            int tempZActionCount = zActionCount;

            for (int k = 0; k < i; k++)
            {
                if (tempYActionCount == 0 && !tempYAction.Equals("None"))
                {
                    tempYAction = "None";
                    tempYActionCount = 0;
                }

                if (tempXActionCount == 0 && !tempXAction.Equals("None"))
                {
                    tempXAction = "None";
                    tempXActionCount = 0;
                }

                if (tempZActionCount == 0 && !tempZAction.Equals("None"))
                {
                    tempZAction = "None";
                    tempZActionCount = 0;
                }

                if (tempYActionCount > 0)
                {
                    switch (tempYAction)
                    {
                        case "forward":
                            if (!isSafeForward(position))
                            {
                                tempYAction = "None";
                                tempYActionCount = 0;
                            }
                            else position = moveForward(position);
                            break;
                        case "backward":
                            if (!isSafeBackward(position))
                            {
                                tempYAction = "None";
                                tempYActionCount = 0;
                            }
                            else position = moveBackward(position);
                            break;
                    }
                    tempYActionCount--;
                }

                if (tempXActionCount > 0)
                {
                    switch (tempXAction)
                    {
                        case "left":
                            if (!isSafeLeft(position))
                            {
                                tempXAction = "None";
                                tempXActionCount = 0;
                            }
                            else position = moveLeft(position);
                            break;
                        case "right":
                            if (!isSafeRight(position))
                            {
                                tempXAction = "None";
                                tempXActionCount = 0;
                            }
                            else position = moveRight(position);
                            break;
                    }
                    tempXActionCount--;
                }

                if (tempZActionCount > 0)
                {
                    switch (tempZAction)
                    {
                        case "up":
                            if (!isSafeUp(position))
                            {
                                tempZAction = "None";
                                tempZActionCount = 0;
                            }
                            else position = moveUp(position);
                            break;
                        case "down":
                            if (!isSafeDown(position))
                            {
                                position.setzPos(0);
                                xAction = "None";
                                xActionCount = 0;
                                yAction = "None";
                                yActionCount = 0;
                                zAction = "None";
                                zActionCount = 0;
                            }
                            else position = moveDown(position);
                            break;
                    }
                    tempZActionCount--;
                }
            }
            return position;
        }

        public int getXPos()
        {
            return position.getxPos();
        }

        public void setXPos(int xPos)
        {
            position.setxPos(xPos);
        }

        public int getYPos()
        {
            return position.getyPos();
        }

        public void setYPos(int yPos)
        {
            position.setyPos(yPos);
        }

        public int getZPos()
        {
            return position.getzPos();
        }

        public void setZPos(int zPos)
        {
            position.setzPos(zPos);
        }

        public int getHeight()
        {
            return position.getzPos();
        }

        public void setHeight(int height)
        {
            position.setzPos(height);
        }

        public Position getPosition()
        {
            return position;
        }

        public int getOrientation()
        {
            return orientation;
        }

        public string getState()
        {
            return state;
        }

        public void setState(string state)
        {
            this.state = state;
        }

        public int getMoves()
        {
            return Math.Max(xActionCount, Math.Max(yActionCount, zActionCount));
        }

        public Position[] getFutPos()
        {
            return futPos;
        }

        override
        public string ToString()
        {
            return "Drone " + id;
        }
    }
}