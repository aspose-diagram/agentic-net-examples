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

            // Path for the exported PDF file
            string outputPath = "output.pdf";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve and display original page dimensions (in inches)
            foreach (Page page in diagram.Pages)
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;
                Console.WriteLine($"Page {page.ID}: Width = {width} in, Height = {height} in");
            }

            // Configure PDF save options to preserve original page size
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";          // fallback font for missing characters
            pdfOptions.EnlargePage = false;            // keep original dimensions
            pdfOptions.ExportHiddenPage = false;       // optional: exclude hidden pages

            // Export the diagram to PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Export to PDF completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
