using System;
using System.Collections.Generic;

namespace AI_RGR_Master
{
    public class Solver
    {
        public Solver(Parameters parameters)
        {
            //this._parameters = parameters;
            this._random_generator = new Random();
            this._colony = new Swarm(parameters.min_coordinates, parameters.max_coordinates);


            _colony_size = parameters.colony_size;
            _max_iteration_number = parameters.max_iteration_number;
        }
        //private Parameters _parameters;
        private Random _random_generator;
        private Swarm _colony;

        private int _colony_size;
        private int _max_iteration_number;

        public double Run( string filename)
        {
            _colony.count_of_clusters = _colony.plane.from_file(filename);
            for (var s = 0; s < _colony_size; s++)
            {
                _colony.sources.Add(_colony.generate_source());
            }

            var last_update_iteration_number = 0;
            for (var iter = 0; iter < _max_iteration_number; iter++)
            {

                for (var i = 0; i < _colony.sources.Count; i++)
                {
                    var j = _random_generator.Next(_colony.count_of_clusters);
                    var k = _random_generator.Next(_colony_size);
                    _colony.update_source(i, j, k);
                }


                var probabilities = new List<double>();
                var sum = .0;
                foreach (var source in _colony.sources)
                {
                    sum += source.current_fitness;
                    probabilities.Add(sum);
                }

                var p = _random_generator.NextDouble() * sum;
                for (var i = 0; i < _colony.sources.Count; i++)
                    if (p <= probabilities[i])
                    {
                        var j = _random_generator.Next(_colony.count_of_clusters);
                        var k = _random_generator.Next(_colony_size);
                        _colony.update_source(i, j, k);
                        break;
                    }

                if (_colony.update_global_best())
                    last_update_iteration_number = iter;


                for (var i = 0; i < _colony.sources.Count; i++)
                    if (_colony.sources[i].count_of_trials == 100)
                        _colony.sources[i] = _colony.generate_source();

            }
            Console.WriteLine($"\nlast_update_iteration_number:  #{last_update_iteration_number}. " +
                              $"\nSSE = {TargetFunctions.sum_of_squared_errors(_colony.global_best.centroids, _colony.plane.points)}" +
                              $"\nSI = {TargetFunctions.silhouette_index(_colony.global_best.centroids, _colony.plane.points)}" +
                              $"\nXB = {TargetFunctions.xie_beni(_colony.global_best.centroids, _colony.plane.points)}");

            _colony.plane.recolor_points(_colony.global_best.centroids);
            foreach (var centroid in _colony.global_best.centroids)
                _colony.plane.points.Add(new PointOfCluster(centroid, 1, System.Drawing.Color.Black));

            var newFileName = $"{filename.Substring(0, filename.Length - 4)}_colored.dat";
            _colony.plane.to_file(newFileName);

            Console.WriteLine($"File with results \"{newFileName}\"");

            return _colony.global_best.current_value_of_function;
        }
    }
}
