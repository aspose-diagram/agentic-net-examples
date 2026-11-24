using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    // Cached AutoSpaceOptions instance reused for all diagrams
    private static readonly AutoSpaceOptions CachedOptions = CreateAutoSpaceOptions();

    // Configure the AutoSpaceOptions once
    private static AutoSpaceOptions CreateAutoSpaceOptions()
    {
        var options = new AutoSpaceOptions();
        // Set desired horizontal and vertical distances (in inches)
        options.DistanceInHorizontal = 2.0;
        options.DistanceInVertical = 2.0;
        return options;
    }

    static void Main(string[] args)
    {
        // Validate arguments
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath1> [<inputDiagramPath2> ...]");
            return;
        }

        // Process each diagram file supplied via command‑line arguments
        foreach (string inputPath in args)
        {
            // Guard: ensure the file exists before attempting to load
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate over all pages explicitly typed as Page
                    foreach (Page page in diagram.Pages)
                    {
                        // Auto‑space all shapes on the current page using the cached options
                        page.AutoSpaceShapes(page.Shapes, CachedOptions);
                    }

                    // Build output file name (original name with "_spaced" suffix)
                    string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_spaced.vsdx");

                    // Save the modified diagram using the required overload
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Successfully processed: {inputPath}");
                    Console.WriteLine($"Saved spaced diagram to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}