using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Node
    {
        public long id { get; set; }
        public long parent_id { get; set; }
        public int move_id { get; set; }
        public List<long> child_ids { get; set; }
        public State state { get; set; }

        public Node(State _state, long _id, long _parent_id, int _move_id)
        {
            this.state = _state;
            this.id = _id;
            this.parent_id = _parent_id;
            this.move_id = _move_id;
            this.child_ids = new List<long>();
        }

        public void AddChild(long _child_id)
        {
            this.child_ids.Add(_child_id);
        }
    }
}
