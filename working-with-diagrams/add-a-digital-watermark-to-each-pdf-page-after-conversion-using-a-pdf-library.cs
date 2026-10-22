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

        string inputVisioPath = args[0];
        string outputPdfPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputVisioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;               // Do not export hidden pages. (bool, not BOOL)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;        // Explicitly set format tracker.
                pdfOptions.DefaultFont = "Arial";                  // Fallback font if needed.

                // Save the diagram to PDF.
                diagram.Save(outputPdfPath, pdfOptions);
            }

            // Open the generated PDF with Aspose.Pdf (fully qualified types to avoid ambiguity).
            using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(outputPdfPath))
            {
                // Iterate over each page to add a watermark.
                for (int i = 1; i <= pdfDocument.Pages.Count; i++)
                {
                    Aspose.Pdf.Page page = pdfDocument.Pages[i];

                    // Create a text fragment for the watermark.
                    Aspose.Pdf.Text.TextFragment watermark = new Aspose.Pdf.Text.TextFragment("CONFIDENTIAL");
                    watermark.TextState.FontSize = 72;                                 // Large font size.
                    // Font is optional; default font will be used.
                    watermark.TextState.Rotation = (float)45;                         // Rotate 45 degrees.
                    watermark.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.78, 0.78, 0.78); // Light gray.

                    // Position the watermark at the center of the page.
                    double centerX = page.PageInfo.Width / 2;
                    double centerY = page.PageInfo.Height / 2;
                    watermark.Position = new Aspose.Pdf.Text.Position(centerX, centerY);

                    // Add the watermark to the page's paragraphs collection.
                    page.Paragraphs.Add(watermark);
                }

                // Save the modified PDF (overwrites the original file).
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine("PDF conversion and watermarking completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
