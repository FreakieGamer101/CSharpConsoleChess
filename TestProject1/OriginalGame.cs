using Chessboard;
using ConsoleChess;
using Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestProject1
{
    public class Tests
    {
        public Match Match;
        [SetUp]
        public void SetUp()
        {
            Match = new Match();
        }


        [Test]
        public void WhiteWins()
        {

        }

        [Test]
        public void BlackWins()
        {

        }


        [Test]
        [System.Obsolete]
        public void QueenSideCastling()
        {
            //Positions to move to, white going first
            string[][] positions = new string[11][] { 
            new string[] {"b2", "b3" }, new string[] { "a7", "a6" }, new string[] { "b1", "c3" }, 
            new string[] { "a6", "a5" }, new string[] { "c1", "a3" }, new string[] { "a5", "a4" }, 
            new string[] { "d1", "c1" }, new string[] { "b7", "b6" }, new string[] { "c1", "b2" }, 
            new string[] { "b6", "b5" }, new string[] { "e1", "a1" } 
            };

            //set piece to look at
            Piece rook = Match.Board.Piece(new Position(7, 0));
            Piece king = Match.Board.Piece(new Position(7, 4));

            //go through turns
            for(int i = 0; i < positions.Length; i++) {
                //Get board positions to conver to positions to pass in
                BoardPosition initial = new BoardPosition(positions[i][0][0], int.Parse(positions[i][0][1].ToString()));
                Position init = initial.ToPosition();
                BoardPosition final = new BoardPosition(positions[i][1][0], int.Parse(positions[i][1][1].ToString()));
                Position fin = final.ToPosition();

                Match.ValidateInitialPosition(init);

                bool[,] GetPossibleMoves = Match.Board.Piece(init).GetAllPossibleMoves();

                Match.ValidateFinalPosition(init, fin);

                Match.PlayTurn(init, fin);

                rook = Match.Board.Piece(new Position(7, 4));
                king = Match.Board.Piece(new Position(7, 0));
            }

            bool result = ((king.GetType() == typeof(King)) && (rook.GetType() == typeof(Rook)));
            Assert.IsTrue(result);
        }
    }
}