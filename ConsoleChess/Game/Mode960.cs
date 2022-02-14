using Chessboard;
using Game;
using System;
using System.Collections.Generic;
using ConsoleChess.Chessboard;

namespace ConsoleChess.Game
{
    public class Mode960
    {
        public Match match;
        public char[] randomizedPieces = new char[8] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        char[] pieces = new char[8] { 'K', 'Q', 'R', 'R', 'H', 'H', 'B', 'B' };

        public void Execute()
        {
            SetUp();
        }

        public void SetUp()
        {
            //create match
            match = new Match();

            //Reset Board
            ClearBoard();

            //Insert Pieces
            InsertPawns();
            InsertRandomPieces();
        }

        public void RandomizeBackRow()
        {
            int i;
            int rooks = 0;
            int bishops = 0;
            int numPieces = 0;
            int kingPlaced = 9, lastRook = 0; 
            bool leftRook = false, rightRook = false;
            bool allSet = false;
            bool evenBishop = false;
            bool oddBishop = false;
            bool king = false;
            var rand = new Random();
            do
            {
                int randomIndex = rand.Next(0, 8);
                i = rand.Next(0, 8);
                char piece = pieces[randomIndex];
                bool isSpace = randomizedPieces[i] == ' ';
                bool isCopied = pieces[randomIndex] == ' ';

                switch (piece)
                {
                    case 'B':
                        if (i % 2 == 0 && !evenBishop && isSpace && !isCopied && (i > 0 && i < 7))
                        {
                            randomizedPieces.SetValue('B', i);
                            evenBishop = true;
                            pieces.SetValue(' ', randomIndex);
                            numPieces++;
                            bishops++;
                        }
                        else if (i % 2 == 1 && !oddBishop && isSpace && !isCopied)
                        {
                            randomizedPieces.SetValue('B', i);
                            oddBishop = true;
                            pieces.SetValue(' ', randomIndex);
                            numPieces++;
                            bishops++;
                        }
                        break;

                    case 'R':
                        bool placed = false;
                        //cut up on placing second rook - left rook on if statement
                        if (king && isSpace && !isCopied)
                        {
                            if (i < kingPlaced && !leftRook)
                            {
                                randomizedPieces.SetValue('R', i);
                                pieces.SetValue(' ', randomIndex);
                                leftRook = true;
                                rooks++;
                            numPieces++;
                            }
                            else if (i > kingPlaced && !rightRook)
                            {
                                randomizedPieces.SetValue('R', i);
                                pieces.SetValue(' ', randomIndex);
                                rightRook = true;
                                rooks++;
                            numPieces++;
                            }
                        }
                        break;

                    case 'K':
                        if (bishops == 2 && isSpace && !isCopied && i > 0 && i < 7)
                        {
                            randomizedPieces.SetValue('K', i);
                            pieces.SetValue(' ', randomIndex);
                            numPieces++;
                            kingPlaced = i;
                            king = true;
                        }
                        break;

                    case 'H':
                        if (isSpace && !isCopied && rooks == 2)
                        {
                            randomizedPieces.SetValue('H', i);
                            pieces.SetValue(' ', randomIndex);
                            numPieces++;
                        }
                        break;

                    case 'Q':
                        if (isSpace && !isCopied && rooks == 2)
                        {
                            randomizedPieces.SetValue('Q', i);
                            pieces.SetValue(' ', randomIndex);
                            numPieces++;
                        }
                        break;

                    default:
                        break;
                }
                if (numPieces >= 8) allSet = true;
            } while (!allSet);
        }
        
        public void ClearBoard()
        {
            foreach(Piece piece in match.Board.GetPieces())
            {
                //17
                if (piece != null)
                {
                    match.Board.RemovePiece(piece.Position);
                }
            }
        }

        public void InsertPawns()
        {
            #region Creat White Pawns
            match.Board.InsertPiece(new Pawn(match.Board, Color.White, match), new BoardPosition('a', 2).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.White, match), new BoardPosition('b', 2).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.White, match), new BoardPosition('c', 2).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.White, match), new BoardPosition('d', 2).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.White, match), new BoardPosition('e', 2).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.White, match), new BoardPosition('f', 2).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.White, match), new BoardPosition('g', 2).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.White, match), new BoardPosition('h', 2).ToPosition());
            #endregion

            #region Creat Black Pawns
            match.Board.InsertPiece(new Pawn(match.Board, Color.Black, match), new BoardPosition('a', 7).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.Black, match), new BoardPosition('b', 7).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.Black, match), new BoardPosition('c', 7).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.Black, match), new BoardPosition('d', 7).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.Black, match), new BoardPosition('e', 7).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.Black, match), new BoardPosition('f', 7).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.Black, match), new BoardPosition('g', 7).ToPosition());
            match.Board.InsertPiece(new Pawn(match.Board, Color.Black, match), new BoardPosition('h', 7).ToPosition());
            #endregion
        }

        public void InsertRandomPieces()
        {
            #region Randomize Back Rows
            RandomizeBackRow();
            for(int i = 0; i < 8; i++)
            {
                switch (randomizedPieces[i])
                {
                    case 'K':
                        match.Board.InsertPiece(new King(match.Board, Color.White, match), new Position(0, i));
                        match.Board.InsertPiece(new King(match.Board, Color.Black, match), new Position(7, i));
                        break;

                    case 'Q':
                        match.Board.InsertPiece(new Queen(match.Board, Color.White), new Position(0, i));
                        match.Board.InsertPiece(new Queen(match.Board, Color.Black), new Position(7, i));
                        break;

                    case 'B':
                        match.Board.InsertPiece(new Bishop(match.Board, Color.White), new Position(0, i));
                        match.Board.InsertPiece(new Bishop(match.Board, Color.Black), new Position(7, i));
                        break;

                    case 'H':
                        match.Board.InsertPiece(new Knight(match.Board, Color.White), new Position(0, i));
                        match.Board.InsertPiece(new Knight(match.Board, Color.Black), new Position(7, i));
                        break;

                    case 'R':
                        match.Board.InsertPiece(new Rook(match.Board, Color.White), new Position(0, i));
                        match.Board.InsertPiece(new Rook(match.Board, Color.Black), new Position(7, i));
                        break;

                    default:
                        break;
                }
            }
            #endregion
        }
    }
}
