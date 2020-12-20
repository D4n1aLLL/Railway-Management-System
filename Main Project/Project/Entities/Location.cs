using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    class Location
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public Location(int _id, string station)
        {
            Id = _id;
            StationName = station;
        }
    }
}