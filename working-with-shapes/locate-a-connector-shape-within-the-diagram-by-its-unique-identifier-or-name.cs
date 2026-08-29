using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    // Entry point of the console application
    public static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string diagramPath = "sample.vsdx";

            // Example identifiers to locate the connector
            long connectorId = 12345;               // Unique shape ID (if known)
            string connectorName = "Connector 1";   // Universal name or name (if known)

            // Load the diagram from file
            Diagram diagram = new Diagram(diagramPath);

            // Attempt to find the connector by ID or name
            Shape connectorShape = FindConnectorShape(diagram, connectorId, connectorName);

            // Output the result
            Console.WriteLine($"Connector found: ID = {connectorShape.ID}, NameU = {connectorShape.NameU}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    /// <summary>
    /// Searches all pages for a connector shape that matches the given ID or name.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
    /// <param name="targetId">The unique shape ID to match (use 0 if not searching by ID).</param>
    /// <param name="targetName">The shape name (Name or NameU) to match (null or empty if not searching by name).</param>
    /// <returns>The matching connector Shape.</returns>
    /// <exception cref="Exception">Thrown when no matching connector is found.</exception>
    private static Shape FindConnectorShape(Diagram diagram, long targetId, string targetName)
    {
        // Iterate through each page in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Iterate through each shape on the current page
            foreach (Shape shape in page.Shapes)
            {
                // Connectors are 1‑D shapes; skip non‑connector shapes
                if (!shape.OneD)
                {
                    continue;
                }

                // Match by ID if a non‑zero ID is provided
                if (targetId != 0 && shape.ID == targetId)
                {
                    return shape;
                }

                // Match by name (Name or universal NameU) if a name is provided
                if (!string.IsNullOrEmpty(targetName) &&
                    (shape.Name == targetName || shape.NameU == targetName))
                {
                    return shape;
                }
            }
        }

        // No matching connector was found
        throw new Exception("Connector shape not found with the specified identifier or name.");
    }
}
