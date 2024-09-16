
using Fussballmanschaft_Uebung_daniel;

namespace Fussballmanschaft_Uebung
    {
        internal class Fussballmanschaft
        {


            List<Feldspieler> feldspielers = new List<Feldspieler>();
            List<Torwart> torwarts = new List<Torwart>();

            public Fussballmanschaft(Feldspieler feldspieler, Torwart torwart)
            {
                feldspielers.Add(feldspieler);
                torwarts.Add(torwart);
            }

            public bool AddFeldspieler(Feldspieler feldspieler)
            {
                if (feldspieler.IstFeldspieler)
                    return false;

                if (feldspielers.Count < 10)
                {
                    feldspielers.Add(feldspieler);
                    feldspieler.IstFeldspieler = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool AddTorwart(Torwart torwart)
            {
                if (torwart.IstTorwart)
                    return false;

                if (torwarts.Count < 1)
                {
                    torwarts.Add(torwart);
                    torwart.IstTorwart = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public string name { get; }

            public int Spielzug(int a)
            {

                int Tore = 0;
                foreach (Feldspieler feldspieler in feldspielers)
                {
                    feldspieler.AufsTorSchiessen();
                    feldspieler.Graetschen();
                    feldspieler.Drippeln();
                    Tore++;
                }
                foreach (Torwart torwart in torwarts)
                {
                    torwart.SchussHalten();
                    Tore--;
                    torwart.Abstoss();
                }
                return Tore;
            }
        }
    }

