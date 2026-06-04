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

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create HTML save options.
            // PNG is the default image format for HTML export, so no additional configuration is required.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Example of setting other optional properties (optional, not required for PNG format)
            // htmlOptions.ExportHiddenPage = false;
            // htmlOptions.IsExportComments = false;

            // Save the diagram as HTML. All images embedded in the HTML will be rendered as PNG.
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
