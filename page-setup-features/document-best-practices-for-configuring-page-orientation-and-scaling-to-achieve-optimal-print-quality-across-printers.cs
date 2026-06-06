using System;
using System.IO;
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

                // Load the diagram inside a using block to ensure resources are released
                using (Diagram diagram = new Diagram(sourcePath))
                {
                    // Iterate over each page explicitly typed as Page
                    foreach (Page page in diagram.Pages)
                    {
                        // ------------------------------
                        // 1. Set page orientation
                        // ------------------------------
                        // Choose Landscape for wide diagrams, Portrait for tall diagrams.
                        // SameAsPrinter lets the printer decide based on its default.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // ------------------------------
                        // 2. Configure scaling
                        // ------------------------------
                        // ScaleX and ScaleY are expressed as a factor (1.0 = 100%).
                        // Values > 1 enlarge the diagram, values < 1 shrink it.
                        // Typical print quality uses 0.75 (75%) to fit more content.
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                        page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                        // ------------------------------
                        // 3. Fit to sheet (optional)
                        // ------------------------------
                        // When true, Visio scales the drawing to fit the specified number of pages.
                        // Setting PagesX and PagesY to 1 forces a single‑sheet print.
                        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                        page.PageSheet.PrintProps.PagesX.Value = 1;
                        page.PageSheet.PrintProps.PagesY.Value = 1;

                        // ------------------------------
                        // 4. Set printer margins
                        // ------------------------------
                        // Margins are in inches. Use a small margin (e.g., 0.25") to maximize printable area
                        // while avoiding clipping on most printers.
                        page.PageSheet.PrintProps.PageTopMargin.Value = 0.25;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = 0.25;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = 0.25;
                        page.PageSheet.PrintProps.PageRightMargin.Value = 0.25;

                        // ------------------------------
                        // 5. Validate page size
                        // ------------------------------
                        // Ensure the page dimensions are reasonable for the target printer.
                        // Typical A4 size is 8.27" x 11.69". Adjust if needed.
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        if (pageWidth <= 0 || pageHeight <= 0)
                        {
                            throw new Exception($"Invalid page size detected on page '{page.Name}'. Width: {pageWidth}, Height: {pageHeight}");
                        }

                        // ------------------------------
                        // 6. Log configuration (optional)
                        // ------------------------------
                        Console.WriteLine($"Configured page '{page.Name}' (ID: {page.ID}):");
                        Console.WriteLine($"  Orientation: {page.PageSheet.PrintProps.PrintPageOrientation.Value}");
                        Console.WriteLine($"  ScaleX: {page.PageSheet.PrintProps.ScaleX.Value}");
                        Console.WriteLine($"  ScaleY: {page.PageSheet.PrintProps.ScaleY.Value}");
                        Console.WriteLine($"  Fit to sheet: {page.PageSheet.PrintProps.OnPage.Value}");
                        Console.WriteLine($"  Margins (in): Top={page.PageSheet.PrintProps.PageTopMargin.Value}, Bottom={page.PageSheet.PrintProps.PageBottomMargin.Value}, Left={page.PageSheet.PrintProps.PageLeftMargin.Value}, Right={page.PageSheet.PrintProps.PageRightMargin.Value}");
                    }

                    // Save the diagram as PDF to preserve print settings.
                    // Using SaveOptions ensures that the print configuration is embedded.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial"; // Fallback font for Unicode characters
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }