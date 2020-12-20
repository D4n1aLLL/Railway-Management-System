using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Factory_Method
{
    class BusinessKarachiExpress:Train
    {
        public BusinessKarachiExpress(int seats)
        {
            base.Seats = seats;
        }

        public override double CalculatePrice(int seats)
        {
            return 7000 * seats;
        }

        public override void BookSeat()
        {
            Seats -= 1;
        }
    }
}
