using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Example input: list of Visio files to convert
            List<string> inputFiles = new List<string>
            {
                @"C:\VisioFiles\Diagram1.vsdx",
                @"C:\VisioFiles\Diagram2.vsd",
                @"C:\VisioFiles\Diagram3.vdx"
                // Add more file paths as needed
            };

            // Output directory where converted files will be saved
            string outputDirectory = @"C:\ConvertedVisio";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Process each file and track progress
            for (int i = 0; i < inputFiles.Count; i++)
            {
                string inputPath = inputFiles[i];
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pdf"); // Convert to PDF

                Console.WriteLine($"Processing file {i + 1} of {inputFiles.Count}: {Path.GetFileName(inputPath)}");

                try
                {
                    // Load the Visio diagram using the constructor that accepts a file path
                    using (Diagram diagram = new Diagram(inputPath))
                    {
                        // Save the diagram in the desired format (PDF in this example)
                        diagram.Save(outputPath, SaveFileFormat.Pdf);
                    }

                    Console.WriteLine($"Successfully saved: {Path.GetFileName(outputPath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(inputPath)}: {ex.Message}");
                }

                // Optional: display simple progress percentage
                double percent = ((i + 1) / (double)inputFiles.Count) * 100;
                Console.WriteLine($"Progress: {percent:0.00}%\n");
            }

            Console.WriteLine("Batch processing completed.");
        }
    }