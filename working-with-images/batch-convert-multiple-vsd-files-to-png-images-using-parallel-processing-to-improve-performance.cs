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
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchConvertVsdToPng <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: input folder must exist.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Collect all VSD files in the input folder.
        List<string> vsdFiles = new List<string>();
        try
        {
            vsdFiles.AddRange(Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly));
            // Optionally include VSDX files as well.
            vsdFiles.AddRange(Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error enumerating files in folder {inputFolder}: {ex.Message}");
            return;
        }

        if (vsdFiles.Count == 0)
        {
            Console.Error.WriteLine("No VSD/VSDX files found in the specified input folder.");
            return;
        }

        // Process each file in parallel to improve performance.
        Parallel.ForEach(vsdFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, filePath =>
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                // Load the Visio diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Export each page of the diagram as a separate PNG image.
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Configure PNG export options.
                        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        pngOptions.PageIndex = i;   // Zero‑based page index.
                        pngOptions.PageCount = 1;   // Export only the current page.

                        // Build output file name: <originalName>_page<index>.png
                        string baseName = Path.GetFileNameWithoutExtension(filePath);
                        string outputFileName = $"{baseName}_page{i + 1}.png";
                        string outputPath = Path.Combine(outputFolder, outputFileName);

                        // Save the page as PNG.
                        diagram.Save(outputPath, pngOptions);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or saving.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch conversion completed.");
    }
}