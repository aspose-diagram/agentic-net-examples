using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPrintPropsRollback
{
    // Snapshot of printable properties for a page
    class PrintPropsSnapshot
    {
        public PrintPageOrientationValue Orientation;
        public double ScaleX;
        public double ScaleY;
        public BOOL OnPage;
        public int PagesX;
        public int PagesY;
        public double TopMargin;
        public double BottomMargin;
        public double LeftMargin;
        public double RightMargin;
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load diagram inside a using block to ensure disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Store original PrintProps for each page
                    var originalProps = new System.Collections.Generic.Dictionary<long, PrintPropsSnapshot>();

                    foreach (Page page in diagram.Pages)
                    {
                        var printProps = page.PageSheet.PrintProps;
                        var snapshot = new PrintPropsSnapshot
                        {
                            Orientation = printProps.PrintPageOrientation.Value,
                            ScaleX = printProps.ScaleX.Value,
                            ScaleY = printProps.ScaleY.Value,
                            OnPage = printProps.OnPage.Value,
                            PagesX = (int)printProps.PagesX.Value,
                            PagesY = (int)printProps.PagesY.Value,
                            TopMargin = printProps.PageTopMargin.Value,
                            BottomMargin = printProps.PageBottomMargin.Value,
                            LeftMargin = printProps.PageLeftMargin.Value,
                            RightMargin = printProps.PageRightMargin.Value
                        };
                        originalProps[page.ID] = snapshot;
                    }

                    // Apply new print settings (example changes)
                    foreach (Page page in diagram.Pages)
                    {
                        var printProps = page.PageSheet.PrintProps;
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                        printProps.ScaleX.Value = 0.8; // 80%
                        printProps.ScaleY.Value = 0.8;
                        printProps.OnPage.Value = BOOL.True;
                        printProps.PagesX.Value = 1;
                        printProps.PagesY.Value = 1;
                        // Set margins to 0.5 inches (Visio uses inches)
                        printProps.PageTopMargin.Value = 0.5;
                        printProps.PageBottomMargin.Value = 0.5;
                        printProps.PageLeftMargin.Value = 0.5;
                        printProps.PageRightMargin.Value = 0.5;
                    }

                    // Validation step
                    bool validationFailed = false;
                    foreach (Page page in diagram.Pages)
                    {
                        var printProps = page.PageSheet.PrintProps;

                        // Example validation rules
                        if (printProps.ScaleX.Value <= 0 || printProps.ScaleY.Value <= 0)
                        {
                            Console.WriteLine($"Validation error on page {page.Name}: Scale must be positive.");
                            validationFailed = true;
                            break;
                        }

                        if (printProps.PageTopMargin.Value < 0 ||
                            printProps.PageBottomMargin.Value < 0 ||
                            printProps.PageLeftMargin.Value < 0 ||
                            printProps.PageRightMargin.Value < 0)
                        {
                            Console.WriteLine($"Validation error on page {page.Name}: Margins cannot be negative.");
                            validationFailed = true;
                            break;
                        }

                        // Additional custom validation can be added here
                    }

                    // Rollback if validation failed
                    if (validationFailed)
                    {
                        Console.WriteLine("Validation failed. Restoring original PrintProps.");
                        foreach (Page page in diagram.Pages)
                        {
                            if (originalProps.TryGetValue(page.ID, out PrintPropsSnapshot snapshot))
                            {
                                var printProps = page.PageSheet.PrintProps;
                                printProps.PrintPageOrientation.Value = snapshot.Orientation;
                                printProps.ScaleX.Value = snapshot.ScaleX;
                                printProps.ScaleY.Value = snapshot.ScaleY;
                                printProps.OnPage.Value = snapshot.OnPage;
                                printProps.PagesX.Value = snapshot.PagesX;
                                printProps.PagesY.Value = snapshot.PagesY;
                                printProps.PageTopMargin.Value = snapshot.TopMargin;
                                printProps.PageBottomMargin.Value = snapshot.BottomMargin;
                                printProps.PageLeftMargin.Value = snapshot.LeftMargin;
                                printProps.PageRightMargin.Value = snapshot.RightMargin;
                            }
                        }

                        // Optionally abort saving
                        Console.WriteLine("Operation aborted due to validation errors.");
                        return;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}