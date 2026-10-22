using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "source.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path
        string outputPath = "cloned_geometry_output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the names of the source and target shapes
                string sourceShapeNameU = "SourceShape";
                string targetShapeNameU = "TargetShape";

                // Locate source shape
                Shape sourceShape = FindShapeByNameU(diagram, sourceShapeNameU);
                if (sourceShape == null)
                {
                    Console.Error.WriteLine($"Source shape \"{sourceShapeNameU}\" not found.");
                    return;
                }

                // Locate target shape
                Shape targetShape = FindShapeByNameU(diagram, targetShapeNameU);
                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Target shape \"{targetShapeNameU}\" not found.");
                    return;
                }

                // Ensure both shapes are not deleted
                if (sourceShape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("Source shape is marked as deleted.");
                    return;
                }
                if (targetShape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("Target shape is marked as deleted.");
                    return;
                }

                // Clear existing geometry of the target shape (if possible)
                // Attempt to clear the collection; if Clear is not available, remove items individually
                try
                {
                    targetShape.Geoms.Clear();
                }
                catch
                {
                    // Fallback: remove geometries one by one
                    while (targetShape.Geoms.Count > 0)
                    {
                        targetShape.Geoms.RemoveAt(0);
                    }
                }

                // Clone geometry from source to target
                for (int i = 0; i < sourceShape.Geoms.Count; i++)
                {
                    // Explicitly type the iterator as Geom
                    Geom srcGeom = (Geom)sourceShape.Geoms[i];

                    // Add the geometry to the target shape
                    // Assuming Add creates a copy; if it adds reference, this still demonstrates the concept
                    targetShape.Geoms.Add(srcGeom);
                }

                // Save the modified diagram to the output path using Vsdx format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Geometry cloned from \"{sourceShapeNameU}\" to \"{targetShapeNameU}\" and saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Finds a shape by its NameU across all pages of the diagram.
    /// Returns null if not found.
    /// </summary>
    private static Shape FindShapeByNameU(Diagram diagram, string nameU)
    {
        // Iterate through each page
        for (int p = 0; p < diagram.Pages.Count; p++)
        {
            Page page = diagram.Pages[p];

            // Iterate through each shape on the page
            for (int s = 0; s < page.Shapes.Count; s++)
            {
                Shape shape = page.Shapes[s];

                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Compare the NameU property
                if (shape.NameU != null && shape.NameU.Equals(nameU, StringComparison.OrdinalIgnoreCase))
                {
                    return shape;
                }
            }
        }

        // Shape not found
        return null;
    }
}