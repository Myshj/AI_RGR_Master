using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Windows.Point;

namespace AI_RGR_Master
{
    public static class TargetFunctions
    {
        public static double sum_of_squared_errors(List<Point> centroids, List<PointOfCluster> points)
        {
            var sum = 0.0;
            foreach(var point in points)
            {
                var min_distance = double.MaxValue;
                foreach(var centroid in centroids)
                {
                    var distance = DistanceCalculator.squared_distance(point.point, centroid);
                    min_distance = Math.Min(distance, min_distance);
                }
                sum += min_distance;
            }
            return sum;
        }

        public static double silhouette_index(List<Point> centroids, List<PointOfCluster> points)
        {
            var current_clusters = new List<List<PointOfCluster>>();
            for (var i = 0; i < centroids.Count; i++)
            {
                current_clusters.Add(new List<PointOfCluster>());
            }
            foreach (var point in points)
            {
                var min_squared_distance = double.MaxValue;
                var nearest_point_number = 0;
                for (var i = 0; i < centroids.Count; i++)
                {
                    var centroid = centroids[i];
                    var squared_distance = DistanceCalculator.squared_distance(point.point, centroid);
                    if (squared_distance < min_squared_distance)
                    {
                        min_squared_distance = squared_distance;
                        nearest_point_number = i;
                    }
                }
                current_clusters[nearest_point_number].Add(point);
            }

            var silhouette_index = 0.0;
            foreach (var point in points)
            {
                var average_distance_from_point_to_neighbors = 0.0;
                var minimal_average_distance_from_point_to_foreigners = double.MaxValue;
                foreach (var cluster in current_clusters)
                {
                    if (cluster.Contains(point))
                    {
                        average_distance_from_point_to_neighbors = 0.0;
                        foreach(var neighbor_point in cluster)
                        {
                            average_distance_from_point_to_neighbors += DistanceCalculator.distance(point.point, neighbor_point.point);
                        }
                        average_distance_from_point_to_neighbors /= cluster.Count - 1;
                    }
                    else
                    {
                        var localB = 0.0;

                        foreach(var foreign_point in cluster)
                        {
                            localB += DistanceCalculator.distance(point.point, foreign_point.point);
                        }
                        localB /= cluster.Count;
                        minimal_average_distance_from_point_to_foreigners = Math.Min(localB, minimal_average_distance_from_point_to_foreigners);
                    }
                }
                silhouette_index += (minimal_average_distance_from_point_to_foreigners - average_distance_from_point_to_neighbors) / Math.Max(average_distance_from_point_to_neighbors, minimal_average_distance_from_point_to_foreigners);
            }
            return silhouette_index;
        }

        public static double xie_beni(List<Point> centroids, List<PointOfCluster> points)
        {

            var current_clusters = new List<List<PointOfCluster>>();
            for (var i = 0; i < centroids.Count; i++)
            {
                current_clusters.Add(new List<PointOfCluster>());
            }
            foreach (var point in points)
            {
                var min_squared_distance = double.MaxValue;
                var nearest_point_number = 0;
                for (var i = 0; i < centroids.Count; i++)
                {
                    var centroid = centroids[i];
                    var squared_distance = DistanceCalculator.squared_distance(point.point, centroid);
                    if (squared_distance < min_squared_distance)
                    {
                        min_squared_distance = squared_distance;
                        nearest_point_number = i;
                    }
                }
                current_clusters[nearest_point_number].Add(point);
            }

            var minimal_squared_distance_between_centroids = double.MaxValue;
            foreach(var first_centroid in centroids)
            {
                foreach (var second_centroid in centroids)
                {
                    if(first_centroid != second_centroid)
                    {
                        var squared_distance = DistanceCalculator.squared_distance(first_centroid, second_centroid);
                        minimal_squared_distance_between_centroids = Math.Min(squared_distance, minimal_squared_distance_between_centroids);
                    }
                }
            }

            var msdoc = points.Sum(point =>
                centroids.Select(centroid =>
                Math.Pow(Point.Subtract(point.point, centroid).Length, 2)).Min());

            var average_squared_distance_from_points_to_their_centroids = 0.0;

            for(var i = 0; i < centroids.Count; i++)
            {
                var local_average = 0.0;
                foreach (var point in current_clusters[i]) {
                    local_average += DistanceCalculator.squared_distance(point.point, centroids[i]);
                }
                local_average /= current_clusters[i].Count;
                average_squared_distance_from_points_to_their_centroids += local_average;
            }
            average_squared_distance_from_points_to_their_centroids /= centroids.Count;

            return average_squared_distance_from_points_to_their_centroids / (points.Count * minimal_squared_distance_between_centroids);
        }
    }
}
