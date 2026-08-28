using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "sample.vsdx";

            // Load the diagram (ensure the file exists at the specified location)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Define the ID of the shape you want to retrieve
                int shapeId = 1; // Change this to the actual shape ID you need

                // Retrieve the shape from the page's shape collection
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} was not found on page '{page.Name}'.");
                }

                // Output basic shape information
                Console.WriteLine($"Shape ID: {shape.ID}");
                Console.WriteLine($"Shape Name: {shape.Name}");
                Console.WriteLine($"Number of geometry sections (Geoms): {shape.Geoms.Count}");

                // Iterate through each geometry (Geom) in the shape
                for (int i = 0; i < shape.Geoms.Count; i++)
                {
                    Aspose.Diagram.Geom geom = (Aspose.Diagram.Geom)shape.Geoms[i];
                    Console.WriteLine($"  Geom {i} contains {geom.CoordinateCol.Count} coordinate entries.");

                    // Iterate through each coordinate entry within the geometry
                    for (int j = 0; j < geom.CoordinateCol.Count; j++)
                    {
                        // Each entry can be a MoveTo, LineTo, ArcTo, etc.
                        object coordinate = geom.CoordinateCol[j];
                        Console.WriteLine($"    Coordinate {j}: Type = {coordinate.GetType().Name}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
