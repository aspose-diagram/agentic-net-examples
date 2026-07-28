using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Select the shape you want to inspect.
            // Here we take the first shape on the first page (skip the page shape at index 0).
            Shape shape = diagram.Pages[0].Shapes[1];

            // Retrieve the gradient angle of the shape's fill.
            // GradientAngle is a DoubleValue; its numeric value is accessed via the Value property.
            double gradientAngle = shape.Fill.GradientFill.GradientAngle?.Value ?? 0.0;

            // Output the angle.
            Console.WriteLine($"Gradient Angle: {gradientAngle}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
