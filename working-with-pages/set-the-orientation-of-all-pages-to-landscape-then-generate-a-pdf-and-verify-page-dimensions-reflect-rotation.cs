using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output PDF path (same folder, same name with .pdf extension)
        string outputPdfPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set orientation of every page to Landscape
                foreach (Page page in diagram.Pages)
                {
                    // PrintPageOrientationValue.Landscape sets the page orientation
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Prepare PDF save options and explicitly set the format
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF
                diagram.Save(outputPdfPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
            return;
        }

        try
        {
            // Open the generated PDF using Aspose.Pdf (fully qualified to avoid namespace conflicts)
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(outputPdfPath);

            // Verify each PDF page dimensions reflect Landscape orientation (width > height)
            foreach (Aspose.Pdf.Page pdfPage in pdfDocument.Pages)
            {
                double width = pdfPage.PageInfo.Width;   // Width in points
                double height = pdfPage.PageInfo.Height; // Height in points

                Console.WriteLine($"PDF Page {pdfPage.Number}: Width = {width} pt, Height = {height} pt");

                if (width <= height)
                {
                    Console.Error.WriteLine($"Verification failed: Page {pdfPage.Number} is not landscape (width <= height).");
                }
                else
                {
                    Console.WriteLine($"Verification passed: Page {pdfPage.Number} is landscape.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error verifying PDF file: {ex.Message}");
        }
    }
}