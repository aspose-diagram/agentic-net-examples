using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> [outputFolder]");
            return;
        }

        string inputFolder = args[0];
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Determine output folder
        string outputFolder = args.Length >= 2 ? args[1] : Path.Combine(inputFolder, "output");
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

        // Get all Visio files in the input folder (common extensions)
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
            {
                // Skip non-Visio files
                continue;
            }

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page and apply auto-spacing
                    foreach (Page page in diagram.Pages)
                    {
                        // Auto-space shapes on the current page using default options
                        AutoSpaceOptions options = new AutoSpaceOptions();
                        page.AutoSpaceShapes(page.Shapes, options);
                    }

                    // Prepare output file path
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save the updated diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}