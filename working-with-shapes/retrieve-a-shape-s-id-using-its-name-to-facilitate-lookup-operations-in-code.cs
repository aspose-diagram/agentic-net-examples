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
            string diagramPath = "input.vsdx";

            // Name of the shape whose ID we want to retrieve
            string targetShapeName = "MyShape";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Find the shape ID by name
            long shapeId = FindShapeIdByName(diagram, targetShapeName);

            // Output the result
            if (shapeId != -1)
            {
                Console.WriteLine($"Shape \"{targetShapeName}\" has ID: {shapeId}");
            }
            else
            {
                Console.WriteLine($"Shape \"{targetShapeName}\" was not found in the diagram.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Searches all pages and shapes for a shape with the specified name.
    // Returns the shape's ID if found; otherwise returns -1.
    static long FindShapeIdByName(Diagram diagram, string shapeName)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Compare both NameU (universal) and Name (local) case‑insensitively
                if (string.Equals(shape.NameU, shapeName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(shape.Name, shapeName, StringComparison.OrdinalIgnoreCase))
                {
                    return shape.ID;
                }
            }
        }
        return -1;
    }
}
