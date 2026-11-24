using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string visioPath = args[0];
        string pdfPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;               // Do not export hidden pages.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;        // Explicitly set the format tracker.

                // Save the diagram as PDF.
                diagram.Save(pdfPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during Visio to PDF conversion: {ex.Message}");
            return;
        }

        try
        {
            // Open the generated PDF using Aspose.Pdf (fully qualified types, no using directive).
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfPath);

            // Iterate over each page to add a watermark.
            for (int i = 1; i <= pdfDocument.Pages.Count; i++)
            {
                Aspose.Pdf.Page page = pdfDocument.Pages[i];

                // Create a TextFragment as the watermark.
                Aspose.Pdf.Text.TextFragment watermark = new Aspose.Pdf.Text.TextFragment("CONFIDENTIAL");
                watermark.TextState.FontSize = 72;                     // Large font size.
                watermark.TextState.Rotation = (float)45;             // Rotate 45 degrees.
                // Position the watermark at the center of the page.
                double centerX = page.PageInfo.Width / 2;
                double centerY = page.PageInfo.Height / 2;
                watermark.Position = new Aspose.Pdf.Text.Position(centerX, centerY);

                // Add the watermark to the page's paragraphs collection.
                page.Paragraphs.Add(watermark);
            }

            // Save the watermarked PDF, overwriting the previous file.
            pdfDocument.Save(pdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error adding watermark to PDF: {ex.Message}");
            return;
        }

        Console.WriteLine($"PDF successfully created with watermark: {pdfPath}");
    }
}