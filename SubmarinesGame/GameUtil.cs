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


        //Placing submarines on the game board
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
                        if (i == 3 || IsVerticalSubmarinePlacementEmpty(randomRow, randomColumn, i+1))
                        {
                            isPositionFound = true;
                            submarinesArray[i] = new VerticalSubmarine(randomRow, randomColumn, i+1);
                        }
                    }
                    if(!isPositionFound && i+1 <= boardWidth - randomColumn)
                    {
                        if(i== 3 ||IsHorizontalSubmarinePlacementEmpty(randomRow, randomColumn, i+1) )
                        {
                            isPositionFound = true;
                            submarinesArray[i]=new  HorizontalSubmarine(randomRow, randomColumn, i+1);
                        }
                    }
                }
                isPositionFound = false;
            }
        }

        //checks if a submarine can be placed on the game board at a vertical position without overriding any other submarines.
        public bool IsVerticalSubmarinePlacementEmpty(int row,int column,int length)
        {
            int CurrentSubmarineRow,CurrentSubmarineColumn,CurrentSubmarineLength;
            for (int i = submarinesArray.Length - 1; i > length - 1; i--)
            {
                if (submarinesArray[i] != null)
                {
                    CurrentSubmarineRow = submarinesArray[i].RowInBoard;
                    CurrentSubmarineColumn = submarinesArray[i].ColumnInBoard;
                    CurrentSubmarineLength = submarinesArray[i].SubmarineLength;
                    if (submarinesArray[i] is HorizontalSubmarine)
                    {
                        if ((row >= CurrentSubmarineRow && row + length - 1 <= CurrentSubmarineRow )||
                            (column <= CurrentSubmarineColumn + CurrentSubmarineLength - 1 && column >= CurrentSubmarineColumn))
                            return false;
                    }
                    else if (submarinesArray[i] is VerticalSubmarine)
                    {
                        if (column == CurrentSubmarineColumn && row >= CurrentSubmarineRow && row <= CurrentSubmarineRow + CurrentSubmarineLength - 1)
                            return false;
                    }
                }
            }
            return true;
        }

        //checks if a submarine can be placed on the game board at a horizontal position without overriding any other submarines.
        public bool IsHorizontalSubmarinePlacementEmpty(int row, int column, int length)
        {
            int CurrentSubmarineRow, CurrentSubmarineColumn, CurrentSubmarineLength;
            for(int i= submarinesArray.Length-1; i>length - 1; i--)
            {
                if (submarinesArray[i] != null)
                {
                    CurrentSubmarineRow = submarinesArray[i].RowInBoard;
                    CurrentSubmarineColumn = submarinesArray[i].ColumnInBoard;
                    CurrentSubmarineLength = submarinesArray[i].SubmarineLength;
                    if (submarinesArray[i] is HorizontalSubmarine)
                    {
                        if (row == CurrentSubmarineRow && column >= CurrentSubmarineColumn && 
                            column <= CurrentSubmarineColumn + CurrentSubmarineLength - 1)
                            return false;
                    }
                    else if (submarinesArray[i] is VerticalSubmarine)
                    {
                        if ((column >= CurrentSubmarineColumn && column + length - 1 <= CurrentSubmarineColumn) ||
                            (row <= CurrentSubmarineRow + CurrentSubmarineLength - 1 && row >= CurrentSubmarineRow))
                            return false;
                    }
                }
            }
            return true;
        }


        //print the state of the game board, displaying an "x" in each cell that contains a submarine
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

        //determine if the requested cell on the game board contains any submarine, either horizontally or vertically, and return the result if its a hit,boom or miss
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
