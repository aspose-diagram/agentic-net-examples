using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

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

        string filePath = args[0];

        // Guard against missing input file.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve shapes connected via connectors.
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                    // Retrieve shapes glued to this shape (1‑D connectors).
                    long[] gluedIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                    bool hasConnections = (connectedIds != null && connectedIds.Length > 0) ||
                                          (gluedIds != null && gluedIds.Length > 0);

                    // If the shape has no connections, output its ID.
                    if (!hasConnections)
                    {
                        Console.WriteLine($"Shape ID: {shape.ID}");
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