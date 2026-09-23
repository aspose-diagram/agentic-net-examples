using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Directory containing Visio files to process
            string inputDirectory = @"C:\VisioFiles";
            // Directory where converted files will be saved
            string outputDirectory = @"C:\ConvertedFiles";

            // Ensure output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Gather all Visio files (e.g., .vsdx, .vsd) from the input directory
            List<string> visioFiles = new List<string>();
            visioFiles.AddRange(Directory.GetFiles(inputDirectory, "*.vsdx"));
            visioFiles.AddRange(Directory.GetFiles(inputDirectory, "*.vsd"));

            int totalFiles = visioFiles.Count;
            if (totalFiles == 0)
            {
                Console.WriteLine("No Visio files found in the specified directory.");
                return;
            }

            Console.WriteLine($"Found {totalFiles} Visio file(s). Starting batch conversion...");

            // Process each file sequentially and track progress
            for (int i = 0; i < totalFiles; i++)
            {
                string inputPath = visioFiles[i];
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pdf"); // Example conversion to PDF

                // Display progress
                Console.WriteLine($"[{i + 1}/{totalFiles}] Converting \"{Path.GetFileName(inputPath)}\" to PDF...");

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Save the diagram in the desired format (PDF in this example)
                    diagram.Save(outputPath, SaveFileFormat.Pdf);

                    Console.WriteLine($"[{i + 1}/{totalFiles}] Successfully saved to \"{Path.GetFileName(outputPath)}\".");
                }
                catch (Exception ex)
                {
                    // Log any errors but continue processing remaining files
                    Console.WriteLine($"[{i + 1}/{totalFiles}] Error processing \"{Path.GetFileName(inputPath)}\": {ex.Message}");
                }
            }

            Console.WriteLine("Batch conversion completed.");
        }
    }