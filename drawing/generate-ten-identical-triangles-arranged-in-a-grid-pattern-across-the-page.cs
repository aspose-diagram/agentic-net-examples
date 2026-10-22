using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

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
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Optional: set page size (in inches)
            page.PageSheet.PageProps.PageWidth.Value = 11;   // width
            page.PageSheet.PageProps.PageHeight.Value = 8.5; // height

            // Parameters for the grid of triangles
            int rows = 2;
            int cols = 5;
            double startX = 1.0;   // starting X coordinate
            double startY = 1.0;   // starting Y coordinate
            double spacingX = 2.0; // horizontal spacing between triangles
            double spacingY = 2.0; // vertical spacing between triangles
            double triangleSize = 1.0; // base width of each triangle

            // Height of an equilateral triangle
            double triangleHeight = Math.Sqrt(3) / 2 * triangleSize;

            // Loop to create ten identical triangles in a grid
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Calculate the lower‑left corner of the current triangle
                    double baseX = startX + col * spacingX;
                    double baseY = startY + row * spacingY;

                    // Define the three vertices of the triangle and close the shape
                    double[] points = new double[]
                    {
                        baseX,               baseY,                     // Vertex 1 (bottom left)
                        baseX + triangleSize, baseY,                    // Vertex 2 (bottom right)
                        baseX + triangleSize / 2, baseY + triangleHeight, // Vertex 3 (top)
                        baseX,               baseY                      // Close back to Vertex 1
                    };

                    // Draw the triangle on the page
                    long shapeId = page.DrawPolyline(points);
                    // shapeId can be used later if needed (e.g., styling)
                }
            }

            // Save the diagram to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}