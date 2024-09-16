using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fussballmanschaft_Uebung_daniel
{
    internal class Torwart
    {
        public bool IstTorwart { get; set; } = false;
        public string Name { get; }
        public Torwart(string name)
        {
            Name = name;
        }


        public bool SchussHalten()
        {
            Console.WriteLine(Name + " hält den Schuss vom Gegner!");
            return true;
        }

        public void Abstoss()
        {
            Console.WriteLine(Name + "macht einen Abstoss!");
        }
    }
}
