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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJpegPath>");
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
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a full‑page watermark text.
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize (in inches).
                // Position at lower‑left corner (0,0) and cover the whole page.
                page.AddText(
                    0,                     // pinX
                    0,                     // pinY
                    pageWidth,             // width
                    pageHeight,            // height
                    "Watermark",           // text
                    "Arial",               // font name
                    "#A5A5A5",             // font color (hex)
                    0.5);                  // font size in inches (36 pt)

                // Configure image save options for JPEG at 150 DPI.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                saveOptions.Resolution = 150f;          // DPI
                saveOptions.JpegQuality = 90;          // Quality (0‑100)
                saveOptions.ExportHiddenPage = false; // Do not export hidden pages

                // Save the diagram as a JPEG image.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}