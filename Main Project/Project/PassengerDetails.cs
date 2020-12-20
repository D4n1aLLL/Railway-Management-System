using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class PassengerDetails
    {
        protected int id { get; set; }
        protected string name { get; set; }
        public PassengerDetails(int _id, string _name)
        {
            this.id = _id;
            this.name = _name;
        }
    }
}
