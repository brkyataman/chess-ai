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

            Board board = new Board();
            board.InitiliazeBoard();
            var x = board.getBoard();
            printBoard(x);

            var y = board.GetPlayableMoves();
            printMoves(y);
            input = System.Console.ReadLine();
        }

        public static void printBoard(Square[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                string s = "";
                for (int j = 0; j < 8; j++)
                {
                    
                    if(!board[i, j].isOccupied){
                        s += "x ";
                    }
                    else
                    {
                        s += board[i, j].occupiedBy.color.ToString() + board[i, j].occupiedBy.GetType().ToString().Substring(17,1) + " ";
                    }
                }

                System.Console.WriteLine( s + "\n");
            }
        }

        public static void printMoves(List<Move> moves)
        {
            string s = "";
            foreach (var move in moves)
            {
                s += "(" + move.from_x + ","
                    + move.from_y + ")" 
                    + " to (" + move.to_x +","
                    + move.to_y + ")\n";
                
            }
            System.Console.WriteLine(s);
        }
    }
}
