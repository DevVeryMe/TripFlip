using ShapesAreas.Shapes;

namespace ShapesAreas
{
    public class AreaCalculator
    {
        public IShape Shape { get; set; }

        public AreaCalculator(IShape shape)
        {
            Shape = shape;
        }

        public double CalculateArea()
        {
            return Shape.CalculateArea();
        }
    }
}
