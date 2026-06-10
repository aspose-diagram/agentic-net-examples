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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output HTML file path
            string outputPath = "output.html";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Set HTML save options to include hidden pages
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.ExportHiddenPage = true;

            // Save the diagram as HTML
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine("Diagram saved to HTML with hidden pages preserved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
