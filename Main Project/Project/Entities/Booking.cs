using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Factory_Method;

namespace Project.Entities
{
    class Booking
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Train Train { get; set; }
        public int SeatNumber { get; set; }
        public int BoxNumber { get; set; }
    }
}