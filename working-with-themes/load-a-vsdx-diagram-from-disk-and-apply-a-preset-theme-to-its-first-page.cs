using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVsdxPath> [outputVsdxPath]");
            return;
        }

        // Input file path.
        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output file path.
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            // Append "_themed" before the extension if no output path is provided.
            string directory = Path.GetDirectoryName(inputPath);
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string extension = Path.GetExtension(inputPath);
            outputPath = Path.Combine(directory, $"{filenameWithoutExt}_themed{extension}");
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0).
                Page firstPage = diagram.Pages[0];

                // Apply a preset theme to the first page.
                // Valid preset theme values include Bubble, etc.
                firstPage.PresetTheme = PresetThemeValue.Bubble;

                // Optionally, a variant can be set (not required for the task).
                // firstPage.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the modified diagram to the output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved with applied theme to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors (Aspose or I/O) to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}