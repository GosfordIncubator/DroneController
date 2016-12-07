using DroneControl;
using System;
using System.Drawing;
using System.Windows.Forms;
using Drone_Wars.Model;
using System.Threading.Tasks;

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
            FieldSizeChooser FieldSizeChooser = new FieldSizeChooser();
            if (FieldSizeChooser.ShowDialog() == DialogResult.OK)
            {
                Network.connect();
                Network.connect2();
                Network.connect3(FieldSizeChooser.X, FieldSizeChooser.Y);

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

        private void noDroneError()
        {
            MessageBox.Show("Please select a drone.", "Error");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            operate();
        }

        public void operate()
        {
            foreach (Drone drone in Field.getDrones())
            {
                drone.operate();
            }
            Network.updatePos();
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
                }
                else
                {
                    MessageBox.Show("Please enter a number greater than 0", "Error");
                    return 0;
                }
            }
            catch (FormatException)
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
    }
}