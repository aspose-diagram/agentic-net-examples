using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with your actual file path)
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Get a shape to examine (here we take the first shape on the page)
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                targetShape = s;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shapes found on the first page.");
                return;
            }

            // Retrieve IDs of all shapes connected to the target shape
            long[] connectedIds = targetShape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

            Console.WriteLine($"Shape ID {targetShape.ID} is connected to {connectedIds.Length} shape(s):");
            foreach (long id in connectedIds)
            {
                Console.WriteLine($"- Connected Shape ID: {id}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
