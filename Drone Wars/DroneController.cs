using DroneControl;
using System;
using System.Drawing;
using System.Windows.Forms;
using Drone_Wars.Model;

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

                Field.setupField(FieldSizeChooser.X, FieldSizeChooser.Y, 5);
                timer1.Enabled = true;

                dronesLb.DataSource = Field.getDrones();
                mapGv.BackgroundColor = DroneController.DefaultBackColor;

                for (int x = 0; x < Field.getFieldLengthX(); x++)
                {
                    DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                    mapGv.Columns.Add(imageCol);
                }

                for (int y = 0; y < Field.getFieldLengthY() - 1; y++)
                {
                    mapGv.Rows.Add();
                }

                for (int x = 0; x < Field.getFieldLengthX(); x++)
                {
                    for (int y = 0; y < Field.getFieldLengthY(); y++)
                    {
                        mapGv.Rows[y].Cells[x].Value = new Bitmap(Properties.Resources.empty);
                    }
                }
                
                int height = 0;
                foreach (DataGridViewRow row in mapGv.Rows)
                {
                    height += row.Height;
                }
                height += mapGv.ColumnHeadersHeight;

                int width = 0;
                foreach (DataGridViewColumn col in mapGv.Columns)
                {
                    width += col.Width;
                }
                width += mapGv.RowHeadersWidth;

                mapGv.ClientSize = new Size(width + 2, height + 2);
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

        public void setDroneSquare(Position position, string state)
        {
            if (position != null)
            {
                switch (state)
                {
                    case "landed":
                        mapGv.Rows[position.getyPos()].Cells[position.getxPos()].Value = new Bitmap(Properties.Resources.landed);
                        break;
                    case "flying":
                        mapGv.Rows[position.getyPos()].Cells[position.getxPos()].Value = new Bitmap(Properties.Resources.flying);
                        break;
                    case "prediction":
                        mapGv.Rows[position.getyPos()].Cells[position.getxPos()].Value = new Bitmap(Properties.Resources.prediction);
                        break;
                }
            }
        }

        public void setEmptySquare(Position position)
        {
            mapGv.Rows[position.getyPos()].Cells[position.getxPos()].Value = new Bitmap(Properties.Resources.empty);
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
                foreach (Position p in drone.getFutPos())
                {
                    if (p != null) setEmptySquare(p);
                }
                setEmptySquare(drone.getPosition());
                drone.operate();
                setDroneSquare(drone.getPosition(), drone.getState());

                if (!drone.getPosition().equals(drone.getFuturePos(1)))
                {
                    foreach (Position p in drone.getFutPos())
                    {
                        if (p != null)
                        {
                            if (!Field.isOccupied(p) && p.isInside(Field.getFieldLengthX(),Field.getFieldLengthY(),Field.getMaxHeight()))
                            {
                                setDroneSquare(p, "prediction");
                            }
                        }
                    }
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
            int ip = Int32.Parse(ipTb.Text.Split('.')[3].Trim());
            Console.WriteLine(ip);
            ipTb.Text = "192.168.1.";
            return ip;
        }
    }
}