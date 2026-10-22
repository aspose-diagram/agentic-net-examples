using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page.
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page.
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No visible shape found on the first page.");
                    return;
                }

                // -----------------------------------------------------------------
                // Set 3D rotation properties.
                // The API does not expose a 'Fixed' RotationType; the closest
                // available option is 'Parallel'. This substitution is documented
                // here as per the guidelines.
                // -----------------------------------------------------------------
                targetShape.ThreeDFormat.RotationType.Value = RotationTypeValue.Parallel; // Fixed not available

                // Apply a 90‑degree rotation around the Y‑axis.
                targetShape.ThreeDFormat.RotationYAngle.Value = 90;

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}