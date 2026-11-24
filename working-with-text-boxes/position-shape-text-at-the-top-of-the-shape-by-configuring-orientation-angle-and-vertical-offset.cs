using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
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

            // ------------------------------------------------------------
            // Position the shape's text at the top of the shape.
            // ------------------------------------------------------------
            // Set vertical offset: locate text block at the top (Y = 0).
            targetShape.TextXForm.TxtLocPinY.Value = 0;

            // Align the text block's pin Y to the shape's height so the text starts at the top.
            targetShape.TextXForm.TxtPinY.Value = targetShape.XForm.Height.Value;

            // Set text orientation angle to 0 degrees (no rotation). The API expects radians.
            double angleDeg = 0;
            targetShape.TextXForm.TxtAngle.Value = (Math.PI / 180) * angleDeg;

            // ------------------------------------------------------------
            // Save the modified diagram.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}