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

        //checks if the requested cell is within the current submarine area
        public override HitResult Hit(int providedRowInBoard, int providedColumnInBoard)
        {
            HitResult hitResult = HitResult.Miss;
            if(providedRowInBoard == RowInBoard && providedColumnInBoard >= ColumnInBoard && providedColumnInBoard <= ColumnInBoard+SubmarineLength-1)
            {
                hitResult = HitResult.Hit;
                SetHasBeenAccessedTrue(providedColumnInBoard - ColumnInBoard);
                if (IsAllBeenAcessed())
                {
                    hitResult = HitResult.Boom;
                }
            }
            return hitResult;
        }
    }
}
