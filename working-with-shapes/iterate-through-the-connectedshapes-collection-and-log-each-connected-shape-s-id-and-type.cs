using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve connected shape IDs for the current shape
                        long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                        // If there are no connections, continue to next shape
                        if (connectedIds == null || connectedIds.Length == 0)
                            continue;

                        // Log each connected shape's ID and type
                        foreach (long connId in connectedIds)
                        {
                            // Retrieve the connected shape instance
                            Shape connectedShape = page.Shapes.GetShape(connId);

                            // Guard against null (should not happen, but be safe)
                            if (connectedShape == null)
                                continue;

                            // Output the connected shape information
                            Console.WriteLine($"Shape ID {shape.ID} is connected to Shape ID {connectedShape.ID}, Type: {connectedShape.Type}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}