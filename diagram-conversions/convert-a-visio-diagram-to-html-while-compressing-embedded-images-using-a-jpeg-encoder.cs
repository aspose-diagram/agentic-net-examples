using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToHtmlConverter
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceVisioPath = "input.vsdx";

            // Path where the resulting HTML will be saved
            string outputHtmlPath = "output.html";

            // Load the Visio diagram from file (uses the provided Diagram constructor)
            Diagram diagram = new Diagram(sourceVisioPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Save the HTML as a single file (images will be embedded as base‑64 strings)
            htmlOptions.SaveAsSingleFile = true;

            // Reduce the resolution to lower the size of generated images.
            // A lower DPI results in smaller, more compressed images.
            htmlOptions.Resolution = 96; // 96 DPI is a common web‑friendly value

            // Optional: limit the number of pages to render (0 = all pages)
            // htmlOptions.PageCount = 0;

            // NOTE:
            // Aspose.Diagram renders embedded images in JPEG format by default when
            // generating HTML. By setting a lower resolution (and optionally
            // SaveAsSingleFile) the resulting JPEG images are automatically compressed.

            // Save the diagram as HTML using the configured options (uses the provided Save method)
            diagram.Save(outputHtmlPath, htmlOptions);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Visio diagram successfully converted to HTML with compressed JPEG images.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
