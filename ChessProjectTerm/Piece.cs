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
        public Piece(int _x, int _y, char _color)
        {
            this.x = _x;
            this.y = _y;
            this.color = _color;
        }
        abstract public IEnumerable<Move> PlayableMoves(Square[,] board);

        public IEnumerable<Move> BaseRule(int _i, int _j, Square[,] board, bool loop, string msg)
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
                            //TODO: Capture Enemy
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

        public bool IsKingChecked(int king_x, int king_y, Square[,] _board)
        {

            for (double k = 0.0; k < 360; k += 90.0)
            {
                //Queen or Rook [(1,0),(0,1),(-1,0),(0,-1)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Sin(k)),
                    Convert.ToInt32(Math.Cos(k)), _board, typeof(Queen), typeof(Rook), true))
                {
                    return true;
                }
                //Queen or Bishop [(1,-1),(-1,1),(-1,1),(1,-1)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Tan(k + 45)),
                    Convert.ToInt32(Math.Tan(360 - 45 - k)), _board, typeof(Queen), typeof(Bishop), true))
                {
                    return true;
                }

                //Knight [(2,-1),(-2,1),(-2,1),(2,-1)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Tan(k + 45)) * 2,
                    Convert.ToInt32(Math.Tan(360 - 45 - k)), _board, typeof(Knight), _loop: false))
                {
                    return true;
                }

                //Knight [(1,-2),(-1,2),(-1,2),(1,-2)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Tan(k + 45)),
                    Convert.ToInt32(Math.Tan(360 - 45 - k)) * 2, _board, typeof(Knight), _loop: false))
                {
                    return true;
                }

                //Pawn [(-1,1),(-1,-1) if opponet is black else (1,1),(1,-1)]
                if (IsThisPiece(king_x, king_y, k < 91.0 ? -1 : -2,
                    Convert.ToInt32(Math.Tan(360 - k + 45)), _board, typeof(Pawn), _loop: false))
                {
                    return true;
                }
                //King-Direct
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Sin(k)),
                    Convert.ToInt32(Math.Cos(k)), _board, typeof(King), _loop: false))
                {
                    return true;
                }
                //King-Diagonal
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Tan(k + 45)),
                    Convert.ToInt32(Math.Tan(360 - 45 - k)), _board, typeof(King), _loop: false))
                {
                    return true;
                }

            }
            return false;
        }

        //Queen-Rook and Queen-Bishop checks same squares, so there is two types.
        private bool IsThisPiece(int king_x, int king_y, int _i, int _j, Square[,] _board, Type _type1, Type _type2 = null, bool _loop = false)
        {

            char oppColor = 'B';
            if (color == 'B')
            {
                oppColor = 'W';
                if (_type1.Equals(typeof(Pawn)))
                {
                    _i = _i * -1;
                }
            }
            int inc_i = _i;
            int inc_j = _j;
            while (isValid(king_x + _i, king_y + _j))
            {
                if (_board[king_x + _i, king_y + _j].isOccupied)
                {
                    if (_board[king_x + _i, king_y + _j].occupiedBy.color != color &&
                        (_board[king_x + _i, king_y + _j].occupiedBy.GetType().Equals(_type1)
                        || _board[king_x + _i, king_y + _j].occupiedBy.GetType().Equals(_type2)))
                    {
                        //TODO: refactor..
                        if (_type1.Equals(typeof(Pawn)) &&
                            Math.Abs(_j) == 2 &&
                            (king_x + _i == 1 || king_x - _i == 6))
                        {
                            return false;
                        }
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

        bool initialPos;

        int forwardIndex;

        public Pawn(int _x, int _y, char _color)
            : base(_x, _y, _color)
        {
            this.initialPos = true;
            if (this.color == 'W') { this.forwardIndex = -1; }
            else { this.forwardIndex = 1; }
        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            //Base - 1 forward - no loop

            //Base - 2 forward - no loop - initial position

            //Diagonal capture

            //En passant

            //Promotion

            foreach (var move in BaseRule(forwardIndex, board, "piyon- 1 ileri"))
            {
                yield return move;
            }
            foreach (var move in BaseRule(forwardIndex * 2, board, "piyon- 2 ileri"))
            {
                yield return move;
            }

            foreach (var move in PawnCapture(forwardIndex, board, "piyon_deneme"))
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

        public IEnumerable<Move> BaseRule(int _i, Square[,] board, string msg)
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
                    if (!board[x + (_i / 2), y].isOccupied)
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

        public IEnumerable<Move> PawnCapture(int _i, Square[,] board, string msg)
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

    }

    public class Knight : Piece
    {

        public Knight(int _x, int _y, char _color)
            : base(_x, _y, _color)
        {

        }

        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            foreach (var move in BaseRule(-2, +1, board, false, "sag-yukari"))
            {
                yield return move;
            }
            foreach (var move in BaseRule(-1, +2, board, false, "sag-yukari"))
            {
                yield return move;
            }
            foreach (var move in BaseRule(+1, +2, board, false, "sag-yukari"))
            {
                yield return move;
            }
            foreach (var move in BaseRule(+2, +1, board, false, "sag-yukari"))
            {
                yield return move;
            }
            foreach (var move in BaseRule(+2, -1, board, false, "sag-yukari"))
            {
                yield return move;
            }
            foreach (var move in BaseRule(+1, -2, board, false, "sag-yukari"))
            {
                yield return move;
            }
            foreach (var move in BaseRule(-1, -2, board, false, "sag-yukari"))
            {
                yield return move;
            }
            foreach (var move in BaseRule(-2, -1, board, false, "sag-yukari"))
            {
                yield return move;
            }
        }

    }

    public class Bishop : Piece
    {

        public Bishop(int _x, int _y, char _color)
            : base(_x, _y, _color)
        {

        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {

            foreach (var move in BaseRule(+1, -1, board, true, "sol-asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, +1, board, true, "sag asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, -1, board, true, "sol yukari"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, +1, board, true, "sag yukari"))
            {
                yield return move;
            }

        }


    }

    public class Rook : Piece
    {
        public Rook(int _x, int _y, char _color)
            : base(_x, _y, _color)
        {

        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            foreach (var move in BaseRule(+1, 0, board, true, "asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, -1, board, true, "sola"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, 0, board, true, "yukarı"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, +1, board, true, "saga"))
            {
                yield return move;
            }


        }

    }

    public class Queen : Piece
    {
        public Queen(int _x, int _y, char _color)
            : base(_x, _y, _color)
        {

        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            foreach (var move in BaseRule(+1, 0, board, true, "asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, -1, board, true, "sola"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, 0, board, true, "yukarı"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, +1, board, true, "saga"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, -1, board, true, "sol-asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, +1, board, true, "sag asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, -1, board, true, "sol yukari"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, +1, board, true, "sag yukari"))
            {
                yield return move;
            }
        }

    }

    public class King : Piece
    {

        public King(int _x, int _y, char _color)
            : base(_x, _y, _color)
        {

        }
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            foreach (var move in BaseRule(+1, 0, board, false, "asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, -1, board, false, "sola"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, 0, board, false, "yukarı"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(0, +1, board, false, "saga"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, -1, board, false, "sol-asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(+1, +1, board, false, "sag asagi"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, -1, board, false, "sol yukari"))
            {
                yield return move;
            }

            foreach (var move in BaseRule(-1, +1, board, false, "sag yukari"))
            {
                yield return move;
            }
        }

        public IEnumerable<Move> Castling()
        {
            return null;
        }

    }
}
