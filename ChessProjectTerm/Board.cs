using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    /*
     Holds state of game with board and turn data.
     */
    public class Board
    {
        private Square[,] board;
        private int turn;

        public Board()
        {
            this.board = new Square[8,8];
            this.turn = 0;
        }

        //Generates a move with given params, eg. from e2 to e3
        public bool GenerateMove(string from, string to){
            
            return true;
        }

        //Evaluates 
        public int Evaluate()
        {
            return 0;
        }
    }
}
