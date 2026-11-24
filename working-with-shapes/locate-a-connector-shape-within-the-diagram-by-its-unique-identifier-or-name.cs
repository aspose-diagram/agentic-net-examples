using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expected <diagramPath> <connectorIdOrName>
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <connectorIdOrName>");
            return;
        }

        string diagramPath = args[0];
        string identifier = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Determine if the identifier is a numeric ID or a name
            bool isIdSearch = long.TryParse(identifier, out long targetId);
            string targetName = isIdSearch ? null : identifier;

            bool found = false;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify connector shapes (OneD shapes)
                    if (!shape.OneD)
                        continue;

                    // Check for match by ID
                    if (isIdSearch && shape.ID == targetId)
                    {
                        PrintConnectorInfo(shape);
                        found = true;
                        break;
                    }

                    // Check for match by NameU (shape name)
                    if (!isIdSearch && shape.NameU == targetName)
                    {
                        PrintConnectorInfo(shape);
                        found = true;
                        break;
                    }
                }

                if (found)
                    break;
            }

            if (!found)
            {
                Console.WriteLine(isIdSearch
                    ? $"Connector with ID {targetId} not found."
                    : $"Connector with name \"{targetName}\" not found.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to output connector details
    private static void PrintConnectorInfo(Shape connector)
    {
        Console.WriteLine("Connector found:");
        Console.WriteLine($"  ID       : {connector.ID}");
        Console.WriteLine($"  NameU    : {connector.NameU}");
        // Master name may give additional context (e.g., "Dynamic connector")
        string masterName = connector.Master?.Name ?? "N/A";
        Console.WriteLine($"  Master   : {masterName}");
    }
}