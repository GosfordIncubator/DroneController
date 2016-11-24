using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drone_Wars.Model
{
    public class Movement
    {
        public int xDistance { get; set; }
        public int yDistance { get; set; }
        public int zDistance { get; set; }
        public string xDirection { get; set; }
        public string yDirection { get; set; }
        public string zDirection { get; set; }

        public Movement(int xDistance, int yDistance, int zDistance, string xDirection, string yDirection, string zDirection)
        {
            this.xDistance = xDistance;
            this.yDistance = yDistance;
            this.zDistance = zDistance;
            this.xDirection = xDirection;
            this.yDirection = yDirection;
            this.zDirection = zDirection;
        }

        
    }
}
