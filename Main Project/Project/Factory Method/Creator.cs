using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Factory_Method
{
    abstract class Creator
    {
        public abstract Train CreateTrain(string type, int seat);
    }
}
