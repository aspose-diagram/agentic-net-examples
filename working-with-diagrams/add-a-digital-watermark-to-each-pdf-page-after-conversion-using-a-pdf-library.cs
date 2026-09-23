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

            // Paths for input Visio diagram and output PDF
            string diagramPath = "input.vsdx";
            string pdfPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                // Explicitly set the format tracker
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF
                diagram.Save(pdfPath, pdfOptions);
            }

            // Open the generated PDF with Aspose.Pdf
            using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfPath))
            {
                // Iterate through each page and add a watermark
                for (int i = 1; i <= pdfDocument.Pages.Count; i++)
                {
                    Aspose.Pdf.Page page = pdfDocument.Pages[i];

                    // Create a text fragment that will serve as the watermark
                    Aspose.Pdf.Text.TextFragment watermark = new Aspose.Pdf.Text.TextFragment("CONFIDENTIAL");
                    // Set font and style
                    watermark.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial");
                    watermark.TextState.FontSize = 72; // points
                    watermark.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
                    // Set color (light gray) – values are 0.0 to 1.0
                    watermark.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.78, 0.78, 0.78);
                    // Rotate the watermark 45 degrees
                    watermark.TextState.Rotation = (float)45;
                    // Position the watermark at the center of the page
                    double centerX = page.PageInfo.Width / 2;
                    double centerY = page.PageInfo.Height / 2;
                    watermark.Position = new Aspose.Pdf.Text.Position(centerX, centerY);

                    // Add the watermark to the page's paragraphs collection
                    page.Paragraphs.Add(watermark);
                }

                // Save the PDF with watermarks (overwrites the original file)
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine("PDF generated with digital watermark on each page.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
