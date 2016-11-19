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
    public class State
    {
        private Square[,] board;
        private char color;
        private int turn;
        private int king_black_x;
        private int king_black_y;
        private int king_white_x;
        private int king_white_y;
        private int numberOfPieces_white;
        private int numberOfPieces_black;
        public double toRadian;
        //private List<string> playableMoves;

        public Square[,] getBoard()
        {
            return this.board;
        }

        public State()
        {
            this.board = new Square[8, 8];
            toRadian = (Math.PI / 180);

            //List<Move> x = new List<Move>();
            //foreach (var move in board[2, 2].occupiedBy.PlayableMoves(this.board))
            //{
            //    x.Add(move);
            //    System.Console.WriteLine(move.from_x + "-" + move.from_y + "\n");
            //}

            //var ajk = board[3, 3].occupiedBy;
            this.turn = 0;
            //TODO: color parametre olarak gelmeli!!! düzelt!!!
            this.color = 'W';
        }
        public void InitiliazeBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Square();
                }
            }
            for (int j = 0; j < 8; j++)
            {
                this.board[1, j].occupiedBy = new Pawn(1, j, 'W');
                this.board[1, j].isOccupied = true;
                this.board[6, j].occupiedBy = new Pawn(6, j, 'B');
                this.board[6, j].isOccupied = true;
            }
            char colorOfPieces = 'W';
            for (int i = 0; i < 2; i++)
            {
                this.board[0 + 7 * i, 0].occupiedBy = new Rook(0 + 7 * i, 0, colorOfPieces);
                this.board[0 + 7 * i, 0].isOccupied = true;
                this.board[0 + 7 * i, 1].occupiedBy = new Knight(0 + 7 * i, 1, colorOfPieces);
                this.board[0 + 7 * i, 1].isOccupied = true;
                this.board[0 + 7 * i, 2].occupiedBy = new Bishop(0 + 7 * i, 2, colorOfPieces);
                this.board[0 + 7 * i, 2].isOccupied = true;
                this.board[0 + 7 * i, 3].occupiedBy = new Queen(0 + 7 * i, 3, colorOfPieces);
                this.board[0 + 7 * i, 3].isOccupied = true;
                this.board[0 + 7 * i, 4].occupiedBy = new King(0 + 7 * i, 4, colorOfPieces);
                this.board[0 + 7 * i, 4].isOccupied = true;
                this.board[0 + 7 * i, 5].occupiedBy = new Bishop(0 + 7 * i, 5, colorOfPieces);
                this.board[0 + 7 * i, 5].isOccupied = true;
                this.board[0 + 7 * i, 6].occupiedBy = new Knight(0 + 7 * i, 6, colorOfPieces);
                this.board[0 + 7 * i, 6].isOccupied = true;
                this.board[0 + 7 * i, 7].occupiedBy = new Rook(0 + 7 * i, 7, colorOfPieces);
                this.board[0 + 7 * i, 7].isOccupied = true;

                colorOfPieces = 'B';
            }

            this.turn = 0;
            numberOfPieces_black = 16;
            numberOfPieces_white = 16;
            king_black_x = 7;
            king_black_y = 4;
            king_white_x = 0;
            king_white_y = 4;



        }
        //Generates a move with given params, eg. from e2 to e3
        public bool GenerateMove(int from_x, int from_y, int to_x, int to_y)
        {

            board[to_x, to_y].occupiedBy = board[from_x, from_y].occupiedBy;
            board[to_x, to_y].occupiedBy.x = to_x;
            board[to_x, to_y].occupiedBy.y = to_y;
            board[to_x, to_y].occupiedBy.initialPos = false;
            board[from_x, from_y].occupiedBy = null;
            board[from_x, from_y].isOccupied = false;
            board[to_x, to_y].isOccupied = true;
            return true;
        }

        //Turns playable moves of current board
        public List<Move> GetPlayableMoves()
        {

            int k = 1;
            int numbOfPieces = numberOfPieces_black;
            if (this.color == 'W')
            {
                k = 0;
                numbOfPieces = numberOfPieces_white;
            }
            

            //If color is White then it will start searching for 'x' from 0 to 7
            //If Black then start searching for 'x' from 7 to 0 
            int i = 0;
            i = i + 7 * k;
            List<Move> playableMoves = new List<Move>();
            while (true) 
            {
                for (int j = 0; j < 8; j++)
                {
                    if (numbOfPieces == 0)
                    {
                        return playableMoves;
                    }
                    if (board[i, j].isOccupied && board[i, j].occupiedBy.color == this.color)
                    {
                        foreach (var move in board[i, j].occupiedBy.PlayableMoves(board))
                        {
                            playableMoves.Add(move);
                        }
                        numbOfPieces--;
                    }
                }
                i = i + (1 - 2 * k);
                if (i == -1 || i == 8) { break; }
            }
            return playableMoves;
        }

        //Evaluates 
        public int Evaluate()
        {
            return 0;
        }

        public bool IsKingChecked(int king_x, int king_y)
        {

            for (double k = 0.0; k < 360; k += 90.0)
            {
                //Queen or Rook [(1,0),(0,1),(-1,0),(0,-1)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Sin(k * toRadian)),
                    Convert.ToInt32(Math.Cos(k * toRadian)), typeof(Queen), typeof(Rook), true))
                {
                    return true;
                }
                //Queen or Bishop [(1,-1),(-1,1),(-1,1),(1,-1)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Tan((k + 45) * toRadian)),
                    Convert.ToInt32(Math.Tan((360 - 45 - k) * toRadian)), typeof(Queen), typeof(Bishop), true))
                {
                    return true;
                }

                //Knight [(2,-1),(-2,1),(-2,1),(2,-1)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Tan((k + 45) * toRadian)) * 2,
                    Convert.ToInt32(Math.Tan((360 - 45 - k) * toRadian)), typeof(Knight), _loop: false))
                {
                    return true;
                }

                //Knight [(1,-2),(-1,2),(-1,2),(1,-2)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Tan((k + 45) * toRadian)),
                    Convert.ToInt32(Math.Tan((360 - 45 - k) * toRadian)) * 2, typeof(Knight), _loop: false))
                {
                    return true;
                }

                //Pawn [(-1,1),(-1,-1) if opponent is white else (1,1),(1,-1)]
                if (IsThisPiece(king_x, king_y, k < 91.0 ? -1 : -2,
                    Convert.ToInt32(Math.Tan((360 - k + 45) * toRadian)), typeof(Pawn), _loop: false))
                {
                    return true;
                }
                //King-Direct
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Sin(k * toRadian)),
                    Convert.ToInt32(Math.Cos(k * toRadian)), typeof(King), _loop: false))
                {
                    return true;
                }
                //King-Diagonal
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Tan((k + 45) * toRadian)),
                    Convert.ToInt32(Math.Tan((360 - 45 - k) * toRadian)), typeof(King), _loop: false))
                {
                    return true;
                }

            }
            return false;
        }

        //Queen-Rook and Queen-Bishop checks same squares, so there is two types.
        private bool IsThisPiece(int king_x, int king_y, int _i, int _j, Type _type1, Type _type2 = null, bool _loop = false)
        {

            if (color == 'W')
            {
                if (_type1.Equals(typeof(Pawn)))
                {
                    _i = _i * -1;
                }
            }
            int inc_i = _i;
            int inc_j = _j;
            while (isValid(king_x + _i, king_y + _j))
            {
                if (board[king_x + _i, king_y + _j].isOccupied)
                {
                    if (board[king_x + _i, king_y + _j].occupiedBy.color != color &&
                        (board[king_x + _i, king_y + _j].occupiedBy.GetType().Equals(_type1)
                        || board[king_x + _i, king_y + _j].occupiedBy.GetType().Equals(_type2)))
                    {
                        //TODO: refactor..
                        if (_type1.Equals(typeof(Pawn)) &&
                            Math.Abs(_i) == 2 &&
                            !((Pawn)board[king_x + _i, king_y + _j].occupiedBy).initialPos)
                        {
                            return false;
                        }
                        System.Console.WriteLine("King is check by a " + (board[king_x + _i, king_y + _j].occupiedBy.GetType().ToString()));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (!_loop) { break; }
                _i += inc_i;
                _j += inc_j;
            }
            return false;
        }

        private bool isValid(int _x, int _y)
        {
            if (_x > 7 || _x < 0 || _y > 7 || _y < 0)
            {
                return false;
            }
            return true;
        }
    }
}
