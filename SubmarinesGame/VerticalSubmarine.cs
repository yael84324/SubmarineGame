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

        public override HitResult Hit(int rowInBoard, int columnInBoard)
        {
            HitResult hitResult = HitResult.Miss;
            if(columnInBoard == ColumnInBoard && rowInBoard >= RowInBoard && rowInBoard <= RowInBoard + SubmarineLength-1)
            {
                hitResult = HitResult.Hit;
                SetHasBeenAccessedTrue(rowInBoard - RowInBoard);
                if (IsAllBeenAcessed())
                {
                    hitResult = HitResult.Boom;
                }
            }
            return hitResult;
        }
    }
}
