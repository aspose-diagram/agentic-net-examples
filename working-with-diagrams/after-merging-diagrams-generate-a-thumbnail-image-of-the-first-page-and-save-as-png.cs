using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first diagram path, second diagram path, output PNG thumbnail path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <firstDiagram.vsdx> <secondDiagram.vsdx> <outputThumbnail.png>");
            return;
        }

        string firstDiagramPath = args[0];
        string secondDiagramPath = args[1];
        string outputPngPath = args[2];

        // Guard: ensure input files exist.
        if (!File.Exists(firstDiagramPath))
        {
            Console.Error.WriteLine($"File not found: {firstDiagramPath}");
            return;
        }
        if (!File.Exists(secondDiagramPath))
        {
            Console.Error.WriteLine($"File not found: {secondDiagramPath}");
            return;
        }

        try
        {
            // Load the first diagram.
            using (Diagram mergedDiagram = new Diagram(firstDiagramPath))
            {
                // Load the second diagram and merge it into the first.
                using (Diagram secondDiagram = new Diagram(secondDiagramPath))
                {
                    mergedDiagram.Combine(secondDiagram);
                }

                // Prepare image save options for PNG thumbnail.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export only the first page.
                pngOptions.PageIndex = 0;
                pngOptions.PageCount = 1;
                // Do not export hidden pages.
                pngOptions.ExportHiddenPage = false;
                // Scale down to create a thumbnail (e.g., 20% of original size).
                pngOptions.Scale = 0.2f;

                // Ensure the output directory exists.
                string? outputDir = Path.GetDirectoryName(outputPngPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Save the first page as a PNG thumbnail.
                mergedDiagram.Save(outputPngPath, pngOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
