using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape (example: shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);

            // Read the gradient angle if a gradient fill is defined
            double gradientAngle = 0.0;
            if (shape.Fill != null && shape.Fill.GradientFill != null)
            {
                gradientAngle = shape.Fill.GradientFill.GradientAngle.Value;
            }

            Console.WriteLine($"Shape ID {shape.ID} gradient angle: {gradientAngle} degrees");

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
