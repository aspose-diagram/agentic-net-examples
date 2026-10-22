using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
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

        // Ensure output folder exists (create if necessary).
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

        // Get all files in the input folder.
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Process only supported Visio extensions.
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd")
            {
                continue;
            }

            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

            try
            {
                // Load diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate over each page and apply auto‑spacing.
                    foreach (Page page in diagram.Pages)
                    {
                        // Create AutoSpaceOptions with desired horizontal and vertical distances.
                        AutoSpaceOptions options = new AutoSpaceOptions
                        {
                            // Set spacing distances (in inches).
                            DistanceInHorizontal = 0.5,
                            DistanceInVertical = 0.5
                        };

                        // Apply auto‑spacing to all shapes on the current page.
                        page.AutoSpaceShapes(page.Shapes, options);
                    }

                    // Save the modified diagram to the output folder in VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}