using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input Visio file and output PNG
            string inputPath = "input.vsdx";
            string outputPath = "output.png";

            // Specify the fallback font name to use for missing glyphs
            string fallbackFont = "Arial";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Set the global fallback font for the diagram rendering engine
            FontConfigs.DefaultFontName = fallbackFont;

            // Configure PNG export options and enforce the fallback font
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.DefaultFont = fallbackFont;

            // Render and save the diagram as a PNG image
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram rendered to PNG with fallback font '{fallbackFont}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
