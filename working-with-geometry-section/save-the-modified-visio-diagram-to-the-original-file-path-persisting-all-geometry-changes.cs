using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the path to the Visio file as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found at '{inputPath}'.");
            return;
        }

        // Load the diagram inside a using block to ensure proper disposal.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Example geometry modification: shift the first non-deleted shape by (1,1) units.
                bool shapeMoved = false;

                // Iterate over pages with explicit type declarations (avoid 'var' as per guidelines).
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply a simple translation to the shape's position.
                        shape.XForm.PinX.Value += 1.0;
                        shape.XForm.PinY.Value += 1.0;

                        shapeMoved = true;
                        break; // Modify only the first eligible shape.
                    }

                    if (shapeMoved)
                        break; // Exit after modifying one shape.
                }

                // Save the diagram back to the original file path, preserving all changes.
                // Use the appropriate SaveFileFormat enum value (Vsdx for .vsdx files).
                diagram.Save(inputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading, modification, or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}