using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files
            string inputFolder = @"C:\VisioFiles";
            // Folder to save the modified files (can be the same as inputFolder)
            string outputFolder = @"C:\VisioFiles\Processed";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all Visio files in the input folder (supports .vsdx, .vsd, .vdx, etc.)
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                // Process only known Visio extensions
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vsx" && extension != ".vtx")
                    continue;

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply Landscape orientation to every page
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }

                    // Determine output file path (overwrite original or save to new location)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save using the same format as the original file
                    // Choose appropriate SaveFileFormat based on extension
                    SaveFileFormat format = extension switch
                    {
                        ".vsdx" => SaveFileFormat.Vsdx,
                        ".vsd" => SaveFileFormat.Vsd,
                        ".vdx" => SaveFileFormat.Vdx,
                        ".vsx" => SaveFileFormat.Vsx,
                        ".vtx" => SaveFileFormat.Vtx,
                        _ => SaveFileFormat.Vsdx
                    };

                    diagram.Save(outputPath, format);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }