using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use arguments if provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Draw a square (rectangle with equal width and height) on the page
            // Parameters: pinX, pinY, width, height
            long shapeId = page.DrawRectangle(1.0, 1.0, 2.0, 2.0);

            // Retrieve the newly created shape using its ID
            Shape squareShape = page.Shapes.GetShape(shapeId);

            // Apply a solid fill pattern (value 1) to the shape
            squareShape.Fill.FillPattern.Value = 1;

            // Set the fill foreground color to a solid green (hex format)
            squareShape.Fill.FillForegnd.Value = "#00FF00";

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}