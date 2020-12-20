using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Factory_Method;

namespace Project.Entities
{
    class Route
    {
        private static Route _route;

        //private constructor to force use of 
        //getInstance() to create Singleton object 
        private Route() { }

        public static Route getInstance(int _id, Location _location1, Location _location2, double _distance)
        {
            if (_route == null)
                _route = new Route(_id,_location1,_location2,_distance);
            return _route;
        }
        public int Id { get; set; }
        public Location Location1 { get; set; }
        public Location Location2 { get; set; }
        public double Distance { get; set; }

        private Route(int _id, Location _location1, Location _location2, double _distance)
        {
            Id = _id;
            Location1 = _location1;
            Location2 = _location2;
            Distance = _distance;
        }
    }
}