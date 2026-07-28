using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (change as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output_watermarked.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add a full‑page watermark
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a text shape that covers the entire page.
                // Positional arguments are used to avoid named‑parameter mismatches.
                page.AddText(
                    0,                 // pinX (left)
                    0,                 // pinY (bottom)
                    pageWidth,         // width (full page width)
                    pageHeight,        // height (full page height)
                    "CONFIDENTIAL",    // watermark text
                    "Arial",           // font name
                    "#a5a5a5",         // font color (hex)
                    0.5);              // font size in inches (~36 pt)
            }

            // Save the modified diagram with the watermark
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}