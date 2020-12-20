using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;
using Project.Factory_Method;

namespace Project.Factory_Method
{
    class BusinessCreator:Creator
    {
        public override Train CreateTrain(string type, int seats)
        {
            if (type == "Karachi Express")
                return new BusinessKarachiExpress(seats);
            else if (type == "Tezgam Express")
                return new BusinessTezgamExpress(seats);
            return new BusinessKarakoramExpress(seats);
        }
    }
}
