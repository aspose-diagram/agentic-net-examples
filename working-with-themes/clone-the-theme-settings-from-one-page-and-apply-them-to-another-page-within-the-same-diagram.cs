using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <sourcePageIndex> <targetPageIndex>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse page indices
        if (!int.TryParse(args[1], out int sourcePageIndex) ||
            !int.TryParse(args[2], out int targetPageIndex))
        {
            Console.Error.WriteLine("Error: Page indices must be valid integers.");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(diagramPath))
            {
                int pageCount = diagram.Pages.Count;

                // Validate source page index
                if (sourcePageIndex < 0 || sourcePageIndex >= pageCount)
                {
                    Console.Error.WriteLine($"Error: sourcePageIndex out of range. Diagram has {pageCount} pages.");
                    return;
                }

                // Validate target page index
                if (targetPageIndex < 0 || targetPageIndex >= pageCount)
                {
                    Console.Error.WriteLine($"Error: targetPageIndex out of range. Diagram has {pageCount} pages.");
                    return;
                }

                // Retrieve the source and target pages
                Page sourcePage = diagram.Pages[sourcePageIndex];
                Page targetPage = diagram.Pages[targetPageIndex];

                // -----------------------------------------------------------------
                // NOTE:
                // The PresetTheme and PresetThemeVariant properties on Page are
                // write‑only (no getter). Because of this limitation the current
                // Aspose.Diagram API does not allow reading the theme values from
                // the source page to assign them to the target page.
                // -----------------------------------------------------------------
                // TODO: When a getter becomes available, clone the theme like:
                // targetPage.PresetTheme = sourcePage.PresetTheme;
                // targetPage.PresetThemeVariant = sourcePage.PresetThemeVariant;
                // For now, this operation is omitted.

                // Define the output path (same directory, with a suffix)
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(diagramPath) + "_ThemeCloned.vsdx");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}