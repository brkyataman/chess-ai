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
            Board chessBoard;
            
            System.Console.WriteLine("Welcome to Ultimate Chess Hell");
            input = System.Console.ReadLine();
            System.Console.WriteLine("You wrote: " + input + "\n");

            Piece _pawn = new Pawn();
            _pawn._x = 3;
            _pawn._y = 4;
            _pawn._id = 11;
            _pawn.Rule();

            input = System.Console.ReadLine();
        }
    }
}
