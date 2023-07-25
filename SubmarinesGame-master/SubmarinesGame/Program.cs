using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmarinesGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var util = new GameUtil();
            util.SetBoard();

            Console.WriteLine(util.Hit(0, 0)); // TODO: should result Miss
            
            Console.WriteLine(util.Hit(0, 0)); // TODO: should result Miss
            Console.WriteLine(util.Hit(1, 0)); // TODO: should result Boom
            Console.WriteLine(util.Hit(0, 3)); // TODO: should result Hit
            Console.WriteLine(util.Hit(0, 4)); // TODO: should result Boom
            Console.WriteLine(util.Hit(4, 1)); // TODO: should result Hit
            Console.WriteLine(util.Hit(4, 2)); // TODO: should result Hit
            Console.WriteLine(util.Hit(4, 3)); // TODO: should result Hit
            Console.WriteLine(util.Hit(4, 4)); // TODO: should result Boom
            Console.WriteLine(util.Hit(4, 0)); // TODO: should result Miss

            Console.ReadLine();
        }
    }
}
