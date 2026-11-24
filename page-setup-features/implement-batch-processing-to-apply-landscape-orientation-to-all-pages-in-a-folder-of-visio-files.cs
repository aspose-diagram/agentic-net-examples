using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing Visio files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Define Visio file extensions to process
        string[] extensions = new[]
        {
            "*.vsdx", "*.vsd", "*.vsx", "*.vtx",
            "*.vssx", "*.vstx", "*.vss", "*.vst",
            "*.vsdm", "*.vssm", "*.vstm"
        };

        // Collect all matching files in the folder (non‑recursive)
        var files = new List<string>();
        foreach (string ext in extensions)
        {
            files.AddRange(Directory.GetFiles(folderPath, ext, SearchOption.TopDirectoryOnly));
        }

        // Process each Visio file
        foreach (string filePath in files)
        {
            // Guard: ensure the file still exists
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block (ensures proper disposal)
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages and set orientation to Landscape
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }

                    // Save the modified diagram, overwriting the original file
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Successfully processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}