using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public class DiagramHelper
{
    // Returns a dictionary where the key is the shape ID of a gluing‑enabled shape
    // and the value is the number of connector shapes attached to it.
    public static Dictionary<long, int> GetConnectorCounts(string diagramPath)
    {
        // Load the Visio diagram from the specified file.
        Diagram diagram = new Diagram(diagramPath);

        // Prepare the result container.
        Dictionary<long, int> connectorCounts = new Dictionary<long, int>();

        // Iterate through every page in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through every shape on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Determine if the shape allows dynamic glue (gluing‑enabled).
                // GlueTypeValue.AllowDynamicGlue indicates that connectors can be glued to the shape.
                if (shape.Misc.GlueType != null &&
                    shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                {
                    // Retrieve all shape IDs that are connected to this shape.
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                    int connectorCount = 0;

                    // Examine each connected shape to see if it is a connector (1‑D shape).
                    foreach (long connectedId in connectedIds)
                    {
                        Shape connectedShape = FindShapeById(diagram, connectedId);
                        if (connectedShape != null && connectedShape.OneD)
                        {
                            connectorCount++;
                        }
                    }

                    // Store the count for this gluing‑enabled shape.
                    connectorCounts[shape.ID] = connectorCount;
                }
            }
        }

        return connectorCounts;
    }

    // Helper method to locate a shape by its ID across all pages.
    private static Shape FindShapeById(Diagram diagram, long shapeId)
    {
        foreach (Page pg in diagram.Pages)
        {
            try
            {
                // GetShape throws if the ID is not present on the page.
                return pg.Shapes.GetShape(shapeId);
            }
            catch
            {
                // Continue searching other pages.
            }
        }
        return null;
    }

    // Example entry point demonstrating usage.
    public static void Main()
    {
        string filePath = "example.vsdx"; // Replace with your diagram file path.

        try
        {
            Dictionary<long, int> counts = GetConnectorCounts(filePath);

            foreach (var kvp in counts)
            {
                Console.WriteLine($"Shape ID {kvp.Key} has {kvp.Value} attached connector(s).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error processing diagram: " + ex.Message);
        }
    }
}
