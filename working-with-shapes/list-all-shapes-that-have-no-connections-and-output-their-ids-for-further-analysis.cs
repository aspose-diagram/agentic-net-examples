using System.IO;
using System;
using Aspose.Diagram;

class ListUnconnectedShapes
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // Replace "input.vsdx" with the path to your Visio file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve IDs of shapes that are connected to the current shape
                    // Using ConnectedShapesAllNodes to consider both incoming and outgoing connections
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                    // If no connections are found, output the shape's ID
                    if (connectedIds == null || connectedIds.Length == 0)
                    {
                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}");
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
