using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Schema;
using Minesweeper.Logic;
using Mineswepper.Logic.Minesweeper.Logic;
using System.Media;

using System;
using Minesweeper.Logic;

namespace Minesweeper
{
    /// <summary>
    /// Hauptklasse, die den Einstiegspunkt für das Minesweeper-Spiel enthält.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Einstiegspunkt des Programms. Startet das Minesweeper-Spiel.
        /// </summary>
        /// <param name="args">Die Befehlszeilenargumente.</param>
        static void Main(string[] args)
        {
            // Begrüßungsnachricht anzeigen
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
███╗   ███╗██╗███╗   ██╗███████╗███████╗█╗    ██╗ ███████╗███████╗██████╗ ███████╗██████╗ 
████╗ ████║██║████╗  ██║██╔════╝██╔════╝██║    ██║██╔════╝██╔════╝██╔══██╗██╔════╝██╔══██╗
██╔████╔██║██║██╔██╗ ██║█████╗  ███████╗██║ █╗ ██║█████╗  █████╗  ██████╔╝█████╗  ██████╔╝
██║╚██╔╝██║██║██║╚██╗██║██╔══╝  ╚════██║██║███╗██║██╔══╝  ██╔══╝  ██╔═══╝ ██╔══╝  ██╔══██╗
██║ ╚═╝ ██║██║██║ ╚████║███████╗███████║╚███╔███╔╝███████╗███████╗██║     ███████╗██║  ██║
╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝ ╚══╝╚══╝ ╚══════╝╚══════╝╚═╝     ╚══════╝╚═╝  ╚═╝
");
            Console.ResetColor();
            Console.WriteLine("\n\nWelcome to Minesweeper!");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("=== Minesweeper ===");
            Console.WriteLine("Willkommen bei Minesweeper!");
            Console.WriteLine("Regeln:");
            Console.WriteLine("- Das Spielfeld besteht aus verdeckten Feldern und einigen Minen.");
            Console.WriteLine("- Öffne Felder, um zu sehen, was darunter ist.");
            Console.WriteLine("- Achte darauf, keine Mine zu treffen!");
            Console.WriteLine("- Gebe die Koordinaten im Format 'Zeile Spalte' ein (z.B. '3 4').");
            Console.WriteLine("- Drücken Sie 2 Mal die '1', um den letzten Zug rückgängig zu machen (Undo).");
            Console.WriteLine();
            View view = new View();
            ILevel level = null;
            Console.WriteLine("Bitte wählen Sie den Schwierigkeitsgrad aus: (easy/medium/difficult)");
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "easy":
                    level = new LevelEasy();
                    break;
                case "medium":
                    level = new LevelMedium();
                    break;
                case "difficult":
                    level = new LevelDifficult();
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe. Bitte wählen Sie zwischen easy, medium oder difficult.");
                    return;
            }
            Model model = new Model(level);
            view.DisplayBoard(model);
            view.Game(model);
        }
    }
}
