using System;
using System.Collections.Generic;
using System.Windows;

namespace AI_RGR_Master
{
    public class Parameters
    {
        public Parameters(Point min_coordinates, Point max_coordinates, int colony_size , int max_iteration_number)
        {
            this._min_coordinates = min_coordinates;
            this._max_coordinates = max_coordinates;
            this._colony_size = colony_size;
            this._max_iteration_number = max_iteration_number;
        }

        public Point min_coordinates
        {
            get { return this._min_coordinates; }
        }
        public Point max_coordinates
        {
            get { return this._max_coordinates; }
        }
        public int max_iteration_number
        {
            get { return this._max_iteration_number; }
        }
        public int colony_size
        {
            get { return this._colony_size; }
        }


        private Point _min_coordinates;
        private Point _max_coordinates;
        private int _max_iteration_number;
        private int _colony_size;
    }
}