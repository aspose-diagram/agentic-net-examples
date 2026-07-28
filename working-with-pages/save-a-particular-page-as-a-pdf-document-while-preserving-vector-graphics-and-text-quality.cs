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

            // Output PDF file path (will contain the selected page)
            string outputPath = "selected_page.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Fallback font to ensure text renders correctly
            pdfOptions.DefaultFont = "Arial";
            // Export only the first page (zero‑based index)
            pdfOptions.PageIndex = 0;
            pdfOptions.PageCount = 1;
            // Explicitly set the save format
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the selected page as a PDF preserving vector graphics and text quality
            diagram.Save(outputPath, pdfOptions);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Page saved to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
