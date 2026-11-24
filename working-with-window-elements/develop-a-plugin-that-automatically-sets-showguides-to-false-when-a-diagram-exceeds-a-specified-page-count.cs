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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [maxPageCount]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Parse optional page‑count threshold; default to 5 if not provided or invalid.
        int maxPageCount = 5;
        if (args.Length >= 2 && int.TryParse(args[1], out int parsedCount) && parsedCount > 0)
        {
            maxPageCount = parsedCount;
        }

        // Determine output path (same folder, suffix "_modified.vsdx").
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Check the total number of pages.
                int pageCount = diagram.Pages.Count;

                // If page count exceeds the specified limit, disable guide visibility.
                if (pageCount > maxPageCount)
                {
                    // Iterate over all windows and set ShowGuides to FALSE.
                    foreach (Window window in diagram.Windows)
                    {
                        window.ShowGuides = BOOL.False;
                    }

                    Console.WriteLine($"Diagram has {pageCount} pages (exceeds {maxPageCount}); ShowGuides set to FALSE.");
                }
                else
                {
                    Console.WriteLine($"Diagram has {pageCount} pages (within limit); no changes made.");
                }

                // Save the (potentially modified) diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}