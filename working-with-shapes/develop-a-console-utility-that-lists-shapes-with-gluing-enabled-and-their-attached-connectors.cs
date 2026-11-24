using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name}");

                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Skip connector shapes (1‑D shapes).
                        if (shape.OneD)
                            continue;

                        // Retrieve IDs of connectors glued to this shape.
                        long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                        // If there are any glued connectors, output the shape information.
                        if (gluedConnectorIds != null && gluedConnectorIds.Length > 0)
                        {
                            string connectorList = string.Join(", ", gluedConnectorIds);
                            Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}, Attached Connectors: {connectorList}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}