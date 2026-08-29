using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the exported PDF file
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Add page number field to the right side of the footer for all pages
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Configure PDF save options (optional: set a default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Export the diagram to PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Export completed: PDF with page numbers created.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
