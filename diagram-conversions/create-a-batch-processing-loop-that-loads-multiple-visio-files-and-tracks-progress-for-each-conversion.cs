using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            // Provide a list of Visio files to convert and an output folder.
            var inputFiles = new List<string>
            {
                @"C:\VisioFiles\Diagram1.vsdx",
                @"C:\VisioFiles\Diagram2.vsd",
                @"C:\VisioFiles\Diagram3.vdx"
            };

            string outputFolder = @"C:\ConvertedFiles";

            // Ensure the output directory exists.
            Directory.CreateDirectory(outputFolder);

            // Process the batch.
            ProcessVisioBatch(inputFiles, outputFolder);
        }

        /// <summary>
        /// Loads each Visio file, converts it to PDF, and tracks progress.
        /// </summary>
        /// <param name="inputFiles">Full paths of the Visio files to process.</param>
        /// <param name="outputFolder">Folder where converted files will be saved.</param>
        static void ProcessVisioBatch(IList<string> inputFiles, string outputFolder)
        {
            int total = inputFiles.Count;
            for (int i = 0; i < total; i++)
            {
                string inputPath = inputFiles[i];
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

                try
                {
                    // Load the Visio diagram using the constructor that accepts a file path.
                    using (Diagram diagram = new Diagram(inputPath))
                    {
                        // Save the diagram as PDF using the Save method with SaveFileFormat.
                        diagram.Save(outputPath, SaveFileFormat.Pdf);
                    }

                    // Report progress.
                    Console.WriteLine($"[{i + 1}/{total}] Converted: {inputPath} -> {outputPath}");
                }
                catch (Exception ex)
                {
                    // Report error but continue processing remaining files.
                    Console.WriteLine($"[{i + 1}/{total}] Failed to convert {inputPath}: {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }