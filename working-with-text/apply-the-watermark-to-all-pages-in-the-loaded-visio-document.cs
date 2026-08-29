using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file and output file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: WatermarkExample <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add a full‑page watermark text shape
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a text shape that covers the entire page.
                // Using positional arguments to match the overload signature.
                page.AddText(
                    0,               // pinX: left edge
                    0,               // pinY: bottom edge
                    pageWidth,       // width: full page width
                    pageHeight,      // height: full page height
                    "CONFIDENTIAL", // watermark text
                    "Arial",         // font name
                    "#808080",       // light gray font color (hex)
                    0.5);            // font size in inches (~36 pt)
            }

            // Save the modified diagram, preserving the original format (VSDX)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Watermark applied and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}