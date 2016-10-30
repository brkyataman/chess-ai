using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Rules
    {
        public IEnumerable<Move> BishopRule(int _i, int _j, Square[,] board)
        {
            //while (true)
            //{
            //    if (isValid(x + _i, y + _j))
            //    {
            //        if (board[x + _i, y + _j].isOccupied)
            //        {
            //            //Occupied by enemy piece
            //            if (board[x + _i, y + _j].occupiedBy.color != this.color)
            //            {
            //                //TODO: Capture Enemy
            //            }
            //            //Occupied by ally piece so do nothing
            //            break;
            //        }
            //        //Else put to playableMoves list that new position.
            //        yield return new Move("x:" + _i, "y:" + _j);
            //        _i += _i;
            //        _j += _j;
            //    }
            //    else
            //    { //Out of bounds
            //        break;
            //    }
            //}
            return null;
        }
    }
}
