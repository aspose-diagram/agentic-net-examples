using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Define input and output directories (adjust as needed)
                string inputFolder = @"C:\Diagrams\Input";
                string outputFolder = @"C:\Diagrams\Output";

                // Ensure the output directory exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Get all files in the input folder (including subfolders if desired)
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);

                foreach (string filePath in diagramFiles)
                {
                    try
                    {
                        // Load the diagram
                        Diagram diagram = new Diagram(filePath);

                        // Update the footer with the current timestamp
                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        diagram.HeaderFooter.FooterRight = $"Generated on {timestamp}";

                        // Prepare the output file path (preserve original file name)
                        string fileName = Path.GetFileName(filePath);
                        string outputPath = Path.Combine(outputFolder, fileName);

                        // Save the diagram in its original format (using VSDX as a safe default)
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);

                        Console.WriteLine($"Processed and saved: {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        // Log any errors for the current file and continue processing others
                        Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
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