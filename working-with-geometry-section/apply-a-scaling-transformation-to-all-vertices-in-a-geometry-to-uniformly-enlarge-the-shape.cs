using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [scaleFactor]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double scaleFactor = 2.0; // Default scaling factor

        // Parse optional scale factor
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
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Iterate through all shapes on the page
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through each geometry of the shape
                        for (int geomIdx = 0; geomIdx < shape.Geoms.Count; geomIdx++)
                        {
                            Geom geom = shape.Geoms[geomIdx];

                            // Iterate through each coordinate object in the geometry
                            for (int coordIdx = 0; coordIdx < geom.CoordinateCol.Count; coordIdx++)
                            {
                                // Use dynamic to access X and Y properties if they exist
                                dynamic vertex = geom.CoordinateCol[coordIdx];
                                try
                                {
                                    // Multiply X and Y values by the scaling factor
                                    vertex.X.Value *= scaleFactor;
                                    vertex.Y.Value *= scaleFactor;
                                }
                                catch
                                {
                                    // If the object does not have X/Y (e.g., a control point), ignore it
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}