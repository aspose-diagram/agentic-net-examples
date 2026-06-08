using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the shape you want to inspect.
            // Here we assume the shape ID is known (e.g., 1) and it resides on the first page.
            int shapeId = 1;
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Retrieve the gradient direction value from the shape's fill formatting.
            IntValue gradientDirValue = shape.Fill.GradientFill.GradientDir;

            // Cast the integer value to the GradientFillDir enumeration for readability.
            GradientFillDir gradientDirection = (GradientFillDir)gradientDirValue.Value;

            // Output the current gradient direction for verification.
            Console.WriteLine($"Gradient Direction of shape {shapeId}: {gradientDirection}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
