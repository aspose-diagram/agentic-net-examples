using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> [output.vsdx]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_themed.vsdx");

        // Load the diagram asynchronously.
        Diagram diagram = null;
        try
        {
            diagram = LoadDiagramAsync(inputPath).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Apply a preset theme to each page concurrently.
        try
        {
            // Collect pages into a typed list because PageCollection is not generic.
            List<Page> pages = new List<Page>();
            foreach (Page p in diagram.Pages)
            {
                pages.Add(p);
            }

            // Use Parallel.ForEach to set the theme on each page.
            Parallel.ForEach(pages, page =>
            {
                // Apply the Bubble preset theme.
                page.PresetTheme = PresetThemeValue.Bubble;
                // Optionally, set a variant if desired (using a known variant value).
                // page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error applying theme: {ex.Message}");
            return;
        }

        // Save the modified diagram.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }

    // Asynchronously loads a Diagram from the specified file path.
    private static async Task<Diagram> LoadDiagramAsync(string path)
    {
        return await Task.Run(() =>
        {
            // The Diagram constructor performs the loading.
            return new Diagram(path);
        });
    }
}