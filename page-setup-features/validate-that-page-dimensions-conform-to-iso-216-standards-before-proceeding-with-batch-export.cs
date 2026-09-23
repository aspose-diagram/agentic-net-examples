using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // ISO 216 A-series sizes in inches (width x height)
        private static readonly (double Width, double Height)[] IsoASeries = new (double, double)[]
        {
            (33.11, 46.81), // A0
            (23.39, 33.11), // A1
            (16.54, 23.39), // A2
            (11.69, 16.54), // A3
            (8.27, 11.69),  // A4
            (5.83, 8.27),   // A5
            (4.13, 5.83),   // A6
            (2.91, 4.13),   // A7
            (2.05, 2.91),   // A8
            (1.46, 2.05),   // A9
            (1.02, 1.46)    // A10
        };

        // Tolerance for floating‑point comparison (in inches)
        private const double Tolerance = 0.02;

        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Validate each page size against ISO 216 A‑series
                foreach (Page page in diagram.Pages)
                {
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    if (!IsIsoASeriesSize(pageWidth, pageHeight))
                    {
                        string message = $"Page \"{page.Name}\" (ID={page.ID}) has non‑ISO dimensions: " +
                                         $"{pageWidth:F2}\" x {pageHeight:F2}\".";
                        // Stop processing and report the problem
                        throw new Exception(message);
                    }
                }

                // All pages are valid – proceed with batch export
                // Example: export each page to a separate PDF file
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    string outputPath = $"Page_{pageIndex + 1}.pdf";

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Export only the current page
                        PageIndex = pageIndex,
                        PageCount = 1,
                        // Use a default font to avoid missing‑font warnings
                        DefaultFont = "Arial"
                    };

                    // Save the diagram (only the selected page) as PDF
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Exported page {pageIndex + 1} to \"{outputPath}\".");
                    pageIndex++;
                }

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Checks whether the given dimensions match any ISO A‑series size (allowing for tolerance)
        private static bool IsIsoASeriesSize(double width, double height)
        {
            foreach (var (isoWidth, isoHeight) in IsoASeries)
            {
                if (Math.Abs(width - isoWidth) <= Tolerance && Math.Abs(height - isoHeight) <= Tolerance)
                    return true;

                // Also accept rotated orientation (height as width, width as height)
                if (Math.Abs(width - isoHeight) <= Tolerance && Math.Abs(height - isoWidth) <= Tolerance)
                    return true;
            }
            return false;
        }
    }