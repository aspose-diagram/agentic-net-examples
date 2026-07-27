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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options to exclude hidden pages (layers are respected by visibility)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.ExportHiddenPage = false; // Do not export hidden pages/layers

            // Save the diagram as HTML using the configured options
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
