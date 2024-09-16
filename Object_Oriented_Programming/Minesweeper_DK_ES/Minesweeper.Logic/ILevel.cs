using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    /// <summary>
    /// Definiert die Eigenschaften eines Schwierigkeitsniveaus im Minesweeper-Spiel.
    /// </summary>
    public interface ILevel
    {
        /// <summary>
        /// Die Anzahl der Zeilen im Spielfeld.
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Die Anzahl der Spalten im Spielfeld.
        /// </summary>
        int Cols { get; }

        /// <summary>
        /// Die Anzahl der Minen im Spielfeld.
        /// </summary>
        int NumMines { get; }
    }
}
