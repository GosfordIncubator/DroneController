using DroneControl;
using System;
using System.Drawing;
using System.Windows.Forms;
using Drone_Wars.Model;
using System.Drawing.Drawing2D;
using System.Net.Sockets;

namespace Drone_Wars
{
    public partial class DroneController : Form
    {
        public DroneController()
        {
            InitializeComponent();
        }

        private void DroneController_Load(object sender, EventArgs e)
        {
            int timeout = 0;

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    Network.connectServer();
                    
                    break;
                } catch (SocketException)
                {
                    timeout++;
                }
            }

            if (timeout == 5)
            {
                MessageBox.Show("Could not find server. Please ensure server is running before starting client.", "Error");
                Close();
            }

            Network.connectPhones();

            FieldSizeChooser FieldSizeChooser = new FieldSizeChooser();
            if (FieldSizeChooser.ShowDialog() == DialogResult.OK)
            {
                if (FieldSizeChooser.X > 0 && FieldSizeChooser.Y > 0)
                {
                    int h = Screen.GetBounds(this).Height;
                    int w = Screen.GetBounds(this).Width;

                    fieldPnl.Width = FieldSizeChooser.X*100;
                    fieldPnl.Height = FieldSizeChooser.Y*100;

                    Field.setupField(FieldSizeChooser.X*100, FieldSizeChooser.Y*100, 5);
                    timer1.Enabled = true;

                    dronesLb.DataSource = Field.getDrones();
                } else
                {
                    MessageBox.Show("Field size invalid.", "Error");
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void addDroneBtn_Click(object sender, EventArgs e)
        {
            if (!Field.isOccupied(null, new Position(20, 20, 0, 0)))
            {
                Field.newDrone(20, 20, 0);
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

        private void moveBtn_Click(object sender, EventArgs e)
        {
            moveDrone("move");
        }

        private void moveDrone(string direction)
        {
            try
            {
                if (getNumber() > 0 && getTheta() > 0.0 && getTheta() <= 2 * Math.PI)
                {
                   getSelectedDrone().command(direction, getNumber(), getTheta());
                }
                else MessageBox.Show("Please enter a valid movement count and angle.", "Error");
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
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;

            foreach (Drone drone in Field.getDrones())
            {
                setDroneSquare(drone.getPosition(), drone.getState(), e);

                foreach (Position p in drone.getFutPos())
                {
                    if (!drone.getPosition().equals(p)) setDroneSquare(p, "prediction", e);
                }
            }
        }
        
        private void setDroneSquare(Position p, string state, PaintEventArgs e)
        {
            int rectSize = 20;
            int circleSize = 40;
            int offSet = 10;
            Graphics formGraphics = e.Graphics;

            if (p != null)
            {
                if (state.Equals("landed"))
                {
                    formGraphics.DrawEllipse(new Pen(Color.Red), new Rectangle(p.getxPos() - offSet * 2, p.getyPos() - offSet * 2, circleSize, circleSize));
                    formGraphics.DrawImage(Properties.Resources.landed, p.getxPos() - offSet, p.getyPos() - offSet, rectSize, rectSize);
                }
                if (state.Equals("flying"))
                {
                    formGraphics.DrawEllipse(new Pen(Color.Red), new Rectangle(p.getxPos() - offSet * 2, p.getyPos() - offSet * 2, circleSize, circleSize));
                    formGraphics.DrawImage(Properties.Resources.flying, p.getxPos() - offSet, p.getyPos() - offSet, rectSize, rectSize);
                }
                if (state.Equals("prediction"))
                {
                    formGraphics.DrawEllipse(new Pen(Color.Gray), new Rectangle(p.getxPos() - offSet * 2, p.getyPos() - offSet * 2, circleSize, circleSize));
                    formGraphics.DrawImage(Properties.Resources.prediction, p.getxPos() - offSet, p.getyPos() - offSet, rectSize, rectSize);
                }
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
                return Int32.Parse(numberTb.Text);
            } catch (FormatException)
            {
                return 0;
            }
        }

        private double getTheta()
        {
            try
            {
                return Double.Parse(thetaTb.Text) * Math.PI;
            } catch (FormatException)
            {
                return 0;
            }
        }

        private void noDroneError()
        {
            MessageBox.Show("Please select a drone.", "Error");
        }

        private void DroneController_FormClosed(Object sender, FormClosedEventArgs e)
        {
           Network.closeServer();
        }
    }
}