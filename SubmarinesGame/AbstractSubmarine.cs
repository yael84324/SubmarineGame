using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SubmarinesGame.GameUtil;

namespace SubmarinesGame
{
    public abstract class AbstractSubmarine
    {
        public int RowInBoard { get; set; }
        public int ColumnInBoard { get; set; }
        public int SubmarineLength { get; set; }
        public bool[] HasBeenAccessed { get; }

        public AbstractSubmarine(int rowInBoard, int columnInBoard, int submarineLength)
        {
            RowInBoard = rowInBoard;
            ColumnInBoard = columnInBoard;
            SubmarineLength = submarineLength;

            //it is automatically initialized with the default value of false for each element
            HasBeenAccessed = new bool[submarineLength];

        }

        //function that receives the position of a point (x, y) and determines whether it hit the submarine, sunk it, or missed
        public abstract HitResult Hit(int rowInBoard, int columnInBoard);
        public void SetHasBeenAccessedTrue(int cellNumber)
        {
            if (cellNumber >= 0 && cellNumber < SubmarineLength)
            {
                HasBeenAccessed[cellNumber] = true;
            }
            else
            {
                throw new ArgumentOutOfRangeException("cellNumber", "Invalid cell number");
            }
        }

        public bool IsAllBeenAcessed()
        {
            for(int i = 0; i < SubmarineLength; i++)
            {
                if (HasBeenAccessed[i] == false)
                    return false;
            }
            return true;
        }
    }
}
