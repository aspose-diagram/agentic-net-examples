using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the existing diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Define the base pentagon points (centered at origin, size 1.0 units).
                // Use Aspose.Drawing.PointF as required by DrawPolyline.
                PointF[] basePentagon = new PointF[]
                {
                    new PointF(0f, -1f),
                    new PointF(0.9511f, -0.3090f),
                    new PointF(0.5878f, 0.8090f),
                    new PointF(-0.5878f, 0.8090f),
                    new PointF(-0.9511f, -0.3090f)
                };

                // Grid configuration.
                int rows = 5;               // Number of rows.
                int cols = 5;               // Number of columns.
                float spacingX = 3.0f;      // Horizontal distance between pentagons.
                float spacingY = 3.0f;      // Vertical distance between pentagons.

                // Starting offset (top‑left corner of the grid).
                float startX = 2.0f;
                float startY = 2.0f;

                // Duplicate the pentagon across the grid.
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        // Compute translation for the current cell.
                        float offsetX = startX + col * spacingX;
                        float offsetY = startY + row * spacingY;

                        // Create translated points for this instance.
                        PointF[] translated = new PointF[basePentagon.Length];
                        for (int i = 0; i < basePentagon.Length; i++)
                        {
                            translated[i] = new PointF(
                                basePentagon[i].X + offsetX,
                                basePentagon[i].Y + offsetY);
                        }

                        // Convert PointF array to a flat double array as required by DrawPolyline.
                        double[] polylineCoords = new double[translated.Length * 2];
                        for (int i = 0; i < translated.Length; i++)
                        {
                            polylineCoords[i * 2] = translated[i].X;
                            polylineCoords[i * 2 + 1] = translated[i].Y;
                        }

                        // Draw the pentagon on the page using the correct overload.
                        page.DrawPolyline(polylineCoords);
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
