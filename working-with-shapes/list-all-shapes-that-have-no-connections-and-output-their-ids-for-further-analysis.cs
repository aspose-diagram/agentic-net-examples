using System.IO;
using System;
using Aspose.Diagram;

class ListUnconnectedShapes
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Get IDs of shapes connected to the current shape (both incoming and outgoing)
                    long[] connected = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                    // Get IDs of shapes that this shape depends on
                    long[] dependsOn = shape.DependsOnShapes();

                    // Get IDs of shapes glued to this shape (all 1‑D and 2‑D glued shapes)
                    long[] glued = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                    // If all three arrays are empty, the shape has no connections
                    if ((connected == null || connected.Length == 0) &&
                        (dependsOn == null || dependsOn.Length == 0) &&
                        (glued == null || glued.Length == 0))
                    {
                        // Output the shape ID for further analysis
                        Console.WriteLine($"Unconnected Shape ID: {shape.ID}");
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
