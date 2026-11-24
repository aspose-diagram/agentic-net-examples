using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx>");
            return;
        }

        string visioPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input file not found: {visioPath}");
            return;
        }

        // Define the output PDF path for the full document.
        string fullPdfPath = Path.ChangeExtension(visioPath, ".pdf");

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through each page to validate orientation after PDF conversion.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the page object.
                    Page page = diagram.Pages[i];

                    // Get the page width and height (in inches) from the page properties.
                    double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Determine the intended orientation from the print settings.
                    PrintPageOrientationValue orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                    bool isVisioLandscape = orientation == PrintPageOrientationValue.Landscape;

                    // Configure PDF save options to export only the current page.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicit format tracker.
                    pdfOptions.PageIndex = i;   // Zero‑based page index.
                    pdfOptions.PageCount = 1;   // Export a single page.

                    // Create a temporary PDF file for this page.
                    string tempPdfPath = Path.Combine(Path.GetTempPath(),
                        $"temp_page_{i}_{Guid.NewGuid()}.pdf");

                    // Save the current page to the temporary PDF.
                    diagram.Save(tempPdfPath, pdfOptions);

                    // Load the generated PDF using Aspose.Pdf (fully qualified to avoid ambiguity).
                    Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(tempPdfPath);
                    // PDF pages are 1‑based.
                    Aspose.Pdf.Page pdfPage = pdfDoc.Pages[1];

                    // Retrieve PDF page dimensions (in points). 1 point = 1/72 inch.
                    double pdfWidthPoints = pdfPage.PageInfo.Width;
                    double pdfHeightPoints = pdfPage.PageInfo.Height;

                    // Convert PDF dimensions to inches for a fair comparison.
                    double pdfWidthInches = pdfWidthPoints / 72.0;
                    double pdfHeightInches = pdfHeightPoints / 72.0;

                    bool isPdfLandscape = pdfWidthInches > pdfHeightInches;

                    // Validate orientation by comparing the expected and actual landscape flags.
                    if (isVisioLandscape != isPdfLandscape)
                    {
                        string message = $"Orientation mismatch on page {i + 1}: " +
                                         $"Visio = {(isVisioLandscape ? "Landscape" : "Portrait")}, " +
                                         $"PDF = {(isPdfLandscape ? "Landscape" : "Portrait")} " +
                                         $"(PDF size {pdfWidthInches:F2}in x {pdfHeightInches:F2}in)";
                        Console.Error.WriteLine(message);
                        // Throw to stop further processing as per original intent.
                        throw new Exception(message);
                    }
                    else
                    {
                        Console.WriteLine($"Page {i + 1} orientation validated successfully.");
                    }

                    // Clean up the temporary PDF file.
                    try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
                }

                // After all pages are validated, save the full diagram as a single PDF.
                PdfSaveOptions fullPdfOptions = new PdfSaveOptions();
                fullPdfOptions.SaveFormat = SaveFileFormat.Pdf;
                diagram.Save(fullPdfPath, fullPdfOptions);
                Console.WriteLine($"Full PDF saved to: {fullPdfPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
