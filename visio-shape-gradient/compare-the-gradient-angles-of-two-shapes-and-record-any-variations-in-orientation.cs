using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with actual path)
            string diagramPath = "input.vsdx";

            // IDs of the two shapes to compare (replace with actual IDs)
            long shapeId1 = 1;
            long shapeId2 = 2;

            CompareGradientAngles(diagramPath, shapeId1, shapeId2);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void CompareGradientAngles(string filePath, long shapeId1, long shapeId2)
    {
        // Load the diagram
        using (Diagram diagram = new Diagram(filePath))
        {
            // Assume both shapes are on the first page
            Page page = diagram.Pages[0];

            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            if (shape1 == null || shape2 == null)
            {
                Console.WriteLine("One or both shapes were not found.");
                return;
            }

            double angle1 = GetGradientAngle(shape1);
            double angle2 = GetGradientAngle(shape2);

            Console.WriteLine($"Shape {shapeId1} gradient angle: {angle1}");
            Console.WriteLine($"Shape {shapeId2} gradient angle: {angle2}");

            // Record any variation in orientation
            if (Math.Abs(angle1 - angle2) > 0.0001)
            {
                Console.WriteLine("Gradient angles differ between the two shapes.");
            }
            else
            {
                Console.WriteLine("Gradient angles are identical.");
            }
        }
    }

    static double GetGradientAngle(Shape shape)
    {
        // Return the gradient angle if a gradient fill is defined; otherwise, default to 0
        if (shape.Fill != null && shape.Fill.GradientFill != null && shape.Fill.GradientFill.GradientAngle != null)
        {
            return shape.Fill.GradientFill.GradientAngle.Value;
        }
        return 0.0;
    }
}
