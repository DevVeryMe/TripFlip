using ShapesAreas.Shapes;

namespace ShapesAreas
{
    public class AreaCalculator
    {
        public IShape Shape { get; set; }

        public double CalculateArea()
        {
            return Shape.CalculateArea();
        }
    }
}
