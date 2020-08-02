using System;
using ShapesAreas.Shapes;

namespace ShapesAreas
{
    class Program
    {
        static readonly AreaCalculator _calculator = new AreaCalculator(new Circle(1));

        static void Main(string[] args)
        {
            var continueLoop = true;

            do
            {
                Console.WriteLine(StringConstants.ShapeChoiceRequest);

                var stringToParse = Console.ReadLine();

                if (int.TryParse(stringToParse, out int value))
                {
                    switch (value)
                    {
                        case 0:
                            continueLoop = false;
                            break;
                        case 1:
                            PrintRequestCircleData(out continueLoop);
                            break;
                        case 2:
                            PrintRequestParallelogramData(out continueLoop);
                            break;
                        case 3:
                            PrintRequestHexagonData(out continueLoop);
                            break;
                        case 4:
                            PrintRequestTriangleData(out continueLoop);
                            break;
                        default:
                            continueLoop = PrintContinueCalculation(StringConstants.EnteredIncorrectSymbol);
                            break;
                    }
                }

            } while (continueLoop);

        }

        static bool PrintContinueCalculation(string errorString)
        {
            Console.WriteLine(errorString);
            Console.WriteLine(StringConstants.ContinueCalculationQuestion);

            var stringToParse = Console.ReadLine();

            return int.TryParse(stringToParse, out var value) && value == 1;
        }

        static void PrintShapeArea(IShape shape)
        {
            _calculator.Shape = shape;
            Console.WriteLine(StringConstants.AreaOutput + " " + Math.Round(_calculator.CalculateArea(), 2));
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

            if (double.TryParse(Console.ReadLine(), out var sideA) &&
                double.TryParse(Console.ReadLine(), out var sideB) &&
                double.TryParse(Console.ReadLine(), out var sideC) &&
                ValidateTriangle(sideA, sideB, sideC))
            {
                PrintShapeArea(new Triangle(sideA, sideB, sideC));
            }
            else
            {
                continueLoop = PrintContinueCalculation(StringConstants.IncorrectTriangleSides);
            }
            
        }

        static bool ValidateTriangle(double sideA, double sideB, double sideC)
        {
            return (sideA > 0 && sideB > 0 && sideC > 0) &&
                   (sideA + sideB > sideC) &&
                   (sideA + sideC > sideB) &&
                   (sideB + sideC > sideA);
        }

        static void PrintRequestParallelogramData(out bool continueLoop)
        {
            continueLoop = true;
            Console.WriteLine(StringConstants.SidesAndAngleRequest);

            if (double.TryParse(Console.ReadLine(), out var sideA) &&
                double.TryParse(Console.ReadLine(), out var sideB) &&
                double.TryParse(Console.ReadLine(), out var angle) &&
                ValidateParallelogram(sideA, sideB, angle))
            {
                PrintShapeArea(new Parallelogram(sideA, sideB, angle));
            }
            else
            {
                continueLoop = PrintContinueCalculation(StringConstants.IncorrectParallelogramData);
            }

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

            if (!double.TryParse(Console.ReadLine(), out var side) ||
                !ValidateHexagon(side))
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
