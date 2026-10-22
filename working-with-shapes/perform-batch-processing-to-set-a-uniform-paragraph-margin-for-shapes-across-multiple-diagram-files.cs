using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchParagraphMargin <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Verify input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Verify (or create) output folder.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Get all VSDX files in the input folder.
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");
        if (diagramFiles.Length == 0)
        {
            Console.Error.WriteLine($"No .vsdx files found in folder: {inputFolder}");
            return;
        }

        // Uniform margin value (in inches) to apply to each paragraph.
        double uniformMarginInches = 0.2;

        foreach (string inputPath in diagramFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load diagram inside a using block as required.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Iterate through each paragraph of the shape.
                            for (int i = 0; i < shape.Paras.Count; i++)
                            {
                                // Set left and right indent (margin) to the uniform value.
                                shape.Paras[i].IndLeft.Value = uniformMarginInches;
                                shape.Paras[i].IndRight.Value = uniformMarginInches;
                            }
                        }
                    }

                    // Save the modified diagram to the output folder, preserving VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing of this file.
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}