using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-visio-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name}");

                    // Iterate through all shapes on the current page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape has dynamic glue enabled.
                        if (shape.Misc != null && shape.Misc.GlueType != null &&
                            shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                        {
                            // Retrieve IDs of all 1‑D (connector) shapes glued to this shape.
                            long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                            // If there are any attached connectors, output the information.
                            if (gluedConnectorIds != null && gluedConnectorIds.Length > 0)
                            {
                                string shapeName = shape.NameU ?? "(unknown)";
                                Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shapeName}");
                                Console.WriteLine("    Attached Connectors:");

                                foreach (long connectorId in gluedConnectorIds)
                                {
                                    // Retrieve the connector shape to obtain its name.
                                    Aspose.Diagram.Shape connectorShape = page.Shapes.GetShape(connectorId);
                                    string connectorName = connectorShape != null ? connectorShape.NameU ?? "(unknown)" : "(missing)";
                                    Console.WriteLine($"      Connector ID: {connectorId}, Name: {connectorName}");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}