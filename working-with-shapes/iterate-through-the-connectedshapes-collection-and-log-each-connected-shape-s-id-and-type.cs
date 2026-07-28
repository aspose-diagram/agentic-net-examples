using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Choose a page (first page in this example)
            Page page = diagram.Pages[0];

            // Choose a shape to inspect (first shape in this example)
            Shape shape = page.Shapes[0];

            // Retrieve IDs of all shapes connected to the selected shape
            long[] connectedShapeIds = shape.ConnectedShapes(
                ConnectedShapesFlags.ConnectedShapesAllNodes,   // include incoming and outgoing connections
                null                                            // no category filter
            );

            // Iterate through each connected shape ID
            foreach (long id in connectedShapeIds)
            {
                // Get the actual Shape object using its ID
                Shape connectedShape = page.Shapes.GetShape(id);

                // Log the ID and the shape type (e.g., "Shape", "Connector", etc.)
                Console.WriteLine($"Connected Shape ID: {connectedShape.ID}, Type: {connectedShape.Type}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
