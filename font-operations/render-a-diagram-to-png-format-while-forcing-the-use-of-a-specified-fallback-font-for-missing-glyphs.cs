using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPngPath> [fallbackFontName]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fallbackFont = args.Length >= 3 ? args[2] : "Arial";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Validate that the fallback font is installed on the system.
        try
        {
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            bool fontExists = fontCollection.Families.Any(f => string.Equals(f.Name, fallbackFont, StringComparison.OrdinalIgnoreCase));

            if (!fontExists)
            {
                Console.Error.WriteLine($"Warning: Fallback font \"{fallbackFont}\" is not installed on this system. Rendering may use another substitute.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while checking installed fonts: {ex.Message}");
            // Continue execution; fallback font will still be set.
        }

        // Configure global font settings before loading the diagram.
        // This ensures Aspose.Diagram uses the specified fallback font for missing glyphs.
        FontConfigs.DefaultFontName = fallbackFont;

        // Load the diagram and render to PNG.
        Diagram diagram = null;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        try
        {
            // Prepare image save options for PNG export.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Force the fallback font for any missing characters.
                DefaultFont = fallbackFont,
                // Export the first page only (optional, adjust as needed).
                PageIndex = 0,
                PageCount = 1
            };

            // Save the diagram as PNG using the configured options.
            diagram.Save(outputPath, pngOptions);
            Console.WriteLine($"Diagram successfully rendered to PNG: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PNG export: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released.
            diagram?.Dispose();
        }
    }
}