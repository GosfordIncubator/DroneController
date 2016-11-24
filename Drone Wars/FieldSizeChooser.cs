
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drone_Wars
{
    public partial class FieldSizeChooser : Form
    {
        public FieldSizeChooser()
        {
            InitializeComponent();
        }

        public int X
        {
            get
            {
                try
                {
                    return Int32.Parse(xTb.Text);
                }
                catch (FormatException)
                {
                    return 0;
                }
            }
        }

        public int Y
        {
            get
            {
                try
                {
                    return Int32.Parse(yTb.Text);
                }
                catch (FormatException)
                {
                    return 0;
                }
            }
        }
    }
}
