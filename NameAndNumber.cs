using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQvsSQL
{
    interface INameAndNumber
    {
        string Name { get; set; }
        string Number { get; set; }
    }

    class NameAndNumber: INameAndNumber
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }
}
