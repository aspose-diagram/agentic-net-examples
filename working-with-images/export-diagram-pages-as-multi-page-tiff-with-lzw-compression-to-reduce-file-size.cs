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
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Output multi‑page TIFF file path
        string outputPath = "output.tiff";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options for TIFF
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
            // Export all pages: start from first page (0) and include all pages
            saveOptions.PageIndex = 0;
            saveOptions.PageCount = diagram.Pages.Count;
            // Note: LZW compression is applied by default for TIFF in Aspose.Diagram;
            // the ImageSaveOptions class does not expose a Compression property.

            // Save the diagram as a multi‑page TIFF using the configured options
            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}