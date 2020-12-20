using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    class Booking
    {
        public int id { get; set; }
        public User user { get; set; }
        public Train train { get; set; }
        public int seatNumber { get; set; }
        public int BoxNumber { get; set; }
    }
}