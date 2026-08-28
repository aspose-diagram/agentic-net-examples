using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPrintPropsRollback
{
    // Simple DTO to hold original PrintProps values for a page
    class PrintPropsSnapshot
    {
        public double PageTopMargin { get; set; }
        public double PageBottomMargin { get; set; }
        public double PageLeftMargin { get; set; }
        public double PageRightMargin { get; set; }
        public PrintPageOrientationValue Orientation { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public BOOL OnPage { get; set; }
        // PagesX/Y are integer counts, so store as int to match the cell type
        public int PagesX { get; set; }
        public int PagesY { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            // Guard: ensure the input file exists before proceeding
            if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
            string outputPath = "output.vsdx";

            Diagram diagram = null;
            try
            {
                // Load the diagram inside a try-catch to capture any loading errors
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Store original PrintProps for each page
            var originalPrintProps = new Dictionary<long, PrintPropsSnapshot>();

            foreach (Page page in diagram.Pages)
            {
                var printProps = page.PageSheet.PrintProps;
                var snapshot = new PrintPropsSnapshot
                {
                    PageTopMargin = printProps.PageTopMargin.Value,
                    PageBottomMargin = printProps.PageBottomMargin.Value,
                    PageLeftMargin = printProps.PageLeftMargin.Value,
                    PageRightMargin = printProps.PageRightMargin.Value,
                    Orientation = printProps.PrintPageOrientation.Value,
                    ScaleX = printProps.ScaleX.Value,
                    ScaleY = printProps.ScaleY.Value,
                    OnPage = printProps.OnPage.Value,
                    // Cast to int because PagesX/Y are integer cells
                    PagesX = (int)printProps.PagesX.Value,
                    PagesY = (int)printProps.PagesY.Value
                };
                originalPrintProps[page.ID] = snapshot;
            }

            // Apply new PrintProps values (example changes)
            foreach (Page page in diagram.Pages)
            {
                var printProps = page.PageSheet.PrintProps;
                // Example: set margins to 0.5 inches, landscape orientation, 75% scaling, fit to 1x1 page
                printProps.PageTopMargin.Value = 0.5;
                printProps.PageBottomMargin.Value = 0.5;
                printProps.PageLeftMargin.Value = 0.5;
                printProps.PageRightMargin.Value = 0.5;
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                printProps.ScaleX.Value = 0.75;
                printProps.ScaleY.Value = 0.75;
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;
            }

            // Perform validation; if any validation fails, rollback
            bool validationPassed = ValidatePrintProps(diagram);
            if (!validationPassed)
            {
                // Rollback to original values
                foreach (Page page in diagram.Pages)
                {
                    if (originalPrintProps.TryGetValue(page.ID, out var snapshot))
                    {
                        var printProps = page.PageSheet.PrintProps;
                        printProps.PageTopMargin.Value = snapshot.PageTopMargin;
                        printProps.PageBottomMargin.Value = snapshot.PageBottomMargin;
                        printProps.PageLeftMargin.Value = snapshot.PageLeftMargin;
                        printProps.PageRightMargin.Value = snapshot.PageRightMargin;
                        printProps.PrintPageOrientation.Value = snapshot.Orientation;
                        printProps.ScaleX.Value = snapshot.ScaleX;
                        printProps.ScaleY.Value = snapshot.ScaleY;
                        printProps.OnPage.Value = snapshot.OnPage;
                        printProps.PagesX.Value = snapshot.PagesX;
                        printProps.PagesY.Value = snapshot.PagesY;
                    }
                }

                Console.WriteLine("Validation failed. Original PrintProps have been restored.");
            }
            else
            {
                Console.WriteLine("Validation succeeded. Changes will be saved.");
            }

            try
            {
                // Save the diagram (using SaveFileFormat.Vsdx)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
            }
            finally
            {
                // Dispose diagram to free resources
                diagram?.Dispose();
            }
        }

        // Example validation method for PrintProps across all pages
        static bool ValidatePrintProps(Diagram diagram)
        {
            foreach (Page page in diagram.Pages)
            {
                var printProps = page.PageSheet.PrintProps;

                // Scale must be greater than 0 and less than or equal to 1 (100%)
                if (printProps.ScaleX.Value <= 0 || printProps.ScaleX.Value > 1 ||
                    printProps.ScaleY.Value <= 0 || printProps.ScaleY.Value > 1)
                {
                    Console.WriteLine($"Invalid scale on page ID {page.ID}.");
                    return false;
                }

                // Margins must be non‑negative
                if (printProps.PageTopMargin.Value < 0 ||
                    printProps.PageBottomMargin.Value < 0 ||
                    printProps.PageLeftMargin.Value < 0 ||
                    printProps.PageRightMargin.Value < 0)
                {
                    Console.WriteLine($"Negative margin on page ID {page.ID}.");
                    return false;
                }

                // If Fit‑to‑Sheet is enabled, PagesX and PagesY must be positive integers
                if (printProps.OnPage.Value == BOOL.True)
                {
                    if (printProps.PagesX.Value <= 0 || printProps.PagesY.Value <= 0)
                    {
                        Console.WriteLine($"Invalid fit‑to‑sheet page count on page ID {page.ID}.");
                        return false;
                    }
                }
            }

            // All checks passed
            return true;
        }
    }
}