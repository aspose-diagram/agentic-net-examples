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

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Index of the page to export (0‑based). Change as needed.
            int pageIndexToExport = 2; // example: export the third page

            // Configure image save options for PNG with custom DPI
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Set the desired resolution (dots per inch). 300 DPI is typical for high‑resolution output.
                Resolution = 300f,

                // Export only the selected page
                PageIndex = pageIndexToExport,
                PageCount = 1,

                // Optional: ensure hidden pages are not exported (set to false if you only want visible pages)
                ExportHiddenPage = false
            };

            // Output file path
            string outputPath = $"Page_{pageIndexToExport + 1}_HighRes.png";

            // Save the selected page as a high‑resolution PNG image
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
