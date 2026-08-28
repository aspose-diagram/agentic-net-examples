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

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Add page number field to the footer (Visio uses '&p' for current page number)
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.SaveAsSingleFile = false;      // Generate a separate HTML file for each page
            htmlOptions.ExportHiddenPage = false;      // Do not export hidden pages
            htmlOptions.IsExportComments = false;      // Optional: exclude comments

            // Save the diagram as HTML
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
