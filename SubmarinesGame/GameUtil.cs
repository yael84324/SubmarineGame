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
        public void SetBoard()
        {
            int row, column;
            bool isPositionFound = false;

            for (int i = submarinesArray.Length - 1; i >= 0; i--)
            {
                while (!isPositionFound)
                {
                    Random random = new Random();
                    row = random.Next(0, boardHeight); 
                    column = random.Next(0, boardWidth);
                    //נראה שמתאים בגובה
                    if (i+1 <= boardHeight - row)
                    {
                        if (i == 3 || (IsVerticalSubmarinePlacementValid(row, column, i+1)))//הוא ודאיי הראשון ויכול להיכנס
                        {
                            isPositionFound = true;
                            submarinesArray[i] = new VerticalSubmarine(row, column, i+1);
                        }
                    }
                    if(!isPositionFound && i+1 <= boardWidth - column)
                    {
                        //
                        if(i== 3 ||(IsHorizontalSubmarinePlacementValid(row, column, i+1) ))
                        {
                            isPositionFound = true;
                            submarinesArray[i]=new  HorizontalSubmarine(row, column, i+1);
                        }
                    }
                }
                isPositionFound = false;
            }
        }
        public bool IsVerticalSubmarinePlacementValid(int row,int column,int length)
        {
            int CurrentSubmarineRow,CurrentSubmarineColumn,CurrentSubmarineLength;
            for (int i = 3; i > length - 1; i--)
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
        public bool IsHorizontalSubmarinePlacementValid(int row, int column, int length)
        {
            int CurrentSubmarineRow, CurrentSubmarineColumn, CurrentSubmarineLength;
            for(int i=3;i>length - 1; i--)
            {
                if (submarinesArray[i] != null)
                {
                    CurrentSubmarineRow = submarinesArray[i].RowInBoard;
                    CurrentSubmarineColumn = submarinesArray[i].ColumnInBoard;
                    CurrentSubmarineLength = submarinesArray[i].SubmarineLength;
                    if (submarinesArray[i] is HorizontalSubmarine)
                    {
                        if (row == CurrentSubmarineRow && column >= CurrentSubmarineColumn && column <= CurrentSubmarineColumn + CurrentSubmarineLength - 1)
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

        public HitResult Hit(int rowInBoard, int columnInBoard)
        {
            HitResult result;
            for (int i = 0; i < submarinesArray.Length; i++)
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
        public void printBoard()
        {
            char[,] board = new char[boardHeight,boardWidth];
            foreach (AbstractSubmarine currentSubmarine in submarinesArray)
            {
                if (currentSubmarine is HorizontalSubmarine)
                {
                    for (int i = currentSubmarine.ColumnInBoard; i < currentSubmarine.ColumnInBoard + currentSubmarine.SubmarineLength; i++)
                    {
                        board[currentSubmarine.RowInBoard, i] = 'x';
                    }
                }
                else if(currentSubmarine is VerticalSubmarine)
                {
                    for(int i = currentSubmarine.RowInBoard; i < currentSubmarine.RowInBoard + currentSubmarine.SubmarineLength; i++)
                    {
                        board[i,currentSubmarine.ColumnInBoard] = 'x';
                    }
                }
            }
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    if (board[i, j] == 'x')
                        Console.Write(board[i, j] + "\t");
                    else
                        Console.Write("-\t");
                }
                Console.WriteLine(i + "i");
            }
        }
    }
}
