using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a diagram file path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Counters for locked properties
                int lockedWidthCount = 0;
                int lockedHeightCount = 0;
                int lockedRotationCount = 0;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Protection object exists before accessing its members
                        if (shape.Protection != null)
                        {
                            // Check if the shape's width is locked
                            if (shape.Protection.LockWidth != null && shape.Protection.LockWidth.Value == BOOL.True)
                            {
                                lockedWidthCount++;
                            }

                            // Check if the shape's height is locked
                            if (shape.Protection.LockHeight != null && shape.Protection.LockHeight.Value == BOOL.True)
                            {
                                lockedHeightCount++;
                            }

                            // Check if the shape's rotation is locked
                            if (shape.Protection.LockRotate != null && shape.Protection.LockRotate.Value == BOOL.True)
                            {
                                lockedRotationCount++;
                            }
                        }
                    }
                }

                // Build JSON summary
                string json = $"{{\"LockedWidthCount\":{lockedWidthCount},\"LockedHeightCount\":{lockedHeightCount},\"LockedRotationCount\":{lockedRotationCount}}}";

                // Output the JSON to console
                Console.WriteLine(json);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}