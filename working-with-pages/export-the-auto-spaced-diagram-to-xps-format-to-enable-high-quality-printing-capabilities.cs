using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToXps
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (auto‑spaced diagram) from a file
            Diagram diagram = new Diagram("input.vsd");

            // Create XPS save options – can customize if needed
            XPSSaveOptions xpsOptions = new XPSSaveOptions
            {
                // Example: do not export hidden pages
                ExportHiddenPage = false,
                // Export all pages (default)
                PageIndex = 0,
                PageCount = int.MaxValue,
                // Save only foreground pages (default)
                SaveForegroundPagesOnly = true
            };

            // Save the diagram to XPS format using the specified options
            diagram.Save("output.xps", xpsOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
