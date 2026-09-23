using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing VSD files
        string inputFolder = @"C:\VisioFiles";
        // Output folder for PNG images
        string outputFolder = @"C:\VisioPngOutput";

        // Ensure output directory exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Collect all supported Visio files (VSD, VSDX, VSDM, etc.)
        string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly);
        string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
        string[] allFiles = new string[vsdFiles.Length + vsdxFiles.Length];
        vsdFiles.CopyTo(allFiles, 0);
        vsdxFiles.CopyTo(allFiles, vsdFiles.Length);

        // Process each file in parallel for better performance
        Parallel.ForEach(allFiles, filePath =>
        {
            // Guard: ensure the file actually exists before attempting to load
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                // Load the Visio diagram from the file
                Diagram diagram = new Diagram(filePath);

                // Iterate pages using an index to avoid missing Page.Index property
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    // Build output file name: OriginalName_Page{index}.png
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"{fileNameWithoutExt}_Page{i}.png");

                    // Configure PNG save options for the current page
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    pngOptions.PageIndex = i;   // zero‑based page index
                    pngOptions.PageCount = 1;   // export only this page

                    // Save the page as a PNG image
                    diagram.Save(outputPath, pngOptions);
                }

                Console.WriteLine($"Successfully processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch conversion completed.");
    }
}