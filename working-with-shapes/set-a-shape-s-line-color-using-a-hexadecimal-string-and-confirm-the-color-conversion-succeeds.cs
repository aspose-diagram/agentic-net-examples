using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to an existing Visio file. Adjust as needed.
            const string inputPath = "input.vsdx";
            const string outputPath = "output.vsdx";

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page.
            Page page = diagram.Pages[0];

            // Retrieve an existing shape or create a new rectangle if none exist.
            Shape shape;
            if (page.Shapes.Count > 0)
            {
                // Get the first shape in the collection.
                foreach (Shape s in page.Shapes)
                {
                    shape = s;
                    // Set and verify line color, then break.
                    SetAndVerifyLineColor(shape);
                    break;
                }
            }
            else
            {
                // Add a rectangle shape (master name "Rectangle") at position (2,2) with size 1x1 inches.
                long shapeId = page.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle");
                shape = page.Shapes.GetShape(shapeId);
                SetAndVerifyLineColor(shape);
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Sets the line color using a hex string and confirms the assignment.
    static void SetAndVerifyLineColor(Shape shape)
    {
        const string hexColor = "#00FF00"; // Green

        // Assign the line color.
        shape.Line.LineColor.Value = hexColor;

        // Verify that the color was set correctly (case‑insensitive comparison).
        if (!string.Equals(shape.Line.LineColor.Value, hexColor, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Line color conversion failed. Expected: {hexColor}, Actual: {shape.Line.LineColor.Value}");
        }

        // Optional: output confirmation to console.
        Console.WriteLine($"Line color set to {shape.Line.LineColor.Value} successfully.");
    }
}
