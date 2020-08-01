using ShapesAreas.AreaCalculators;

namespace ShapesAreas.Shapes
{
    public class Circle : Shape
    {
        public Circle(IAreaCalculator calculator) : base(calculator)
        {
        }

        public override string ToString()
        {
            return "Circle";
        }
    }
}
