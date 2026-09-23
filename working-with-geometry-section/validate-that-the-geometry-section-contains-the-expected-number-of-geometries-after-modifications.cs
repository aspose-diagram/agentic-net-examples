using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                throw new Exception("No shapes found on the first page.");
            }

            // Record the original number of geometries
            int originalGeomCount = shape.Geoms.Count;

            // ----- Begin geometry modification -----
            // Create a new geometry (Geom) and add a simple line segment
            Geom newGeom = new Geom();

            // Start point (MoveTo)
            MoveTo move = new MoveTo();
            move.X.Value = 0.0;
            move.Y.Value = 0.0;
            newGeom.CoordinateCol.Add(move);

            // End point (LineTo)
            LineTo line = new LineTo();
            line.X.Value = 2.0;
            line.Y.Value = 2.0;
            newGeom.CoordinateCol.Add(line);

            // Append the new geometry to the shape's geometry collection
            shape.Geoms.Add(newGeom);
            // ----- End geometry modification -----

            // Expected geometry count after adding one geometry
            int expectedGeomCount = originalGeomCount + 1;

            // Validate the geometry count
            if (shape.Geoms.Count != expectedGeomCount)
            {
                throw new Exception($"Geometry count mismatch. Expected: {expectedGeomCount}, Actual: {shape.Geoms.Count}");
            }
            else
            {
                Console.WriteLine($"Geometry count validation passed. Count = {shape.Geoms.Count}");
            }

            // Optionally, save the modified diagram
            // string outputPath = "output.vsdx";
            // diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
