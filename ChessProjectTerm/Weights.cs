using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessProjectTerm
{
    
    public static class Weights
    {
        static public double PAWN_VAL = 100;
        static public double KNIGHT_VAL = 320;
        static public double BISHOP_VAL = 330;
        static public double ROOK_VAL = 500;
        static public double QUEEN_VAL = 900;

        static public double BISHOP_PAIR = 28;


        static public int[,] PAWN_TABLE;
        static public int[,] ROOK_TABLE;
        static public int[,] KNIGHT_TABLE;
        static public int[,] BISHOP_TABLE;
        static public int[,] QUEEN_TABLE;
        static public int[,] KING_TABLE;

        static Weights()
        {
            //From perspective of white. For black iterate from 7 to 0.
            PAWN_TABLE = new int[8, 8]
            {
                { 0,  0,  0,  0,  0,  0,  0,  0},
                { 5, 10, 10,-20,-20, 10, 10,  5},
                { 5, -5,-10,  0,  0,-10, -5,  5},
                { 0,  0,  0, 20, 20,  0,  0,  0},
                { 5,  5, 10, 25, 25, 10,  5,  5},
                {10, 10, 20, 30, 30, 20, 10, 10},
                {50, 50, 50, 50, 50, 50, 50, 50},
                { 100,  100,  100,  100,  100,  100,  100,  100},
            };
            KNIGHT_TABLE = new int[8, 8]
            {
                {-50,-40,-30,-30,-30,-30,-40,-50},
                { -40,-20,  0,  5,  5,  0,-20,-40},
                {-30,  5, 10, 15, 15, 10,  5,-30},
                {-30,  0, 15, 20, 20, 15,  0,-30},
                {-30,  5, 15, 20, 20, 15,  5,-30},
                {-30,  0, 10, 15, 15, 10,  0,-30},
                { -40,-20,  0,  0,  0,  0,-20,-40},
                { -50,-40,-30,-30,-30,-30,-40,-50},
            };
            BISHOP_TABLE = new int[8, 8]
            {
                {-20,-10,-10,-10,-10,-10,-10,-20},
                {-10,  5,  0,  0,  0,  0,  5,-10},
                {-10, 10, 10, 10, 10, 10, 10,-10},
                {-10,  0, 10, 10, 10, 10,  0,-10},
                {-10,  5,  5, 10, 10,  5,  5,-10},
                {-10,  0,  5, 10, 10,  5,  0,-10},
                {-10,  0,  0,  0,  0,  0,  0,-10},
                {-20,-10,-10,-10,-10,-10,-10,-20},
            }; 
            ROOK_TABLE = new int[8, 8]
            {
                { 0,  0,  0,  5,  5,  0,  0,  0},
                { -5,  0,  0,  0,  0,  0,  0, -5},
                { -5,  0,  0,  0,  0,  0,  0, -5},
                { -5,  0,  0,  0,  0,  0,  0, -5},
                { -5,  0,  0,  0,  0,  0,  0, -5},
                { -5,  0,  0,  0,  0,  0,  0, -5},
                {  5, 10, 10, 10, 10, 10, 10,  5},
                { 0,  0,  0,  0,  0,  0,  0,  0,},
            };
            QUEEN_TABLE = new int[8, 8]
            {
                {-20,-10,-10, -5, -5,-10,-10,-20},
                {-10,  0,  5,  0,  0,  0,  0,-10},
                {-10,  5,  5,  5,  5,  5,  0,-10},
                {  0,  0,  5,  5,  5,  5,  0, -5},
                { -5,  0,  5,  5,  5,  5,  0, -5},
                { -10,  0,  5,  5,  5,  5,  0,-10},
                {-10,  0,  0,  0,  0,  0,  0,-10},
                {-20,-10,-10, -5, -5,-10,-10,-20},
            };
            KING_TABLE = new int[8, 8]
            {
                {-20,-10,-10, -5, -5,-10,-10,-20},
                {-10,  0,  5,  0,  0,  0,  0,-10},
                {-10,  5,  5,  5,  5,  5,  0,-10},
                {  0,  0,  5,  5,  5,  5,  0, -5},
                { -5,  0,  5,  5,  5,  5,  0, -5},
                { -10,  0,  5,  5,  5,  5,  0,-10},
                {-10,  0,  0,  0,  0,  0,  0,-10},
                {-20,-10,-10, -5, -5,-10,-10,-20},
            }; 
        }
    }
}
