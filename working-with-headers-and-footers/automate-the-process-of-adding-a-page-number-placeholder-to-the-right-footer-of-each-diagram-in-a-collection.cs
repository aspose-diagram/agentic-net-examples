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

                // Folder containing the Visio diagrams to process
                string inputFolder = @"C:\Diagrams\Input";
                // Folder where the updated diagrams will be saved
                string outputFolder = @"C:\Diagrams\Output";

                // Ensure the output directory exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Process each .vsdx file in the input folder
                foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
                {
                    try
                    {
                        // Load the diagram from file
                        Diagram diagram = new Diagram(filePath);

                        // Set the right footer to display the page number placeholder
                        diagram.HeaderFooter.FooterRight = "Page: &p";

                        // Determine output file path (same file name in the output folder)
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                        // Save the updated diagram in VSDX format
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }
                    catch (Exception ex)
                    {
                        // Log any errors for the current file
                        Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                    }
                }

                Console.WriteLine("Footer update completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }