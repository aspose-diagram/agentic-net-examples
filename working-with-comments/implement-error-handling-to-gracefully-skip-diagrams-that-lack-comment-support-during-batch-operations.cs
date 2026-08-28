using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input folder containing Visio files
                string inputFolder = args.Length > 0 ? args[0] : "InputDiagrams";
                // Output folder for processed files
                string outputFolder = args.Length > 1 ? args[1] : "ProcessedDiagrams";

                // Ensure output directory exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Get all Visio files (VSDX, VSD, VDX, etc.) in the input folder
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string filePath in diagramFiles)
                {
                    // Process only supported Visio extensions
                    string extension = Path.GetExtension(filePath).ToLowerInvariant();
                    if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    {
                        Console.WriteLine($"Skipping unsupported file: {Path.GetFileName(filePath)}");
                        continue;
                    }

                    try
                    {
                        // Load the diagram
                        Diagram diagram = new Diagram(filePath);

                        // Iterate through each page and attempt to add a comment
                        foreach (Page page in diagram.Pages)
                        {
                            try
                            {
                                // Add a comment at coordinates (1,1) with sample text
                                page.AddComment(1.0, 1.0, "Batch processed comment");
                            }
                            catch (Exception ex)
                            {
                                // If the page does not support comments, log and continue with next page
                                Console.WriteLine($"Comment not supported on page '{page.Name}' in file '{Path.GetFileName(filePath)}': {ex.Message}");
                                continue;
                            }
                        }

                        // Save the modified diagram to the output folder with the same file name
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Processed and saved: {Path.GetFileName(outputPath)}");
                    }
                    catch (Exception ex)
                    {
                        // If loading or saving fails, log and skip this file
                        Console.WriteLine($"Failed to process file '{Path.GetFileName(filePath)}': {ex.Message}");
                    }
                }

                Console.WriteLine("Batch processing completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }