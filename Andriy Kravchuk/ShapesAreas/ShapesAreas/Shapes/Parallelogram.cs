using ShapesAreas.AreaCalculators;

namespace ShapesAreas.Shapes
{
    public class Parallelogram : Shape
    {
        public Parallelogram(IAreaCalculator calculator) : base(calculator)
        {
        }

        public override string ToString()
        {
            return "Parallelogram";
        }
    }
}
