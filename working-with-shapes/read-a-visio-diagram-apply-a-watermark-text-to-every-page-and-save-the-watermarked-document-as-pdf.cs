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
            // Path for the output PDF file
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add a watermark text shape
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define watermark properties
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Calibri";
                string fontColor = "#CCCCCC"; // Light gray
                double fontSizeInInches = 0.5; // Approx. 36 points

                // Add a text shape that covers the whole page.
                // PinX and PinY are set to 0 to start at the lower-left corner.
                page.AddText(0, 0, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInInches);
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as a PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
