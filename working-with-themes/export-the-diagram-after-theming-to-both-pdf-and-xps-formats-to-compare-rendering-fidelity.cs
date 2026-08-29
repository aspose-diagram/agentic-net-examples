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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Export the diagram to PDF format
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

            // Prepare XPS save options (customize as needed)
            XPSSaveOptions xpsOptions = new XPSSaveOptions
            {
                // Example: include hidden pages in the XPS output
                ExportHiddenPage = true
            };

            // Export the diagram to XPS format using the specified options
            diagram.Save("output.xps", xpsOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
