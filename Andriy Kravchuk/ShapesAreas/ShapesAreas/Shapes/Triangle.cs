using System;

namespace ShapesAreas.Shapes
{
    public class Triangle : IShape
    {
        public double SideA { get; set; }

        public double SideB { get; set; }

        public double SideC { get; set; }

        public Triangle(double sideA, double sideB, double sideC)
        {
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }

        public double CalculateArea()
        {
            var semiPerimeter = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(
                semiPerimeter * (semiPerimeter - SideA) * (semiPerimeter - SideB) * (semiPerimeter - SideC));
        }
    }
}
