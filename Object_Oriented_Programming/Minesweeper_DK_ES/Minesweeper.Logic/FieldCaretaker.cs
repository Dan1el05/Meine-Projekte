using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic
{
    /// <summary>
    /// Verwaltet die Memento-Objekte und ermöglicht das Hinzufügen und Entfernen von Mementos.
    /// </summary>
    public class FieldCaretaker
    {
        private List<MementoField> mementos = new List<MementoField>();

        /// <summary>
        /// Fügt ein Memento zur Liste hinzu.
        /// </summary>
        /// <param name="memento">Das Memento, das hinzugefügt werden soll.</param>
        public void Push(MementoField memento)
        {
            mementos.Add(memento);
        }

        /// <summary>
        /// Entfernt das zuletzt hinzugefügte Memento aus der Liste und gibt es zurück.
        /// </summary>
        /// <returns>Das zuletzt hinzugefügte Memento oder null, wenn die Liste leer ist.</returns>
        public MementoField Pop()
        {
            if (mementos.Count > 0)
            {
                var memento = mementos[mementos.Count - 1];
                mementos.RemoveAt(mementos.Count - 1); // Letztes Element wird entfernt
                return memento;
            }
            return null;
        }
    }
}
