using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assumes the triangle is on this page).
            Page page = diagram.Pages[0];

            // Original triangle vertices (example coordinates).
            // The triangle is defined by three points and closed by repeating the first point.
            double[] originalPoints = new double[]
            {
                1.0, 1.0,   // Point 1 (PinX, PinY)
                5.0, 1.0,   // Point 2
                3.0, 4.0,   // Point 3
                1.0, 1.0    // Close the polygon by returning to Point 1
            };

            // Scale factor.
            double scaleFactor = 0.5;

            // Compute scaled points.
            double[] scaledPoints = new double[originalPoints.Length];
            for (int i = 0; i < originalPoints.Length; i++)
            {
                scaledPoints[i] = originalPoints[i] * scaleFactor;
            }

            // Draw the smaller triangle on the same page.
            // The DrawPolyline method expects a flat array of coordinates.
            long newShapeId = page.DrawPolyline(scaledPoints);

            // Optionally, you could modify the new shape's properties here (e.g., fill color).

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}