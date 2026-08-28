using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file.
            Diagram diagram = new Diagram("input.vsd");

            // Configure HTML save options.
            // SaveAsSingleFile = true embeds all resources (including SVG) directly into the HTML.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true,
                ExportHiddenPage = false,
                ExportGuideShapes = false,
                // Optional: set resolution, page size, etc., as needed.
            };

            // Export the diagram to an HTML file with inline SVG markup.
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
