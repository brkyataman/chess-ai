using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Square
    {

        public bool isOccupied{ get ; set; }

        public Piece occupiedBy { get; set; }

        public Square()
        {
            this.isOccupied = false;
            this.occupiedBy = null;
        }
        public Square(bool _isOccupied, Piece _occupiedBy)
        {
            this.isOccupied = _isOccupied;
            this.occupiedBy = _occupiedBy;
        }
       
    }
}
