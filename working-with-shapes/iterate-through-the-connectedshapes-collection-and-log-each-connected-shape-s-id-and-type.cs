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
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API guidelines
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

                        // Retrieve all connected shape IDs for the current shape
                        long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                        // If there are no connections, continue to next shape
                        if (connectedIds == null || connectedIds.Length == 0)
                            continue;

                        // Log each connected shape's ID and type
                        foreach (long connId in connectedIds)
                        {
                            // Retrieve the connected shape using its ID (cast to int as required by GetShape)
                            Shape connectedShape = page.Shapes.GetShape((int)connId);
                            if (connectedShape == null)
                                continue; // Safety check

                            Console.WriteLine($"Shape ID {shape.ID} is connected to Shape ID {connectedShape.ID}, Type: {connectedShape.Type}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}