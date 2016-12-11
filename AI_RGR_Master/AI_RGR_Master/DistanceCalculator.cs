using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Windows.Point;

namespace AI_RGR_Master
{
    public static class DistanceCalculator
    {
        public static double distance(Point from, Point to)
        {
            return Point.Subtract(from, to).Length;
        }

        public static double squared_distance(Point from, Point to)
        {
            return Point.Subtract(from, to).LengthSquared;
        }
    }
}
