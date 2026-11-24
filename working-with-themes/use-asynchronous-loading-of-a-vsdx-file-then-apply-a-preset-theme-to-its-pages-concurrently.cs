using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Aspose.Diagram;

class Program
{
    // Asynchronously loads a Visio diagram from the specified file path.
    private static Task<Diagram> LoadDiagramAsync(string filePath)
    {
        return Task.Run(() =>
        {
            // The Diagram constructor loads the file synchronously.
            return new Diagram(filePath);
        });
    }

    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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

        Diagram diagram = null;

        // Load the diagram asynchronously and handle any loading errors.
        try
        {
            Task<Diagram> loadTask = LoadDiagramAsync(inputPath);
            diagram = loadTask.GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Prepare a typed list of pages for parallel processing.
        List<Page> pages = new List<Page>();
        foreach (Page p in diagram.Pages)
        {
            pages.Add(p);
        }

        // Apply a preset theme to each page concurrently.
        try
        {
            Parallel.ForEach(pages, page =>
            {
                // Set the preset theme; Bubble is a valid enum member.
                page.PresetTheme = PresetThemeValue.Bubble;
                // Optionally, set a variant if desired (using a valid enum value).
                // page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error applying theme: {ex.Message}");
            return;
        }

        // Save the modified diagram to the output path.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}