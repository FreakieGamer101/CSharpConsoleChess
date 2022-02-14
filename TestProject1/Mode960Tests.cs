using NUnit.Framework;
using ConsoleChess.Game;
using Game;
using Chessboard;

namespace TestProject1
{
    class Mode960Tests
    {
        [Test]
        public void RandomizePieces()
        {
            Mode960 game = new Mode960();
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
    }
}
