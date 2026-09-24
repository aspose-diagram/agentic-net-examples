using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – adjust as needed.
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path.
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram has no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page.
            Shape? targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                // Skip shapes that are marked as deleted.
                if (s.Del == BOOL.False)
                {
                    targetShape = s;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No non‑deleted shape found on the first page.");
                return;
            }

            // Set the 3‑D rotation type to Parallel (Fixed is not available in this version).
            targetShape.ThreeDFormat.RotationType.Value = RotationTypeValue.Parallel; // Updated to a valid enum member

            // Apply a 90‑degree rotation around the Y‑axis.
            targetShape.ThreeDFormat.RotationYAngle.Value = 90;

            // Save the modified diagram to the output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}