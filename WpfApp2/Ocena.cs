using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Ocena
    {
        public string przedmiot { get; set; }
        public double ocena { get; set; }

        public Ocena(string przedmiot, double ocena)
        {
            this.przedmiot = przedmiot;
            this.ocena = ocena;
        }

        public Ocena() : this("", 0)
        {
        }
    }
}
