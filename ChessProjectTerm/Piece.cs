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
        public int id { get; set; }
        public char color { get; set; }
        abstract public IEnumerable<Move> PlayableMoves(Square[,] board);
        public bool isValid(int _x, int _y)
        {
            if (_x > 7 || _x < 0 || _y > 7 || _y < 0)
            {
                return false;
            }
            return true;
        }

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
                            yield return new Move(msg, msg);
                        }
                        //Occupied by ally piece so do nothing
                        break;
                    }
                    else
                    {
                        //Else put to playableMoves list that new position.
                        //TODO: Move to free square
                        yield return new Move(msg, msg);

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
    }

    public class Pawn : Piece
    {
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            //System.Console.WriteLine("I am a PAWN with ID: " + this.id + "\n");
            //System.Console.WriteLine("My 'X': " + this.x + "\n");
            //System.Console.WriteLine("My 'Y': " + this.y + "\n");

            int forward = 8;

            if (this.color == 'W') { forward = -1; }
            else { forward = 1; }

            foreach (var move in Rule(forward, board, "piyon_deneme"))
            {
                yield return move;
            }

        }

        public IEnumerable<Move> Rule(int _i, Square[,] board, string msg)
        {
            //One or Two forward or En Passant
            if (isValid(x + _i, y))
            {
                if (!board[x + _i, y].isOccupied)
                {
                    //Can jump 2 squares to forward if at the beginning position and 2 square forward is free
                    if (x == (3.5 - _i * 1.5) && !board[x + (_i * 2), y].isOccupied)
                    {

                        yield return new Move(msg, msg);

                        if (true)
                        {
                            //TODO: en passant
                        }
                    }

                    //Can move one forward
                    //TODO: move to one square forward
                    yield return new Move(msg, msg);
                }
            }
            //Capture right forward enemy
            if (isValid(x + _i, y + 1))
            {
                if (board[x + _i, y + 1].isOccupied)
                {
                    if (board[x + _i, y + 1].occupiedBy.color != this.color)
                    {
                        //TODO: Capture right forward enemy
                        yield return new Move(msg, msg);
                    }
                }

            }

            //Captue left forward enemy
            if (isValid(x + _i, y - 1))
            {
                if (board[x + _i, y - 1].isOccupied)
                {
                    if (board[x + _i, y - 1].occupiedBy.color != this.color)
                    {
                        //TODO: Capture left forward enemy
                        yield return new Move(msg, msg);
                    }
                }
            }
        }
    }

    public class Knight : Piece
    {
        public override IEnumerable<Move> PlayableMoves(Square[,] board)
        {
            System.Console.WriteLine("I am a KNIGHT with ID: " + this.id + "\n");
            System.Console.WriteLine("My 'X': " + this.x + "\n");
            System.Console.WriteLine("My 'Y': " + this.y + "\n");

            List<Move> playableMoves = new List<Move>();

            return playableMoves;
        }
    }

    public class Bishop : Piece
    {
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

       
    }
}
