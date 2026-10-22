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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page.
                Shape shape = page.Shapes[0];

                // Ensure the shape has at least one geometry section.
                if (shape.Geoms.Count == 0)
                {
                    Console.Error.WriteLine("The selected shape has no geometry sections.");
                    return;
                }

                // Retrieve the first geometry.
                Geom geom = (Geom)shape.Geoms[0];

                // Ensure the geometry has coordinate entries (vertices).
                if (geom.CoordinateCol.Count == 0)
                {
                    Console.Error.WriteLine("The geometry contains no vertices to modify.");
                    return;
                }

                // Example scaling factor – adjust as needed for your new dimensions.
                double scaleFactor = 2.0;

                // Iterate over each coordinate object and update its X and Y values.
                for (int i = 0; i < geom.CoordinateCol.Count; i++)
                {
                    // Use dynamic to avoid compile‑time type checking for X/Y properties.
                    dynamic coord = geom.CoordinateCol[i];

                    try
                    {
                        // Scale X and Y if they exist.
                        coord.X.Value *= scaleFactor;
                        coord.Y.Value *= scaleFactor;
                    }
                    catch
                    {
                        // If the coordinate does not have X/Y (unlikely), ignore.
                    }

                    // Handle ArcTo control point A if present.
                    try
                    {
                        if (coord.A != null)
                        {
                            coord.A.X.Value *= scaleFactor;
                            coord.A.Y.Value *= scaleFactor;
                        }
                    }
                    catch
                    {
                        // No control point A – ignore.
                    }

                    // Note: ArcTo.B is not available in the current API version, so it is omitted.
                }

                // Refresh the shape to apply geometry changes.
                shape.RefreshData();

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
