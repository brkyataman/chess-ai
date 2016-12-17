using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Test
    {
        public void printBoard(Square[,] board)
        {
            string s = "";

            for (int i = 7; i > -1; i--)
            {
                s = i + "  ";
                for (int j = 0; j < 8; j++)
                {

                    if (!board[i, j].isOccupied)
                    {
                        s += "x  ";
                    }
                    else
                    {
                        s += board[i, j].occupiedBy.color.ToString() + board[i, j].occupiedBy.GetType().ToString().Substring(17, 1) + " ";
                    }
                }

                System.Console.WriteLine(s + "\n");
            }

            s = "   ";
            for (int i = 0; i < 8; i++)
            {
                s += i + "  ";

            }
            System.Console.WriteLine(s + "\n");
        }
    }
}
