using Minesweeper.Logic;
using System.Data;
using System.Diagnostics;
using System.Media;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;


namespace Mineswepper.Logic
{
    namespace Minesweeper.Logic
    {
        /// <summary>
        /// Klasse, die das Spielmodell für Minesweeper darstellt.
        /// </summary>
        public class Model
        {
            private MementoField memento;
            private Field[,] board;
            private int rows;
            private int cols;
            private int numMines;
            private View view;
            public ILevel Level { get; }
            private FieldCaretaker caretaker = new FieldCaretaker();

            /// <summary>
            /// Konstruktor für das Model-Objekt.
            /// </summary>
            /// <param name="level">Das Schwierigkeitsniveau des Spiels.</param>
            public Model(ILevel level)
            {
                this.Level = level;
                numMines = level.NumMines;
                memento = new MementoField();
                InitializeBoard();
                PlaceMines();
                UpdateAdjacentMinesCount();
            }

            /// <summary>
            /// Initialisiert das Spielfeld.
            /// </summary>
            public void InitializeBoard()
            {
                rows = Level.Rows;
                cols = Level.Cols;
                board = new Field[rows, cols];
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        board[row, col] = new Field();
                    }
                }
            }

            /// <summary>
            /// Platziert Minen auf dem Spielfeld.
            /// </summary>
            public void PlaceMines()
            {
                Random random = new Random();
                int minesPlaced = 0;

                while (minesPlaced < numMines)
                {
                    int row = random.Next(0, rows);
                    int col = random.Next(0, cols);

                    if (!board[row, col].FieldBombe)
                    {
                        board[row, col].FieldBombe = true;
                        minesPlaced++;
                    }
                }
            }


            /// <summary>
            /// Aktualisiert die Anzahl der Minen in benachbarten Feldern.
            /// </summary>
            public void UpdateAdjacentMinesCount()
            {
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (!board[row, col].FieldBombe)
                        {
                            board[row, col].NumberBombesNearby = CountAdjacentMines(row, col);
                        }
                    }
                }
            }
            /// <summary>
            /// Zählt die Minen in den benachbarten Feldern.
            /// </summary>
            public int CountAdjacentMines(int row, int col)
            {
                int count = 0;
                for (int r = Math.Max(0, row - 1); r <= Math.Min(row + 1, rows - 1); r++)
                {
                    for (int c = Math.Max(0, col - 1); c <= Math.Min(col + 1, cols - 1); c++)
                    {
                        if (board[r, c].FieldBombe)
                        {
                            count++;
                        }
                    }
                }
                // Subtrahiere 1, falls das aktuelle Feld selbst eine Mine ist
                if (board[row, col].FieldBombe)
                {
                    count--;
                }
                return count;
            }


            /// <summary>
            /// Gibt das Feld an den angegebenen Koordinaten zurück.
            /// </summary>
            public Field GetField(int row, int col)
            {
                return board[row, col];
            }

            /// <summary>
            /// Führt einen Spielzug durch.
            /// </summary>
            public void MakeMove(int row, int col)
            {
                if (IsValidMove(row, col))
                {
                    Field field = board[row, col];
                    // Überprüfen, ob das Feld bereits aufgedeckt ist oder nicht
                    if (!field.IsFieldUncovered)
                    {
                        field.IsFieldUncovered = true;
                        if (field.FieldBombe)
                        {
                            Console.WriteLine("Bombe! Sie haben verloren.");
                            int choice = View.GetGameOverChoice(this);
                            if (choice == 1)
                            {
                                Reset();
                                return;
                            }
                            else if(choice == 2)
                            {
                                Environment.Exit(0);
                            }
                            else if (choice == 3)
                            {
                                Undo(); 
                            }

                        }
                        else if (IsGameWon())
                        {
                            Console.WriteLine("Herzlichen Glückwunsch! Sie haben gewonnen.");
                            int choice = View.GetGameOverChoice(this);
                            if (choice == 1)
                            {
                                Reset();
                                return;
                            }
                            else
                            {
                                Environment.Exit(0);
                            }
                        }

                        SaveState(); // Speichere den aktuellen Spielzustand nach jedem Zug
                    }
                    else
                    {
                        Console.WriteLine("Das Feld wurde bereits aufgedeckt. Wählen Sie ein anderes Feld.");
                    }
                }
            }


            /// <summary>
            /// Überprüft, ob ein Spielzug gültig ist.
            /// </summary>
            private bool IsValidMove(int row, int col)
            {
               
                if (row < 0 || row >= Level.Rows || col < 0 || col >= Level.Cols)
                {
                    Console.WriteLine("Ungültige Koordinaten. Bitte geben Sie gültige Koordinaten ein.");
                    return false;
                }
                return true;
            }

            /// <summary>
            /// Speichert den aktuellen Spielzustand.
            /// </summary>
            public void SaveState()
            {
                MementoField memento = new MementoField();
                memento.board = CloneFieldArray(board);
                caretaker.Push(memento);
            }

            /// <summary>
            /// Führt einen Rückgängig-Befehl aus.
            /// </summary>
            public void Undo()
            {
                MementoField memento = caretaker.Pop();
                if (memento != null)
                {
                    board = memento.board;

                 
                    foreach (var field in board)
                    {
                        field.IsFieldUncovered = false;
                    }
                }
            }


            /// <summary>
            /// Klont das Spielfeld.
            /// </summary>
            private Field[,] CloneFieldArray(Field[,] original)
            {
                int rows = original.GetLength(0);
                int cols = original.GetLength(1);
                Field[,] clone = new Field[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        clone[i, j] = new Field();
                        clone[i, j].IsFieldUncovered = original[i, j].IsFieldUncovered;
                        clone[i, j].FieldBombe = original[i, j].FieldBombe;
                        clone[i, j].NumberBombesNearby = original[i, j].NumberBombesNearby;
                    }
                }
                return clone;
            }

            /// <summary>
            /// Überprüft, ob das Spiel gewonnen wurde.
            /// </summary>
            private bool IsGameWon()
            {
                foreach (Field field in board)
                {
                    if (!field.FieldBombe && !field.IsFieldUncovered)
                        return false;
                }
                return true;
            }

            public void Reset()
            {
                InitializeBoard();
                PlaceMines();
                UpdateAdjacentMinesCount();
            }
        }
    }
}