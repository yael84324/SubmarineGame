using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SubmarinesGame.GameUtil;

namespace SubmarinesGame
{
    public class HorizontalSubmarine : AbstractSubmarine
    {
        public HorizontalSubmarine(int rowInBoard, int columnInBoard, int submarineLength) : base(rowInBoard, columnInBoard, submarineLength)
        {
        }

        /* This function checks if the requested cell is within the current submarine area
         * 
         * Checking on a horizontal submarine, if the requested cell is inside the submarine's area, it marks it as "hit" and checks if all cells of the current submarine have been accessed.
         * If all cells have been accessed, it returns "boom", indicating that the submarine has sunk. If there was a hit but the submarine has not sunk, it returns "hit".
         * If the requested point is not within the area of the current submarine, it returns "miss".
         * */
        public override HitResult Hit(int providedRowInBoard, int providedColumnInBoard)
        {
            HitResult hitResult = HitResult.Miss;
            if(providedRowInBoard == RowInBoard && providedColumnInBoard >= ColumnInBoard && providedColumnInBoard <= ColumnInBoard+SubmarineLength-1)
            {
                hitResult = HitResult.Hit;
                SetHasBeenAccessedTrue(providedColumnInBoard - ColumnInBoard);
                if (IsAllBeenAccessed())
                {
                    hitResult = HitResult.Boom;
                }
            }
            return hitResult;
        }
    }
}
