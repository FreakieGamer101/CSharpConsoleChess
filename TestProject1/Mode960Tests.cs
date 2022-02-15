using NUnit.Framework;
using ConsoleChess.Game;
using Game;
using Chessboard;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;

namespace TestProject1
{
    class Mode960Tests
    {
        Mode960 game;

        [Test]
        public void RandomizePieces()
        {
            game.Execute();
            Position WhiteKing = new Position(5, 5), WhiteLeftRook = new Position(5, 5), WhiteRightRook = new Position(5, 0), BlackKing = new Position(5, 5), BlackLeftRook = new Position(5, 5), BlackRightRook = new Position(5, 0);
            int kingCount = 0, rookCount = 0, queenCount = 0, bishopCount = 0, knightCount = 0;

            int index = 0;
            foreach(char piece in game.randomizedPieces)
            {
                switch (piece)
                {
                    case 'K':
                        kingCount++;
                        WhiteKing.Line = 0;
                        WhiteKing.Column = index;
                        BlackKing.Line = 7;
                        BlackKing.Column = index;
                        break;
                    case 'Q':
                        queenCount++;
                        break;
                    case 'H':
                        knightCount++;
                        break;
                    case 'R':
                        rookCount++;
                        if(rookCount == 1)
                        {
                            WhiteLeftRook.Line = 0;
                            WhiteLeftRook.Column = index;
                            BlackLeftRook.Line = 7;
                            BlackLeftRook.Column = index;
                        }
                        else
                        {
                            WhiteRightRook.Line = 0;
                            WhiteRightRook.Column = index;
                            BlackRightRook.Line = 7;
                            BlackRightRook.Column = index;
                        }
                        break;
                    case 'B':
                        bishopCount++;
                        break;

                    default:
                        break;
                }
                index++;
            }

            bool whiteKingBetweenRooks = WhiteLeftRook.Column < WhiteKing.Column && WhiteRightRook.Column > WhiteKing.Column;
            bool blackKingBetweenRooks = BlackLeftRook.Column < BlackKing.Column && BlackRightRook.Column > BlackKing.Column;
            bool countsAreCorrect = kingCount == 1 && queenCount == 1 && bishopCount == 2 && rookCount == 2 && knightCount == 2;
            bool piecesCorrespond = WhiteLeftRook.Column == BlackLeftRook.Column && WhiteKing.Column == BlackKing.Column && WhiteRightRook.Column == BlackRightRook.Column;

            bool result = whiteKingBetweenRooks && blackKingBetweenRooks && countsAreCorrect && piecesCorrespond;

            Assert.IsTrue(result);
        }

        [Test]
        public void Castling()
        {
            game.Execute();
            Board board = game.match.Board;

            King king = new King(board, Color.White, new Match());
            Rook leftRook = new Rook(board, Color.White);
            Rook rightRook = new Rook(board, Color.White);

            int rooks = 0;

            foreach(Piece piece in board.GetPieces())
            {
                if(piece != null && piece.Color == Color.White)
                {
                    if (piece.ToString() == "K")
                    {
                        king = (King)piece;
                    }
                    else if (piece.ToString() == "R" && rooks == 0)
                    {
                        leftRook = (Rook)piece;
                        rooks++;
                    }
                    else if (piece.ToString() == "R" && rooks == 1)
                    {
                        rightRook = (Rook)piece;
                    }
                }
            }

            //leftRook, king, rightRook asserted correctly

            //Use pieces columns to determine whice testing would be shorter
            int direction = 0, column = 1, row = 7;
            if(king.Position.Column - leftRook.Position.Column < rightRook.Position.Column - king.Position.Column)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            do
            {
                if(game.match.CurrentPlayer == Color.Black)
                {
                    foreach(Piece piece in game.match.Board.GetPieces())
                    {
                        if(piece.Color == Color.Black && piece.GetType() == typeof(Pawn) && piece.HasPossibleMoves())
                        {
                            int position = -1;
                            bool[,] possible = piece.GetAllPossibleMoves();
                            foreach (bool place in possible)
                            {
                                position++;
                                if (place)
                                {
                                    decimal divided = position / 8;
                                    game.match.PlayTurn(piece.Position, new Position((int)Math.Floor(divided), (int)Math.Ceiling(divided)));

                                }
                            }
                        }
                    }
                }
                //Position initial = Canvas.ReadPieceMovimentInput().ToPosition();

                //bool[,] GetPossibleMoves = game.match.Board.Piece(initial).GetAllPossibleMoves();

                //Position final = Canvas.ReadPieceMovimentInput().ToPosition();

                //game.match.PlayTurn(initial, final);

            } while (!game.match.GameOver);

            Assert.IsTrue(leftRook.Position.Column < king.Position.Column && rightRook.Position.Column > king.Position.Column);
        }
    }
}
