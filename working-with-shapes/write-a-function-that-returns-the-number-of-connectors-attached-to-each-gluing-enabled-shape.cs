using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public static class DiagramHelper
{
    /// <summary>
    /// Returns a dictionary where each key is the ID of a shape that has dynamic glue enabled,
    /// and the value is the number of connector shapes attached to that shape.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance to analyze.</param>
    /// <returns>Dictionary mapping shape IDs to connector counts.</returns>
    public static Dictionary<long, int> GetConnectorCounts(Diagram diagram)
    {
        var result = new Dictionary<long, int>();

        // Iterate through all pages in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Pre‑compute the set of connector shape IDs on this page (OneD shapes)
            var connectorIds = new HashSet<long>();
            foreach (Shape shape in page.Shapes)
            {
                if (shape.OneD) // 1‑D shapes are connectors
                {
                    connectorIds.Add(shape.ID);
                }
            }

            // Examine each non‑connector shape
            foreach (Shape shape in page.Shapes)
            {
                // Skip connector shapes themselves
                if (shape.OneD)
                    continue;

                // Determine if the shape allows dynamic glue
                // GlueTypeValue.AllowDynamicGlue indicates gluing is enabled
                var glueType = shape.Misc.GlueType.Value;
                if (glueType != GlueTypeValue.AllowDynamicGlue)
                    continue;

                int attachedConnectorCount = 0;

                // Count connections where the other end is a connector shape
                foreach (Connect conn in page.Connects)
                {
                    if (conn.FromSheet == shape.ID && connectorIds.Contains(conn.ToSheet))
                    {
                        attachedConnectorCount++;
                    }
                    else if (conn.ToSheet == shape.ID && connectorIds.Contains(conn.FromSheet))
                    {
                        attachedConnectorCount++;
                    }
                }

                result[shape.ID] = attachedConnectorCount;
            }
        }

        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
