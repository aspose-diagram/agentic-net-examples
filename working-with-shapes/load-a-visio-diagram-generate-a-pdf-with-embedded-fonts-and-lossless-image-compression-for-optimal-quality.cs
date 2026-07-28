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

            // Path for the generated PDF
            string outputPath = "output.pdf";

            // Configure font folder (required before loading the diagram)
            // The second argument indicates whether to search sub‑folders recursively.
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Ensure the format is explicitly set (required to avoid ambiguity)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Set a default fallback font in case a diagram font is missing
                pdfOptions.DefaultFont = "Arial";

                // Use lossless Flate compression for PDF content streams (including text)
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Do not export hidden pages (optional, improves file size)
                pdfOptions.ExportHiddenPage = false;

                // Save the diagram as PDF with the configured options
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("PDF generated successfully at: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
