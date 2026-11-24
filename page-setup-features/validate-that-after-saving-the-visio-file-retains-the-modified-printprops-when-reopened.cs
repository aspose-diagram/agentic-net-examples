using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use arguments if provided)
        string sourcePath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing source file
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the source diagram
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page page = diagram.Pages[0];

                // Modify print properties
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set margins (in inches)
                printProps.PageTopMargin.Value = 0.5;
                printProps.PageBottomMargin.Value = 0.5;
                printProps.PageLeftMargin.Value = 0.5;
                printProps.PageRightMargin.Value = 0.5;

                // Set orientation to Landscape
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set scaling to 75%
                printProps.ScaleX.Value = 0.75;
                printProps.ScaleY.Value = 0.75;

                // Enable Fit to Sheet across 2x2 pages
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 2;
                printProps.PagesY.Value = 2;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Re-open the saved diagram to validate the changes
            using (Diagram reloaded = new Diagram(outputPath))
            {
                if (reloaded.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Reloaded diagram contains no pages.");
                    return;
                }

                Page rePage = reloaded.Pages[0];
                PrintProps rp = rePage.PageSheet.PrintProps;

                // Validate each property
                bool isValid = true;

                if (Math.Abs(rp.PageTopMargin.Value - 0.5) > 0.001) isValid = false;
                if (Math.Abs(rp.PageBottomMargin.Value - 0.5) > 0.001) isValid = false;
                if (Math.Abs(rp.PageLeftMargin.Value - 0.5) > 0.001) isValid = false;
                if (Math.Abs(rp.PageRightMargin.Value - 0.5) > 0.001) isValid = false;

                if (rp.PrintPageOrientation.Value != PrintPageOrientationValue.Landscape) isValid = false;

                if (Math.Abs(rp.ScaleX.Value - 0.75) > 0.001) isValid = false;
                if (Math.Abs(rp.ScaleY.Value - 0.75) > 0.001) isValid = false;

                if (rp.OnPage.Value != BOOL.True) isValid = false;
                if (rp.PagesX.Value != 2) isValid = false;
                if (rp.PagesY.Value != 2) isValid = false;

                if (isValid)
                {
                    Console.WriteLine("PrintProps validation succeeded. Modified settings were retained after saving.");
                }
                else
                {
                    throw new Exception("PrintProps validation failed. Modified settings were not retained.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}