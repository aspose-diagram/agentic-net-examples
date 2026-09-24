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

                // Folder containing Visio files to process
                string inputFolder = @"C:\VisioFiles\Input";
                // Folder where rotated files will be saved
                string outputFolder = @"C:\VisioFiles\Output";

                // Ensure output folder exists
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Get all Visio files (VSDX, VSD, VDX) in the input folder
                string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string filePath in visioFiles)
                {
                    string extension = Path.GetExtension(filePath).ToLowerInvariant();
                    if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                        continue; // Skip non‑Visio files

                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Ensure the TextXForm object exists
                            if (shape.TextXForm != null)
                            {
                                // Convert 30 degrees to radians
                                double angleRad = (Math.PI / 180.0) * 30.0;
                                shape.TextXForm.TxtAngle.Value = angleRad;
                            }
                        }
                    }

                    // Build output file path (preserve original file name)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save the modified diagram back to VSDX format (or original format if desired)
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Batch processing completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }