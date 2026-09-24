using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and connector identifier (ID or NameU)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ConnectorLocator <diagramPath> <connectorIdOrName>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        string identifier = args[1];

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Flag to indicate if the connector was found
            bool found = false;

            // Try to parse the identifier as a numeric ID
            bool isNumeric = long.TryParse(identifier, out long targetId);

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Identify connector shapes: they are 1‑D and usually based on the "Dynamic connector" master
                if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                {
                    // Match by ID
                    if (isNumeric && shape.ID == targetId)
                    {
                        PrintConnectorInfo(page, shape);
                        found = true;
                        break;
                    }

                    // Match by universal name (NameU)
                    if (!isNumeric && string.Equals(shape.NameU, identifier, StringComparison.OrdinalIgnoreCase))
                    {
                        PrintConnectorInfo(page, shape);
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"Connector with identifier '{identifier}' was not found on page '{page.Name}'.");
            }
        }
        catch (Exception ex)
        {
            // Report any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to output connector details, including source and target shape IDs
    private static void PrintConnectorInfo(Page page, Shape connector)
    {
        Console.WriteLine("Connector found:");
        Console.WriteLine($"  ID       : {connector.ID}");
        Console.WriteLine($"  NameU    : {connector.NameU}");
        Console.WriteLine($"  Master   : {connector.Master?.Name}");
        Console.WriteLine($"  Position : PinX={connector.XForm.PinX.Value}, PinY={connector.XForm.PinY.Value}");

        // Retrieve the two connection entries that reference this connector
        long fromShapeId = 0;
        long toShapeId = 0;
        int foundCount = 0;

        foreach (Connect conn in page.Connects)
        {
            // Each Connect where ToSheet equals the connector ID represents one end of the connector
            if (conn.ToSheet == connector.ID)
            {
                if (foundCount == 0)
                    fromShapeId = conn.FromSheet; // First end (source shape)
                else if (foundCount == 1)
                    toShapeId = conn.FromSheet;   // Second end (target shape)

                foundCount++;
                if (foundCount >= 2) break; // We only need the two ends
            }
        }

        Console.WriteLine($"  FromShape ID: {fromShapeId}");
        Console.WriteLine($"  ToShape ID  : {toShapeId}");
    }
}