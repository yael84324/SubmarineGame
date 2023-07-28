using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmarinesGame
{
    public class GameUtil
    {
        public enum HitResult
        {
            Hit,
            Miss,
            Boom
        }

        public AbstractSubmarine[] submarinesArray = new AbstractSubmarine[4];
        public int boardWidth = 5;
        public int boardHeight = 5;

        //The default value, not randomly
        //public void SetBoard()
        //{
        //    submarinesArray[0] = new VerticalSubmarine(0, 4, 4);
        //    submarinesArray[1] = new HorizontalSubmarine(3, 0, 3);
        //    submarinesArray[2] = new VerticalSubmarine(0, 0, 2);
        //    submarinesArray[3] = new HorizontalSubmarine(4, 1, 1);
        //}


        //function that placing submarines on the game board
        /*
         * Generates a random location on the game board and checks if the location is valid for placing a submarine. If the location is not valid,
         * it generates a new random location until a valid one is found.
         *
         * A valid location for a submarine is determined based on the following criteria:
         * 1. The submarine can enter horizontally or vertically from the drawn position on the board, without extending beyond the boundaries of the board.
         * 2. All the positions where the submarine will sit must be empty.
         * */
        public void SetBoard()
        {
            int randomRow, randomColumn;
            bool isPositionFound = false;

            for (int i = submarinesArray.Length - 1; i >= 0; i--)
            {
                while (!isPositionFound)
                {
                    Random random = new Random();
                    randomRow = random.Next(0, boardHeight); 
                    randomColumn = random.Next(0, boardWidth);
                    if (i+1 <= boardHeight - randomRow)
                    {
                        if (i == submarinesArray.Length-1 || IsVerticalSubmarinePlacementEmpty(randomRow, randomColumn, i+1))
                        {
                            isPositionFound = true;
                            submarinesArray[i] = new VerticalSubmarine(randomRow, randomColumn, i+1);
                        }
                    }
                    if(!isPositionFound && i+1 <= boardWidth - randomColumn)
                    {
                        if(i== submarinesArray.Length - 1 || IsHorizontalSubmarinePlacementEmpty(randomRow, randomColumn, i+1) )
                        {
                            isPositionFound = true;
                            submarinesArray[i]=new  HorizontalSubmarine(randomRow, randomColumn, i+1);
                        }
                    }
                }
                isPositionFound = false;
            }
        }

        // the function ensure that a submarine can be placed on the game board at a vertical position without overriding any other submarines.
        // checks if there are a specified number of consecutive free spaces on the game board vertically, starting from a given position, considering the length of the submarine to be placed.

        public bool IsVerticalSubmarinePlacementEmpty(int providedRow, int providedColumn,int providedLength)
        {
            int CurrentSubmarineRow,CurrentSubmarineColumn,CurrentSubmarineLength;
            for (int i = submarinesArray.Length - 1; i > providedLength - 1; i--)
            {
                if (submarinesArray[i] != null)
                {
                    CurrentSubmarineRow = submarinesArray[i].RowInBoard;
                    CurrentSubmarineColumn = submarinesArray[i].ColumnInBoard;
                    CurrentSubmarineLength = submarinesArray[i].SubmarineLength;
                    if (submarinesArray[i] is HorizontalSubmarine)
                    {
                        if ((providedRow >= CurrentSubmarineRow && providedRow + providedLength - 1 <= CurrentSubmarineRow )||
                            (providedColumn <= CurrentSubmarineColumn + CurrentSubmarineLength - 1 && providedColumn >= CurrentSubmarineColumn))
                            return false;
                    }
                    else if (submarinesArray[i] is VerticalSubmarine)
                    {
                        if (providedColumn == CurrentSubmarineColumn && providedRow >= CurrentSubmarineRow && providedRow <= CurrentSubmarineRow + CurrentSubmarineLength - 1)
                            return false;
                    }
                }
            }
            return true;
        }

        // the function ensure that a submarine can be placed on the game board at a horizontal position without overriding any other submarines.
        // checks if there are a specified number of consecutive free spaces on the game board horizontally, starting from a given position, considering the length of the submarine to be placed.
        public bool IsHorizontalSubmarinePlacementEmpty(int providedRow, int providedColumn, int providedLength)
        {
            int CurrentSubmarineRow, CurrentSubmarineColumn, CurrentSubmarineLength;
            for(int i= submarinesArray.Length-1; i> providedLength - 1; i--)
            {
                if (submarinesArray[i] != null)
                {
                    CurrentSubmarineRow = submarinesArray[i].RowInBoard;
                    CurrentSubmarineColumn = submarinesArray[i].ColumnInBoard;
                    CurrentSubmarineLength = submarinesArray[i].SubmarineLength;
                    if (submarinesArray[i] is HorizontalSubmarine)
                    {
                        if (providedRow == CurrentSubmarineRow && providedColumn >= CurrentSubmarineColumn &&
                            providedColumn <= CurrentSubmarineColumn + CurrentSubmarineLength - 1)
                            return false;
                    }
                    else if (submarinesArray[i] is VerticalSubmarine)
                    {
                        if ((providedColumn >= CurrentSubmarineColumn && providedColumn + providedLength - 1 <= CurrentSubmarineColumn) ||
                            (providedRow <= CurrentSubmarineRow + CurrentSubmarineLength - 1 && providedRow >= CurrentSubmarineRow))
                            return false;
                    }
                }
            }
            return true;
        }


        // print the state of the game board, displaying an "x" in each cell that contains a submarine
        public void printBoard()
        {
            char[,] board = new char[boardHeight,boardWidth];
            foreach (AbstractSubmarine currentSubmarine in submarinesArray)
            {
                if (currentSubmarine is HorizontalSubmarine)
                {
                    for (int column = currentSubmarine.ColumnInBoard; column < currentSubmarine.ColumnInBoard + currentSubmarine.SubmarineLength; column++)
                    {
                        board[currentSubmarine.RowInBoard, column] = 'x';
                    }
                }
                else if(currentSubmarine is VerticalSubmarine)
                {
                    for(int row = currentSubmarine.RowInBoard; row < currentSubmarine.RowInBoard + currentSubmarine.SubmarineLength; row++)
                    {
                        board[row, currentSubmarine.ColumnInBoard] = 'x';
                    }
                }
            }
            for (int row = 0; row < boardHeight; row++)
            {
                for (int column = 0; column < boardWidth; column++)
                {
                    if (board[row, column] == 'x')
                        Console.Write(board[row, column] + "\t");
                    else
                        Console.Write("-\t");
                }

                Console.WriteLine(row);
            }
        }

        //  the function determine if the requested cell on the game board contains any submarine, either horizontally or vertically, and return the result if its a hit,boom or miss

        /* This function scans all the submarines on the game board and checks if the specified position, received as a parameter, corresponds to any of the submarines. It returns the result of the hit or miss.
        * If all parts of a submarine at the specified position have been damaged, the function returns "boom", indicating that the submarine has been sunk.
        * If there is damage to the submarine at the specified position but not all parts have been damaged, the function returns "hit", indicating that the submarine has been hit but not completely destroyed.
        * If there is no damage to any submarine at the specified position, the function returns "miss", indicating that there is no submarine present in that location.
        * */
        public HitResult Hit(int rowInBoard, int columnInBoard)
        {
            HitResult result;
            for(int i = 0; i < submarinesArray.Length; i++)
            {
                if (submarinesArray[i] != null)
                {
                    result = submarinesArray[i].Hit(rowInBoard, columnInBoard);
                    if (result != HitResult.Miss)
                    {
                        return result;
                    }
                }
            }
            return HitResult.Miss;
        }
    }
}
