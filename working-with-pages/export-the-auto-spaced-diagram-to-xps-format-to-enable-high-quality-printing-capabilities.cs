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

            // Load the source diagram (replace with your actual file path)
            Diagram diagram = new Diagram("inputDiagram.vsdx");

            // Initialize XPS save options
            XPSSaveOptions xpsOptions = new XPSSaveOptions();
            // Example: do not export hidden pages (optional)
            xpsOptions.ExportHiddenPage = false;
            // Example: export only foreground pages (optional)
            xpsOptions.SaveForegroundPagesOnly = true;
            // You can set other options such as DefaultFont, PageCount, etc., if needed

            // Save the diagram to XPS format using the specified options
            diagram.Save("outputDiagram.xps", xpsOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
