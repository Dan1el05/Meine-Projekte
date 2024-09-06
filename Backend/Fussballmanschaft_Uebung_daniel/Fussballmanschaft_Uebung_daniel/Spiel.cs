using Fussballmanschaft_Uebung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fussballmanschaft_Uebung_daniel
{
    internal class Spiel
    {
        public Fussballmanschaft HeimMannschaft { get; }
        public Fussballmanschaft AuswaertsMannschaft { get; }
        public Resultat Ergebnis { get; private set; }



        public Spiel(Fussballmanschaft heimMannschaft, Fussballmanschaft auswaertsMannschaft)
        {
            HeimMannschaft = heimMannschaft;
            AuswaertsMannschaft = auswaertsMannschaft;
        }


        public void StartSpiel()
        {
            int heimTore = HeimMannschaft.Spielzug(90);
            int auswaertsTore = AuswaertsMannschaft.Spielzug(90);
            Ergebnis = new Resultat(heimTore, auswaertsTore);


        }

        public string ErgebnisAnzeigen()
        {
            return $"{Ergebnis.HeimTore} : {Ergebnis.AuswaertsTore}"; ;
        }
    }
}
