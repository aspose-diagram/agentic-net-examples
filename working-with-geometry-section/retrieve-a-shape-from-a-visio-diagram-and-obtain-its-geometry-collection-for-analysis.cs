using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with actual file path)
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Access the first page (you can also retrieve by name)
            Page page = diagram.Pages[0];

            // Example: retrieve a shape by its ID (replace with actual ID)
            int shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);

            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Obtain the geometry collection of the shape
            GeomCollection geoms = shape.Geoms;
            Console.WriteLine($"Shape ID {shapeId} contains {geoms.Count} geometry sections.");

            // Iterate through each geometry section
            for (int i = 0; i < geoms.Count; i++)
            {
                Geom geom = (Geom)geoms[i];
                Console.WriteLine($"Geometry {i} has {geom.CoordinateCol.Count} coordinate entries.");

                // Iterate through each coordinate (MoveTo, LineTo, etc.)
                for (int j = 0; j < geom.CoordinateCol.Count; j++)
                {
                    Aspose.Diagram.Coordinate coord = (Aspose.Diagram.Coordinate)geom.CoordinateCol[j];
                    Console.WriteLine($"  Coordinate {j} type: {coord.GetType().Name}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
