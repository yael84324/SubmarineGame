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
            util.printBoard();

            //The first number represents the row and the second number represents the column

            //Console.WriteLine(util.Hit(0, 0)); // TODO: should result Hit
            //Console.WriteLine(util.Hit(1, 0)); // TODO: should result Boom
            //Console.WriteLine(util.Hit(0, 3)); // TODO: should result Miss
            //Console.WriteLine(util.Hit(0, 4)); // TODO: should result Hit
            //Console.WriteLine(util.Hit(4, 1)); // TODO: should result Boom
            //Console.WriteLine(util.Hit(4, 2)); // TODO: should result Miss
            //Console.WriteLine(util.Hit(4, 3)); // TODO: should result Miss
            //Console.WriteLine(util.Hit(4, 4)); // TODO: should result Miss
            //Console.WriteLine(util.Hit(4, 0)); // TODO: should result Miss

            Console.WriteLine("Please enter the row and column numbers of the position you want on the game board every time(both integers):");
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    int rowInBoard = int.Parse(Console.ReadLine());
                    int columnInBoard = int.Parse(Console.ReadLine());
                    Console.WriteLine(util.Hit(rowInBoard, columnInBoard));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            util.printBoard();
            Console.ReadLine();
        }
    }
}
