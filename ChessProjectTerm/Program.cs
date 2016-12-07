using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectTerm
{
    class Program
    {

        static void Main()
        {
            string input;
            //Board chessBoard;
            char color = 'W';
            char oppColor = 'B';


            System.Console.WriteLine("Welcome.\nChoose a color(w/b): ");
            input = System.Console.ReadLine();

            if (input[0] == 'W' || input[0] == 'w')
            {
                color = 'B';
                oppColor = 'W';
            }


            //input = System.Console.ReadLine();
            //System.Console.WriteLine("You wrote: " + input + "\n");

            State state = new State();
            state.InitiliazeBoard();
            state.setColor(color);


            Tree t = new Tree(3);
            var xxx = t.BuildTree(state);

            var kl = state.GetPlayableMoves();
            printMoves(kl);

            System.Console.WriteLine("which move?");
            int z = Convert.ToInt32(System.Console.ReadLine());

            t.ExpandWithNewRoot(z);
            return;

            Test test = new Test();
            while (true)
            {

                var y = state.GetPlayableMoves();
                printMoves(y);

                var x = state.getBoard();
                test.printBoard(x);

                if (!state.IsKingChecked())
                {
                    System.Console.WriteLine("No check!");
                }

                //System.Console.WriteLine("From X:");
                //string from_x = System.Console.ReadLine();

                //System.Console.WriteLine("From Y:");
                //string from_y = System.Console.ReadLine();

                //System.Console.WriteLine("To X:");
                //string to_x = System.Console.ReadLine();

                //System.Console.WriteLine("To Y:");
                //string to_y = System.Console.ReadLine();

                //state.GenerateMove((int)from_x[0] - 48, (int)from_y[0] - 48, (int)to_x[0] - 48, (int)to_y[0] - 48);


                int move_choice = -1;
                while (!(move_choice < y.Count && move_choice > -1))
                {
                    System.Console.WriteLine("which move?");
                    move_choice = Convert.ToInt32(System.Console.ReadLine());
                    if (move_choice == -1)
                        goto exit;
                }
                state.GenerateMove(y[move_choice]);

                state.setColor(++z % 2 == 0 ? color : oppColor);
            }
            input = System.Console.ReadLine();

        exit:
            System.Console.WriteLine("Exiting! Bye..");
            return;
        }

        

        public static void printMoves(List<Move> moves)
        {
            string s = "";
            int k = 0;
            foreach (var move in moves)
            {
                s += k++ + ". (" + move.from_x + ","
                    + move.from_y + ")"
                    + " to (" + move.to_x + ","
                    + move.to_y
                    + ") " + move.msg + "\n";

            }
            System.Console.WriteLine(s);
        }

    }

}

