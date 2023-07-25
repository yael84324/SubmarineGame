using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SubmarinesGame.GameUtil;

namespace SubmarinesGame
{
    public class VerticalSubmarine : AbstractSubmarine
    {
        public VerticalSubmarine(int rowInBoard, int columnInBoard, int submarineLength) : base(rowInBoard, columnInBoard, submarineLength)
        {
        }

        //checks if the requested cell is within the current submarine area
        public override HitResult Hit(int providedRowInBoard, int providedColumnInBoard)
        {
            HitResult hitResult = HitResult.Miss;
            if(providedColumnInBoard == ColumnInBoard && providedRowInBoard >= RowInBoard && providedRowInBoard <= RowInBoard + SubmarineLength-1)
            {
                hitResult = HitResult.Hit;
                SetHasBeenAccessedTrue(providedRowInBoard - RowInBoard);
                if (IsAllBeenAcessed())
                {
                    hitResult = HitResult.Boom;
                }
            }
            return hitResult;
        }
    }
}
