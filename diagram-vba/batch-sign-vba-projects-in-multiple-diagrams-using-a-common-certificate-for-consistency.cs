using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files to process
            string inputFolder = @"C:\VisioFiles";
            // Output folder for processed files
            string outputFolder = @"C:\VisioFiles\Signed";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all Visio files (including macro-enabled formats)
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                // Process only supported Visio extensions
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsdm")
                {
                    Console.WriteLine($"Skipping unsupported file: {Path.GetFileName(filePath)}");
                    continue;
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Access the VBA project
                    VbaProject vba = diagram.VbaProject;

                    // Report signing status
                    if (vba.IsSigned)
                    {
                        Console.WriteLine($"File '{Path.GetFileName(filePath)}' is already signed.");
                    }
                    else
                    {
                        Console.WriteLine($"File '{Path.GetFileName(filePath)}' is NOT signed. Signing is not supported in this API version.");
                        // Placeholder: signing cannot be performed because VbaProject.Sign does not exist.
                    }

                    // Save the diagram preserving VBA (use macro-enabled format)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + ".vsdm");
                    diagram.Save(outputPath, SaveFileFormat.Vsdm);
                    Console.WriteLine($"Saved processed file to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }