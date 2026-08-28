using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output directories (adjust as needed)
                string inputFolder = @"C:\Visio\Input";
                string outputFolder = @"C:\Visio\Output";

                // Ensure output folder exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Process each Visio file in the input folder
                string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string filePath in visioFiles)
                {
                    // Filter supported Visio extensions
                    string extension = Path.GetExtension(filePath).ToLowerInvariant();
                    if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    {
                        continue;
                    }

                    try
                    {
                        // Load the diagram
                        Diagram diagram = new Diagram(filePath);

                        // Configure auto‑spacing options
                        AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                        {
                            DistanceInHorizontal = 2.0, // horizontal spacing in inches
                            DistanceInVertical = 2.0    // vertical spacing in inches
                        };

                        // Apply auto‑spacing to every page
                        foreach (Page page in diagram.Pages)
                        {
                            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                        }

                        // Determine output file path (same name, different folder)
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

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
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }