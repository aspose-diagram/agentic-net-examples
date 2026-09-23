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

            // Paths to the source Visio file and the target PDF file
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options to preserve vector graphics and text quality
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";               // Fallback font if needed
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;     // Explicitly set the format
            pdfOptions.ExportHiddenPage = false;            // Do not export hidden pages

            // Save the diagram as a PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved as PDF: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
