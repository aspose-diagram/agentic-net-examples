using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Add page number placeholder to the footer (will be replaced per page)
        diagram.HeaderFooter.FooterRight = "Page: &p";

        // Set PDF save options (optional: specify default font)
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";

        // Export the diagram to PDF
        diagram.Save(outputPath, pdfOptions);

        Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
    }
}
