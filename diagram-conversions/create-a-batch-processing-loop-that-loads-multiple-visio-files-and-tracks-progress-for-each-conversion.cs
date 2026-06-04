using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class VisioBatchConverter
{
    // Entry point
    static void Main()
    {
        // Define input Visio files (could be populated dynamically)
        List<string> inputFiles = new List<string>
        {
            @"C:\VisioFiles\Diagram1.vsdx",
            @"C:\VisioFiles\Diagram2.vsd",
            @"C:\VisioFiles\Diagram3.vdx"
        };

        // Define output folder
        string outputFolder = @"C:\ConvertedFiles";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each file
        for (int i = 0; i < inputFiles.Count; i++)
        {
            string inputPath = inputFiles[i];
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

            try
            {
                // Load Visio diagram using the constructor that accepts a file path
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Save the diagram to PDF format
                    diagram.Save(outputPath, SaveFileFormat.Pdf);
                }

                // Report progress
                Console.WriteLine($"[{i + 1}/{inputFiles.Count}] Converted '{inputPath}' to PDF successfully.");
            }
            catch (Exception ex)
            {
                // Report error but continue processing remaining files
                Console.WriteLine($"[{i + 1}/{inputFiles.Count}] Failed to convert '{inputPath}'. Error: {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
