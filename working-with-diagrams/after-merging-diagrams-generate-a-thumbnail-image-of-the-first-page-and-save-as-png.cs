using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <baseDiagram.vsdx> <diagramToMerge1.vsdx> [<diagramToMerge2.vsdx> ...]");
            return;
        }

        string basePath = args[0];

        // Guard: ensure the base diagram file exists.
        if (!File.Exists(basePath))
        {
            Console.Error.WriteLine($"Base diagram file not found: {basePath}");
            return;
        }

        // Load the base diagram inside a using block (required pattern).
        using (Diagram baseDiagram = new Diagram(basePath))
        {
            // Merge additional diagrams into the base diagram.
            for (int i = 1; i < args.Length; i++)
            {
                string mergePath = args[i];

                // Guard: ensure each merge diagram file exists.
                if (!File.Exists(mergePath))
                {
                    Console.Error.WriteLine($"Diagram to merge not found: {mergePath}");
                    return;
                }

                try
                {
                    // Load the diagram to be merged.
                    using (Diagram mergeDiagram = new Diagram(mergePath))
                    {
                        // Combine all pages, masters, and other resources.
                        baseDiagram.Combine(mergeDiagram);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to merge diagram '{mergePath}': {ex.Message}");
                    return;
                }
            }

            // Define output thumbnail path (same directory as base diagram).
            string thumbnailPath = Path.Combine(Path.GetDirectoryName(basePath) ?? string.Empty, "thumbnail.png");

            try
            {
                // Configure image save options for PNG thumbnail.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the first page.
                    PageIndex = 0,
                    PageCount = 1,
                    // Scale down to create a thumbnail (e.g., 20% of original size).
                    Scale = 0.2f,
                    // Do not export hidden pages.
                    ExportHiddenPage = false
                };

                // Save the thumbnail image.
                baseDiagram.Save(thumbnailPath, pngOptions);
                Console.WriteLine($"Thumbnail saved to: {thumbnailPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to generate thumbnail: {ex.Message}");
            }
        }
    }
}