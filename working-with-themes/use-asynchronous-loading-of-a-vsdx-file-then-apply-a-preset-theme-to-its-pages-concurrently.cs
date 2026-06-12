using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Aspose.Diagram;

class Program
    {
        static async Task Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Asynchronously read the Visio file into a byte array
                byte[] fileBytes = await File.ReadAllBytesAsync(inputPath);

                // Load the diagram from a memory stream
                using var stream = new MemoryStream(fileBytes);
                using var diagram = new Diagram(stream);

                // Collect pages into a list for Parallel.ForEach (required by API)
                List<Page> pages = new List<Page>();
                foreach (Page pg in diagram.Pages)
                {
                    pages.Add(pg);
                }

                // Apply a preset theme to each page concurrently
                Parallel.ForEach(pages, page =>
                {
                    // Example theme: Bubble with Variant1
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                });

                // Save the modified diagram back to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }