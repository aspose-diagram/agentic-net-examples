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
                string inputFolder = @"C:\Diagrams\Input";
                // Output folder for processed files
                string outputFolder = @"C:\Diagrams\Output";

                // Ensure output directory exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Process each .vsdx file in the input folder
                foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Identify title shapes by name (case‑insensitive contains "Title")
                                if (!string.IsNullOrEmpty(shape.NameU) &&
                                    shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    // Rotate the text block by 180 degrees (π radians)
                                    shape.TextXForm.TxtAngle.Value = Math.PI;
                                }
                            }
                        }

                        // Save the modified diagram to the output folder
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }

                    Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
                }

                Console.WriteLine("Batch processing completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }