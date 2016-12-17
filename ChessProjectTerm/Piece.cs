using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    abstract public class Piece
    {

        public int x { get; set; }
        public int y { get; set; }
        public char color { get; set; }
        public bool initialPos { get;set;}
        public Piece(int _x, int _y, char _color, bool _initialPos)
        {
            this.x = _x;
            this.y = _y;
            this.color = _color;
            this.initialPos = _initialPos;
        }

        public void UpdateData(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
            this.initialPos = false;
        }
        abstract public IEnumerable<Move> PlayableMoves(Square[,] board);

        public IEnumerable<Move> BaseRule(int _i, int _j, Square[,] board, bool loop )
        {
            int inc_i = _i;
            int inc_j = _j;
            while (true)
            {
                if (isValid(x + _i, y + _j))
                {
                    if (board[x + _i, y + _j].isOccupied)
                    {
                        //Occupied by enemy piece
                        if (board[x + _i, y + _j].occupiedBy.color != this.color)
                        {
                            if(!board[x+ _i , y + _j].occupiedBy.GetType().Equals(typeof(King)))
                                yield return new Move(x, y, x + _i, y + _j, 'C');
                        }
                        //Occupied by ally piece so do nothing
                        break;
                    }
                    else
                    {
                        //Else put to playableMoves list that new position.
                        //TODO: Move to free square
                        yield return new Move(x, y, x + _i, y + _j, 'M');

                        if (!loop)
                        {
                            break;
                        }

                        _i += inc_i;
                        _j += inc_j;
                    }
                }
                else
                { //Out of bounds
                    break;
                }
            }

        }

        //HEPSINE OZEL COPY PIECE EKLE sonra da state(prm,prm) constructorında yenileri al...
        abstract public Piece GetCopy( );
        public bool isValid(int _x, int _y)
        {
            if (_x > 7 || _x < 0 || _y > 7 || _y < 0)
            {
                return false;
            }
            return true;
        }
    }

    public class Pawn : Piece
    {

        int forwardIndex;

        public Pawn(int _x, int _y, char _color, bool _initialPos)
            : base(_x, _y, _color, _initialPos)
        {
            if (this.color == 'b') { this.forwardIndex = -1; }
            else { this.forwardIndex = 1; }
        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {           
            //TODO:
            //En passant
            //Promotion

            //Base - 1 forward - no loop
            foreach (var move in BaseRule(forwardIndex, board))
            {
                yield return move;
            }
            //Base - 2 forward - no loop - initial position
            foreach (var move in BaseRule(forwardIndex * 2, board))
            {
                yield return move;
            }
            //Diagonal capture
            foreach (var move in PawnCapture(forwardIndex, board))
            {
                yield return move;
            }
            //TODO:::::
            //foreach (var move in EnPassant(forwardIndex, board, "piyon_deneme"))
            //{
            //    //TODO:içini doldur enpassant
            //    yield return move;
            //}
            //foreach (var move in PawnPromotion(forwardIndex, board, "piyon_deneme"))
            //{
            //    //TODO: içini doldur pawnpromotion
            //    yield return move;
            //}

        }

        public IEnumerable<Move> BaseRule(int _i, Square[,] board)
        {
            if (isValid(x + _i, y))
            {

                if (Math.Abs(_i) == 1)
                {
                    if (!board[x + _i, y].isOccupied)
                    {
                        //TODO: Move
                        yield return new Move(x, y, x + _i, y, 'M');
                    }
                }
                else
                {
                    if (this.initialPos && !board[x + (_i / 2), y].isOccupied)
                    {
                        if (!board[x + _i, y].isOccupied)
                        {
                            //TODO: Move
                            yield return new Move(x, y, x + _i, y, 'M');
                        }
                    }
                }
            }
        }

        public IEnumerable<Move> PawnCapture(int _i, Square[,] board)
        {
            //Right diagonal
            if (isValid(x + _i, y + 1))
            {
                if (board[x + _i, y + 1].isOccupied)
                {
                    if (board[x + _i, y + 1].occupiedBy.color != this.color)
                    {
                        //TODO: CAPTURE
                        yield return new Move(x, y, x + _i, y + 1, 'C');
                    }
                }
            }

            //Left diagonal
            if (isValid(x + _i, y - 1))
            {
                if (board[x + _i, y - 1].isOccupied)
                {
                    if (board[x + _i, y - 1].occupiedBy.color != this.color)
                    {
                        //TODO: CAPTURE
                        yield return new Move(x, y, x + _i, y - 1, 'C');
                    }
                }
            }
        }

        public IEnumerable<Move> EnPassant(int _i, Square[,] board, string msg)
        {
            return null;
        }

        public IEnumerable<Move> PawnPromotion(int _i, Square[,] board, string msg)
        {
            return null;
        }

        public override Piece GetCopy()
        {
            return new Pawn(this.x, this.y, this.color,this.initialPos);
        }
    }

    public class Knight : Piece
    {

        public Knight(int _x, int _y, char _color, bool _initialPos )
            : base(_x, _y, _color, _initialPos)
        {

        }

        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            foreach (var move in BaseRule(-2, +1, board, false))
            {
                yield return move;
            }
            foreach (var move in BaseRule(-1, +2, board, false))
            {
                yield return move;
            }
            foreach (var move in BaseRule(+1, +2, board, false))
            {
                yield return move;
            }
            foreach (var move in BaseRule(+2, +1, board, false))
            {
                yield return move;
            }
            foreach (var move in BaseRule(+2, -1, board, false))
            {
                yield return move;
            }
            foreach (var move in BaseRule(+1, -2, board, false))
            {
                yield return move;
            }
            foreach (var move in BaseRule(-1, -2, board, false))
            {
                yield return move;
            }
            foreach (var move in BaseRule(-2, -1, board, false))
            {
                yield return move;
            }
        }

        public override Piece GetCopy()
        {
            return new Knight(this.x, this.y, this.color,this.initialPos);
        }
    }

    public class Bishop : Piece
    {

        public Bishop(int _x, int _y, char _color, bool _initialPos)
            : base(_x, _y, _color, _initialPos)
        {

        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {

            foreach (var move in BaseRule(+1, -1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, +1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, -1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, +1, board, true))
            {
                yield return move;
            }

        }

        public override Piece GetCopy()
        {
            return new Bishop(this.x, this.y, this.color,this.initialPos);
        }

    }

    public class Rook : Piece
    {
        public Rook(int _x, int _y, char _color, bool _initialPos)
            : base(_x, _y, _color, _initialPos)
        {

        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            foreach (var move in BaseRule(+1, 0, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, -1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, 0, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, +1, board, true))
            {
                yield return move;
            }


        }
        public override Piece GetCopy()
        {
            return new Rook(this.x, this.y, this.color, this.initialPos);
        }
    }

    public class Queen : Piece
    {
        public Queen(int _x, int _y, char _color, bool _initialPos)
            : base(_x, _y, _color, _initialPos)
        {

        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            foreach (var move in BaseRule(+1, 0, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, -1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, 0, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, +1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, -1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, +1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, -1, board, true))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, +1, board, true))
            {
                yield return move;
            }
        }

        public override Piece GetCopy()
        {
            return new Queen(this.x, this.y, this.color,this.initialPos);
        }
    }

    public class King : Piece
    {
        private bool noRightCastling;
        private bool noLeftCastling;
        public King(int _x, int _y, char _color, bool _initialPos)
            : base(_x, _y, _color, _initialPos)
        {
            noLeftCastling = false;
            noRightCastling = false;
        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            foreach (var move in BaseRule(+1, 0, board, false))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, -1, board, false))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, 0, board, false))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, +1, board, false))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, -1, board, false))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, +1, board, false))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, -1, board, false))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, +1, board, false))
            {
                yield return move;
            }

            if (!noRightCastling)
            {
                foreach (var move in Castling(+1, board))
                {
                    yield return move;
                }
            }

            if (!noLeftCastling)
            {
                foreach (var move in Castling(-1, board))
                {
                    yield return move;
                }
            }

        }

        public IEnumerable<Move> Castling(int _j, Square[,] board)
        {

            if (this.initialPos && board[x, (int)((double)y - 0.5 + 3.5 * _j)].occupiedBy != null && 
                board[x, (int)((double)y - 0.5 + 3.5 * _j)].occupiedBy.initialPos)
            {
                if (!board[this.x, this.y + _j].isOccupied) {
                    if (!board[this.x, this.y + _j * 2].isOccupied)
                    {
                        if (_j == 1)
                        {
                            //TODO: right castling!!!
                            yield return new Move(x, y, x, y + _j * 2, 'R');
                        }
                        else if (!board[this.x, this.y + _j *3].isOccupied)
                        {
                            //TODO: left castling
                            yield return new Move(x, y, x, y + _j * 2, 'R');
                        }
                    }
                }
            }
            else {
                if (_j == 1){ noRightCastling = true; }
                else { noLeftCastling = true; }
            }
            
        }

        public override Piece GetCopy()
        {
            return new King(this.x, this.y, this.color, this.initialPos);
        }

    }
}
