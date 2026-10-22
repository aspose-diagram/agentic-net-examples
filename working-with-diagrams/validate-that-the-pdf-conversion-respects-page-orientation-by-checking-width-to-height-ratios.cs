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

        // Array to hold original page width/height ratios.
        double[] originalRatios;

        // Load Visio diagram, extract page dimensions, and save as PDF.
        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Capture width‑to‑height ratios for each page.
                originalRatios = new double[diagram.Pages.Count];
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;
                    originalRatios[i] = width / height; // >1 => Landscape, <1 => Portrait
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicit format tracker
                // Optional: set a default font to avoid missing‑font warnings.
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF.
                diagram.Save(pdfPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during Visio to PDF conversion: {ex.Message}");
            return;
        }

        // Verify that the generated PDF respects the original page orientations.
        try
        {
            // Load the PDF using Aspose.Pdf (fully qualified to avoid ambiguity).
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfPath);

            const double epsilon = 0.01; // tolerance for floating‑point comparison

            // PDF pages are 1‑based indexed.
            for (int i = 1; i <= pdfDocument.Pages.Count; i++)
            {
                Aspose.Pdf.Page pdfPage = pdfDocument.Pages[i];
                // Use PageInfo to obtain dimensions (Width/Height are not direct members).
                double pdfWidth = pdfPage.PageInfo.Width;   // corrected: use PageInfo
                double pdfHeight = pdfPage.PageInfo.Height; // corrected: use PageInfo
                double pdfRatio = pdfWidth / pdfHeight;

                int visioIndex = i - 1; // convert to 0‑based index for the original ratios array
                if (visioIndex >= originalRatios.Length)
                {
                    Console.Error.WriteLine($"PDF contains more pages than the source Visio document (PDF page {i}).");
                    continue;
                }

                double visioRatio = originalRatios[visioIndex];

                bool visioLandscape = visioRatio > 1.0 + epsilon;
                bool pdfLandscape = pdfRatio > 1.0 + epsilon;

                if (visioLandscape != pdfLandscape)
                {
                    string visioOrient = visioLandscape ? "Landscape" : "Portrait";
                    string pdfOrient = pdfLandscape ? "Landscape" : "Portrait";
                    Console.Error.WriteLine($"Orientation mismatch on page {i}: Visio={visioOrient}, PDF={pdfOrient}");
                    throw new Exception($"Page {i} orientation validation failed.");
                }
            }

            Console.WriteLine("All pages orientation validated successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF orientation validation: {ex.Message}");
        }
    }
}
