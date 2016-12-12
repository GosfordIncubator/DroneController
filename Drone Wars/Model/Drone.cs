using Drone_Wars.Model;
using System;

namespace DroneControl
{
    class Drone
    {
        private int id;
        private Position position = new Position(0, 0, 0);
        private Position[] futPos = new Position[50];
        private bool landed;
        private string state;

        private float inputLag = 0;

        private int FieldLengthX;
        private int FieldLengthY;
        private int maxHeight;
        private int orientation;
        private int ip;

        private string xAction = "None";
        private string yAction = "None";
        private string zAction = "None";

        private int xActionCount = 0;
        private int yActionCount = 0;
        private int zActionCount = 0;

        public Drone(int id, int xPos, int yPos, int orientation, int FieldLengthX, int FieldLengthY, int maxHeight, int ip)
        {
            this.id = id;
            setXPos(xPos);
            setYPos(yPos);
            setZPos(0);
            this.orientation = orientation;
            this.FieldLengthX = FieldLengthX;
            this.FieldLengthY = FieldLengthY;
            this.maxHeight = maxHeight;
            landed = true;
            state = "landed";
            this.ip = ip;
            Network.sendNewDrone(id, ip);
        }
        
        public void operate()
        {
            //position = Network.getDronePos(id);

            for (int i = 0; i < futPos.Length; i++)
            {
                futPos[i] = getFuturePos(i + 1);
            }

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
                            if (inputLag == 0) inputLag+=2;
                            Network.sendForward(id);
                            yAction = action;
                            yActionCount = actionCount;
                        }
                        break;
                    case "backward":
                        if (isSafeBackward(position))
                        {
                            if (inputLag == 0) inputLag += 2;
                            Network.sendBackward(id);
                            yAction = action;
                            yActionCount = actionCount;
                        }
                        break;
                    case "left":
                        if (isSafeLeft(position))
                        {
                            if (inputLag == 0) inputLag+=2;
                            Network.sendLeft(id);
                            xAction = action;
                            xActionCount = actionCount;
                        }
                        break;
                    case "right":
                        if (isSafeRight(position))
                        {
                            if (inputLag == 0) inputLag+=2;
                            Network.sendRight(id);
                            xAction = action;
                            xActionCount = actionCount;
                        }
                        break;
                    case "up":
                        if (isSafeUp(position))
                        {
                            if (inputLag == 0) inputLag += 2;
                            Network.sendUp(id);
                            zAction = action;
                            zActionCount = actionCount;
                        }
                        break;
                    case "down":
                        if (isSafeDown(position))
                        {
                            if (inputLag == 0) inputLag += 2;
                            Network.sendDown(id);
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

        public void cardinalCommand(string action, int actionCount)
        {
            switch (action)
            {
                case "North":
                    moveNorth(actionCount);
                    break;
                case "South":
                    moveSouth(actionCount);
                    break;
                case "West":
                    moveWest(actionCount);
                    break;
                case "East":
                    moveEast(actionCount);
                    break;
            }
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
            xAction = "None";
            xActionCount = 0;
            yAction = "None";
            yActionCount = 0;
            zAction = "None";
            zActionCount = 0;
        }

        private void stopX()
        {
            Network.sendStopX(id);
            xAction = "None";
            xActionCount = 0;
        }

        private void stopY()
        {
            Network.sendStopY(id);
            yAction = "None";
            yActionCount = 0;
        }

        private void stopZ()
        {
            Network.sendStopZ(id);
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

        private void moveNorth(int actionCount)
        {
            switch (orientation)
            {
                //Facing North
                case 0:
                    command("forward",actionCount);
                    break;
                //Facing North-East
                case 1:
                    rotateTo(0);
                    command("forward", actionCount);
                    break;
                //Facing East
                case 2:
                    command("left", actionCount);
                    break;
                //Facing South-East
                case 3:
                    rotateTo(2);
                    command("left", actionCount);
                    break;
                //Facing South
                case 4:
                    command("backward", actionCount);
                    break;
                //Facing South-West
                case 5:
                    rotateTo(4);
                    command("backward", actionCount);
                    break;
                //Facing West
                case 6:
                    command("right", actionCount);
                    break;
                //Facing North-West
                case 7:
                    rotateTo(6);
                    command("right", actionCount);
                    break;
            }
        }

        private void moveSouth(int actionCount)
        {
            switch (orientation)
            {
                //Facing North
                case 0:
                    command("backward", actionCount);
                    break;
                //Facing North-East
                case 1:
                    rotateTo(0);
                    command("backward", actionCount);
                    break;
                //Facing East
                case 2:
                    command("right", actionCount);
                    break;
                //Facing South-East
                case 3:
                    rotateTo(2);
                    command("right", actionCount);
                    break;
                //Facing South
                case 4:
                    command("forward", actionCount);
                    break;
                //Facing South-West
                case 5:
                    rotateTo(4);
                    command("forward", actionCount);
                    break;
                //Facing West
                case 6:
                    command("left", actionCount);
                    break;
                //Facing North-West
                case 7:
                    rotateTo(6);
                    command("left", actionCount);
                    break;
            }
        }

        private void moveEast(int actionCount)
        {
            switch (orientation)
            {
                //Facing North
                case 0:
                    command("right", actionCount);
                    break;
                //Facing North-East
                case 1:
                    rotateTo(0);
                    command("right", actionCount);
                    break;
                //Facing East
                case 2:
                    command("forward", actionCount);
                    break;
                //Facing South-East
                case 3:
                    rotateTo(2);
                    command("forward", actionCount);
                    break;
                //Facing South
                case 4:
                    command("left", actionCount);
                    break;
                //Facing South-West
                case 5:
                    rotateTo(4);
                    command("left", actionCount);
                    break;
                //Facing West
                case 6:
                    command("backward", actionCount);
                    break;
                //Facing North-West
                case 7:
                    rotateTo(6);
                    command("backward", actionCount);
                    break;
            }
        }

        private void moveWest(int actionCount)
        {
            switch (orientation)
            {
                //Facing North
                case 0:
                    command("left", actionCount);
                    break;
                //Facing North-East
                case 1:
                    rotateTo(0);
                    command("left", actionCount);
                    break;
                //Facing East
                case 2:
                    command("backward", actionCount);
                    break;
                //Facing South-East
                case 3:
                    rotateTo(2);
                    command("backward", actionCount);
                    break;
                //Facing South
                case 4:
                    command("right", actionCount);
                    break;
                //Facing South-West
                case 5:
                    rotateTo(4);
                    command("right", actionCount);
                    break;
                //Facing West
                case 6:
                    command("forward", actionCount);
                    break;
                //Facing North-West
                case 7:
                    rotateTo(6);
                    command("forward", actionCount);
                    break;
            }
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
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this,p);
                //Facing North-East
                case 1:
                    //North-East
                    p = new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing East
                case 2:
                    //East
                    p = new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South-East
                case 3:
                    //South-East
                    p = new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South
                case 4:
                    //South
                    p = new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South-West
                case 5:
                    //South-West
                    p = new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing West
                case 6:
                    //West
                    p = new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing North-West
                case 7:
                    //North-West
                    p = new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
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
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing North-East
                case 1:
                    //South-West
                    p = new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing East
                case 2:
                    //West
                    p = new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South-East
                case 3:
                    //North-West
                    p = new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South
                case 4:
                    //North
                    p = new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South-West
                case 5:
                    //North-East
                    p = new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing West
                case 6:
                    //East
                    p = new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing North-West
                case 7:
                    //South-East
                    p = new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
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
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing North-East
                case 1:
                    //North-West
                    p = new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing East
                case 2:
                    //North
                    p = new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South-East
                case 3:
                    //North-East
                    p = new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South
                case 4:
                    //East
                    p = new Position(position.getxPos() + 1, position.getyPos(), position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South-West
                case 5:
                    //South-East
                    p = new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing West
                case 6:
                    //South
                    p = new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing North-West
                case 7:
                    //South-West
                    p = new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
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
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing North-East
                case 1:
                    //South-East
                    p = new Position(position.getxPos() + 1, position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing East
                case 2:
                    //South
                    p = new Position(position.getxPos(), position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South-East
                case 3:
                    //South-West
                    p = new Position(position.getxPos() - 1, position.getyPos() + 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South
                case 4:
                    //West
                    p = new Position(position.getxPos() - 1, position.getyPos(), position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing South-West
                case 5:
                    //North-West
                    p = new Position(position.getxPos() - 1, position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing West
                case 6:
                    //North
                    p = new Position(position.getxPos(), position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
                //Facing North-West
                case 7:
                    //North-East
                    p = new Position(position.getxPos() + 1, position.getyPos() - 1, position.getzPos());
                    return !Field.isOccupied(p) && p.isInside() && !Field.crossesPath(this, p);
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

        private void rotateTo(int o)
        {
            orientation = o;
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

        public int getIp()
        {
            return ip;
        }

        override
        public string ToString()
        {
            return "Drone " + id;
        }
    }
}