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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the HTML output
            string outputPath = "output.html";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options to preserve hidden pages
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.ExportHiddenPage = true;

            // Save the diagram as HTML
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
