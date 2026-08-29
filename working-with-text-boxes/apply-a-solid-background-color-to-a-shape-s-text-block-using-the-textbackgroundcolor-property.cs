using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page of the diagram
            Page page = diagram.Pages[0];

            // Find the first shape on the page (skip deleted shapes)
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False) // ensure the shape is not marked as deleted
                {
                    targetShape = shp;
                    break;
                }
            }

            // If no suitable shape was found, report and exit
            if (targetShape == null)
            {
                Console.Error.WriteLine("No non‑deleted shape found on the first page.");
                return;
            }

            // Apply a solid background color to the shape's text block
            // Use the RGB() string format for the TextBkgnd property
            targetShape.TextBlock.TextBkgnd.Ufe.F = "RGB(255,204,0)"; // amber background

            // Set background transparency to fully opaque (0 % transparent)
            targetShape.TextBlock.TextBkgndTrans.Value = 0;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}