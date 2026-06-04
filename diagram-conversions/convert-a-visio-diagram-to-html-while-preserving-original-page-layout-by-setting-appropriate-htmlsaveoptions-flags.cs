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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options to keep the original page layout
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                EnlargePage = false,            // Do not enlarge the page; keep original size
                SaveAsSingleFile = true,        // Save everything into a single HTML file
                SaveForegroundPagesOnly = false,// Include background pages as well
                ExportHiddenPage = false,       // Do not export hidden pages
                SaveToolBar = true,             // Keep the toolbar in the HTML output
                Resolution = 96                 // Standard screen resolution (optional)
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
