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

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            bool foundIsolated = false;

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve IDs of shapes connected to the current shape
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                    // If there are no connections, flag the shape
                    if (connectedIds == null || connectedIds.Length == 0)
                    {
                        foundIsolated = true;

                        // Add a comment to the shape for review (if the comment cell exists)
                        if (shape.Misc != null && shape.Misc.Comment != null)
                        {
                            shape.Misc.Comment.Value = "Isolated shape - review";
                        }

                        Console.WriteLine($"Isolated shape ID {shape.ID} on page '{page.Name}' flagged for review.");
                    }
                }
            }

            // Save the modified diagram with a new file name
            string outputDir = Path.GetDirectoryName(inputPath) ?? "";
            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_flagged.vsdx";
            string outputPath = Path.Combine(outputDir, outputFileName);

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Processing complete. Modified diagram saved to: {outputPath}");

            if (!foundIsolated)
            {
                Console.WriteLine("No isolated shapes were found.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}