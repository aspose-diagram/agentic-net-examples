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
                // Preserve original page size (do not enlarge to fit content)
                EnlargePage = false,
                // Export all pages, not only the foreground ones
                SaveForegroundPagesOnly = false,
                // Do not include hidden pages in the output
                ExportHiddenPage = false,
                // Generate separate HTML files per page (not a single combined file)
                SaveAsSingleFile = false,
                // Keep the toolbar in the generated HTML (optional)
                SaveToolBar = true,
                // Render starting from the first page
                PageIndex = 0,
                // Render all pages
                PageCount = int.MaxValue
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
