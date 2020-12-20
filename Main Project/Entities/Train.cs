using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    class Train
    {
        public int id { get; set; }
        public Route route { get; set; }
        public int BusinessSeats { get; set; }
        public int NormalSeats { get; set; }
        public int EconomySeats { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
