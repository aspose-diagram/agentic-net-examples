using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and connector shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <connectorShapeId>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Parse the connector shape ID.
        if (!long.TryParse(args[1], out long connectorId))
        {
            Console.Error.WriteLine($"Invalid connector shape ID: {args[1]}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool connectorFound = false;

                // Iterate through all pages to locate the connector by its ID.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the shape by ID; GetShape returns null if not found.
                    Shape shape = page.Shapes.GetShape(connectorId);
                    if (shape == null)
                        continue;

                    // Ensure the shape is a 1‑D connector (OneD is a native bool).
                    if (!shape.OneD)
                        continue;

                    // Read the current line jump style from the layout sub‑object.
                    ConLineJumpStyleValue jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                    // Log the retrieved line jump style.
                    Console.WriteLine($"Connector ID {connectorId} Line Jump Style: {jumpStyle}");

                    connectorFound = true;
                    break;
                }

                if (!connectorFound)
                {
                    Console.Error.WriteLine($"Connector with ID {connectorId} not found in the diagram.");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}