using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AI_RGR_Master
{
    internal static class Program
    {
        private static void Main()
        {
            var parameters = new Parameters
            (
                min_coordinates: new Point(0, 0),
                max_coordinates: new Point(1100, 600),
                colony_size: 50,
                max_iteration_number: 5000
            );

            var solver = new Solver(
                parameters
            );
            solver.Run(filename: "C:\\Users\\Alexey\\Desktop\\AI_RGR_Master\\Skewdistribution_3.dat");
            Console.ReadKey();
        }
    }
}
