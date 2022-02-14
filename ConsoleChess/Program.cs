using System;
using Chessboard;
using ConsoleChess.Chessboard;
using Game;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = GameMode();
            switch (choice)
            {
                case 1:
                    OriginalGame();
                    break;

                case 2: 
                    Mode960();
                    break;

                default:
                    break;
            }
        }

        //Classic chess game
        static void OriginalGame()
        {
            try
            {
                Match match = new Match();

                while (!match.GameOver)
                {

                    try
                    {
                        Console.Clear();
                        Canvas.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Type initial position: ");
                        Position initial = Canvas.ReadPieceMovimentInput().ToPosition();
                        match.ValidateInitialPosition(initial);

                        bool[,] GetPossibleMoves = match.Board.Piece(initial).GetAllPossibleMoves();

                        Console.Clear();
                        Canvas.PrintBoard(match.Board, GetPossibleMoves);

                        Console.WriteLine();
                        Console.Write("Type final position: ");
                        Position final = Canvas.ReadPieceMovimentInput().ToPosition();
                        match.ValidateFinalPosition(initial, final);

                        match.PlayTurn(initial, final);

                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine("\r\n" + ex.Message);
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                Canvas.PrintBoard(match.Board);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        //needs to be customed to 960Mode
        static void Mode960()
        {
            try
            {
                Match match = new Match();

                while (!match.GameOver)
                {

                    try
                    {
                        Console.Clear();
                        Canvas.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Type initial position: ");
                        Position initial = Canvas.ReadPieceMovimentInput().ToPosition();
                        match.ValidateInitialPosition(initial);

                        bool[,] GetPossibleMoves = match.Board.Piece(initial).GetAllPossibleMoves();

                        Console.Clear();
                        Canvas.PrintBoard(match.Board, GetPossibleMoves);

                        Console.WriteLine();
                        Console.Write("Type final position: ");
                        Position final = Canvas.ReadPieceMovimentInput().ToPosition();
                        match.ValidateFinalPosition(initial, final);

                        match.PlayTurn(initial, final);

                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine("\r\n" + ex.Message);
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                Canvas.PrintBoard(match.Board);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        //needs to catch parsing exception
        static int GameMode()
        {
            Console.WriteLine("Which game mode would you like to play?");
            Console.WriteLine("1. Classic");
            Console.WriteLine("2. 960");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }
    }
}
