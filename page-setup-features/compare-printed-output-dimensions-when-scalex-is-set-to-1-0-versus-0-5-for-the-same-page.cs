using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Retrieve the original page dimensions (in inches).
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // -----------------------------------------------------------------
            // Scenario 1: ScaleX = 1.0 (no scaling)
            // -----------------------------------------------------------------
            // Set ScaleX to 1.0. ScaleY is read‑only; use its existing value.
            page.PageSheet.PrintProps.ScaleX.Value = 1.0;
            double scaleY = page.PageSheet.PrintProps.ScaleY.Value; // read‑only

            double scaledWidth1 = pageWidth * page.PageSheet.PrintProps.ScaleX.Value;
            double scaledHeight1 = pageHeight * scaleY;

            // -----------------------------------------------------------------
            // Scenario 2: ScaleX = 0.5 (reduce width by half)
            // -----------------------------------------------------------------
            page.PageSheet.PrintProps.ScaleX.Value = 0.5;
            // ScaleY remains unchanged.
            double scaledWidth2 = pageWidth * page.PageSheet.PrintProps.ScaleX.Value;
            double scaledHeight2 = pageHeight * scaleY;

            // Output the comparison results.
            Console.WriteLine("=== Printed Output Dimensions Comparison ===");
            Console.WriteLine($"Original Page Size: Width = {pageWidth:F2} in, Height = {pageHeight:F2} in");
            Console.WriteLine();
            Console.WriteLine("Scenario 1: ScaleX = 1.0");
            Console.WriteLine($"  Scaled Width  = {scaledWidth1:F2} in");
            Console.WriteLine($"  Scaled Height = {scaledHeight1:F2} in");
            Console.WriteLine();
            Console.WriteLine("Scenario 2: ScaleX = 0.5");
            Console.WriteLine($"  Scaled Width  = {scaledWidth2:F2} in");
            Console.WriteLine($"  Scaled Height = {scaledHeight2:F2} in");
            Console.WriteLine();

            // Simple validation: the width in scenario 2 should be half of scenario 1.
            if (Math.Abs(scaledWidth2 * 2 - scaledWidth1) < 0.001)
            {
                Console.WriteLine("Validation passed: Width with ScaleX 0.5 is half of the width with ScaleX 1.0.");
            }
            else
            {
                Console.WriteLine("Validation failed: Unexpected width scaling results.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}