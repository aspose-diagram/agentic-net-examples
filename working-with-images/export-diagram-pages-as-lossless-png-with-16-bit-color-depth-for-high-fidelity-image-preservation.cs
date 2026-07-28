using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToPng
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for lossless 16‑bit PNG
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Ensure full‑color output (no grayscale conversion)
                ImageColorMode = ImageColorMode.None,
                // Set a high resolution to preserve detail (e.g., 300 DPI)
                Resolution = 300,
                // Render all pages; each page will be saved as a separate PNG file
                PageIndex = 0,
                PageCount = diagram.Pages.Count,
                // Optional: keep original page size
                EnlargePage = false
            };

            // Save the diagram pages as PNG images.
            // For multi‑page diagrams, Aspose.Diagram appends the page index to the file name.
            diagram.Save("output.png", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
