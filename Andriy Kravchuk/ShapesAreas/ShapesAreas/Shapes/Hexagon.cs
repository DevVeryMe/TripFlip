using System;

namespace ShapesAreas.Shapes
{
    public class Hexagon : IShape
    {
        public double Side { get; set; }

        public Hexagon(double side)
        {
            Side = side;
        }

        public double CalculateArea()
        {
            return 1.5 * Math.Sqrt(3) * Side;
        }
    }
}