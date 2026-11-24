using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the active page where the pentagon will be drawn.
                Page page = diagram.ActivePage;

                // Define pentagon vertices (centered at (5,5) with radius 2).
                // The points are specified as a flat double array: {x1, y1, x2, y2, ..., xn, yn, x1, y1}
                double[] pentagonPoints = new double[]
                {
                    5.0, 3.0,          // Vertex 1 (top)
                    6.9022, 4.3820,   // Vertex 2
                    6.1756, 6.6180,   // Vertex 3
                    3.8244, 6.6180,   // Vertex 4
                    3.0978, 4.3820,   // Vertex 5
                    5.0, 3.0           // Close the shape by returning to Vertex 1
                };

                // Draw the pentagon on the page. The method returns the shape ID (long) but it is not needed here.
                long pentagonShapeId = page.DrawPolyline(pentagonPoints);

                // Optionally, you could modify the newly created shape (e.g., set fill color, line weight) using the shape ID.
                // Shape pentagonShape = page.Shapes.GetShape(pentagonShapeId);
                // pentagonShape.Line.LineWeight.Value = 0.02; // Example: set line weight.

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors and write them to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}