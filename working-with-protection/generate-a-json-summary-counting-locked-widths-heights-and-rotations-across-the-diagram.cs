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
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Counters for locked properties
        int lockedWidthCount = 0;
        int lockedHeightCount = 0;
        int lockedRotateCount = 0;

        try
        {
            // Load the Visio diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape's width is locked
                        if (shape.Protection.LockWidth.Value == BOOL.True)
                            lockedWidthCount++;

                        // Check if the shape's height is locked
                        if (shape.Protection.LockHeight.Value == BOOL.True)
                            lockedHeightCount++;

                        // Check if the shape's rotation is locked
                        if (shape.Protection.LockRotate.Value == BOOL.True)
                            lockedRotateCount++;
                    }
                }
            }

            // Build JSON summary
            string json = $"{{\"lockedWidth\":{lockedWidthCount},\"lockedHeight\":{lockedHeightCount},\"lockedRotate\":{lockedRotateCount}}}";

            // Output the JSON to the console
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}