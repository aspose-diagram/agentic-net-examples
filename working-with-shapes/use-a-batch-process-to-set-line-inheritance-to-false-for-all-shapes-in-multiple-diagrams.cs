using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine input and output directories
            string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            string outputFolder = args.Length > 1 ? args[1] : inputFolder;

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                try
                {
                    Directory.CreateDirectory(outputFolder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                    return;
                }
            }

            // Process each Visio file in the input folder
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");
            foreach (string filePath in visioFiles)
            {
                try
                {
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

                            // Set line color explicitly to break inheritance.
                            // Using black as a default non-inherited color.
                            shape.Line.LineColor.Value = "#000000";

                            // Optionally, you can also set other line properties to ensure inheritance is broken.
                            // For example, set line weight explicitly.
                            shape.Line.LineWeight.Value = shape.Line.LineWeight.Value; // keep existing weight
                        }
                    }

                    // Prepare output file path
                    string fileName = Path.GetFileName(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    diagram.Dispose();

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }