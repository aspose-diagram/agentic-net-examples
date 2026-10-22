using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_centered.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define triangle vertices (relative to origin) and close the shape
            // Format: x1, y1, x2, y2, x3, y3, x1, y1
            double[] trianglePoints = new double[]
            {
                0, 0,   // Vertex 1
                2, 0,   // Vertex 2
                1, 2,   // Vertex 3
                0, 0    // Close the polygon
            };

            // Draw the triangle on the page; returns the shape ID
            long triangleId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the ID
            Shape triangleShape = page.Shapes.GetShape(triangleId);

            // Center the triangle on the page by setting its PinX and PinY to page center
            triangleShape.XForm.PinX.Value = pageWidth / 2.0;
            triangleShape.XForm.PinY.Value = pageHeight / 2.0;

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}