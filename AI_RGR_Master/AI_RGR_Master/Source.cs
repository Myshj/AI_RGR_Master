using System.Collections.Generic;
using System.Windows;

namespace AI_RGR_Master
{
    public class Source
    {
        public List<Point> centroids
        {
            get { return _centroids; }
        }
        public double current_fitness
        {
            get { return _current_fitness; }
        }
        public int count_of_trials
        {
            get { return _count_of_trials; }
            set { _count_of_trials = value; }
        }
        public double current_value_of_function
        {
            get { return _current_value_of_function; }
            set
            {
                _current_fitness = value < 0 ? 1 - value : 1 / (1 + value);
                _current_value_of_function = value;
            }
        }


        private List<Point> _centroids = new List<Point>();
        private double _current_value_of_function = double.MaxValue;
        private double _current_fitness;
        private int _count_of_trials;    
    }
}
