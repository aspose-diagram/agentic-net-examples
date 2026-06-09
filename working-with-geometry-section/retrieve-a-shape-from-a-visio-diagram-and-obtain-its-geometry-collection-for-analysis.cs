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
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Locate the shape you want to analyze.
            // This example searches by the universal name "MyShape".
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.NameU != null && shp.NameU.Equals("MyShape", StringComparison.OrdinalIgnoreCase))
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Retrieve the Geometry collection of the shape
            foreach (Geom geom in targetShape.Geoms)
            {
                Console.WriteLine($"Geometry section contains {geom.CoordinateCol.Count} coordinate entries.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
