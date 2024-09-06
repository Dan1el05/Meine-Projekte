using Fussballmanschaft_Uebung;

namespace Fussballmanschaft_Uebung_daniel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Feldspieler Hans = new Feldspieler("Hans");

            Feldspieler Ben = new Feldspieler("Ben");

            Feldspieler Ernst = new Feldspieler("Ernst");

            Feldspieler Fritz = new Feldspieler("Fritz");

            Feldspieler Tom = new Feldspieler("Tom");

            Feldspieler Samuel = new Feldspieler("Samuel");

            Feldspieler Manuel = new Feldspieler("Manuel");

            Feldspieler Sämi = new Feldspieler("Sämi");

            Feldspieler Fabio = new Feldspieler("Fabio");

            Feldspieler Leo = new Feldspieler("Leo");


            Torwart Mateo = new Torwart("Mateo");

            Fussballmanschaft fussballmanschaft1 = new Fussballmanschaft(Hans, Mateo);


            fussballmanschaft1.AddFeldspieler(Hans);
            fussballmanschaft1.AddFeldspieler(Ben);
            fussballmanschaft1.AddFeldspieler(Ernst);
            fussballmanschaft1.AddFeldspieler(Fritz);
            fussballmanschaft1.AddFeldspieler(Tom);
            fussballmanschaft1.AddFeldspieler(Samuel);
            fussballmanschaft1.AddFeldspieler(Manuel);
            fussballmanschaft1.AddFeldspieler(Sämi);
            fussballmanschaft1.AddFeldspieler(Fabio);
            fussballmanschaft1.AddFeldspieler(Leo);


            Feldspieler Timo = new Feldspieler("Timo");

            Feldspieler Christoph = new Feldspieler("Christoph");

            Feldspieler Meike = new Feldspieler("Meike");

            Feldspieler Jenny = new Feldspieler("Jenny");

            Feldspieler Max = new Feldspieler("Max");

            Feldspieler Ulf = new Feldspieler("Ulf");

            Feldspieler Sigfried = new Feldspieler("Sigfrid");

            Feldspieler Saxon = new Feldspieler("Saxon");

            Feldspieler Hoffman = new Feldspieler("Hoffman");

            Feldspieler Heinz = new Feldspieler("Heinz");

            Torwart Henrich = new Torwart("Henrich");

            Fussballmanschaft fussballmanschaft2 = new Fussballmanschaft(Timo, Henrich);


            fussballmanschaft2.AddFeldspieler(Timo);
            fussballmanschaft2.AddFeldspieler(Christoph);
            fussballmanschaft2.AddFeldspieler(Meike);
            fussballmanschaft2.AddFeldspieler(Jenny);
            fussballmanschaft2.AddFeldspieler(Max);
            fussballmanschaft2.AddFeldspieler(Ulf);
            fussballmanschaft2.AddFeldspieler(Sigfried);
            fussballmanschaft2.AddFeldspieler(Saxon);
            fussballmanschaft2.AddFeldspieler(Hoffman);
            fussballmanschaft2.AddFeldspieler(Heinz);

            Spiel spiel1 = new Spiel(fussballmanschaft1, fussballmanschaft2);
            spiel1.StartSpiel();
            string ergebnis = spiel1.ErgebnisAnzeigen();
            Console.WriteLine($"Spiel-Ergebnis: {ergebnis}");
        }
    }
}