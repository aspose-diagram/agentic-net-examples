using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve IDs of shapes connected to the current shape
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);
                    if (connectedIds == null) continue;

                    // Log each connected shape's ID and type
                    foreach (long id in connectedIds)
                    {
                        Shape connectedShape = page.Shapes.GetShape(id);
                        if (connectedShape != null)
                        {
                            Console.WriteLine($"Connected Shape ID: {connectedShape.ID}, Type: {connectedShape.Type}");
                        }
                    }
                }
            }

            // Save the diagram (unchanged) to demonstrate proper lifecycle usage
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
