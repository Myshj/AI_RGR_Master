using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AI_RGR_Master
{
    public class Swarm
    {
        public Swarm(Point min_coordinates, Point max_coordinates)
        {
            this._min_coordinates = min_coordinates;
            this._max_coordinates = max_coordinates;
            this._random_generator = new Random();
        }

        private Point _min_coordinates;
        private Point _max_coordinates;
        private Random _random_generator;

        public List<Source> sources { get; } = new List<Source>();

        public Source global_best { get; } = new Source();

        public PlaneWithPoints plane { get; } = new PlaneWithPoints();

        public int count_of_clusters { get; set; }

        public Source generate_source()
        {
            var source = new Source();
            for (var d = 0; d < count_of_clusters; d++)
            {
                var centroid = new Point
                {
                    X = _min_coordinates.X + _random_generator.NextDouble()*(_max_coordinates.X - _min_coordinates.X),
                    Y = _min_coordinates.Y + _random_generator.NextDouble()*(_max_coordinates.Y - _min_coordinates.Y)
                };
                source.centroids.Add(centroid);
            }
            source.current_value_of_function = TargetFunctions.sum_of_squared_errors(source.centroids, plane.points);
            return source;
        }

        public void update_source(int number_of_first_source, int number_of_centroid, int number_of_second_source)
        {
            var phi = -1 + _random_generator.NextDouble() * 2;
            var vX = sources[number_of_first_source].centroids[number_of_centroid].X + phi * (sources[number_of_first_source].centroids[number_of_centroid].X - sources[number_of_second_source].centroids[number_of_centroid].X);
            var vY = sources[number_of_first_source].centroids[number_of_centroid].Y + phi * (sources[number_of_first_source].centroids[number_of_centroid].Y - sources[number_of_second_source].centroids[number_of_centroid].Y);

            var newSource = new Source();
            for (var x = 0; x < sources[number_of_first_source].centroids.Count; x++)
            {
                newSource.centroids.Add(x == number_of_centroid ? new Point(vX, vY) : sources[number_of_first_source].centroids[x]);
            }
            newSource.current_value_of_function = TargetFunctions.sum_of_squared_errors(newSource.centroids, plane.points);

            if (newSource.current_value_of_function < sources[number_of_first_source].current_value_of_function)
            {
                sources[number_of_first_source] = newSource;
                sources[number_of_first_source].count_of_trials = 0;
            }
            else
            {
                sources[number_of_first_source].count_of_trials++;
            }
        }

        public bool update_global_best()
        {
            var min = sources.First();
            foreach (var source in sources)
            {
                if (source.current_value_of_function < min.current_value_of_function)
                {
                    min = source;
                }
            }

            if (!(min.current_value_of_function < global_best.current_value_of_function))
            {
                return false;
            }

            global_best.centroids.Clear();
            global_best.centroids.AddRange(min.centroids);
            global_best.current_value_of_function = min.current_value_of_function;
            return true;
        }
    }
}
