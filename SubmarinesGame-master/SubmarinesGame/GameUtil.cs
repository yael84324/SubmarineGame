using System;
using System.Collections.Generic;
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

        public void SetBoard()
        {
            // TODO: Implement SetBoard

            // --------------------- // 
            // - X -   -   -   - X - // 
            // --------------------- // 
            // - X -   -   -   - X - // 
            // --------------------- // 
            // -   -   -   -   - X - // 
            // --------------------- // 
            // - X - X - X -   - X - // 
            // --------------------- // 
            // -   - X -   -   -   - // 
            // --------------------- // 


            //   y
            //   ^
            // 4 |
            //   |
            // 3 |
            //   |
            // 2 |
            //   |
            // 1 |
            //   |
            // 0 L__________________> x
            //   0   1   2   3   4  

        }

        public HitResult Hit(int x, int y)
        {
            // TODO: Implement Hit

            return HitResult.Hit;
        }
    }
}
