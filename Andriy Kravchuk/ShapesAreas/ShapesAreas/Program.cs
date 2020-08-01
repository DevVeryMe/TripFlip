using System;
using ShapesAreas.Shapes;

namespace ShapesAreas
{
    class Program
    {
        static readonly AreaCalculator _calculator = new AreaCalculator();

        static void Main(string[] args)
        {
            var continueLoop = true;

            do
            {
                Console.WriteLine(StringConstants.ShapeChoiceRequest);

                var symbol = Console.Read();
                Console.Read();
                Console.Read();

                switch (symbol)
                {
                    case 'e':
                        continueLoop = false;
                        break;
                    case 'c':
                        PrintRequestCircleData(out continueLoop);
                        break;
                    case 'p':
                        PrintRequestParallelogramData(out continueLoop);
                        break;
                    case 'h':
                        PrintRequestHexagonData(out continueLoop);
                        break;
                    case 't':
                        PrintRequestTriangleData(out continueLoop);
                        break;
                    default:
                        continueLoop = PrintContinueCalculation(StringConstants.EnteredIncorrectSymbol);
                        break;
                }
            } while (continueLoop);

        }

        static bool PrintContinueCalculation(string errorStr)
        {
            Console.WriteLine(errorStr);
            Console.WriteLine(StringConstants.ContinueCalculationQuestion);

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

        static void PrintRequestCircleData(out bool continueLoop)
        {
            continueLoop = true;
            Console.WriteLine(StringConstants.RadiusRequest);

            if (!double.TryParse(Console.ReadLine(), out var radius) || !ValidateCircle(radius))
            {
                continueLoop = PrintContinueCalculation(StringConstants.IncorrectRadius);
                return;
            }

            PrintShapeArea(new Circle(radius));
        }

        static bool ValidateCircle(double radius)
        {
            return radius > 0;
        }

        static void PrintRequestTriangleData(out bool continueLoop)
        {
            continueLoop = true;
            Console.WriteLine(StringConstants.SidesRequest);

            if (!double.TryParse(Console.ReadLine(), out var sideA) ||
                !double.TryParse(Console.ReadLine(), out var sideB) ||
                !double.TryParse(Console.ReadLine(), out var sideC) ||
                !ValidateTriangle(sideA, sideB, sideC))
            {
                continueLoop = PrintContinueCalculation(StringConstants.IncorrectTriangleSides);
            }
            else
                PrintShapeArea(new Triangle(sideA, sideB, sideC));

        }

        static bool ValidateTriangle(double sideA, double sideB, double sideC)
        {
            return (sideA > 0 || sideB > 0 || sideC > 0) &&
                   (sideA + sideB > sideC) &&
                   (sideA + sideC > sideB) &&
                   (sideB + sideC > sideA);
        }

        static void PrintRequestParallelogramData(out bool continueLoop)
        {
            continueLoop = true;
            Console.WriteLine(StringConstants.SidesAndAngleRequest);

            if (!double.TryParse(Console.ReadLine(), out var sideA) ||
                !double.TryParse(Console.ReadLine(), out var sideB) ||
                !double.TryParse(Console.ReadLine(), out var angle) ||
                !ValidateParallelogram(sideA, sideB, angle))
            {
                continueLoop = PrintContinueCalculation(StringConstants.IncorrectParallelogramData);
            }
            else
                PrintShapeArea(new Parallelogram(sideA, sideB, angle));

        }

        static bool ValidateParallelogram(double sideA, double sideB, double angle)
        {
            return sideA > 0 && sideB > 0 && 
                   angle > 0 && angle < Math.PI;
        }

        static void PrintRequestHexagonData(out bool continueLoop)
        {
            continueLoop = true;
            Console.WriteLine(StringConstants.SideRequest);

            if (!double.TryParse(Console.ReadLine(), out var side))
            {
                continueLoop = PrintContinueCalculation(StringConstants.IncorrectSide);
                return;
            }

            PrintShapeArea(new Hexagon(side));
        }

        static bool ValidateHexagon(double side)
        {
            return side > 0;
        }
    }
}
