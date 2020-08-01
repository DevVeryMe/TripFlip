using ShapesAreas.AreaCalculators;

namespace ShapesAreas.Shapes
{
    public class Triangle : Shape
    {
        public Triangle(IAreaCalculator calculator) : base(calculator)
        {
        }

        public override string ToString()
        {
            return "Triangle";
        }
    }
}
