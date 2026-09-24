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
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set the footer text to the current date (centered on each page)
            diagram.HeaderFooter.FooterCenter = DateTime.Now.ToString("yyyy-MM-dd");

            // Optional: adjust footer margin (distance from page edge) if needed
            // diagram.HeaderFooter.FooterMargin.Value = 0.2; // inches

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";               // fallback font
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;     // ensure correct format

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
