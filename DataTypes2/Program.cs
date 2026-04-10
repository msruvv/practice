using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes2
{
    /// <summary>
    /// Программа для рисования ромба из символов X
    /// </summary>
    internal class Program
    {
        private const int DiamontSize = 5;

        /// <summary>
        /// Создает двумерный массив для ромба
        /// </summary>
        /// <param name="n">Размер ромба</param>
        /// <returns>Двумерный массив символов</returns>
        private static char[][] CreateDiamond(int n)
        {
            int middle = n / 2;
            char[][] diamond = new char[n][];

            for (var row = 0; row < n; row++)
            {
                int distanceFromCenter = Math.Abs(row - middle);
                int leftXPosition = distanceFromCenter;
                int rightXPosition = n - 1 - distanceFromCenter;

                diamond[row] = new char[n];

                for (var col = 0; col < n; col++)
                {
                    diamond[row][col] =
                        col == leftXPosition || col == rightXPosition
                        ? 'X'
                        : ' ';
                }
            }

            return diamond;
        }

        /// <summary>
        /// Выводит ромб в консоль
        /// </summary>
        /// <param name="n">Размер ромба</param>
        public static void PrintDiamond(int n)
        {
            var diamond = CreateDiamond(n);

            for (var row = 0; row < diamond.Length; row++)
            {
                for (var col = 0; col < diamond[row].Length; col++)
                {
                    Console.Write(diamond[row][col]);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Главный метод программы
        /// </summary>
        static void Main(string[] args)
        {
            PrintDiamond(DiamontSize);
        }
    }
}
