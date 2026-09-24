using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public class DiagramConnectorAnalyzer
{
    /// <summary>
    /// Returns a dictionary where each key is the ID of a shape that has dynamic glue enabled,
    /// and the value is the number of 1‑D connector shapes attached (glued) to it.
    /// </summary>
    /// <param name="filePath">Path to the Visio file to analyze.</param>
    /// <returns>Dictionary mapping shape IDs to connector counts.</returns>
    public static Dictionary<long, int> GetConnectorCounts(string filePath)
    {
        // Load the diagram from the specified file.
        Diagram diagram = new Diagram(filePath);

        // Prepare the result container.
        Dictionary<long, int> shapeConnectorCounts = new Dictionary<long, int>();

        // Iterate through all pages in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Check if the shape allows dynamic glue.
                // GlueTypeValue.AllowDynamicGlue indicates that the shape can be glued to connectors.
                if (shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                {
                    // Retrieve all 1‑D (connector) shapes glued to this shape.
                    // The method returns an array of connector shape IDs.
                    long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                    // If the method returns null, treat it as zero connectors.
                    int connectorCount = gluedConnectorIds != null ? gluedConnectorIds.Length : 0;

                    // Store the count using the shape's unique ID.
                    shapeConnectorCounts[shape.ID] = connectorCount;
                }
            }
        }

        return shapeConnectorCounts;
    }

    // Example entry point demonstrating usage.
    public static void Main()
    {
        try
        {

            // Replace with the actual path to your Visio file.
            string visioPath = "example.vsdx";

            try
            {
                Dictionary<long, int> result = GetConnectorCounts(visioPath);

                Console.WriteLine("Connector counts for gluing‑enabled shapes:");
                foreach (KeyValuePair<long, int> kvp in result)
                {
                    Console.WriteLine($"Shape ID {kvp.Key}: {kvp.Value} connector(s)");
                }
            }
            catch (Exception ex)
            {
                // Propagate any errors (e.g., file not found, load failure).
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
