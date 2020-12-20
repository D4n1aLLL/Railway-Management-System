using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Factory_Method
{
    abstract class Train
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public int Seats { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public abstract double CalculatePrice(int seats);
        public abstract void BookSeat();
    }
}
