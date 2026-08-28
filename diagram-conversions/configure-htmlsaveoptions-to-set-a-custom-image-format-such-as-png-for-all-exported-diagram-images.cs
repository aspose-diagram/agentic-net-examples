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

            // Load an existing Visio diagram.
            // Replace "input.vsdx" with the path to your source file.
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML export options.
            // PNG is the default image format for HTML export, so no explicit
            // image format property exists. The options object can be used to
            // control other aspects such as resolution, hidden pages, etc.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Example: set resolution to 96 DPI (default is 96).
                Resolution = 96,
                // Ensure hidden pages are not exported.
                ExportHiddenPage = false,
                // Export comments if needed.
                IsExportComments = false
            };

            // Save the diagram as HTML. All images embedded in the HTML will be PNG.
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
