using System;
using ShapesAreas.Shapes;

namespace ShapesAreas
{
    class Program
    {
        static AreaCalculator _calculator = new AreaCalculator();

        static void Main(string[] args)
        {
            var continueCycle = true;

            do
            {
                Console.WriteLine(StringConstants.ShapeChoice);

                var symbol = Console.Read();
                Console.Read();
                Console.Read();

                switch (symbol)
                {
                    case 'e':
                        continueCycle = false;
                        break;
                    case 'c':
                        PrintCircle(out continueCycle);
                        break;
                    case 'p':
                        PrintParallelogram(out continueCycle);
                        break;
                    case 'h':
                        PrintHexagon(out continueCycle);
                        break;
                    case 't':
                        PrintTriangle(out continueCycle);
                        break;
                    default:
                        continueCycle = ContinueCalculation();
                        break;
                }
            } while (continueCycle);
        }

        static bool ContinueCalculation()
        {
            Console.WriteLine(StringConstants.EnteredBadSymbol);

            var symbol = Console.Read();
            Console.Read();
            Console.Read();

            return symbol == 'y';
        }

        static void PrintShapeArea(IShape shape)
        {
            _calculator.Shape = shape;
            Console.WriteLine(_calculator.CalculateArea());
        }

        static void PrintCircle(out bool continueCycle)
        {
            continueCycle = true;
            Console.WriteLine(StringConstants.EnterRadius);

            if (!double.TryParse(Console.ReadLine(), out var radius))
            {
                continueCycle = ContinueCalculation();
                return;
            }

            PrintShapeArea(new Circle(radius));
        }

        static void PrintTriangle(out bool continueCycle)
        {
            continueCycle = true;
            Console.WriteLine(StringConstants.EnterSides);

            if (double.TryParse(Console.ReadLine(), out var sideA) &&
                double.TryParse(Console.ReadLine(), out var sideB) &&
                double.TryParse(Console.ReadLine(), out var sideC))
            {
                if (!ValidateTriangle(sideA, sideB, sideC))
                {
                    Console.WriteLine(StringConstants.IncorrectTriangleSides);
                    continueCycle = ContinueCalculation();
                }
                else
                    PrintShapeArea(new Triangle(sideA, sideB, sideC));
            }
            else
                continueCycle = ContinueCalculation();
        }

        static bool ValidateTriangle(double sideA, double sideB, double sideC)
        {
            return (sideA > 0 || sideB > 0 || sideC > 0) &&
                   (sideA + sideB > sideC) &&
                   (sideA + sideC > sideB) &&
                   (sideB + sideC > sideA);
        }

        static void PrintParallelogram(out bool continueCycle)
        {
            continueCycle = true;
            Console.WriteLine(StringConstants.EnterSidesAndAngle);

            if (double.TryParse(Console.ReadLine(), out var sideA) &&
                double.TryParse(Console.ReadLine(), out var sideB) &&
                double.TryParse(Console.ReadLine(), out var angle))
            {
                if (!ValidateParallelogram(sideA, sideB, angle))
                {
                    Console.WriteLine(StringConstants.IncorrectParallelogramData);
                    continueCycle = ContinueCalculation();
                }
                else
                    PrintShapeArea(new Parallelogram(sideA, sideB, angle));
            }
            else
                continueCycle = ContinueCalculation();
        }

        static bool ValidateParallelogram(double sideA, double sideB, double angle)
        {
            return sideA > 0 && sideB > 0 && 
                   angle > 0 && angle < Math.PI;
        }

        static void PrintHexagon(out bool continueCycle)
        {
            continueCycle = true;
            Console.WriteLine(StringConstants.EnterSide);

            if (!double.TryParse(Console.ReadLine(), out var side))
            {
                continueCycle = ContinueCalculation();
                return;
            }

            PrintShapeArea(new Hexagon(side));
        }
    }
}
