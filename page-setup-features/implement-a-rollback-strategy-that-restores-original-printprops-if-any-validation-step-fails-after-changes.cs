using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    // Helper class to store a snapshot of PrintProps for a page
    class PrintPropsBackup
    {
        public PrintPageOrientationValue Orientation;
        public double ScaleX;
        public double ScaleY;
        public BOOL OnPage;
        public int PagesX;
        public int PagesY;
        public double PageTopMargin;
        public double PageBottomMargin;
        public double PageLeftMargin;
        public double PageRightMargin;
    }

    static void Main(string[] args)
    {
        // Expect at least input file path
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> [output.vsdx]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Dictionary to hold original PrintProps per page ID
        var originalPrintProps = new Dictionary<int, PrintPropsBackup>();

        try
        {
            // Backup original PrintProps for each page
            foreach (Page page in diagram.Pages)
            {
                var backup = new PrintPropsBackup
                {
                    Orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value,
                    ScaleX = page.PageSheet.PrintProps.ScaleX.Value,
                    ScaleY = page.PageSheet.PrintProps.ScaleY.Value,
                    OnPage = page.PageSheet.PrintProps.OnPage.Value,
                    PagesX = (int)page.PageSheet.PrintProps.PagesX.Value,
                    PagesY = (int)page.PageSheet.PrintProps.PagesY.Value,
                    PageTopMargin = page.PageSheet.PrintProps.PageTopMargin.Value,
                    PageBottomMargin = page.PageSheet.PrintProps.PageBottomMargin.Value,
                    PageLeftMargin = page.PageSheet.PrintProps.PageLeftMargin.Value,
                    PageRightMargin = page.PageSheet.PrintProps.PageRightMargin.Value
                };
                originalPrintProps[page.ID] = backup;
            }

            // Apply some modifications to PrintProps (example changes)
            foreach (Page page in diagram.Pages)
            {
                // Set orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set scaling to 0.5 (50%)
                page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                page.PageSheet.PrintProps.ScaleY.Value = 0.5;

                // Enable Fit to Sheet across 2x2 pages
                page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                page.PageSheet.PrintProps.PagesX.Value = 2;
                page.PageSheet.PrintProps.PagesY.Value = 2;

                // Set margins to 0.5 inches (Visio uses points; 1 inch = 72 points)
                double marginInPoints = 0.5 * 72.0;
                page.PageSheet.PrintProps.PageTopMargin.Value = marginInPoints;
                page.PageSheet.PrintProps.PageBottomMargin.Value = marginInPoints;
                page.PageSheet.PrintProps.PageLeftMargin.Value = marginInPoints;
                page.PageSheet.PrintProps.PageRightMargin.Value = marginInPoints;
            }

            // Validation step: ensure all modified values are within acceptable ranges
            bool validationFailed = false;
            foreach (Page page in diagram.Pages)
            {
                // Validate scaling (must be between 0.1 and 2.0)
                if (page.PageSheet.PrintProps.ScaleX.Value < 0.1 || page.PageSheet.PrintProps.ScaleX.Value > 2.0 ||
                    page.PageSheet.PrintProps.ScaleY.Value < 0.1 || page.PageSheet.PrintProps.ScaleY.Value > 2.0)
                {
                    Console.Error.WriteLine($"Validation error on page {page.ID}: Scale out of range.");
                    validationFailed = true;
                    break;
                }

                // Validate margins (must be non‑negative)
                if (page.PageSheet.PrintProps.PageTopMargin.Value < 0 ||
                    page.PageSheet.PrintProps.PageBottomMargin.Value < 0 ||
                    page.PageSheet.PrintProps.PageLeftMargin.Value < 0 ||
                    page.PageSheet.PrintProps.PageRightMargin.Value < 0)
                {
                    Console.Error.WriteLine($"Validation error on page {page.ID}: Negative margin.");
                    validationFailed = true;
                    break;
                }

                // Validate Fit‑to‑Sheet settings
                if (page.PageSheet.PrintProps.OnPage.Value == BOOL.True)
                {
                    if (page.PageSheet.PrintProps.PagesX.Value <= 0 || page.PageSheet.PrintProps.PagesY.Value <= 0)
                    {
                        Console.Error.WriteLine($"Validation error on page {page.ID}: PagesX/PagesY must be > 0 when OnPage is True.");
                        validationFailed = true;
                        break;
                    }
                }
            }

            // If any validation failed, rollback to original PrintProps
            if (validationFailed)
            {
                Console.WriteLine("Validation failed. Rolling back to original PrintProps.");
                foreach (Page page in diagram.Pages)
                {
                    if (originalPrintProps.TryGetValue(page.ID, out PrintPropsBackup backup))
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = backup.Orientation;
                        page.PageSheet.PrintProps.ScaleX.Value = backup.ScaleX;
                        page.PageSheet.PrintProps.ScaleY.Value = backup.ScaleY;
                        page.PageSheet.PrintProps.OnPage.Value = backup.OnPage;
                        page.PageSheet.PrintProps.PagesX.Value = backup.PagesX;
                        page.PageSheet.PrintProps.PagesY.Value = backup.PagesY;
                        page.PageSheet.PrintProps.PageTopMargin.Value = backup.PageTopMargin;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = backup.PageBottomMargin;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = backup.PageLeftMargin;
                        page.PageSheet.PrintProps.PageRightMargin.Value = backup.PageRightMargin;
                    }
                }
            }
            else
            {
                Console.WriteLine("All validations passed. Changes will be saved.");
            }

            // Save the diagram (whether rolled back or not)
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Dispose diagram to free resources
            diagram?.Dispose();
        }
    }
}