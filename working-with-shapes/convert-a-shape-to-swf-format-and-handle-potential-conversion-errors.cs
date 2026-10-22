using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeNameU>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Search for the shape with the specified NameU across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // Ensure the Shapes collection is not null.
                if (page.Shapes == null) continue;

                // Iterate through all shapes on the page.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];
                    // Guard against null shape reference.
                    if (shape == null) continue;

                    // Compare the universal name of the shape.
                    if (string.Equals(shape.NameU, targetShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with NameU '{targetShapeName}' not found in the diagram.");
                return;
            }

            // NOTE:
            // Aspose.Diagram does not provide a direct Shape.ToSwf method.
            // The closest supported operation is exporting the entire diagram to SWF.
            // Therefore, we will export the whole diagram to SWF format.
            // If per‑shape SWF export becomes available in future versions,
            // replace the following block with the appropriate Shape conversion call.

            string outputSwfPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                $"{Path.GetFileNameWithoutExtension(diagramPath)}_full.swf");

            // Export the entire diagram to SWF.
            diagram.Save(outputSwfPath, SaveFileFormat.Swf);
            Console.WriteLine($"Diagram exported to SWF successfully: {outputSwfPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}