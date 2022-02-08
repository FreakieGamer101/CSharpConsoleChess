namespace Chessboard
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementQuantity { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            MovementQuantity = 0;
        }

        public void IncrementMovementQuantity()
        {
            MovementQuantity++;
        }

        public void DecrementMovementQuantity()
        {
            MovementQuantity--;
        }

        public bool HasPossibleMoves()
        {
            bool[,] possibleMoves = GetAllPossibleMoves();
            for (int l = 0; l < Board.Lines; l++)
            {
                for (int c = 0; c < Board.Columns; c++)
                {
                    if (possibleMoves[l, c])
                        return true;
                }
            }

            return false;
        }

        public bool IsPossibleMoveTo(Position position)
        {
            return GetAllPossibleMoves()[position.Line, position.Column];
        }

        public abstract bool[,] GetAllPossibleMoves();

        public string ToUnicodeString()
        {
            switch (this.ToString())
            {
                case "K":
                    return "K";
                case "Q":
                    return "Q";
                case "R":
                    return "R";
                case "B":
                    return "B";
                case "H":
                    return "H";
                case "P":
                    return "P";
                default:
                    return "";
            }
        }
    }
}