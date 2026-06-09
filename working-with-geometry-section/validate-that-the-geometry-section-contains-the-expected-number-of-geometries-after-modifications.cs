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
            // Replace with the actual path to your .vsdx file
            string inputPath = "inputDiagram.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Choose a shape to work with (e.g., the first shape on the first page)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Store the original number of geometries
            int originalCount = shape.Geoms.Count;

            // ------------------------------
            // Modification: add a new Geom
            // ------------------------------
            Geom newGeom = new Geom();
            // (Optionally configure the newGeom properties here)
            shape.Geoms.Add(newGeom);

            // Expected count after addition
            int expectedAfterAdd = originalCount + 1;

            // Validate the count
            if (shape.Geoms.Count == expectedAfterAdd)
            {
                Console.WriteLine($"Add operation successful. Geometry count is {shape.Geoms.Count} as expected.");
            }
            else
            {
                Console.WriteLine($"Add operation failed. Expected {expectedAfterAdd} geometries, but found {shape.Geoms.Count}.");
            }

            // ------------------------------
            // Modification: remove the previously added Geom
            // ------------------------------
            shape.Geoms.Remove(newGeom);

            // Expected count after removal (should be back to original)
            int expectedAfterRemove = originalCount;

            // Validate the count again
            if (shape.Geoms.Count == expectedAfterRemove)
            {
                Console.WriteLine($"Remove operation successful. Geometry count is {shape.Geoms.Count} as expected.");
            }
            else
            {
                Console.WriteLine($"Remove operation failed. Expected {expectedAfterRemove} geometries, but found {shape.Geoms.Count}.");
            }

            // Save the modified diagram (optional)
            // Replace with the desired output path
            string outputPath = "outputDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
