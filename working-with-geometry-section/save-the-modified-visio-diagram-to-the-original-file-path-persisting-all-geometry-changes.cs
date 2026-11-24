using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard against missing input file.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found – {filePath}");
            return;
        }

        // Load the Visio diagram inside a using block to ensure proper disposal.
        try
        {
            using (Diagram diagram = new Diagram(filePath))
            {
                // Example geometry modification:
                // Iterate pages and shapes, find the first non‑deleted shape,
                // and shift its position by (1,1) units.
                bool shapeModified = false;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply a simple translation to the shape's PinX and PinY.
                        shape.XForm.PinX.Value += 1.0;
                        shape.XForm.PinY.Value += 1.0;

                        shapeModified = true;
                        break; // Modify only the first eligible shape.
                    }

                    if (shapeModified)
                        break;
                }

                // Save the modified diagram back to the original file path,
                // persisting all geometry changes.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading, modification, or saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}