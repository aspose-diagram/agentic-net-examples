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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Export the diagram to PDF using the built‑in SaveFileFormat enum
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

            // Configure XPS save options (e.g., include hidden pages)
            XPSSaveOptions xpsOptions = new XPSSaveOptions
            {
                ExportHiddenPage = true
            };

            // Export the diagram to XPS using the configured options
            diagram.Save("output.xps", xpsOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
