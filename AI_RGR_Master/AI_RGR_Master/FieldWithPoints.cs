using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Point = System.Windows.Point;

namespace AI_RGR_Master
{
    public class PlaneWithPoints
    {
        public List<PointOfCluster> points {
            get { return this._points; }
        }

        public int from_file(string filename)
        {
            var full_path = Path.GetFullPath(filename);

            if (!File.Exists(full_path))
            {
                return -1;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(full_path, FileMode.Open)))
            {
                var size = reader.ReadInt32();

                for (var i = 0; i < size; i++)
                {
                    var x = reader.ReadDouble();
                    var y = reader.ReadDouble();

                    var alpha = reader.ReadInt32();
                    var r = reader.ReadInt32();
                    var g = reader.ReadInt32();
                    var b = reader.ReadInt32();
                    var cnt = reader.ReadInt32();

                    points.Add(new PointOfCluster(
                    
                        point: new Point(x, y),
                        color: Color.FromArgb(alpha, r, g, b),
                        is_centroid: cnt
                    ));
                }
            }
            return Convert.ToInt32(filename[filename.Length - 5].ToString());
        }

        public void to_file(string fileName)
        {
            var full_path = Path.GetFullPath(fileName);

            using (BinaryWriter writer = new BinaryWriter(File.Open(full_path, FileMode.Create)))
            {
                writer.Write(points.Count);
                foreach (var point in points)
                {
                    writer.Write(point.point.X);
                    writer.Write(point.point.Y);
                    writer.Write((int)point.Color.A);
                    writer.Write((int)point.Color.R);
                    writer.Write((int)point.Color.G);
                    writer.Write((int)point.Color.B);
                    writer.Write(point.is_centroid);
                }
            }
        }

        public void recolor_points(List<Point> centroids)
        {
            var random = new Random();

            var colors = new List<Color>();
            foreach (var centroid in centroids)
            {
                colors.Add(
                    Color.FromArgb(
                        255,
                        random.Next(255),
                        random.Next(255),
                        random.Next(255)
                    )
                );
            }


            foreach (var point in points)
            {
                recolor_point_to_nearest_centroid(centroids, colors, point);
            }
        }

        private static void recolor_point_to_nearest_centroid(List<Point> centroids, List<Color> colors, PointOfCluster point)
        {
            var min_distance = double.MaxValue;
            var number_of_nearest_centroid = 0;

            for (var i = 0; i < centroids.Count; i++)
            {
                var centroid = centroids[i];
                var distance = Point.Subtract(point.point, centroid).Length;
                if (distance < min_distance)
                {
                    min_distance = distance;
                    number_of_nearest_centroid = i;
                }
            }

            point.Color = colors[number_of_nearest_centroid];
        }

        private List<PointOfCluster> _points = new List<PointOfCluster>();
    }
}
