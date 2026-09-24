using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    // Asynchronously loads a VSDX file into a Diagram object.
    private static async Task<Diagram> LoadDiagramAsync(string path)
    {
        // Open the file with asynchronous I/O enabled.
        using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
        var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        // Construct the Diagram from the in‑memory stream.
        return new Diagram(memoryStream);
    }

    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the diagram asynchronously.
        Diagram diagram = await LoadDiagramAsync(inputPath);
        try
        {
            // Gather pages into a typed list for Parallel.ForEach (type inference does not work directly on PageCollection).
            var pages = new List<Page>();
            foreach (Page p in diagram.Pages)
                pages.Add(p);

            // Apply a preset theme to each page concurrently.
            Parallel.ForEach(pages, page =>
            {
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            });

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        finally
        {
            // Ensure resources are released.
            diagram.Dispose();
        }
    }
}
