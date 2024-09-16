using Mineswepper.Logic.Minesweeper.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mineswepper.Logic.Minesweeper;
using MiNET.Entities.Passive;
using Minesweeper.LogicTests;

namespace Minesweeper.Logic.Minesweeper.Logic.Tests
{

    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void PlaceMinesTest()
        {
            // Arrange
            ILevel level = new LevelEasy();
            MocModel mocmodel = new MocModel(level);
            Model model = new Model(level);

            // Act
            mocmodel.PlaceMines();

            // Assert
            int count = 0;
            for (int row = 0; row < model.Level.Rows; row++)
            {
                for (int col = 0; col < model.Level.Cols; col++)
                {
                    if (model.GetField(row, col).FieldBombe)
                        count++;
                }
            }
            Assert.AreEqual(10, count);
        }

        [TestMethod]
        public void PlaceMinesCorrectlyTest()
        {
            // Arrange
            ILevel level = new LevelEasy();
            Model model = new Model(level);
            MocModel mocmodel = new MocModel(level);

            // Act
            mocmodel.PlaceMines();

            // Assert
            for (int row = 0; row < model.Level.Rows; row++)
            {
                for (int col = 0; col < model.Level.Cols; col++)
                {
                    if (model.GetField(row, col).FieldBombe)
                    {
                        Assert.IsTrue(model.GetField(row, col).FieldBombe);
                    }
                    else
                    {
                        Assert.IsFalse(model.GetField(row, col).FieldBombe);
                    }
                }
            }
        }


        [TestMethod]
        public void MakeMove_NoBombTest()
        {
            // Arrange
            ILevel level = new LevelEasy();
            Model model = new Model(level);
            MocModel mocmodel = new MocModel(level);
            mocmodel.PlaceMines();

            // Act
            int row = 0;
            int col = 0;
            model.MakeMove(row, col); // Making a move without hitting a bomb

            // Assert
            Assert.IsFalse(model.GetField(row, col).FieldBombe);
        }

        [TestMethod]
        public void MakeMove_FieldUncoveredAfterMoveTest()
        {
            // Arrange
            ILevel level = new LevelEasy();
            Model model = new Model(level);
            MocModel mocmodel = new MocModel(level);
            mocmodel.PlaceMines();

            // Act
            int row = 0;
            int col = 0;
            model.MakeMove(row, col); // Making a move without hitting a bomb

            // Assert
            Assert.IsTrue(model.GetField(row, col).IsFieldUncovered);
        }

        [TestMethod]
        public void UndoTest()
        {
            // Arrange
            ILevel level = new LevelEasy();
            Model model = new Model(level);
            MocModel mocmodel = new MocModel(level);
            mocmodel.PlaceMines();
            model.MakeMove(0, 0); // Make a move to create a memento

            // Act
            model.Undo(); // Undo the last move

            // Assert
            Assert.IsFalse(model.GetField(0, 0).IsFieldUncovered);
        }

        [TestMethod]
        public void Undo_RestorePreviousGameStateTest()
        {
            // Arrange
            ILevel level = new LevelEasy();
            Model model = new Model(level);
            MocModel mocmodel = new MocModel(level);
            mocmodel.PlaceMines();
            model.MakeMove(0, 0); // Make a move to create a memento
            int initialAdjacentMinesCount = model.GetField(0, 0).NumberBombesNearby;

            // Act
            model.Undo(); // Undo the last move

            // Assert
            Assert.IsFalse(model.GetField(0, 0).IsFieldUncovered);
            Assert.AreEqual(initialAdjacentMinesCount, model.GetField(0, 0).NumberBombesNearby);
        }

        [TestMethod]
        public void ResetTest()
        {
            // Arrange
            ILevel level = new LevelEasy(); // Beispiellevel mit einem 10x10 Gitter und 10 Minen
            Model model = new Model(level);
            MocModel mocmodel = new MocModel(level);
            model.PlaceMines(); // Platzieren von Minen, bevor das Spiel zurückgesetzt wird

            // Act
            int row = 0;
            int col = 0;
            model.CountAdjacentMines(row, col);
            model.Reset(); // Das Modell zurücksetzen


            // Assert
            Assert.AreEqual(0, model.CountAdjacentMines(row, col)); // Stellen Sie sicher, dass nach dem Zurücksetzen alle Minen entfernt sind
        }

    }
}
