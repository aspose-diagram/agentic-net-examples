using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – change as needed.
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path – the modified diagram will be saved here.
        string outputPath = "output.vsdx";

        // Shape identifier to modify – replace with the actual shape ID.
        int targetShapeId = 1;

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0) – adjust if a different page is required.
            Page page = diagram.Pages[0];

            // Get the shape by its ID. The GetShape method accepts a long, so cast the int.
            Shape shape = page.Shapes.GetShape((long)targetShapeId);

            // Verify that the shape was found.
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // ----- Disable line inheritance -----
            // By explicitly setting a line color, the shape no longer inherits the line color
            // from its master or style. Use the current color to keep the visual appearance.
            string currentLineColor = shape.Line.LineColor.Value;
            shape.Line.LineColor.Value = currentLineColor; // forces explicit value

            // ----- Assign a new line weight -----
            // LineWeight is a DoubleValue; assign the desired thickness in inches.
            double newWeightInInches = 0.05; // example: 0.05 inches (~1.27 mm)
            shape.Line.LineWeight.Value = newWeightInInches;

            // Save the modified diagram to the output path using the Vsdx format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}