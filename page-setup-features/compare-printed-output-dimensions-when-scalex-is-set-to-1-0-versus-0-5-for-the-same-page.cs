using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with an actual file path)
                const string inputPath = "input.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure there is at least one page
                    if (diagram.Pages.Count == 0)
                    {
                        throw new Exception("The diagram contains no pages.");
                    }

                    // Work with the first page
                    Page page = diagram.Pages[0];

                    // Retrieve the original page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Access the PrintProps collection for scaling settings
                    var printProps = page.PageSheet.PrintProps;

                    // --- Scenario 1: ScaleX = 1.0 (100% size) ---
                    printProps.ScaleX.Value = 1.0;
                    printProps.ScaleY.Value = 1.0; // keep vertical scaling unchanged

                    double printedWidthFull = pageWidth * printProps.ScaleX.Value;
                    double printedHeightFull = pageHeight * printProps.ScaleY.Value;

                    Console.WriteLine("ScaleX = 1.0");
                    Console.WriteLine($"Original page size:  Width = {pageWidth:F2} in, Height = {pageHeight:F2} in");
                    Console.WriteLine($"Printed size:        Width = {printedWidthFull:F2} in, Height = {printedHeightFull:F2} in");
                    Console.WriteLine();

                    // --- Scenario 2: ScaleX = 0.5 (50% size) ---
                    printProps.ScaleX.Value = 0.5;
                    // ScaleY remains at 1.0 for this comparison
                    double printedWidthHalf = pageWidth * printProps.ScaleX.Value;
                    double printedHeightHalf = pageHeight * printProps.ScaleY.Value;

                    Console.WriteLine("ScaleX = 0.5");
                    Console.WriteLine($"Original page size:  Width = {pageWidth:F2} in, Height = {pageHeight:F2} in");
                    Console.WriteLine($"Printed size:        Width = {printedWidthHalf:F2} in, Height = {printedHeightHalf:F2} in");
                    Console.WriteLine();

                    // Simple verification (optional)
                    if (Math.Abs(printedWidthHalf * 2 - printedWidthFull) > 0.001)
                    {
                        throw new Exception("Printed width does not scale as expected.");
                    }
                    else
                    {
                        Console.WriteLine("Verification passed: Width at 0.5 scale is exactly half of width at 1.0 scale.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }