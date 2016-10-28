using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    abstract public class Piece
    {
    
        public int _x {get; set;}
        public int _y {get; set;}
        public int _id {get; set;}
        public char _color {get; set;}
        abstract public void Rule();
    }

    public class Pawn : Piece
    {
        public override void Rule()
        {
            System.Console.WriteLine("I am a pawn with ID: " + this._id + "\n");
            System.Console.WriteLine("My 'X': " + this._x + "\n");
            System.Console.WriteLine("My 'Y': " + this._y + "\n");
        }
    }

    public class Knight : Piece
    {
        public override void Rule()
        {
            throw new NotImplementedException();
        }
    }

    public class Bishop : Piece
    {
        public override void Rule()
        {

        }
    }

    public class Rook : Piece
    {
        public override void Rule()
        {

        }
    }

    public class Queen : Piece
    {
        public override void Rule()
        {

        }
    }

    public class King : Piece
    {
        public override void Rule()
        {

        }
    }
}
