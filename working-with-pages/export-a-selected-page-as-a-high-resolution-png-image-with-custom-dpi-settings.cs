using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportPageAsHighResPng
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourcePath = "input.vsdx";

            // Path for the exported PNG image
            string outputPath = "page_2_highres.png";

            // Index of the page to export (0‑based). For example, export page 2.
            int pageIndex = 1;

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Configure image save options for PNG with custom DPI
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Set desired resolution (dots per inch)
                Resolution = 300f,          // Horizontal and vertical DPI

                // Export only the selected page
                PageIndex = pageIndex,      // First page to render (0‑based)
                PageCount = 1,              // Number of pages to render

                // Optional: keep original page size without enlargement
                EnlargePage = false
            };

            // Save the selected page as a high‑resolution PNG
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
