using Drone_Wars.Model;
using System;

namespace DroneControl
{
    class Drone
    {
        private int id;
        private Position position = new Position(0, 0, 0, 0);
        private Position[] futPos = new Position[50];
        private bool landed;
        private string state;
        private bool connected = false;

        private int inputLag = 0;
        private string horizontalAction = "none";
        private string verticalAction = "none";
        private int horizontalActionCount = 0;
        private int verticalActionCount = 0;
        private double theta = 0;

        public Drone(int id, int xPos, int yPos, double orientation)
        {
            this.id = id;
            setXPos(xPos);
            setYPos(yPos);
            setZPos(0);
            setOrientation(orientation);
            landed = true;
            state = "landed";
            Network.sendNewDrone(id);
        }
        
        public void operate()
        {
            //position = Network.getDronePos(id);

            for (int i = 0; i < futPos.Length; i++)
            {
                futPos[i] = getFuturePos(i + 1);
                //Console.WriteLine(futPos[i].getyPos());
            }

            if (inputLag == 0)
            {
                if (horizontalAction.Equals("move") && horizontalActionCount > 0)
                {
                    if (!isSafe(position, theta + getOrientation()))
                    {
                        stop();
                    } else
                    {
                        position = move(position, theta + getOrientation());
                        Network.moveTo(id, position);
                        horizontalActionCount--;
                    }
                }
                
                if (verticalAction.Equals("up") && verticalActionCount > 0)
                {
                    if (!isSafeUp(position))
                    {
                        stop();
                    } else
                    {
                        position = moveUp(position);
                        Network.moveTo(id, position);
                        verticalActionCount--;
                    }
                }

                if (verticalAction.Equals("down") && verticalActionCount > 0)
                {
                    if (!isSafeDown(position))
                    {
                        stop();
                    } else
                    {
                        position = moveDown(position);
                        Network.moveTo(id, position);
                        verticalActionCount--;
                    }
                }
            } else inputLag--;
        }

        public void command(string action, int actionCount, double theta)
        {
            if (!landed)
            {
                switch(action)
                {
                    case "move":
                        if (isSafe(position, theta))
                        {
                            if (inputLag == 0) inputLag++;
                            horizontalAction = action;
                            horizontalActionCount = actionCount;
                            this.theta = theta;
                        }
                        break;
                    case "up":
                        if (isSafeUp(position))
                        {
                            if (inputLag == 0) inputLag++;
                            verticalAction = action;
                            verticalActionCount = actionCount;
                        }
                        break;
                    case "down":
                        if (isSafeDown(position))
                        {
                            if (inputLag == 0) inputLag++;
                            verticalAction = action;
                            verticalActionCount = actionCount;
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
                Network.sendTakeOff(id);
            }
        }

        public void land()
        {
            if (!landed)
            {
                Network.sendLand(id);
                setZPos(0);
                landed = true;
                state = "landed";
                stop();
            }
        }

        public void stop()
        {
            Network.sendStop(id);
            horizontalAction = "none";
            horizontalActionCount = 0;
            verticalAction = "none";
            verticalActionCount = 0;
        }

        private int increment = 25;
        private int heightIncrement = 0;

        private Position move(Position position, double theta)
        {
            double PI = Math.PI;
            double x;
            double y;

            if (theta == 0) theta = 2 * PI;

            if (theta > Math.PI*1.5)
            {
                x = increment * Math.Sin(2 * PI - theta);
                y = increment * Math.Cos(2 * PI - theta);
                return new Position(position.getxPos() - (int)x, position.getyPos() - (int)y, position.getzPos(), position.getOrientation());
            } else if (theta > Math.PI)
            {
                x = increment * Math.Sin(theta - PI);
                y = increment * Math.Cos(theta - PI);
                return new Position(position.getxPos() - (int)x, position.getyPos() + (int)y, position.getzPos(), position.getOrientation());
            } else if (theta > Math.PI*0.5)
            {
                x = increment * Math.Sin(PI - theta);
                y = increment * Math.Cos(PI - theta);
                return new Position(position.getxPos() + (int)x, position.getyPos() + (int)y, position.getzPos(), position.getOrientation());
            } else if (theta > 0)
            {
                x = increment * Math.Sin(theta);
                y = increment * Math.Cos(theta);
                return new Position(position.getxPos() + (int)x, position.getyPos() - (int)y, position.getzPos(), position.getOrientation());
            }
            throw new NullReferenceException();
        }

        private Position moveUp(Position position)
        {
            return new Position(getXPos(), getYPos(), getZPos() + heightIncrement, getOrientation());
        }

        private Position moveDown(Position position)
        {
            Position p = new Position(getXPos(), getYPos(), getZPos() - heightIncrement, getOrientation());
            if (p.getzPos() < 10)
            {
                land();
                return null;
            } else return p;
        }

        private bool isSafe(Position position, double theta)
        {
            double PI = Math.PI;
            double x;
            double y;

            if (theta > Math.PI * 1.5)
            {
                x = increment * Math.Sin(2 * PI - theta);
                y = increment * Math.Cos(2 * PI - theta);
                Position p = new Position(getXPos() - (int)x, getYPos() - (int)y, getZPos(), getOrientation());
                return !Field.isOccupied(this, p) && p.isInside() && !Field.crossesPath(this, p);
            } else if (theta > Math.PI)
            {
                x = increment * Math.Sin(theta - PI);
                y = increment * Math.Cos(theta - PI);
                Position p = new Position(getXPos() - (int)x, getYPos() + (int)y, getZPos(), getOrientation());
                return !Field.isOccupied(this, p) && p.isInside() && !Field.crossesPath(this, p);
            } else if (theta > Math.PI * 0.5)
            {
                x = increment * Math.Sin(PI - theta);
                y = increment * Math.Cos(PI - theta);
                Position p = new Position(getXPos() + (int)x, getYPos() + (int)y, getZPos(), getOrientation());
                return !Field.isOccupied(this, p) && p.isInside() && !Field.crossesPath(this, p);
            } else if (theta > 0)
            {
                x = increment * Math.Sin(theta);
                y = increment * Math.Cos(theta);
                Position p = new Position(getXPos() + (int)x, getYPos() - (int)y, getZPos(), getOrientation());
                return !Field.isOccupied(this, p) && p.isInside() && !Field.crossesPath(this, p);
            }
            throw new NullReferenceException();
        }
        
        private bool isSafeUp(Position position)
        {
            Position p = new Position(getXPos(), getYPos(), getZPos() + heightIncrement, getOrientation());
            return p.isInside();
        }

        private bool isSafeDown(Position position)
        {
            Position p = new Position(getXPos(), getYPos(), getZPos() - heightIncrement, getOrientation());
            return p.isInside();
        }

        private Position getFuturePos(int i)
        {
            string tempHorizontalAction = horizontalAction;
            int tempHorizontalActionCount = horizontalActionCount;
            string tempVerticalAction = verticalAction;
            int tempVerticalActionCount = verticalActionCount;
            double tempTheta = theta;
            double tempOrientation = getOrientation();

            Position tempPosition = new Position(0,0,0,0);
            tempPosition.set(position);

            for (int j = 0; j < i; j++)
            {
                if (tempHorizontalAction.Equals("move") && tempHorizontalActionCount > 0)
                {
                    if (!isSafe(tempPosition, tempTheta + tempOrientation))
                    {
                        tempHorizontalAction = "none";
                        tempHorizontalActionCount = 0;
                        tempVerticalAction = "none";
                        tempVerticalActionCount = 0;
                    } else
                    {
                        tempPosition = move(tempPosition, tempTheta + tempOrientation);
                        tempHorizontalActionCount--;
                    }
                }

                if (tempVerticalAction.Equals("up") && tempVerticalActionCount > 0)
                {
                    if (!isSafeUp(tempPosition))
                    {
                        tempHorizontalAction = "none";
                        tempHorizontalActionCount = 0;
                        tempVerticalAction = "none";
                        tempVerticalActionCount = 0;
                    } else
                    {
                        tempPosition = moveUp(tempPosition);
                        tempVerticalActionCount--;
                    }
                }

                if (tempVerticalAction.Equals("down") && tempVerticalActionCount > 0)
                {
                    if (!isSafeDown(tempPosition))
                    {
                        tempHorizontalAction = "none";
                        tempHorizontalActionCount = 0;
                        tempVerticalAction = "none";
                        tempVerticalActionCount = 0;
                    } else
                    {
                        tempPosition = moveDown(tempPosition);
                        tempVerticalActionCount--;
                    }
                }
            }
            return tempPosition;
        }

        private void rotateTo(double o)
        {
            setOrientation(o);
        }

        public int getId()
        {
            return id;
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

        public Position getPosition()
        {
            return position;
        }

        public double getOrientation()
        {
            return position.getOrientation();
        }

        public void setOrientation(double o)
        {
            position.setOrientation(o);
        }

        public string getState()
        {
            return state;
        }

        public void setState(string state)
        {
            this.state = state;
        }

        public bool getConnected()
        {
            return connected;
        }

        public void setConnected(bool connected)
        {
            this.connected = connected;
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