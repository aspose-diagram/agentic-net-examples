using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace the path with your file)
            Diagram diagram = new Diagram("input.vsdx");

            // Select a shape to examine – here we take the first shape on the first page
            Shape shape = diagram.Pages[0].Shapes[0];

            // Retrieve IDs of all shapes connected to the selected shape (both incoming and outgoing)
            long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

            // Iterate through each connected shape ID
            foreach (long id in connectedIds)
            {
                // Locate the actual Shape object by its ID across all pages
                Shape connectedShape = FindShapeById(diagram, id);

                // Log the ID and the shape type (if found)
                if (connectedShape != null)
                {
                    Console.WriteLine($"Connected Shape ID: {id}, Type: {connectedShape.Type}");
                }
                else
                {
                    Console.WriteLine($"Connected Shape ID: {id}, Type: Not found");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to search for a shape with a given ID in the entire diagram
    static Shape FindShapeById(Diagram diagram, long id)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape s in page.Shapes)
            {
                if (s.ID == id)
                    return s;
            }
        }
        return null;
    }
}
