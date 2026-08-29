using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // ID of the shape whose connections we want to inspect
            long targetShapeId = 1; // replace with the actual shape ID

            // Retrieve the shape from the first page (adjust page index if needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeId);

            // Get IDs of all shapes connected to the target shape (incoming and outgoing)
            long[] connectedShapeIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

            // Output the connected shape IDs
            Console.WriteLine("Connected shape IDs:");
            foreach (long id in connectedShapeIds)
            {
                Console.WriteLine(id);
            }

            // Optionally save the diagram after any modifications
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
