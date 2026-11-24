using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (hard‑coded for this example)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output diagram path
        string outputPath = "output.vsdx";
        // Shape ID to modify (example uses shape with ID 1)
        long shapeId = 1;
        // Desired fill color in hexadecimal format
        string fillColor = "#FF0000";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the target shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page.");
                return;
            }

            // -----------------------------------------------------------------
            // Disable fill inheritance and apply a custom solid fill color
            // -----------------------------------------------------------------
            // Set fill pattern to solid (value 1)
            shape.Fill.FillPattern.Value = 1;
            // Assign the custom fill foreground color
            shape.Fill.FillForegnd.Value = fillColor;

            // -----------------------------------------------------------------
            // Save the modified diagram
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}