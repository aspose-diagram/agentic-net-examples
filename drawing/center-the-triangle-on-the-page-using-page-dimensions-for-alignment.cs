using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0).
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define desired triangle size.
                double triangleWidth = 2.0;   // inches
                double triangleHeight = 2.0;  // inches

                // Calculate lower‑left corner so the triangle is centered.
                double left = (pageWidth - triangleWidth) / 2.0;
                double bottom = (pageHeight - triangleHeight) / 2.0;

                // Define triangle vertices (clockwise) and close the shape by repeating the first point.
                double[] points = new double[]
                {
                    left, bottom,                                 // Vertex 1
                    left + triangleWidth, bottom,                 // Vertex 2
                    left + triangleWidth / 2.0, bottom + triangleHeight, // Vertex 3 (top)
                    left, bottom                                  // Close polygon
                };

                // Draw the triangle on the page.
                long shapeId = page.DrawPolyline(points);

                // (Optional) Retrieve the shape if further modifications are needed.
                // Shape triangle = page.Shapes.GetShape(shapeId);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}