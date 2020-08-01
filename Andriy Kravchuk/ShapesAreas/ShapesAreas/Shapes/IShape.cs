using ShapesAreas.AreaCalculators;

namespace ShapesAreas.Shapes
{
    public abstract class Shape
    {
        protected IAreaCalculator Calculator;

        protected Shape(IAreaCalculator calculator)
        {
            Calculator = calculator;
        }
    }
}
