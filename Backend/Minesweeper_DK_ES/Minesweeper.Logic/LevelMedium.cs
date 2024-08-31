using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    /// <summary>
    /// Stellt die mittlere Schwierigkeitsstufe für das Minesweeper-Spiel dar.
    /// </summary>
    public class LevelMedium : ILevel
    {
        /// <summary>
        /// Gibt die Anzahl der Zeilen des Spielfelds zurück.
        /// </summary>
        public int Rows { get { return 16; } }

        /// <summary>
        /// Gibt die Anzahl der Spalten des Spielfelds zurück.
        /// </summary>
        public int Cols { get { return 16; } }

        /// <summary>
        /// Gibt die Anzahl der Minen auf dem Spielfeld zurück.
        /// </summary>
        public int NumMines { get { return 40; } }
    }
}

