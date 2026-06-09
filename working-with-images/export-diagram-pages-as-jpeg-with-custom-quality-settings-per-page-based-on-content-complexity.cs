using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramPageExporter
{
    // Export each page of a diagram to a JPEG file with quality based on page complexity.
    public static void ExportPagesWithCustomQuality(string diagramPath, string outputFolder)
    {
        // Load the diagram (lifecycle rule: use provided load method)
        Diagram diagram = new Diagram(diagramPath);

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Iterate through all pages in the diagram
        for (int i = 0; i < diagram.Pages.Count; i++)
        {
            // Determine page complexity (example: number of shapes on the page)
            int shapeCount = diagram.Pages[i].Shapes.Count;

            // Map complexity to JPEG quality (0‑100). Higher complexity → higher quality.
            // This simple mapping can be replaced with any custom logic.
            int quality = MapComplexityToQuality(shapeCount);

            // Configure image save options for JPEG
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
            {
                // Set the page to render
                PageIndex = i,
                PageCount = 1,

                // Apply the calculated JPEG quality
                JpegQuality = quality,

                // Optional: set resolution if needed (default is 96 DPI)
                // Resolution = 150,

                // Ensure the format is JPEG (redundant but explicit)
                SaveFormat = SaveFileFormat.Jpeg
            };

            // Build output file name: Page_0.jpg, Page_1.jpg, etc.
            string outputFile = Path.Combine(outputFolder, $"Page_{i}.jpg");

            // Save the specific page as JPEG (lifecycle rule: use provided save method)
            diagram.Save(outputFile, saveOptions);
        }
    }

    // Simple helper to convert shape count to a JPEG quality value (0‑100)
    private static int MapComplexityToQuality(int shapeCount)
    {
        // Example mapping:
        // 0‑10 shapes  → quality 70
        // 11‑30 shapes → quality 85
        // >30 shapes   → quality 100
        if (shapeCount <= 10)
            return 70;
        if (shapeCount <= 30)
            return 85;
        return 100;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramPageExporter.ExportPagesWithCustomQuality("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
