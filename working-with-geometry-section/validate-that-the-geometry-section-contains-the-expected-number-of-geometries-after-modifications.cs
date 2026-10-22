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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape to work with.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape (using the correct API).
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Record the original number of geometries.
            int originalGeomCount = shape.Geoms.Count;

            // -----------------------------------------------------------------
            // Modification: add a new geometry to the shape.
            // -----------------------------------------------------------------
            // Create a new empty geometry object.
            Geom newGeom = new Geom();

            // Add the new geometry to the shape's Geoms collection.
            shape.Geoms.Add(newGeom);

            // Expected geometry count after the addition.
            int expectedGeomCount = originalGeomCount + 1;

            // -----------------------------------------------------------------
            // Validation: check that the geometry count matches the expectation.
            // -----------------------------------------------------------------
            int actualGeomCount = shape.Geoms.Count;

            if (actualGeomCount == expectedGeomCount)
            {
                Console.WriteLine($"Validation succeeded: Geometry count is {actualGeomCount} as expected.");
            }
            else
            {
                // Throw an exception to indicate failure as per the rules.
                throw new Exception($"Validation failed: Expected {expectedGeomCount} geometries, but found {actualGeomCount}.");
            }

            // -----------------------------------------------------------------
            // Save the modified diagram to the specified output path.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}