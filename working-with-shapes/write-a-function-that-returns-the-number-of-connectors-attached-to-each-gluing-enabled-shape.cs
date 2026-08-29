using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Returns a dictionary where each key is the ID of a shape that has gluing enabled
    /// (i.e., has at least one connector glued to it) and the value is the number of
    /// connectors (1‑D shapes) attached to that shape.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram Diagram instance.</param>
    /// <returns>Dictionary mapping shape ID to count of attached connectors.</returns>
    public static Dictionary<long, int> GetConnectorCountsPerGluedShape(Diagram diagram)
    {
        var connectorCounts = new Dictionary<long, int>();

        // Iterate through all pages and shapes in the diagram
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve IDs of all 1‑D shapes (connectors) glued to this shape.
                // Using GluedShapesAll1D returns both incoming and outgoing connectors.
                long[] gluedConnectorIds = shape.GluedShapes(
                    GluedShapesFlags.GluedShapesAll1D,   // flag to get all 1‑D glued shapes
                    null,                               // no category filter
                    null);                              // no additional shape filter

                // If there are any glued connectors, record the count.
                if (gluedConnectorIds != null && gluedConnectorIds.Length > 0)
                {
                    connectorCounts[shape.ID] = gluedConnectorIds.Length;
                }
            }
        }

        return connectorCounts;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
