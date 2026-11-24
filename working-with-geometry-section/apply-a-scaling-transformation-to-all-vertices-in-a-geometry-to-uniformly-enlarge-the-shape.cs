using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [scaleFactor]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double scaleFactor = 2.0; // default scaling factor

        // Optional scaling factor argument
        if (args.Length >= 3 && !double.TryParse(args[2], out scaleFactor))
        {
            Console.Error.WriteLine("Invalid scale factor. Using default value 2.0.");
            scaleFactor = 2.0;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                for (int p = 0; p < diagram.Pages.Count; p++)
                {
                    Page page = diagram.Pages[p];

                    // Iterate through all shapes on the page
                    for (int s = 0; s < page.Shapes.Count; s++)
                    {
                        Shape shape = page.Shapes[s];

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through all geometries of the shape
                        for (int g = 0; g < shape.Geoms.Count; g++)
                        {
                            Geom geom = shape.Geoms[g];

                            // Scale each coordinate in the geometry
                            for (int i = 0; i < geom.CoordinateCol.Count; i++)
                            {
                                // The collection holds various coordinate types (MoveTo, LineTo, ArcTo, etc.)
                                // All of them expose X and Y DoubleValue properties.
                                object coord = geom.CoordinateCol[i];

                                if (coord is MoveTo move)
                                {
                                    move.X.Value *= scaleFactor;
                                    move.Y.Value *= scaleFactor;
                                }
                                else if (coord is LineTo line)
                                {
                                    line.X.Value *= scaleFactor;
                                    line.Y.Value *= scaleFactor;
                                }
                                else if (coord is ArcTo arc)
                                {
                                    arc.X.Value *= scaleFactor;
                                    arc.Y.Value *= scaleFactor;
                                    // Additional arc parameters (A, B, C) are radii/angles; they are not vertex positions
                                    // and are left unchanged to preserve the arc shape.
                                }
                                // Add other coordinate types here if needed (e.g., EllipticalArcTo, SplineKnot, etc.)
                            }
                        }

                        // Refresh shape data to apply geometry changes
                        shape.RefreshData();
                    }
                }

                // Save the modified diagram using a valid overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}