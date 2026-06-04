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

            // Load an existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Configure HTML save options to embed CSS styles directly in the HTML output
            var htmlOptions = new HTMLSaveOptions
            {
                // Save as a single HTML file with embedded CSS and other resources
                SaveAsSingleFile = true,
                // Optional: do not export hidden pages
                ExportHiddenPage = false
            };

            // Save the diagram as HTML using the configured options
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
