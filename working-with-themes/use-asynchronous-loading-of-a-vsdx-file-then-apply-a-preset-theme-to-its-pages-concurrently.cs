using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Asynchronously loads a Visio diagram from a file path.
    private static async Task<Diagram> LoadDiagramAsync(string filePath)
    {
        // Read the file into a memory stream to avoid keeping the FileStream open.
        var memoryStream = new MemoryStream();
        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
        {
            await fileStream.CopyToAsync(memoryStream);
        }

        memoryStream.Position = 0; // Reset stream position for reading.
        // Create the Diagram from the in‑memory stream.
        return new Diagram(memoryStream);
    }

    static async Task Main(string[] args)
    {
        try
        {

            // Input and output file paths.
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram asynchronously.
            using (Diagram diagram = await LoadDiagramAsync(inputPath))
            {
                // Collect pages into a list for Parallel.ForEach (type inference does not work directly on the collection).
                List<Page> pages = new List<Page>();
                foreach (Page p in diagram.Pages)
                {
                    pages.Add(p);
                }

                // Apply a preset theme to each page concurrently.
                Parallel.ForEach(pages, page =>
                {
                    // Example theme: Bubble. Adjust as needed.
                    page.PresetTheme = PresetThemeValue.Bubble;
                    // Optionally set a variant.
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                });

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
