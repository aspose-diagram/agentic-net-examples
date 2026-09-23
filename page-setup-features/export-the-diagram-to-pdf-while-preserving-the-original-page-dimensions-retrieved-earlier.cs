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

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Path for the exported PDF
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve and display original page dimensions (in inches)
            foreach (Page page in diagram.Pages)
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;
                Console.WriteLine($"Page \"{page.Name}\" dimensions: {width} x {height} inches");
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";               // Fallback font
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;     // Explicitly set format
            // pdfOptions.ExportHiddenPage = false;          // Optional: exclude hidden pages

            // Export the diagram to PDF while preserving page sizes
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram exported to PDF: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
