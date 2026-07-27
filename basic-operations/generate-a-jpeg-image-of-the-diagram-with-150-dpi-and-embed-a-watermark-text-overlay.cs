using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input Visio file (replace with actual path)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output JPEG file
        string outputPath = "output.jpg";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Add a watermark text that covers the whole page
            // Using positional arguments because the overload does not support named parameters
            page.AddText(
                0,                 // pinX (left)
                0,                 // pinY (bottom)
                pageWidth,         // width (full page width)
                pageHeight,        // height (full page height)
                "CONFIDENTIAL",    // watermark text
                "Arial",           // font name
                "#CCCCCC",         // font color in hex
                0.5                // font size in inches (~36 points)
            );

            // Configure image export options for JPEG at 150 DPI
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            saveOptions.Resolution = 150f; // DPI

            // Save the diagram as a JPEG image with the watermark
            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}