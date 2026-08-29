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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page and add a watermark text shape
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Use the full page size for the text shape so it spans the page
                    double shapeWidth = pageWidth;
                    double shapeHeight = pageHeight;

                    // Watermark properties
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Arial";
                    string fontColor = "#CCCCCC"; // Light gray
                    double fontSizeInInches = 0.5; // Approx. 36 points (0.5 inch)

                    // Add the watermark text shape to the page
                    page.AddText(pinX, pinY, shapeWidth, shapeHeight, watermarkText, fontName, fontColor, fontSizeInInches);
                }

                // Configure PDF save options (optional: set default font)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as a PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Watermarked PDF saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
