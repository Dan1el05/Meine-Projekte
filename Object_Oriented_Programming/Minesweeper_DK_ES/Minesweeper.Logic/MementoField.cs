using Mineswepper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    /// <summary>
    /// Stellt einen Memento-Zustand für das Spielfeld im Minesweeper-Spiel dar.
    /// </summary>
    public class MementoField
    {
        /// <summary>
        /// Das Spielfeld im Memento-Zustand.
        /// </summary>
        public Field[,] board;
    }
}
