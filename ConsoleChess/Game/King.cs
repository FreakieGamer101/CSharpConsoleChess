using Chessboard;

namespace Game
{
    public class King : Piece
    {
        public Match Match { get; set; }
        public King(Board board, Color color, Match match) : base(board, color)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position possiblePosition)
        {
            Piece piece = Board.Piece(possiblePosition);
            return piece == null || piece.Color != Color;
        }

        private bool ValidateRookCanCastle(Position position)
        {
            //not being used
            Piece piece = Board.Piece(position);

            return piece != null && piece is Rook && piece.Color == Color && piece.MovementQuantity == 0;

        }

        public override bool[,] GetAllPossibleMoves()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];
            Position possiblePosition = new Position(0, 0);

            // north
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // northeast
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // east
            possiblePosition.UpdateValues(Position.Line, Position.Column + 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // southeast
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // south
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // southwest
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // west
            possiblePosition.UpdateValues(Position.Line, Position.Column - 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // northwest
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // special castling move
            if (MovementQuantity == 0 && !Match.Checkmate)
            {
                // king-side castling
                Position expectedRookPosition1 = new Position(Position.Line, Position.Column + 3);
                if (ValidateRookCanCastle(expectedRookPosition1))
                {
                    Piece RightSlot1 = Board.Piece(Position.Line, Position.Column + 1);
                    Piece RightSlot2 = Board.Piece(Position.Line, Position.Column + 2);

                    if (RightSlot1 == null && RightSlot2 == null)
                    {
                        matrix[Position.Line, Position.Column + 3] = true;
                    }
                }

                // queen-side castling
                Position expectedRookPosition2 = new Position(Position.Line, Position.Column - 4);
                if (ValidateRookCanCastle(expectedRookPosition2))
                {

                    Piece LeftSlot1 = Board.Piece(Position.Line, Position.Column - 1);
                    Piece LeftSlot2 = Board.Piece(Position.Line, Position.Column - 2);
                    Piece LeftSlot3 = Board.Piece(Position.Line, Position.Column - 3);

                    if (LeftSlot1 == null && LeftSlot2 == null && LeftSlot3 == null)
                    {
                        matrix[Position.Line, Position.Column - 4] = true;
                    }
                }
            }

            return matrix;
        }
    }
}