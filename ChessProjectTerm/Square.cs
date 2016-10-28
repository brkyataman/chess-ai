using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Square
    {

        private bool isOccupied{ get ; set; }
        private string XoccupiedBy { get; set; }
        public Piece occupiedBy;

        public void NewPiece()
        {
            Piece _pawn = new Pawn();
            occupiedBy = _pawn;
            occupiedBy = null;
        }
        
        
       
    }
}
