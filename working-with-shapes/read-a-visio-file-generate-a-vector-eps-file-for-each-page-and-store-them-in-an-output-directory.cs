using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = @"C:\Input\diagram.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output directory for EPS (vector) files
        string outputDir = @"C:\Output\EpsPages";
        // Guard: create the directory if it does not exist
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Aspose.Diagram does not provide a native EPS format.
            // Use EMF (a vector format) and write files with an .eps extension.
            ImageSaveOptions epsOptions = new ImageSaveOptions(SaveFileFormat.Emf)
            {
                // Export one page at a time
                PageCount = 1
            };

            // Iterate through each page and save as a separate EPS (EMF) file
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Set the page index to export (zero‑based)
                epsOptions.PageIndex = i;

                // Build the output file name (e.g., Page_1.eps)
                string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.eps");

                // Save the current page using the EMF vector data
                diagram.Save(outputPath, epsOptions);
            }

            Console.WriteLine("Export completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}