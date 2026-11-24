using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output PNG path, fallback font name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputPngPath> <fallbackFontName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fallbackFontName = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Validate that the fallback font is installed on the system
        try
        {
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            bool fontFound = false;
            foreach (var family in fontCollection.Families)
            {
                if (string.Equals(family.Name, fallbackFontName, StringComparison.OrdinalIgnoreCase))
                {
                    fontFound = true;
                    break;
                }
            }

            if (!fontFound)
            {
                Console.Error.WriteLine($"Warning: Fallback font \"{fallbackFontName}\" is not installed on this system. It will be used as a fallback, but missing glyphs may still appear.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while checking installed fonts: {ex.Message}");
            // Continue execution; fallback font will still be set
        }

        try
        {
            // Configure global fallback font for Aspose.Diagram rendering
            FontConfigs.DefaultFontName = fallbackFontName;

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare PNG save options with the specified fallback font
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.DefaultFont = fallbackFontName;

            // Save the diagram as PNG
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram successfully rendered to PNG at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
        }
    }
}