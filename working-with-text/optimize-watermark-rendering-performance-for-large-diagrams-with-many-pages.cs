using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare output path (same folder, suffix "_watermarked").
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_watermarked.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect pages into a list for safe iteration.
                List<Page> pages = new List<Page>();
                foreach (Page p in diagram.Pages)
                {
                    pages.Add(p);
                }

                // Add a full‑page watermark to each page.
                foreach (Page page in pages)
                {
                    // Retrieve page dimensions.
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Watermark parameters.
                    double pinX = 0;                     // left edge
                    double pinY = 0;                     // top edge
                    double width = pageWidth;            // full page width
                    double height = pageHeight;          // full page height
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Calibri";
                    string fontColor = "#a5a5a5";
                    double fontSize = 0.25;              // example size

                    // Add the watermark text covering the entire page.
                    page.AddText(pinX, pinY, width, height, watermarkText, fontName, fontColor, fontSize);
                }

                // Save the modified diagram with watermarks.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Watermarked diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}