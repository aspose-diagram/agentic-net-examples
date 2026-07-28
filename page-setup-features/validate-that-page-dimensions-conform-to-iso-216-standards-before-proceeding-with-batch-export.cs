using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // ISO 216 A series sizes in millimeters (width x height)
        private static readonly (double WidthMm, double HeightMm)[] IsoASeries = new (double, double)[]
        {
            (841, 1189), // A0
            (594, 841),  // A1
            (420, 594),  // A2
            (297, 420),  // A3
            (210, 297),  // A4
            (148, 210),  // A5
            (105, 148),  // A6
            (74, 105),   // A7
            (52, 74),    // A8
            (37, 52),    // A9
            (26, 37)     // A10
        };

        // Conversion factor from millimeters to inches
        private const double MmToInch = 1.0 / 25.4;

        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(sourcePath))
                {
                    // Validate each page size against ISO 216 standards
                    foreach (Page page in diagram.Pages)
                    {
                        double pageWidthIn = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeightIn = page.PageSheet.PageProps.PageHeight.Value;

                        // Ensure width is the smaller dimension (portrait orientation)
                        double widthIn = Math.Min(pageWidthIn, pageHeightIn);
                        double heightIn = Math.Max(pageWidthIn, pageHeightIn);

                        bool matchesIso = false;
                        const double toleranceIn = 0.01; // ~0.25 mm tolerance

                        foreach (var (wMm, hMm) in IsoASeries)
                        {
                            double wIn = wMm * MmToInch;
                            double hIn = hMm * MmToInch;

                            if (Math.Abs(widthIn - wIn) <= toleranceIn && Math.Abs(heightIn - hIn) <= toleranceIn)
                            {
                                matchesIso = true;
                                break;
                            }
                        }

                        if (!matchesIso)
                        {
                            throw new Exception($"Page \"{page.Name}\" (ID {page.ID}) size {pageWidthIn:F3}\" x {pageHeightIn:F3}\" does not conform to any ISO 216 A‑series dimensions.");
                        }
                    }

                    // All pages validated – proceed with batch export (PDF in this example)
                    string outputPath = "output.pdf";

                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Ensure a default font is set to avoid missing‑font issues
                        DefaultFont = "Arial",
                        // Export hidden pages if needed (set to false to exclude)
                        ExportHiddenPage = true
                    };

                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine($"Diagram successfully exported to \"{outputPath}\".");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }