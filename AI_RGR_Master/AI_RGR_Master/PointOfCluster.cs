using System.Drawing;
using Point = System.Windows.Point;

namespace AI_RGR_Master
{
    public class PointOfCluster
    {
        /**
         * Represents point on the field;
         * */
        public PointOfCluster(Point point, int is_centroid, Color color)
        {
            this._point = point;
            this._is_centroid = is_centroid;
            this._color = color;
        }
        public Point point {
            get { return this._point; }
        }

        public int is_centroid {
            get { return this._is_centroid; }
            set { this.is_centroid = value; }
        }

        public Color Color {
            get { return this._color; }
            set { this._color = value; }
        }

        private Point _point;
        private int _is_centroid;
        private Color _color;
    };
}