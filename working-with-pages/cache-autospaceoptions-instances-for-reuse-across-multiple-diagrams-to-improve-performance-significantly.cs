using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Cached AutoSpaceOptions instance reused for all diagrams
    private static readonly AutoSpaceOptions CachedAutoSpaceOptions = CreateAutoSpaceOptions();

    // Helper method to configure the AutoSpaceOptions
    private static AutoSpaceOptions CreateAutoSpaceOptions()
    {
        AutoSpaceOptions options = new AutoSpaceOptions();
        // Set horizontal and vertical spacing (in inches)
        options.DistanceInHorizontal = 2;
        options.DistanceInVertical = 2;
        return options;
    }

    static void Main(string[] args)
    {
        // Validate input arguments
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath1> [<inputDiagramPath2> ...]");
            return;
        }

        // Process each diagram file provided in the arguments
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
                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate over each page in the diagram
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Retrieve the page by index
                        Page page = diagram.Pages[i];

                        // Auto-space all shapes on the current page using the cached options
                        page.AutoSpaceShapes(page.Shapes, CachedAutoSpaceOptions);
                    }

                    // Construct output file path (append "_spaced" before the extension)
                    string directory = Path.GetDirectoryName(inputPath) ?? "";
                    string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_spaced.vsdx");

                    // Save the modified diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}