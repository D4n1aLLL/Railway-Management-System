using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Ticket:PassengerDetails
    {
        public string seatClass { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        private double tickerPrice;
        public Ticket(int _id, string _name, string _seatclass, string _from, string _to) : base(_id, _name)
        {
            this.seatClass = _seatclass;
            this.from = _from;
            this.to = _to;
        }

        public string ticketGenerate() { return null; }
    }
}
