using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Move
    {
        public string from { get; set; }
        public string to { get; set; }

        public Move(string _from, string _to)
        {
            this.from = _from;
            this.to = _to;
        }
    }
}
