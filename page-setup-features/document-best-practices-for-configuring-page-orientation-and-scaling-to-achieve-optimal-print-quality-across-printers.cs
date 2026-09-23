using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Path to the output PDF file
                string outputPath = "output.pdf";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(sourcePath))
                {
                    // Iterate through all pages to apply consistent print settings
                    foreach (Page page in diagram.Pages)
                    {
                        // ------------------------------------------------------------
                        // 1. Set page orientation
                        // ------------------------------------------------------------
                        // Choose Landscape for wide diagrams or Portrait for tall diagrams.
                        // SameAsPrinter lets the printer decide based on its default.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // ------------------------------------------------------------
                        // 2. Configure scaling
                        // ------------------------------------------------------------
                        // ScaleX and ScaleY are expressed as a factor (1.0 = 100%).
                        // Reducing the factor can help fit large diagrams on smaller paper.
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75; // 75% width
                        page.PageSheet.PrintProps.ScaleY.Value = 0.75; // 75% height

                        // ------------------------------------------------------------
                        // 3. Fit to sheet (optional)
                        // ------------------------------------------------------------
                        // When enabled, the diagram is forced onto a single sheet.
                        // PagesX and PagesY define how many pages across/down the output spans.
                        page.PageSheet.PrintProps.OnPage.Value = BOOL.True; // Enable fit‑to‑sheet
                        page.PageSheet.PrintProps.PagesX.Value = 1;          // One page horizontally
                        page.PageSheet.PrintProps.PagesY.Value = 1;          // One page vertically

                        // ------------------------------------------------------------
                        // 4. Set printer margins
                        // ------------------------------------------------------------
                        // Margins are specified in inches. Typical printers have ~0.25" non‑printable area.
                        // Adjust as needed for the target printer model.
                        page.PageSheet.PrintProps.PageTopMargin.Value = 0.25;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = 0.25;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = 0.25;
                        page.PageSheet.PrintProps.PageRightMargin.Value = 0.25;

                        // ------------------------------------------------------------
                        // 5. (Optional) Adjust page size for custom paper
                        // ------------------------------------------------------------
                        // If you need a specific paper size, set width and height in inches.
                        // Example: A4 size (8.27" x 11.69")
                        page.PageSheet.PageProps.PageWidth.Value = 8.27;
                        page.PageSheet.PageProps.PageHeight.Value = 11.69;
                    }

                    // ------------------------------------------------------------
                    // Save the diagram as PDF with print settings applied
                    // ------------------------------------------------------------
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Ensure a fallback font is available if the diagram uses missing fonts.
                        DefaultFont = "Arial"
                    };

                    // Save using the overload that accepts SaveOptions.
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Print configuration applied and diagram saved to PDF.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }