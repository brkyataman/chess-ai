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
        //private List<string> playableMoves;

        public Board()
        {
            this.board = new Square[8,8];           
            InitiliazeBoard(); //TODO: complete initiliazation

            List<Move> x = new List<Move>();
            foreach (var move in board[2, 2].occupiedBy.PlayableMoves(this.board)) { 
                x.Add(move);
            }
            this.turn = 0;
        }
        public void InitiliazeBoard()
        {
            //for(int i = 0 ; i < 7; i++){
            //    this.board[0,0].occupiedBy = new Bishop();
            //}
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Square();
                }
            }
            var a = new Bishop();
            a.x = 2; a.y = 2;
            board[2, 2].isOccupied = true;
            board[2, 2].occupiedBy = a;

            //var a = new Bishop();
            //a.x = 2; a.y = 2;
            //this.board[2, 2] = new Square(true, a);
            
        }
        //Generates a move with given params, eg. from e2 to e3
        public bool GenerateMove(string from, string to){
            
            return true;
        }

        //Turns playable moves of current board
        public List<Move> GetPlayableMoves()
        {
            List<Move> playableMoves = new List<Move>();
            
            return playableMoves;
        }

        //Evaluates 
        public int Evaluate()
        {
            return 0;
        }
    }
}
