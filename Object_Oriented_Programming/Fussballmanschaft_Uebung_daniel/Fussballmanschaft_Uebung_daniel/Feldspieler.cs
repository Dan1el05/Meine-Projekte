using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fussballmanschaft_Uebung_daniel
{
    internal class Feldspieler
    {
        public bool IstFeldspieler { get; set; } = false;
        public string Name { get; }
        public Feldspieler(string name)
        {
            Name = name;
        }


        public void AufsTorSchiessen()
        {
            Console.WriteLine(Name + "schiesst auf Tor");
        }
        public void Graetschen()
        {
            Console.WriteLine(Name + "macht eine Blut Graetsche! ");
        }
        public void Drippeln()
        {
            Console.WriteLine(Name + " dribbelt den Gegner schwindlig ! ");
        }
    }
}
