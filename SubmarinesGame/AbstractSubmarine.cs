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

        // receives the position of a point and determines whether it hit the submarine, drown it, or missed
        public abstract HitResult Hit(int rowInBoard, int columnInBoard);


        /* The function receives the cell number from the sequence section that comprises a submarine and marks the corresponding cell as "hit", indicating that it has been damaged.
         * 
         * The purpose of this function is to track the damaged cells of a submarine by marking them as "hit". By marking each damaged cell,
         * it allows for future checks to determine if all the cells of the submarine have been marked, indicating that the submarine has been sunk
         * */
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

        /* The function checks if the entire area of the submarine has been damaged
        * if all cells of a submarine have been marked as "damaged", it returns true, indicating that the submarine has been sunk. Otherwise, it returns false.
        * */
        public bool IsAllBeenAccessed()
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
