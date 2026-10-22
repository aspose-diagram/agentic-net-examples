using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and desired font cache size (not supported).
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <fontCacheSize>");
            return;
        }

        string inputPath = args[0];
        string cacheSizeArg = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse cache size (just for demonstration; not used because API does not support it).
        if (!int.TryParse(cacheSizeArg, out int cacheSize) || cacheSize <= 0)
        {
            Console.Error.WriteLine("Error: Font cache size must be a positive integer.");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // TODO: Configure font cache size.
            // Aspose.Diagram does not provide an API to set the font cache size.
            // If future versions add this capability, it can be set here using the appropriate property.

            // Example: set a default fallback font to ensure rendering succeeds.
            FontConfigs.DefaultFontName = "Arial";

            // Define output path.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? "",
                Path.GetFileNameWithoutExtension(inputPath) + "_processed.vsdx");

            // Save the diagram after (potential) configuration.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram processed and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}