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
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process each geometry of the shape.
                        for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
                        {
                            // Retrieve the geometry object.
                            Geom geom = shape.Geoms[geomIndex];

                            // ------------------------------------------------------------
                            // Example modification: add a new vertex (LineTo) to the geometry.
                            // ------------------------------------------------------------
                            LineTo newVertex = new LineTo();
                            // Set coordinates for the new vertex (example values).
                            newVertex.X.Value = 2.0; // X coordinate
                            newVertex.Y.Value = 2.0; // Y coordinate

                            // Append the new vertex to the geometry's coordinate collection.
                            geom.CoordinateCol.Add(newVertex);

                            // ------------------------------------------------------------
                            // Document the change by adding a comment linked to the shape.
                            // ------------------------------------------------------------
                            // The comment is stored as a page annotation associated with the shape.
                            page.AddComment(shape, "Added extra vertex to adjust shape geometry.");
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}