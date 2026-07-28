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

            // Load the Visio diagram (auto‑spaced diagram)
            Diagram diagram = new Diagram("input.vsd");

            // Configure XPS save options
            XPSSaveOptions xpsOptions = new XPSSaveOptions
            {
                // Do not export hidden pages (optional, adjust as needed)
                ExportHiddenPage = false,
                // Save only foreground pages for a cleaner output (optional)
                SaveForegroundPagesOnly = true
            };

            // Save the diagram to XPS format for high‑quality printing
            diagram.Save("output.xps", xpsOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
