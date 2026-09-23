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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Set a custom document title
            diagram.DocumentProps.Title = "My Custom PDF Title";

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Export hidden pages
            pdfOptions.ExportHiddenPage = true;
            // Specify a fallback font (Aspose.Diagram will embed fonts when possible)
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF with the configured options
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
