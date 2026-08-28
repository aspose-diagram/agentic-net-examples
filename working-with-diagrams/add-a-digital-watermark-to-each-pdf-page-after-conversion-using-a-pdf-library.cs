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
            string visioPath = "input.vsdx";

            // Output PDF file path
            string pdfPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Save the diagram as PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save(pdfPath, pdfOptions);

            // Load the generated PDF using Aspose.Pdf (fully qualified)
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfPath);

            // Add a watermark to each page
            foreach (Aspose.Pdf.Page page in pdfDocument.Pages)
            {
                // Create the watermark text fragment
                Aspose.Pdf.Text.TextFragment watermark = new Aspose.Pdf.Text.TextFragment("CONFIDENTIAL");

                // Set font size
                watermark.TextState.FontSize = 72;

                // Set rotation (float, degrees)
                watermark.TextState.Rotation = (float)45;

                // Set light gray color (values are 0.0‑1.0)
                watermark.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.78, 0.78, 0.78);

                // Position the watermark at the center of the page
                double centerX = page.MediaBox.Width / 2;
                double centerY = page.MediaBox.Height / 2;
                watermark.Position = new Aspose.Pdf.Text.Position(centerX, centerY);

                // Add the watermark to the page
                page.Paragraphs.Add(watermark);
            }

            // Save the PDF with watermarks (overwrites the previous file)
            pdfDocument.Save(pdfPath);

            // Clean up resources
            diagram.Dispose();
            pdfDocument.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
