using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportVisioPage
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = @"input.vsdx";

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram(sourceFile);

            // Index of the page to export (0‑based). Change as needed.
            int pageIndex = 0;

            // Configure PNG export options with custom DPI
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Set the desired resolution (dots per inch)
                Resolution = 300,

                // Specify which page to render
                PageIndex = pageIndex
            };

            // Export the selected page as a high‑resolution PNG (uses the provided save rule)
            string outputFile = @"output_page_" + pageIndex + ".png";
            diagram.Save(outputFile, pngOptions);

            Console.WriteLine($"Page {pageIndex} exported to {outputFile} at {pngOptions.Resolution} DPI.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
