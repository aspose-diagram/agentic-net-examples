using System;
using System.IO;
using Aspose.Diagram;

namespace DiagramPrintPropsRollback
{
    // Snapshot of the printable properties for a page
    class PrintPropsSnapshot
    {
        public PrintPageOrientationValue? Orientation { get; set; }
        public double? ScaleX { get; set; }
        public double? ScaleY { get; set; }
        public BOOL? OnPage { get; set; }
        // PagesX/Y are integer cell values, so use int? instead of double?
        public int? PagesX { get; set; }
        public int? PagesY { get; set; }
        public double? PageTopMargin { get; set; }
        public double? PageBottomMargin { get; set; }
        public double? PageLeftMargin { get; set; }
        public double? PageRightMargin { get; set; }
    }

    class Program
    {
        // Capture current PrintProps of a page
        static PrintPropsSnapshot CapturePrintProps(Page page)
        {
            var pp = page.PageSheet.PrintProps;
            return new PrintPropsSnapshot
            {
                Orientation = pp.PrintPageOrientation.Value,
                ScaleX = pp.ScaleX.Value,
                ScaleY = pp.ScaleY.Value,
                OnPage = pp.OnPage.Value,
                PagesX = pp.PagesX.Value,          // integer value
                PagesY = pp.PagesY.Value,          // integer value
                PageTopMargin = pp.PageTopMargin.Value,
                PageBottomMargin = pp.PageBottomMargin.Value,
                PageLeftMargin = pp.PageLeftMargin.Value,
                PageRightMargin = pp.PageRightMargin.Value
            };
        }

        // Restore previously captured PrintProps to a page
        static void RestorePrintProps(Page page, PrintPropsSnapshot snapshot)
        {
            var pp = page.PageSheet.PrintProps;
            if (snapshot.Orientation.HasValue) pp.PrintPageOrientation.Value = snapshot.Orientation.Value;
            if (snapshot.ScaleX.HasValue) pp.ScaleX.Value = snapshot.ScaleX.Value;
            if (snapshot.ScaleY.HasValue) pp.ScaleY.Value = snapshot.ScaleY.Value;
            if (snapshot.OnPage.HasValue) pp.OnPage.Value = snapshot.OnPage.Value;
            if (snapshot.PagesX.HasValue) pp.PagesX.Value = snapshot.PagesX.Value; // integer assignment
            if (snapshot.PagesY.HasValue) pp.PagesY.Value = snapshot.PagesY.Value; // integer assignment
            if (snapshot.PageTopMargin.HasValue) pp.PageTopMargin.Value = snapshot.PageTopMargin.Value;
            if (snapshot.PageBottomMargin.HasValue) pp.PageBottomMargin.Value = snapshot.PageBottomMargin.Value;
            if (snapshot.PageLeftMargin.HasValue) pp.PageLeftMargin.Value = snapshot.PageLeftMargin.Value;
            if (snapshot.PageRightMargin.HasValue) pp.PageRightMargin.Value = snapshot.PageRightMargin.Value;
        }

        // Example validation: ensure scaling factors are positive and margins are non‑negative
        static bool ValidatePrintProps(Page page)
        {
            var pp = page.PageSheet.PrintProps;
            if (pp.ScaleX.Value <= 0 || pp.ScaleY.Value <= 0)
                return false;
            if (pp.PageTopMargin.Value < 0 || pp.PageBottomMargin.Value < 0 ||
                pp.PageLeftMargin.Value < 0 || pp.PageRightMargin.Value < 0)
                return false;
            return true;
        }

        static void Main(string[] args)
        {
            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Guard: ensure input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Path for the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Process each page
                foreach (Page page in diagram.Pages)
                {
                    // Capture original settings
                    PrintPropsSnapshot original = CapturePrintProps(page);

                    try
                    {
                        // Apply desired changes
                        var pp = page.PageSheet.PrintProps;
                        pp.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                        pp.ScaleX.Value = 0.75;
                        pp.ScaleY.Value = 0.75;
                        pp.OnPage.Value = BOOL.True;
                        pp.PagesX.Value = 1;               // integer assignment
                        pp.PagesY.Value = 1;               // integer assignment
                        // Example margin change (convert points to inches)
                        pp.PageTopMargin.Value = 0.5;      // 0.5 inches
                        pp.PageBottomMargin.Value = 0.5;
                        pp.PageLeftMargin.Value = 0.5;
                        pp.PageRightMargin.Value = 0.5;

                        // Validate the new settings
                        if (!ValidatePrintProps(page))
                        {
                            // Validation failed – rollback
                            RestorePrintProps(page, original);
                            Console.WriteLine($"Validation failed on page '{page.Name}'. Changes rolled back.");
                        }
                        else
                        {
                            Console.WriteLine($"Page '{page.Name}' updated successfully.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // In case of unexpected errors, also rollback
                        RestorePrintProps(page, original);
                        Console.WriteLine($"Error processing page '{page.Name}': {ex.Message}. Changes rolled back.");
                    }
                }

                // Save the diagram if needed
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
    }
}