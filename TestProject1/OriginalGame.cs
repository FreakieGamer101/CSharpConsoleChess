using Chessboard;
using Game;
using NUnit.Framework;

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
        [TestCase (true, "a1")]
        [TestCase (false, "h1")]
        public void Castling(bool isQueenSide, string kingExpected)
        {
            string[][] positions;
            //Positions to move to, white going first
            if (isQueenSide)
            {
                positions = new string[][] {
                    new string[] {"b2", "b3" }, new string[] { "a7", "a6" }, 
                    new string[] { "b1", "c3" }, new string[] { "a6", "a5" }, 
                    new string[] { "c1", "a3" }, new string[] { "a5", "a4" },
                    new string[] { "d1", "c1" }, new string[] { "b7", "b6" }, 
                    new string[] { "c1", "b2" }, new string[] { "b6", "b5" }, 
                    new string[] { "e1", "a1" }
                };
            }
            else
            {
                positions = new string[][] {
                    new string[] {"g2", "g3"}, new string[] {"a7", "a6"},
                    new string[] {"g1", "f3"}, new string[] {"a6", "a5"},
                    new string[] {"f1", "h3"}, new string[] {"a5", "a4"},
                    new string[] {"e1", "h1"}
                };
            }

            BoardPosition kingExpectedPosition = new BoardPosition(kingExpected[0], int.Parse(kingExpected[1].ToString()));

            //set piece to look at
            Piece rook = Match.Board.Piece(new Position(7, 4));
            Piece king = Match.Board.Piece(kingExpectedPosition.ToPosition());

            //go through turns
            for (int i = 0; i < positions.Length; i++) {
                //Get board positions to conver to positions to pass in
                BoardPosition initial = new BoardPosition(positions[i][0][0], int.Parse(positions[i][0][1].ToString()));
                Position init = initial.ToPosition();
                BoardPosition final = new BoardPosition(positions[i][1][0], int.Parse(positions[i][1][1].ToString()));
                Position fin = final.ToPosition();

                Match.PlayTurn(init, fin);

                //reset pieces to get latest pieces in positions
                rook = Match.Board.Piece(new Position(7, 4));
                king = Match.Board.Piece(kingExpectedPosition.ToPosition());
            }

            bool result = ((king.GetType() == typeof(King)) && (rook.GetType() == typeof(Rook)));
            Assert.IsTrue(result);
        }
    }
}