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

        public override HitResult Hit(int rowInBoard, int columnInBoard)
        {
            HitResult hitResult = HitResult.Miss;
            //האם אני בכלל בשטח הצוללת
            if(rowInBoard == RowInBoard && columnInBoard >= ColumnInBoard && columnInBoard <= ColumnInBoard+SubmarineLength-1)
            {
                hitResult = HitResult.Hit;
                SetHasBeenAccessedTrue(columnInBoard - ColumnInBoard);
                if (IsAllBeenAcessed())
                {
                    hitResult = HitResult.Boom;
                }
            }
            return hitResult;
        }


        //בודק אם קיים במקום  הזה

    }
}
