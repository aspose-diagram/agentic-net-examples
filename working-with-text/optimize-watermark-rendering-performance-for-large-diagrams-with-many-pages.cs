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

            // Load the source Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Watermark configuration
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#CCCCCC"; // Light gray
            double fontSizePoints = 72; // 72 points = 1 inch
            double fontSizeInches = fontSizePoints / 72.0;

            // Add the watermark to every page – minimal per‑page work for performance
            foreach (Page page in diagram.Pages)
            {
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a full‑page text shape; the shape will be rendered as a watermark
                page.AddText(0, 0, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInches);
            }

            // Save the diagram as PDF with a fallback font
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
