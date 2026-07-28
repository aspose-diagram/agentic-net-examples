using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio diagram file (can be changed as needed)
                string diagramPath = "input.vsdx";

                // Output PDF file with watermark
                string outputPdfPath = "output_watermarked.pdf";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Prepare PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    // Save diagram to a memory stream as PDF
                    using (MemoryStream pdfStream = new MemoryStream())
                    {
                        diagram.Save(pdfStream, pdfOptions);
                        pdfStream.Position = 0; // Reset stream position for reading

                        // Load the generated PDF using Aspose.Pdf (fully qualified types)
                        Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfStream);

                        // Iterate through each page and add a watermark
                        foreach (Aspose.Pdf.Page page in pdfDocument.Pages)
                        {
                            // Create a text fragment for the watermark
                            Aspose.Pdf.Text.TextFragment watermark = new Aspose.Pdf.Text.TextFragment("CONFIDENTIAL");

                            // Set watermark appearance
                            watermark.TextState.FontSize = 72; // points
                            watermark.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
                            watermark.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.78, 0.78, 0.78); // light gray
                            watermark.TextState.Rotation = (float)45; // degrees as float

                            // Position the watermark at the center of the page
                            double centerX = page.PageInfo.Width / 2;
                            double centerY = page.PageInfo.Height / 2;
                            watermark.Position = new Aspose.Pdf.Text.Position(centerX, centerY);

                            // Add the watermark to the page
                            page.Paragraphs.Add(watermark);
                        }

                        // Save the final PDF with watermarks
                        pdfDocument.Save(outputPdfPath);
                    }
                }

                Console.WriteLine($"PDF saved with watermark to: {outputPdfPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }