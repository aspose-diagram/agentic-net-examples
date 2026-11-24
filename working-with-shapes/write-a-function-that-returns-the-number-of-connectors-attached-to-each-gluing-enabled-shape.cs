using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio diagram file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Build a lookup of connector IDs for quick checking (optional).
                // Connectors are 1‑D shapes (shape.OneD == true).
                var connectorIds = new System.Collections.Generic.HashSet<long>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD == true) // Connectors are 1‑D shapes.
                    {
                        connectorIds.Add(shape.ID);
                    }
                }

                // For each non‑connector shape, count how many connectors are glued to it.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip connectors themselves.
                    if (shape.OneD == true)
                        continue;

                    // Count connectors attached via the Connects collection.
                    int attachedConnectorCount = 0;
                    foreach (Connect connect in page.Connects)
                    {
                        // A connector is considered attached if either end references the shape's ID.
                        if (connect.FromSheet == shape.ID || connect.ToSheet == shape.ID)
                        {
                            // Ensure the connecting shape is actually a connector.
                            if (connectorIds.Contains(connect.FromSheet) || connectorIds.Contains(connect.ToSheet))
                            {
                                attachedConnectorCount++;
                            }
                        }
                    }

                    // Output the result for the current shape.
                    string shapeName = string.IsNullOrEmpty(shape.Name) ? "(no name)" : shape.Name;
                    Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shapeName}, Attached Connectors: {attachedConnectorCount}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}