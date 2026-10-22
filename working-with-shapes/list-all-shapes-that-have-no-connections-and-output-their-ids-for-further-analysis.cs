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
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve IDs of shapes connected to the current shape.
                    // Using ConnectedShapesAllNodes to consider all connection types.
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                    // If there are no connections, output the shape's ID.
                    bool hasNoConnections = connectedIds == null || connectedIds.Length == 0;
                    if (hasNoConnections)
                    {
                        Console.WriteLine($"Shape ID with no connections: {shape.ID}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}