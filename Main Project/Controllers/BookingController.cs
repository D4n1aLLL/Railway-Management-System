using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Controllers
{
    class BookingController
    {
        public List<Train> ViewBookings()
        {
            return new List<Train>();
        }

        public List<Booking> ViewBookingHistory(User user)
        {
            return new List<Booking>();
        }

        public int BookSeats(List<Booking> bookings)
        {
            return -1;
        }
    }
}
