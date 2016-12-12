using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Node
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public int move_id { get; set; }
        public List<int> child_ids { get; set; }
        public State state { get; set; }

        public Node(State _state, int _id, int _parent_id, int _move_id)
        {
            this.state = _state;
            this.id = _id;
            this.parent_id = _parent_id;
            this.move_id = _move_id;
            this.child_ids = new List<int>();
        }

        public void AddChild(int _child_id)
        {
            this.child_ids.Add(_child_id);
        }
    }
}
