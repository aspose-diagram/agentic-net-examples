using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the Visio diagram file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagramFilePath>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Initialize counters for locked dimensions and rotation.
            int lockedWidthCount = 0;
            int lockedHeightCount = 0;
            int lockedRotationCount = 0;

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Protection section before accessing lock cells.
                    if (shape.Protection != null)
                    {
                        // Increment counter if the width lock is set to TRUE.
                        if (shape.Protection.LockWidth.Value == BOOL.True)
                            lockedWidthCount++;

                        // Increment counter if the height lock is set to TRUE.
                        if (shape.Protection.LockHeight.Value == BOOL.True)
                            lockedHeightCount++;

                        // Increment counter if the rotation lock is set to TRUE.
                        if (shape.Protection.LockRotate.Value == BOOL.True)
                            lockedRotationCount++;
                    }
                }
            }

            // Build a simple JSON summary of the lock counts.
            string json = $"{{\"LockedWidth\":{lockedWidthCount},\"LockedHeight\":{lockedHeightCount},\"LockedRotation\":{lockedRotationCount}}}";

            // Output the JSON to the console.
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}