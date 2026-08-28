using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access a shape to work with (first shape on the first page)
            // Shape IDs start at 1, so we use index 1
            Shape shape = diagram.Pages[0].Shapes[1];

            // Define the expected number of Geom objects after modifications
            int expectedGeomCount = 3;

            // OPTIONAL: Clear existing geometries to start from a known state
            shape.Geoms.Clear();

            // Add the expected number of Geom objects to the shape's Geometry collection
            for (int i = 0; i < expectedGeomCount; i++)
            {
                Geom newGeom = new Geom();          // Create a new Geom instance
                shape.Geoms.Add(newGeom);           // Add it to the collection
            }

            // Validate that the Geometry collection now contains the expected count
            int actualGeomCount = shape.Geoms.Count;
            if (actualGeomCount == expectedGeomCount)
            {
                Console.WriteLine($"Validation succeeded: Geometry count is {actualGeomCount} as expected.");
            }
            else
            {
                Console.WriteLine($"Validation failed: Geometry count is {actualGeomCount}, expected {expectedGeomCount}.");
            }

            // Save the modified diagram (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
