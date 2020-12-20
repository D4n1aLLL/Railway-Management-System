using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    class Route
    {
        public int id { get; set; }
        public Location location1 { get; set; }
        public Location location2 { get; set; }
        public double distance { get; set; }
    }
}
