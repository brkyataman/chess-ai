using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Tree
    {
        private Dictionary<int, Node> tree;
        private Node root;
        private int id_generator;
        private int ply;
        public Tree(int _ply)
        {
            tree = new Dictionary<int, Node>();
            this.id_generator = 1;
            this.ply = _ply;
        }
        public Dictionary<int, Node> BuildTree(State _source)
        {
            //Initiliaze root node.
            int p_id = 0;
            tree.Add(id_generator, new Node(_source, this.id_generator, p_id));
            //List<int> parent_q = new List<int>();
            List<int> children_q = new List<int>();
            children_q.Add(this.id_generator++);

            for (int k = 0; k < ply; k++)
            {
                List<int> parent_q = new List<int>(children_q);
                children_q.Clear();
                while(parent_q.Count > 0)
                {
                    p_id = parent_q[0];
                    parent_q.RemoveAt(0);

                    Node p = tree[p_id];
                    //Get parents playable moves and generate all.
                    List<Move> playableMoves = p.state.GetPlayableMoves();

                    for (int i = 0; i < playableMoves.Count; i++)
                    {
                        State new_state = p.state.GetCopy();
                        new_state.GenerateMove(playableMoves[i]);
                        new_state.setColor(new_state.color == 'W' ? 'B' : 'W');
                        new_state.turn++;
                        Node c = new Node(new_state, this.id_generator, p_id);
                        tree[p_id].AddChild(this.id_generator);
                        tree.Add(this.id_generator, c);
                        children_q.Add(this.id_generator++);
                    }
                }
            }
            //Ardışık gelmedi sonuçlar. Statelerde colorları check et.
            
            Test t = new Test();
            t.printBoard(tree[2].state.getBoard());
            t.printBoard(tree[21].state.getBoard());
            return tree;
        }
       
    }

    public class Node
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        List<int> child_ids;
        public State state { get; set; }

        public Node(State _state, int _id, int _parent_id)
        {
            this.state = _state;
            this.id = _id; 
            this.parent_id = _parent_id;
            child_ids = new List<int>();
        }

        public void AddChild(int _child_id)
        {
            this.child_ids.Add(_child_id);
        }
    }
}
