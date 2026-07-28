using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // IDs of the two shapes whose gradient angles will be compared
            long shapeId1 = 1;
            long shapeId2 = 2;

            // Retrieve the shapes from the first page
            Shape shape1 = diagram.Pages[0].Shapes.GetShape(shapeId1);
            Shape shape2 = diagram.Pages[0].Shapes.GetShape(shapeId2);

            // Extract gradient angles; use NaN if the shape has no gradient fill defined
            double angle1 = shape1.Fill?.GradientFill?.GradientAngle?.Value ?? double.NaN;
            double angle2 = shape2.Fill?.GradientFill?.GradientAngle?.Value ?? double.NaN;

            // Compare the angles and record any variation
            if (double.IsNaN(angle1) || double.IsNaN(angle2))
            {
                Console.WriteLine("One or both shapes do not have a gradient angle defined.");
            }
            else if (Math.Abs(angle1 - angle2) > 0.0001) // tolerance for floating‑point comparison
            {
                Console.WriteLine($"Gradient angles differ: Shape {shapeId1} = {angle1}°, Shape {shapeId2} = {angle2}°");
            }
            else
            {
                Console.WriteLine($"Gradient angles are identical: {angle1}°");
            }

            // Save the diagram (lifecycle rule: save)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
