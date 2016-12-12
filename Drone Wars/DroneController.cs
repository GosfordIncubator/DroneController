using DroneControl;
using System;
using System.Drawing;
using System.Windows.Forms;
using Drone_Wars.Model;
using System.Drawing.Drawing2D;

namespace Drone_Wars
{
    public partial class DroneController : Form
    {
        int cellSize = 80;
        int offSet;
        int rectSize;

        public DroneController()
        {
            InitializeComponent();
        }

        private void DroneController_Load(object sender, EventArgs e)
        {
            upBtn.Hide();
            downBtn.Hide();

            FieldSizeChooser FieldSizeChooser = new FieldSizeChooser();
            if (FieldSizeChooser.ShowDialog() == DialogResult.OK)
            {
                Network.connect();
                Network.connect2();

                int h = Screen.GetBounds(this).Height;
                int w = Screen.GetBounds(this).Width;

                for (int i = 0; i < 3; i++) {
                    if (cellSize * FieldSizeChooser.Y > h) {
                        cellSize /= 2;
                    }
                }

                if (cellSize == 80) { rectSize = 60; offSet = 10; }
                if (cellSize == 40) { rectSize = 30; offSet = 5; }
                if (cellSize == 20) { rectSize = 16; offSet = 2; }
                if (cellSize == 10) { rectSize = 7; offSet = 2; }

                fieldPnl.Width = cellSize * FieldSizeChooser.X;
                fieldPnl.Height = cellSize * FieldSizeChooser.Y;
                fieldPnl.Width++;
                fieldPnl.Height++;

                Field.setupField(FieldSizeChooser.X, FieldSizeChooser.Y, 5);
                timer1.Enabled = true;

                dronesLb.DataSource = Field.getDrones();
            }
            else
            {
                Close();
            }
        }

        private void addDroneBtn_Click(object sender, EventArgs e)
        {
            if (!Field.isOccupied(new Position(0, 0, 0)))
            {
                int ip = getIp();
                if (ip > 1 && !Field.ipExists(ip))
                {
                    Drone drone = Field.newDrone(0, 0, 0, ip);
                }
                else MessageBox.Show("Invalid IP", "Error");
            }
            else MessageBox.Show("Cannot create drone, a drone already occupies starting space.", "Error");
        }

        private void removeDroneBtn_Click(object sender, EventArgs e)
        {
            Drone drone = getSelectedDrone();
            Field.removeDrone(drone);
        }

        private void flyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                getSelectedDrone().takeOff();
            }
            catch (NullReferenceException)
            {
                noDroneError();
            }
        }

        private void landBtn_Click(object sender, EventArgs e)
        {
            try
            {
                getSelectedDrone().land();
            }
            catch (NullReferenceException)
            {
                noDroneError();
            }
        }

        private void forwardBtn_Click(object sender, EventArgs e)
        {
            moveDrone("forward");
        }

        private void backwardsBtn_Click(object sender, EventArgs e)
        {
            moveDrone("backward");
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            moveDrone("left");
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            moveDrone("right");
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            moveDrone("up");
        }

        private void downBtn_Click(object sender, EventArgs e)
        {
            moveDrone("down");
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            try
            {
                getSelectedDrone().stop();
            }
            catch (NullReferenceException)
            {
                noDroneError();
            }
        }

        private void moveDrone(string direction)
        {
            try
            {
                if (getNumber() > 0)
                {
                    getSelectedDrone().command(direction, getNumber());
                }
                else MessageBox.Show("Please enter a valid movement count.", "Error");
            }
            catch (LandedException)
            {
                MessageBox.Show("Drone cannot move while landed.", "Error");
            }
            catch (NullReferenceException)
            {
                noDroneError();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Drone drone in Field.getDrones())
            {
                drone.operate();
            }
            fieldPnl.Refresh();
        }

        private void fieldPnl_Paint(object sender, PaintEventArgs e)
        {
            if (cellSize >= 40)
            {
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            }

            drawGrid(e);

            foreach (Drone drone in Field.getDrones())
            {
                setDroneSquare(drone.getPosition(), drone.getState(), e);

                foreach (Position p in drone.getFutPos())
                {
                    if (!drone.getPosition().equals(p)) setDroneSquare(p, "prediction", e);
                }
            }
        }

        private void drawGrid(PaintEventArgs e)
        {
            Graphics formGraphics = e.Graphics;
            Pen myPen = new Pen(Color.Black);

            for (int i = 0; i <= fieldPnl.Width / cellSize; i++)
            {
                formGraphics.DrawLine(myPen, i * cellSize, 0, i * cellSize, fieldPnl.Height);
            }

            for (int j = 0; j <= fieldPnl.Height / cellSize; j++)
            {
                formGraphics.DrawLine(myPen, 0, j * cellSize, fieldPnl.Width, j * cellSize);
            }

            myPen.Dispose();
        }

        private void setDroneSquare(Position p, string state, PaintEventArgs e)
        {
            Graphics formGraphics = e.Graphics;

            if (state.Equals("landed"))
            {
                SolidBrush myBrush = new SolidBrush(Color.Black);
                if (cellSize >= 20)
                {
                    formGraphics.DrawImage(Properties.Resources.landed, cellSize * p.getxPos() + offSet, cellSize * p.getyPos() + offSet, rectSize, rectSize);
                } else formGraphics.FillRectangle(myBrush, new Rectangle(cellSize * p.getxPos() + offSet, cellSize * p.getyPos() + offSet, rectSize, rectSize));
            }
            if (state.Equals("flying"))
            {
                SolidBrush myBrush = new SolidBrush(Color.Red);
                if (cellSize >= 20)
                {
                    formGraphics.DrawImage(Properties.Resources.flying, cellSize * p.getxPos() + offSet, cellSize * p.getyPos() + offSet, rectSize, rectSize);
                } else formGraphics.FillRectangle(myBrush, new Rectangle(cellSize * p.getxPos() + offSet, cellSize * p.getyPos() + offSet, rectSize, rectSize));
            }
            if (state.Equals("prediction"))
            {
                SolidBrush myBrush = new SolidBrush(Color.Gray);
                if (cellSize >= 20)
                {
                    formGraphics.DrawImage(Properties.Resources.prediction, cellSize * p.getxPos() + offSet, cellSize * p.getyPos() + offSet, rectSize, rectSize);
                } else formGraphics.FillRectangle(myBrush, new Rectangle(cellSize * p.getxPos() + offSet, cellSize * p.getyPos() + offSet, rectSize, rectSize));
            }
        }

        private Drone getSelectedDrone()
        {
            return dronesLb.SelectedItem as Drone;
        }

        private int getNumber()
        {
            try
            {
                if (Int32.Parse(numberTb.Text) > 0)
                {
                    return Int32.Parse(numberTb.Text);
                } else
                {
                    MessageBox.Show("Please enter a number greater than 0", "Error");
                    return 0;
                }
            } catch (FormatException)
            {
                return 0;
            }
        }

        private int getIp()
        {
            int ip;
            try
            {
                ip = Int32.Parse(ipTb.Text.Split('.')[3].Trim());
            } catch (FormatException)
            {
                ip = 0;
            }
            ipTb.Text = "192.168.1.";
            return ip;
        }

        private void noDroneError()
        {
            MessageBox.Show("Please select a drone.", "Error");
        }
    }
}