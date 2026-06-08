using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve a shape (example: shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                throw new Exception("Shape with ID 1 not found.");
            }

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient angle to 90 degrees (vertical fill)
            shape.Fill.GradientFill.GradientAngle.Value = 90;

            // Ensure the gradient direction is linear (optional)
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
