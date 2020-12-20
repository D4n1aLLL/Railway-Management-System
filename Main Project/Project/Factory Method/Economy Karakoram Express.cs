using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Factory_Method
{
    class EconomyKarakoramExpress : Train
    {
        public EconomyKarakoramExpress(int seats)
        {
            base.Seats = seats;
        }

        public override double CalculatePrice(int seats)
        {
            return 6000 * seats;
        }
        public override void BookSeat()
        {
            Seats -= 1;
        }
    }
}
