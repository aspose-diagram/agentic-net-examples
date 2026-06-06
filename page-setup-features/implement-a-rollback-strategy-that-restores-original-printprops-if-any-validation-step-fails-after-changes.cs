using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

namespace PrintPropsRollbackDemo
{
    // Helper class to store original PrintProps values for a page
    class PrintPropsSnapshot
    {
        public PrintPageOrientationValue Orientation { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public BOOL OnPage { get; set; }
        public int PagesX { get; set; }
        public int PagesY { get; set; }
        public double TopMargin { get; set; }
        public double BottomMargin { get; set; }
        public double LeftMargin { get; set; }
        public double RightMargin { get; set; }

        // Capture current values from a page
        public static PrintPropsSnapshot Capture(Page page)
        {
            var pp = page.PageSheet.PrintProps;
            return new PrintPropsSnapshot
            {
                Orientation = pp.PrintPageOrientation.Value,
                ScaleX = pp.ScaleX.Value,
                ScaleY = pp.ScaleY.Value,
                OnPage = pp.OnPage.Value,
                PagesX = pp.PagesX.Value,
                PagesY = pp.PagesY.Value,
                TopMargin = pp.PageTopMargin.Value,
                BottomMargin = pp.PageBottomMargin.Value,
                LeftMargin = pp.PageLeftMargin.Value,
                RightMargin = pp.PageRightMargin.Value
            };
        }

        // Restore saved values to a page
        public void Restore(Page page)
        {
            var pp = page.PageSheet.PrintProps;
            pp.PrintPageOrientation.Value = this.Orientation;
            pp.ScaleX.Value = this.ScaleX;
            pp.ScaleY.Value = this.ScaleY;
            pp.OnPage.Value = this.OnPage;
            pp.PagesX.Value = this.PagesX;
            pp.PagesY.Value = this.PagesY;
            pp.PageTopMargin.Value = this.TopMargin;
            pp.PageBottomMargin.Value = this.BottomMargin;
            pp.PageLeftMargin.Value = this.LeftMargin;
            pp.PageRightMargin.Value = this.RightMargin;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
            string outputPath = "output.vsdx";

            // Load the diagram inside a using block to ensure disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Store snapshots of original PrintProps for each page
                var originalSnapshots = new Dictionary<long, PrintPropsSnapshot>();
                foreach (Page page in diagram.Pages)
                {
                    originalSnapshots[page.ID] = PrintPropsSnapshot.Capture(page);
                }

                try
                {
                    // Apply new print settings to each page
                    foreach (Page page in diagram.Pages)
                    {
                        var pp = page.PageSheet.PrintProps;

                        // Example modifications
                        pp.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                        pp.ScaleX.Value = 0.75; // 75% scaling
                        pp.ScaleY.Value = 0.75;
                        pp.OnPage.Value = BOOL.True;
                        pp.PagesX.Value = 1;
                        pp.PagesY.Value = 1;

                        // Margins in inches (Visio uses inches)
                        pp.PageTopMargin.Value = 0.5;
                        pp.PageBottomMargin.Value = 0.5;
                        pp.PageLeftMargin.Value = 0.5;
                        pp.PageRightMargin.Value = 0.5;
                    }

                    // Validation step: ensure scaling factors are positive
                    foreach (Page page in diagram.Pages)
                    {
                        var pp = page.PageSheet.PrintProps;
                        if (pp.ScaleX.Value <= 0 || pp.ScaleY.Value <= 0)
                        {
                            throw new Exception("Invalid scaling factor detected.");
                        }
                    }

                    // If validation passes, save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved successfully.");
                }
                catch (Exception ex)
                {
                    // Rollback: restore original PrintProps for each page
                    foreach (Page page in diagram.Pages)
                    {
                        if (originalSnapshots.TryGetValue(page.ID, out var snapshot))
                        {
                            snapshot.Restore(page);
                        }
                    }

                    // Optionally, save the rolled-back diagram or just report the error
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    Console.WriteLine("Changes have been rolled back to original PrintProps.");
                }
            }
        }
    }
}