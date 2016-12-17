using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    public class Information
    {
        public Dictionary<string, int> info { get; set; }
        
        public Information()
        {
            info = new Dictionary<string, int>();
            info.Add("b_pawn",8);
            info.Add("w_pawn", 8);
            info.Add("b_rook", 2);
            info.Add("w_rook", 2);
            info.Add("b_knight", 2);
            info.Add("w_knight", 2);
            info.Add("b_bishop", 2);
            info.Add("w_bishop", 2);
            info.Add("b_queen", 1);
            info.Add("w_queen", 1);
            info.Add("b_king", 1);
            info.Add("w_king", 1);
            info.Add("b_king_x", 7);
            info.Add("w_king_x", 0);
            info.Add("b_king_y", 4);
            info.Add("w_king_y", 4);
            info.Add("w_piece_num", 16);
            info.Add("b_piece_num", 16);
        }

        public Information GetCopy()
        {
            Information new_information = new Information();
            foreach (var item in this.info)
            {
                new_information.info[item.Key] = item.Value;
            }
            return new_information;
        }
        public int GatherInformation(Square[,] board){
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string key = "";
                    if (board[i, j].isOccupied) { 
                        //Piece number
                        key += board[i, j].occupiedBy.color.ToString() + "_";
                        key += board[i, j].occupiedBy.GetType().Name.ToLower();
                        info[key] += 1;

                        key = "";
                        //King pos
                        if (board[i, j].occupiedBy.GetType().Equals(typeof(King)))
                        {
                            key += board[i, j].occupiedBy.color.ToString() + "_king_";
                            info[key + "x"] = i;
                            info[key + "y"] = j;

                        }
                    }

                }
            }
            return 1;
        }

    }
}
