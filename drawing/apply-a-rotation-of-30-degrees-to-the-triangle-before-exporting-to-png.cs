using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <inputVisioFile> <outputPngFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page of the diagram.
                Page page = diagram.Pages[0];

                // Define triangle vertices (X1,Y1, X2,Y2, X3,Y3) and close the shape by repeating the first point.
                double[] trianglePoints = new double[] { 1, 1, 5, 1, 3, 4, 1, 1 };

                // Draw the triangle; DrawPolyline returns the shape ID (long).
                long triangleId = page.DrawPolyline(trianglePoints);

                // Retrieve the shape object using the returned ID.
                Shape triangleShape = page.Shapes.GetShape(triangleId);

                // Apply a rotation of 30 degrees to the triangle.
                triangleShape.SetAngle(30);

                // Prepare PNG export options.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

                // Export the modified diagram to PNG.
                diagram.Save(outputPath, pngOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}