using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fussballmanschaft_Uebung_daniel
{
    internal class Resultat
    {
        public int HeimTore { get; }
        public int AuswaertsTore { get; }

        public Resultat(int heimTore, int auswaertsTore)
        {
            HeimTore = heimTore;
            AuswaertsTore = auswaertsTore;
        }
    }
}
