using System;

namespace ShapesAreas.Shapes
{
    public class Parallelogram : IShape
    {
        public double SideA { get; set; }

        public double SideB { get; set; }

        public double Angle { get; set; }

        public Parallelogram(double sideA, double sideB, double angle)
        {
            SideA = sideA;
            SideB = sideB;
            Angle = angle;
        }

        public double CalculateArea()
        {
            return SideA * SideB * Math.Sin(Angle);
        }
    }
}
