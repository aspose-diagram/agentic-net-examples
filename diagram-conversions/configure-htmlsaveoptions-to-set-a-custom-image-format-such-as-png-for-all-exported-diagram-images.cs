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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options.
            // PNG is the default image format for HTML export, so no explicit property is needed.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Example of additional settings (optional)
                ExportHiddenPage = false,
                IsExportComments = false
            };

            // Save the diagram as HTML. All images will be rendered in PNG format.
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
