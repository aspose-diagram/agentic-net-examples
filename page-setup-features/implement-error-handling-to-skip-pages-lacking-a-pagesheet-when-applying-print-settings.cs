using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page explicitly typed as Page
                foreach (Page page in diagram.Pages)
                {
                    // Skip pages that do not have a PageSheet
                    if (page.PageSheet == null)
                    {
                        Console.WriteLine($"Skipping page '{page.Name}' (ID: {page.ID}) because PageSheet is missing.");
                        continue;
                    }

                    try
                    {
                        // Access the print properties of the page
                        var printProps = page.PageSheet.PrintProps;

                        // Example print settings
                        // 1. Set orientation to Landscape
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // 2. Set scaling to 75%
                        printProps.ScaleX.Value = 0.75;
                        printProps.ScaleY.Value = 0.75;

                        // 3. Fit to a single sheet (1 page across, 1 page down)
                        printProps.OnPage.Value = BOOL.True;
                        printProps.PagesX.Value = 1;
                        printProps.PagesY.Value = 1;

                        // 4. Set uniform margins (0.5 inch)
                        double marginInches = 0.5;
                        printProps.PageTopMargin.Value = marginInches;
                        printProps.PageBottomMargin.Value = marginInches;
                        printProps.PageLeftMargin.Value = marginInches;
                        printProps.PageRightMargin.Value = marginInches;

                        Console.WriteLine($"Applied print settings to page '{page.Name}' (ID: {page.ID}).");
                    }
                    catch (Exception ex)
                    {
                        // Log any errors but continue processing other pages
                        Console.WriteLine($"Error applying print settings to page '{page.Name}': {ex.Message}");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
