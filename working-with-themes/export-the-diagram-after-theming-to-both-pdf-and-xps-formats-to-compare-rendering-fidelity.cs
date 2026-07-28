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

            // Export the themed diagram to PDF
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

            // Export the same diagram to XPS using XPSSaveOptions
            XPSSaveOptions xpsOptions = new XPSSaveOptions();
            // Example option: do not export hidden pages (default is false)
            xpsOptions.ExportHiddenPage = false;
            diagram.Save("output.xps", xpsOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
