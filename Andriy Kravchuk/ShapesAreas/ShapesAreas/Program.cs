using System;
using System.Collections.Generic;
using ShapesAreas.AreaCalculators;
using ShapesAreas.Shapes;

namespace ShapesAreas
{
    class Program
    {
        private static List<Shape> shapes = new List<Shape>
        {
            new Circle(new CircleAreaCalculator(1))
        };

        static void Main(string[] args)
        {
            Console.WriteLine(shapes[0]);
        }
    }
}
