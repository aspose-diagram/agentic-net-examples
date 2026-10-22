using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Add a full‑page watermark to each page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add watermark text covering the entire page
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize
                page.AddText(
                    0,                     // pinX (left)
                    0,                     // pinY (top)
                    pageWidth,             // width
                    pageHeight,            // height
                    "CONFIDENTIAL",        // watermark text
                    "Arial",               // font name
                    "#C0C0C0",             // light gray color in hex
                    36);                   // font size
            }

            // Export each page as a PNG preview image
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Configure image save options for a single page
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = pageIndex;   // zero‑based page index
                options.PageCount = 1;           // export only this page

                // Build output file path
                string outputPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}_preview.png");

                // Save the page image
                diagram.Save(outputPath, options);
                Console.WriteLine($"Saved preview: {outputPath}");

                pageIndex++;
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}