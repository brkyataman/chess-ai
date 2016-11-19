using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    class Program
    {
        static void Main()
        {
            string input;
            //Board chessBoard;

            System.Console.WriteLine("Welcome to Ultimate Chess Hell");
            input = System.Console.ReadLine();
            System.Console.WriteLine("You wrote: " + input + "\n");

            State state = new State();
            state.InitiliazeBoard();


            while (true) {
                var x = state.getBoard();
                printBoard(x);

                var y = state.GetPlayableMoves();
                printMoves(y);

                System.Console.WriteLine("From X:");
                string from_x = System.Console.ReadLine();

                System.Console.WriteLine("From Y:");
                string from_y = System.Console.ReadLine();

                System.Console.WriteLine("To X:");
                string to_x = System.Console.ReadLine();

                System.Console.WriteLine("To Y:");
                string to_y = System.Console.ReadLine();

                state.GenerateMove((int)from_x[0]-48, (int)from_y[0]-48, (int)to_x[0]-48, (int)to_y[0]-48);
                if (!state.IsKingChecked(0, 4))
                {
                    System.Console.WriteLine("No check!");
                }

            }
            input = System.Console.ReadLine();
        }

        public static void printBoard(Square[,] board)
        {
            for (int i = 7; i > -1; i--)
            {
                string s = "";
                for (int j = 0; j < 8; j++)
                {

                    if (!board[i, j].isOccupied)
                    {
                        s += "x ";
                    }
                    else
                    {
                        s += board[i, j].occupiedBy.color.ToString() + board[i, j].occupiedBy.GetType().ToString().Substring(17, 1) + " ";
                    }
                }

                System.Console.WriteLine(s + "\n");
            }
        }

        public static void printMoves(List<Move> moves)
        {
            string s = "";
            foreach (var move in moves)
            {
                s += "(" + move.from_x + ","
                    + move.from_y + ")"
                    + " to (" + move.to_x + ","
                    + move.to_y
                    + ") " + move.msg + "\n";

            }
            System.Console.WriteLine(s);
        }
    }
}
