using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments: expect diagram file path and connector shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path> <connector-shape-id>");
            return;
        }

        string diagramPath = args[0];
        string connectorIdArg = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse connector shape ID.
        if (!long.TryParse(connectorIdArg, out long connectorId))
        {
            Console.Error.WriteLine($"Error: Invalid connector shape ID '{connectorIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the connector shape by its ID.
            Shape connectorShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is a 1-D connector and matches the requested ID.
                    if (shape.OneD && shape.ID == connectorId)
                    {
                        connectorShape = shape;
                        break;
                    }
                }
                if (connectorShape != null)
                    break;
            }

            if (connectorShape == null)
            {
                Console.Error.WriteLine($"Error: Connector with ID {connectorId} not found in the diagram.");
                return;
            }

            // Read the current line jump style from the connector's layout.
            // The value is of type ConLineJumpStyleValue enum.
            var jumpStyle = connectorShape.Layout.ConLineJumpStyle.Value;

            // Log the retrieved line jump style.
            Console.WriteLine($"Connector ID {connectorId} line jump style: {jumpStyle}");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}