using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files to process
            string inputFolder = @"C:\VisioFiles";
            // Folder to save processed files
            string outputFolder = @"C:\VisioFiles\Processed";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all Visio files (VSDX, VDX, VSD) in the input folder
            string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                // Process only supported Visio extensions
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vdx" && ext != ".vsd")
                    continue;

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply auto‑spacing to each page
                    foreach (Page page in diagram.Pages)
                    {
                        AutoSpaceOptions options = new AutoSpaceOptions();
                        options.DistanceInHorizontal = 2; // horizontal spacing in inches
                        options.DistanceInVertical = 2;   // vertical spacing in inches

                        // Auto‑space all shapes on the page
                        page.AutoSpaceShapes(page.Shapes, options);
                    }

                    // Build output file name (preserve original name, add suffix)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + "_spaced.vsdx");

                    // Save the updated diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch auto‑spacing completed.");
        }
    }