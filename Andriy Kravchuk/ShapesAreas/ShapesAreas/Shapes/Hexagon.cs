using ShapesAreas.AreaCalculators;

namespace ShapesAreas.Shapes
{
    public class Hexagon : Shape
    {
        public Hexagon(IAreaCalculator calculator) : base(calculator)
        {
        }

        public override string ToString()
        {
            return "Hexagon";
        }
    }
}