using Mineswepper.Logic.Minesweeper.Logic;
using Mineswepper.Logic;
using Minesweeper.Logic;

/// <summary>
/// Die Klasse View stellt Methoden bereit, um Informationen an den Benutzer auszugeben und Benutzereingaben zu erhalten.
/// </summary>
public class View
{
    /// <summary>
    /// Fordert den Benutzer auf, eine Zeilennummer einzugeben.
    /// </summary>
    /// <returns>Die eingegebene Zeilennummer.</returns>
    public static int GetRowInput()
    {
        Console.WriteLine("Geben Sie die Zeilennummer ein:");
        string rowInput = Console.ReadLine();
        int row;
        while (!int.TryParse(rowInput, out row))
        {
            Console.WriteLine("Fehlerhafte Eingabe. Bitte geben Sie eine gültige ganze Zahl für die Zeilennummer ein:");
            rowInput = Console.ReadLine();
        }
        return row;
    }

    /// <summary>
    /// Fordert den Benutzer auf, eine Spaltennummer einzugeben.
    /// </summary>
    /// <returns>Die eingegebene Spaltennummer.</returns>
    public static int GetColInput()
    {
        Console.WriteLine("Geben Sie die Spaltennummer ein:");
        string colInput = Console.ReadLine();
        int col;
        while (!int.TryParse(colInput, out col))
        {
            Console.WriteLine("Fehlerhafte Eingabe. Bitte geben Sie eine gültige ganze Zahl für die Spaltennummer ein:");
            colInput = Console.ReadLine();
        }
        return col;
    }

    /// <summary>
    /// Zeigt eine Nachricht auf der Konsole an.
    /// </summary>
    /// <param name="message">Die anzuzeigende Nachricht.</param>
    public static void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Fordert den Benutzer auf, eine Wahl nach dem Spielende zu treffen.
    /// </summary>
    /// <param name="model">Das Model-Objekt.</param>
    /// <returns>Die vom Benutzer getroffene Wahl.</returns>
    public static int GetGameOverChoice(Model model)
    {
        Console.WriteLine("Du hast eine Mine getroffen. Das Spiel ist vorbei.");
        Console.WriteLine("Möchten Sie nochmals eine Runde spielen? Drücken Sie 1 für Ja, 2 für Nein oder 3, um den letzten Zug rückgängig zu machen.");

        string choiceInput = Console.ReadLine();
        int choice;
        while (!int.TryParse(choiceInput, out choice) || (choice != 1 && choice != 2 && choice != 3))
        {
            Console.WriteLine("Ungültige Eingabe. Bitte geben Sie 1 für Ja, 2 für Nein oder 3, um den letzten Zug rückgängig zu machen:");
            choiceInput = Console.ReadLine();
        }

        if (choice == 1)
        {
            // Neue Runde starten
            return 1;
        }
        else if (choice == 2)
        {
            // Spiel verlassen
            Environment.Exit(0);
            return 2; // Dies wird nie erreicht, da Environment.Exit(0) das Programm beendet, aber der Compiler erwartet einen Rückgabewert
        }
        else
        {
            // Letzten Zug rückgängig machen und Spiel fortsetzen
            model.Undo();
            return 0;
        }
    }

    /// <summary>
    /// Zeigt das Spielfeld auf der Konsole an.
    /// </summary>
    /// <param name="model">Das Model-Objekt.</param>
    public void DisplayBoard(Model model)
    {
        Console.Write("    ");
        for (int col = 0; col < model.Level.Cols; col++)
        {
            if (col < 10)
            {
                Console.Write($"{col,2} ");
            }
            else
            {
                Console.Write($"{col,3} ");
            }
        }
        Console.WriteLine();

        for (int i = 0; i < model.Level.Rows; i++)
        {
            Console.Write($"{i,2}  ");
            for (int j = 0; j < model.Level.Cols; j++)
            {
                Field field = model.GetField(i, j);
                if (field.IsFieldUncovered && field.IsUndo) // Feld anzeigen, wenn es nicht rückgängig gemacht wurde
                {
                    if (field.FieldBombe)
                    {
                        if (j <= 10)
                        {
                            Console.Write(" X ");
                        }
                        else
                        {
                            Console.Write("  X ");
                        }
                    }
                    else
                    {
                        if (field.NumberBombesNearby == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            if (j >= 10)
                            {
                                Console.Write($"  {field.NumberBombesNearby} ");
                            }
                            else
                            {
                                Console.Write($" {field.NumberBombesNearby} ");
                            }
                        }
                        else if (field.NumberBombesNearby == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            if (j >= 10)
                            {
                                Console.Write($"  {field.NumberBombesNearby} ");
                            }
                            else
                            {
                                Console.Write($" {field.NumberBombesNearby} ");
                            }
                        }
                        else if (field.NumberBombesNearby >= 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (j >= 10)
                            {
                                Console.Write($"  {field.NumberBombesNearby} ");
                            }
                            else
                            {
                                Console.Write($" {field.NumberBombesNearby} ");
                            }
                        }
                        else if (field.NumberBombesNearby == 0 && model.Level.Cols >= 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            if (j >= 10)
                            {
                                Console.Write($"  {field.NumberBombesNearby} ");
                            }
                            else
                            {
                                Console.Write($" {field.NumberBombesNearby} ");
                            }
                        }
                        else
                        {
                            Console.Write($"{field.NumberBombesNearby} ");
                        }
                        Console.ResetColor();
                    }
                }
                else
                {
                    if (j < 10)
                    {
                        Console.Write(" # ");
                    }
                    else
                    {
                        Console.Write("  # ");
                    }
                }
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Startet das Spiel und steuert den Ablauf.
    /// </summary>
    /// <param name="model">Das Model-Objekt.</param>
    public void Game(Model model)
    {
        bool continuePlaying = true;

        while (continuePlaying)
        {
            // Das aktuelle Spielfeld anzeigen
            Console.Clear();
            DisplayBoard(model);

            // Anleitung zum Rückgängigmachen eines Zugs
            Console.WriteLine("Drücken Sie 2 Mal die '1', um den letzten Zug rückgängig zu machen oder eine andere Taste zum Fortfahren.");

            // Überprüfen, ob der Benutzer den letzten Zug rückgängig machen möchte
            int input;
            if (int.TryParse(Console.ReadLine(), out input) && input == 1)
            {
                model.Undo();
                continue; // Fortfahren mit dem aktuellen Spielfeld nach dem Rückgängigmachen
            }

            // Einen Zug machen und das Spielfeld aktualisieren
            int row = GetRowInput();
            int col = GetColInput();
            model.MakeMove(row, col);
        }
    }
}



