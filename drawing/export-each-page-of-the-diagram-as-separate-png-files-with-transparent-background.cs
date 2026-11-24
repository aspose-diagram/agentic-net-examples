using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output directory (use current directory if not supplied).
        string outputDir = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();

        // Guard: ensure the output directory exists or create it.
        try
        {
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: Unable to create output directory \"{outputDir}\": {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for PNG export.
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    // Export only the current page.
                    saveOptions.PageIndex = i;
                    saveOptions.PageCount = 1;

                    // NOTE: Aspose.Diagram does not provide a TransparentBackground option for PNG.
                    // The exported PNG will contain the page's background as rendered by the library.

                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.png");

                    try
                    {
                        // Save the current page as a PNG file.
                        diagram.Save(outputPath, saveOptions);
                        Console.WriteLine($"Page {i + 1} exported to \"{outputPath}\"");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error exporting page {i + 1}: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}