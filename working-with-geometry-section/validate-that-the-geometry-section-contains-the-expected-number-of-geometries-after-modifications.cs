using System.IO;
using System;
using Aspose.Diagram;

class GeometryValidation
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Choose the first page and first shape for demonstration
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Expected number of Geom objects after modifications
            int expectedGeomCount = 3;

            // --- Begin modifications to the Geometry section ---

            // Clear existing geometries (optional, depending on scenario)
            shape.Geoms.Clear();

            // Add new Geom objects to the shape
            for (int i = 0; i < expectedGeomCount; i++)
            {
                Geom geom = new Geom();
                // Example: add a simple line coordinate (optional)
                // Each Geom can have its own CoordinateCol, but for count validation we only need the Geom objects
                shape.Geoms.Add(geom);
            }

            // --- End modifications ---

            // Validate that the Geometry section contains the expected number of Geom objects
            int actualGeomCount = shape.Geoms.Count;

            if (actualGeomCount == expectedGeomCount)
            {
                Console.WriteLine($"Validation succeeded: Geometry contains {actualGeomCount} Geom objects as expected.");
            }
            else
            {
                Console.WriteLine($"Validation failed: Geometry contains {actualGeomCount} Geom objects, expected {expectedGeomCount}.");
            }

            // Save the modified diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
