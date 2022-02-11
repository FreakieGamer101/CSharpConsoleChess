using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1
{
    public static class TestFunctions
    {
        public static void EnterPosition(string postition)
        {
            Console.Write(postition);
            PressEnterKey();
        }

        public static ConsoleKey PressEnterKey()
        {
            return ConsoleKey.Enter;
        }

        public static void MovePiece(string initial, string final)
        {
            EnterPosition(initial);
            EnterPosition(final);
        }
    }
}
