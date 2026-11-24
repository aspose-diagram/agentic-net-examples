using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "TriangleDiagram.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Define absolute coordinates for the triangle vertices
                // Example points: (1,1), (5,1), (3,4)
                double x1 = 1.0, y1 = 1.0;
                double x2 = 5.0, y2 = 1.0;
                double x3 = 3.0, y3 = 4.0;

                // Draw the triangle using a closed polyline (repeat first point at the end)
                double[] points = new double[]
                {
                    x1, y1,
                    x2, y2,
                    x3, y3,
                    x1, y1 // close the shape
                };
                page.DrawPolyline(points);

                // Save the diagram to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}