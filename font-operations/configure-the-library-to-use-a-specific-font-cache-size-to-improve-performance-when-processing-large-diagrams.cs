using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Font configuration (example: set a default fallback font).
            // -----------------------------------------------------------------
            FontConfigs.DefaultFontName = "Arial";

            // -----------------------------------------------------------------
            // TODO: Configure font cache size.
            // The Aspose.Diagram API does not expose a FontCacheSize property
            // or related setting. If future versions add such capability,
            // it should be configured here before any rendering or saving.
            // -----------------------------------------------------------------

            // Save the diagram to a new file (preserving the original format).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}