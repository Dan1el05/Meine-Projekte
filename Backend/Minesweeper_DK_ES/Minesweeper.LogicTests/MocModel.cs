using Minesweeper.Logic;
using Minesweeper.LogicTests;
using Mineswepper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.LogicTests
{
    /// <summary>
    /// Eine Mock-Implementierung des Spielmodells für Testzwecke.
    /// </summary>
    public class MocModel
    {
        private Field[,] board;
        private int rows;
        private int cols;
        private int numMines;
        private View view;
        public ILevel Level { get; }

        /// <summary>
        /// Konstruktor für das MocModel-Objekt.
        /// </summary>
        /// <param name="level">Das Schwierigkeitsniveau des Spiels.</param>
        public MocModel(ILevel level)
        {
            this.Level = level;
            this.numMines = level.NumMines;
            this.board = new Field[level.Rows, level.Cols];
            this.InitializeBoard();
            this.PlaceMines();
            this.UpdateAdjacentMinesCount();
        }

        private void InitializeBoard()
        {
            rows = Level.Rows;
            cols = Level.Cols;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    board[row, col] = new Field();
                }
            }
        }

        /// <summary>
        /// Platziert Minen auf dem Spielfeld basierend auf vordefinierten Positionen.
        /// </summary>
        public void PlaceMines()
        {
            int[,] minePositions = { { 0, 0 }, { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 5, 6 }, { 6, 7 }, { 7, 0 }, { 7, 1 } };

            for (int i = 0; i < minePositions.GetLength(0); i++)
            {
                int row = minePositions[i, 0];
                int col = minePositions[i, 1];
                board[row, col].FieldBombe = true;
            }
        }

        /// <summary>
        /// Aktualisiert die Anzahl der Minen in den benachbarten Feldern.
        /// </summary>
        public void UpdateAdjacentMinesCount()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (!board[row, col].FieldBombe)
                    {
                        int adjacentMines = CountAdjacentMines(row, col);
                        board[row, col].NumberBombesNearby = adjacentMines;
                    }
                }
            }
        }

        /// <summary>
        /// Zählt die Anzahl der Minen in den benachbarten Feldern.
        /// </summary>
        private int CountAdjacentMines(int row, int col)
        {
            int count = 0;
            for (int r = System.Math.Max(0, row - 1); r <= System.Math.Min(row + 1, rows - 1); r++)
            {
                for (int c = System.Math.Max(0, col - 1); c <= System.Math.Min(col + 1, cols - 1); c++)
                {
                    if (board[r, c].FieldBombe)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
