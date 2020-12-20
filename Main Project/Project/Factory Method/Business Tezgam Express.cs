using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Factory_Method
{
    class BusinessTezgamExpress:Train
    {
        public BusinessTezgamExpress(int seats)
        {
            base.Seats = seats;
        }

        public override double CalculatePrice(int seats)
        {
            return 5000 * seats;
        }
        public override void BookSeat()
        {
            Seats -= 1;
        }
    }
}
