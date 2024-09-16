using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    /// <summary>
    /// Stellt ein leichtes Schwierigkeitsniveau im Minesweeper-Spiel dar.
    /// </summary>
    public class LevelEasy : ILevel
    {
        /// <summary>
        /// Die Anzahl der Zeilen im Spielfeld.
        /// </summary>
        public int Rows { get { return 8; } }

        /// <summary>
        /// Die Anzahl der Spalten im Spielfeld.
        /// </summary>
        public int Cols { get { return 8; } }

        /// <summary>
        /// Die Anzahl der Minen im Spielfeld.
        /// </summary>
        public int NumMines { get { return 10; } }
    }
}
