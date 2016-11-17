using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Move
    {
        public int from_x { get; set; }
        public int to_x { get; set; }

        public int from_y { get; set; }
        public int to_y { get; set; }

        public char? msg { get; set; }
        public Move(int _from_x, int _from_y, int _to_x, int _to_y,char? _msg = null)
        {
            this.from_x = _from_x;
            this.from_y = _from_y;
            this.to_x = _to_x;
            this.to_y = _to_y;
            this.msg = _msg;
        }
    }
}
