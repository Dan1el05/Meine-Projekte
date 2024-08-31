using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    /// <summary>
    /// Repräsentiert ein einzelnes Feld im Minesweeper-Spiel.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Gibt an, ob das Feld aufgedeckt wurde oder nicht.
        /// </summary>
        public bool IsFieldUncovered { get; set; }

        /// <summary>
        /// Gibt an, ob dieses Feld eine Bombe enthält oder nicht.
        /// </summary>
        public bool FieldBombe { get; set; }

        /// <summary>
        /// Die Anzahl der Bomben, die in den benachbarten Feldern dieses Feldes vorhanden sind.
        /// </summary>
        public int NumberBombesNearby { get; set; }

        /// <summary>
        /// Gibt an, ob das Feld rückgängig gemacht werden kann.
        /// </summary>
        public bool IsUndo { get; set; }

        /// <summary>
        /// Konstruktor für ein Feld. Standardmäßig wird das Feld als nicht aufgedeckt, ohne Bombe, ohne benachbarte Bomben und rückgängig machbar initialisiert.
        /// </summary>
        public Field()
        {
            IsFieldUncovered = false;
            FieldBombe = false;
            NumberBombesNearby = 0;
            IsUndo = true; // Standardmäßig kann das Feld rückgängig gemacht werden
        }
    }
}