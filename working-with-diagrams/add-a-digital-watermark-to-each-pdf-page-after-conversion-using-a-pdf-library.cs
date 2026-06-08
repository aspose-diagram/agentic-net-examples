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

                // Input Visio file path (replace with actual path)
                string visioPath = "input.vsdx";

                // Temporary PDF generated from Visio
                string tempPdfPath = "temp.pdf";

                // Final PDF with watermark
                string outputPdfPath = "output.pdf";

                // 1. Load the Visio diagram and save it as PDF using Aspose.Diagram
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Ensure a fallback font is set to avoid missing glyphs
                        DefaultFont = "Arial"
                    };

                    // Save the diagram to a temporary PDF file
                    diagram.Save(tempPdfPath, pdfOptions);
                }

                // 2. Open the generated PDF with Aspose.Pdf (fully qualified types to avoid ambiguity)
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(tempPdfPath);

                // 3. Add a digital watermark to each page
                foreach (Aspose.Pdf.Page page in pdfDocument.Pages)
                {
                    // Create a text fragment that will serve as the watermark
                    Aspose.Pdf.Text.TextFragment watermark = new Aspose.Pdf.Text.TextFragment("Confidential");

                    // Set visual properties of the watermark
                    watermark.TextState.FontSize = 72;                                   // Large font size
                    watermark.TextState.Rotation = (float)45;                            // Rotate 45 degrees
                    watermark.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.78, 0.78, 0.78); // Light gray

                    // Position the watermark at the center of the page
                    // PageInfo provides page dimensions in points
                    double centerX = page.PageInfo.Width / 2;
                    double centerY = page.PageInfo.Height / 2;
                    watermark.Position = new Aspose.Pdf.Text.Position(centerX, centerY);

                    // Add the watermark to the page's paragraph collection
                    page.Paragraphs.Add(watermark);
                }

                // 4. Save the final PDF with watermarks
                pdfDocument.Save(outputPdfPath);

                // 5. Clean up the temporary PDF file
                if (File.Exists(tempPdfPath))
                {
                    File.Delete(tempPdfPath);
                }

                Console.WriteLine($"Watermarked PDF saved to: {outputPdfPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }