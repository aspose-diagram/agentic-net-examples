using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Folder containing source Visio files
                string inputFolder = @"C:\Visio\Input";
                // Folder where modified files will be saved
                string outputFolder = @"C:\Visio\Output";

                // Ensure output directory exists
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Uniform margin value in inches (e.g., 0.1 inch)
                double uniformMargin = 0.1;

                // Process each .vsdx file in the input folder
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");
                foreach (string filePath in diagramFiles)
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
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Apply margin to each paragraph of the shape
                                foreach (Para para in shape.Paras)
                                {
                                    // Set left, right, and first line indent to the uniform margin
                                    para.IndLeft.Value = uniformMargin;
                                    para.IndRight.Value = uniformMargin;
                                    para.IndFirst.Value = uniformMargin;
                                }
                            }
                        }

                        // Build output file path (preserve original file name)
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                        // Save the modified diagram using the correct overload
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