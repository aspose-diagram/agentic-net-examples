using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default) and guard its existence.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output file path (second argument or default) and ensure its directory exists.
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the diagram asynchronously using Task.Run to avoid blocking the main thread.
            Task<Diagram> loadTask = Task.Run(() => new Diagram(inputPath));
            diagram = loadTask.Result;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Collect pages into a typed list to enable Parallel.ForEach (type inference does not work directly).
        List<Page> pages = new List<Page>();
        foreach (Page p in diagram.Pages)
        {
            pages.Add(p);
        }

        // Apply a preset theme to each page concurrently.
        Parallel.ForEach(pages, page =>
        {
            try
            {
                // Set the main theme (Bubble) and a variant (Variant1) for the page.
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing an individual page.
                Console.Error.WriteLine($"Error applying theme to page '{page.Name}': {ex.Message}");
            }
        });

        try
        {
            // Save the modified diagram back to VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released.
            diagram?.Dispose();
        }
    }
}