using System.IO;
using System;
using Aspose.Diagram;

class GradientDirectionReader
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Select the page and shape you want to inspect
            // Here we assume the first page and a shape with ID = 1
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1);

            // Access the gradient fill of the shape
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Read the gradient direction (IntValue) and cast to the enum
            int dirValue = gradientFill.GradientDir.Value;
            GradientFillDir gradientDirection = (GradientFillDir)dirValue;

            // Output the gradient direction for verification
            Console.WriteLine($"Gradient Direction: {gradientDirection} (Value = {dirValue})");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
