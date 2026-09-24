using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output directory for preview images
        string outputDir = "PreviewImages";
        // Guard: create the output directory if it does not exist
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram from the specified file
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 0;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add a watermark text shape that covers the entire page
                    // Note: using positional arguments to match the AddText overload signature
                    page.AddText(
                        0,                 // pinX (left)
                        0,                 // pinY (bottom)
                        pageWidth,         // width (full page width)
                        pageHeight,        // height (full page height)
                        "WATERMARK",       // text content
                        "Arial",           // font name
                        "#CCCCCC",         // font color in hex
                        0.5);              // font size in inches (~36 points)

                    // Configure image save options for PNG export
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    saveOptions.PageIndex = pageIndex; // zero‑based page index

                    // Build output file path for the current page
                    string outputPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.png");

                    // Save the current page as an image with the watermark overlay
                    diagram.Save(outputPath, saveOptions);

                    // Increment page index for the next iteration
                    pageIndex++;
                }
            }

            Console.WriteLine("Preview images with watermarks have been generated.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}