using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Factory_Method
{
    class NormalCreator:Creator
    {
        public override Train CreateTrain(string type, int seats)
        {
            if (type == "Karachi Express")
                return new NormalKarachiExpress(seats);
            else if (type == "Tezgam Express")
                return new NormalTezgamExpress(seats);
            return new NormalKarakoramExpress(seats);
        }
    }
}
