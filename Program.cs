using System;

namespace SudokuSolver
{
    class Program
    {
        public static bool isVeilig(int[,] board, int row, int col, int num)
        {
            //controleer of het getal uniek is binnen de rijen 
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[row, i] == num) // Als het getal voorkomt return false
                    return false;

            //controleer of het getal uniek is binnen de collumns 
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[i, col] == num) // Als het getal voorkomt return false
                    return false;

            //check of binnen het vakje (3x3) een match is.
            int squareroot = (int)Math.Sqrt(board.GetLength(0));
            int rowStart = row - row % squareroot;
            int colStart = col - col % squareroot;

            for (int i = rowStart; i < rowStart + squareroot; i++)
                for (int j = colStart; j < colStart + squareroot; j++)
                    if (board[i, j] == num)
                        return false; //niet 'safe'

            // als er geen overeenkomst is, is het 'safe'
            return true;
        }

        public static bool losOp(int[,] board, int n)
        {
            int row = 0;
            int col = 0;

            bool isEmpty = true; //vakje is standaard leeg

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) //twee for loops om te het hele bord te kunnen 'scannen'
                {
                    if (board[i, j] == 0) //check of het vakje al is ingevuld
                    {
                        row = i; //is later nodig voor controle
                        col = j; //is later nodig voor controle

                        // Nog lege vakjes in het spel.

                        isEmpty = false; //vakje is niet meer legen
                        break;
                    }
                }
                if (!isEmpty) //als het vakje al is ingevuld doorgaan naar het volgende vakje
                {
                    break;
                }
            }

            // volledig opgelost
            if (isEmpty) //als de soduko volledig is opgelost return true
            {
                return true;
            }


            for (int num = 1; num <= n; num++)
            {
                if (isVeilig(board, row, col, num)) //controlleer of 'veilig is'
                {
                    board[row, col] = num; //definitief het vakje invullen
                    if (losOp(board, n)) //als het bord volledig is opgelost return true (recursief gebruik maken van de functie)
                    {
                        return true;
                    }
                    else
                    {
                        board[row, col] = 0; // niet goed, vervangen.
                    }
                }
            }
            return false;
        }

        public static void printOplossing(int[,] board, int N)
        {
            //Oplossing afdrukken op het scherm.
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(board[i, j]);
                    Console.Write(" ");
                }
                Console.Write("\n");

                if ((i + 1) % (int)Math.Sqrt(N) == 0)
                {
                    Console.Write("");
                }
            }
        }

        public static void Main(String[] args)
        {

            int[,] board = new int[,] //bord array aanmaken
            {
            {3, 0, 0, 0, 0, 8, 0, 0, 0},
            {7, 0, 8, 3, 2, 0, 0, 0, 5},
            {0, 0, 0, 9, 0, 0, 0, 1, 0},
            {9, 0, 0, 0, 0, 4, 0, 2, 0},
            {0, 0, 0, 0, 1, 0, 0, 0, 0},
            {0, 7, 0, 8, 0, 0, 0, 0, 9},
            {0, 5, 0, 0, 0, 3, 0, 0, 0},
            {8, 0, 0, 0, 4, 7, 5, 0, 3},
            {0, 0, 0, 5, 0, 0, 0, 0, 6}
            };

            int N = board.GetLength(0); //lengte van het bord

            if (losOp(board, N))
                printOplossing(board, N);
            else
                Console.Write("Geen oplossing");
        }
    }
}
