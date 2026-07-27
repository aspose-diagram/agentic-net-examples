using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string inputPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Insert page number field code into the footer (right side)
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            // Generate a separate HTML file for each page
            htmlOptions.SaveAsSingleFile = false;
            // Do not export hidden pages (optional)
            htmlOptions.ExportHiddenPage = false;

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
