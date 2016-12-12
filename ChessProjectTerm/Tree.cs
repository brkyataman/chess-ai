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
        private int root_id;
        private int id_generator;
        private int ply;
        private int bestMove_id;
        public Tree(int _ply)
        {
            tree = new Dictionary<int, Node>();
            this.id_generator = 1;
            this.ply = _ply;
        }

        public int MinimaxAlphaBeta(int _id, int _alpha, int _beta, int _current_ply)
        {
            //check if ply limit
            if (_current_ply == this.ply)
            {
                return tree[_id].state.Evaluate();
            }

            var children = tree[_id].child_ids;

            if (children.Count == 0)
            {
                return tree[_id].state.Evaluate();
            }

            //initiliazing best move
            if (_id == root_id)
            {
                bestMove_id = children[0];
                if (children.Count == 1)
                    return -1;
            }

            //MAX's play
            if (_current_ply % 2 == 0) 
            {

                for (int i = 0; i < children.Count; i++)
                {
                    int result = MinimaxAlphaBeta(children[i], _alpha, _beta, _current_ply + 1);
                    if (result > _alpha)
                    {
                        _alpha = result;
                        if (_id == root_id)
                        {
                            bestMove_id = children[i];
                        }
                    }
                    if (_alpha >= _beta)
                        return _alpha;
                }
                return _alpha;
            }
            //MIN's play
            else 
            {
                for (int i = 0; i < children.Count; i++)
                {
                    int result = MinimaxAlphaBeta(children[i], _alpha, _beta, _current_ply + 1);
                    if (result < _beta)
                    {
                        if (_id == root_id)
                        {
                            bestMove_id = children[i];
                        }
                    }
                    if (_beta <= _alpha)
                        return _beta;
                }
                return _beta;
            }
        }


        public Dictionary<int, Node> BuildTree(State _source)
        {
            //Initiliaze root node.
            int p_id = 0;
            int move_id = 0;
            tree.Add(id_generator, new Node(_source, this.id_generator, p_id, move_id));
            root = tree[id_generator];
            List<int> children_q = new List<int>();
            List<int> parent_q = new List<int>();
            children_q.Add(this.id_generator++);

            for (int k = 0; k < ply; k++)
            {
                parent_q.AddRange(children_q);
                children_q.Clear();
                children_q = ExpandTree(parent_q);
                parent_q.Clear();
            }
            //Ardışık gelmedi sonuçlar. Statelerde colorları check et.

            Test t = new Test();
            t.printBoard(tree[2].state.getBoard());
            if (tree[22].parent_id == 2)
            {
                t.printBoard(tree[22].state.getBoard());
            }

            return tree;
        }

        //Expands tree one ply.
        private List<int> ExpandTree(List<int> parent_q)
        {
            int p_id;
            List<int> children_q = new List<int>();
            while (parent_q.Count > 0)
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
                    new_state.color = new_state.color == 'W' ? 'B' : 'W';
                    new_state.turn++;
                    Node c = new Node(new_state, this.id_generator, p_id, i);
                    tree[p_id].AddChild(this.id_generator);
                    tree.Add(this.id_generator, c);
                    children_q.Add(this.id_generator++);
                }
            }
            return children_q;
        }

        private List<int> FindUnusedNodes(int _move_id)
        {
            List<int> unused_nodes = new List<int>();
            List<int> temp = null;
            unused_nodes.Add(root.id);

            for (int i = 0; i < root.child_ids.Count; i++)
            {
                if (tree[root.child_ids[i]].move_id != _move_id)
                {
                    unused_nodes.Add(root.child_ids[i]);
                    temp = FindDescendants(root.child_ids[i], 1);
                    if (temp != null)
                        unused_nodes.AddRange(temp);
                    temp = null;
                }
            }
            return unused_nodes;
        }

        //Finds every child of a node.
        private List<int> FindDescendants(int _id, int current_ply)
        {
            //TODO: Kaldırılabilir. Belki plyin son elemanlarınında çocuğu olabilir.(ama nasıl?)
            if (current_ply == this.ply)
                return null;

            List<int> children_list = null;
            int number_of_children = 0;
            if (tree[_id].child_ids.Count > 0) { 
                children_list = new List<int>(tree[_id].child_ids);
                number_of_children = children_list.Count;
            }

            List<int> temp_list = null;            
            int c_id;

            for (int i = 0; i < number_of_children; i++)
            {
                c_id = tree[_id].child_ids[i];
                temp_list = FindDescendants(c_id, current_ply + 1);
                if (temp_list != null)
                    children_list.AddRange(temp_list);
                
            }
            return children_list;
        }
        public Dictionary<int, Node> ExpandWithNewRoot(int _move_id)
        {
            //Get unused nodes
            List<int> unused_nodes = FindUnusedNodes(_move_id);

            //Change root to new node..
            for (int i = 0; i < this.root.child_ids.Count; i++)
            {
                if (tree[this.root.child_ids[i]].move_id == _move_id)
                {
                    this.root = tree[this.root.child_ids[i]];
                    break;
                }
            }

            //Delete un-used nodes..
            for (int i = 0; i < unused_nodes.Count; i++)
            {
                tree.Remove(unused_nodes[i]);
            }


            //Get leaf child ids for expanding
            List<int> children_list = new List<int>();
            List<int> parent_list = new List<int>();
            children_list.Add(root.id);
            int p_id;
            for (int i = 0; i < ply - 1; i++)
            {
                parent_list.AddRange(children_list);
                children_list.Clear();
                while (parent_list.Count > 0)
                {
                    p_id = parent_list[0];
                    parent_list.RemoveAt(0);
                    if (tree[p_id].child_ids.Count > 0)
                        children_list.AddRange(tree[p_id].child_ids);
                }
                parent_list.Clear();
            }

            //Expand one ply
            ExpandTree(children_list);
            
            return tree;
        }
    }

    
}
