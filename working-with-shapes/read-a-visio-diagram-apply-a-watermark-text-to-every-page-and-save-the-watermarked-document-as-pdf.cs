using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (you can modify or pass via command line)
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPdfPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Watermark settings
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Calibri";
                string fontColor = "#A0A0A0"; // light gray
                double fontSizeInPoints = 72; // 1 inch (72 points)
                double fontSizeInInches = fontSizeInPoints / 72.0;

                // Apply watermark to each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Position the watermark at the center of the page
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Use the full page size for the text box so the text can be centered
                    double textBoxWidth = pageWidth;
                    double textBoxHeight = pageHeight;

                    // Add the watermark text shape
                    // AddText(pinX, pinY, width, height, text, fontName, fontColor, fontSize)
                    page.AddText(pinX, pinY, textBoxWidth, textBoxHeight, watermarkText, fontName, fontColor, fontSizeInInches);
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // fallback font if needed

                // Save the diagram as PDF
                diagram.Save(outputPdfPath, pdfOptions);

                Console.WriteLine($"Watermarked PDF saved to: {outputPdfPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }