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

            // Paths for input Visio file and output PDF
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Modify each page's dimensions (example: set to A4 size in inches)
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = 8.27;   // Width in inches
                page.PageSheet.PageProps.PageHeight.Value = 11.69; // Height in inches
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";          // Fallback font
            pdfOptions.ExportHiddenPage = false;       // Do not export hidden pages
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format tracker

            // Export the diagram to PDF while preserving layout
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram successfully exported to PDF.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
