using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input VDX file path (adjust as needed)
        string inputPath = "input.vdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Output VDX file path
        string outputPath = "output.vdx";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Retrieve shape with ID 5
            long targetShapeId = 5L;
            Shape shape = page.Shapes.GetShape(targetShapeId);

            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {targetShapeId} not found.");
                return;
            }

            // Create a dynamic string to replace the shape's text
            string newText = $"Updated at {DateTime.Now}";

            // Clear existing text and add the new text
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(newText));

            // Save the modified diagram back to VDX format
            diagram.Save(outputPath, SaveFileFormat.Vdx);

            Console.WriteLine($"Shape ID {targetShapeId} text updated and diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}