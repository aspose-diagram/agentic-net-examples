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

                // Input folder containing Visio files (VSDX). Adjust as needed.
                string inputFolder = @"C:\VisioFiles\Input";
                // Output folder where modified files will be saved.
                string outputFolder = @"C:\VisioFiles\Output";

                // Ensure output directory exists.
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Process each .vsdx file in the input folder.
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");
                foreach (string filePath in diagramFiles)
                {
                    try
                    {
                        // Load the diagram.
                        Diagram diagram = new Diagram(filePath);

                        // Iterate through all pages.
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page.
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip shapes that are marked as deleted.
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Break line inheritance by assigning explicit line properties.
                                // Set a default line color (black) and a solid dash pattern.
                                shape.Line.LineColor.Value = "#000000";
                                shape.Line.LinePattern.Value = LinePatternValue.Solid;
                                // Set a minimal line weight (in inches).
                                shape.Line.LineWeight.Value = 0.01;
                            }
                        }

                        // Determine output file path.
                        string fileName = Path.GetFileName(filePath);
                        string outputPath = Path.Combine(outputFolder, fileName);

                        // Save the modified diagram, preserving the original format.
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }
                    catch (Exception ex)
                    {
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