using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the shape whose gradient angle you want to read
            // Replace 1 with the actual shape ID you are interested in
            long shapeId = 1;

            // Retrieve the shape from the first page (adjust page index if needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Access the shape's fill gradient information
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Read the current gradient angle (in degrees)
            double gradientAngle = gradientFill.GradientAngle.Value;

            // Output the angle value
            Console.WriteLine($"Gradient Angle: {gradientAngle} degrees");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
