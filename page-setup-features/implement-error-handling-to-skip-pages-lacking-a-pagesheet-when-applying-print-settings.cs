using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
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
                        // Apply print settings to the page

                        // Set orientation to Landscape
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set scaling to 75%
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                        page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                        // Enable fit-to-sheet (single page)
                        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                        page.PageSheet.PrintProps.PagesX.Value = 1;
                        page.PageSheet.PrintProps.PagesY.Value = 1;

                        // Set page margins (in inches)
                        page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur while applying settings to this page
                        Console.WriteLine($"Error applying print settings to page '{page.Name}': {ex.Message}");
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving
            Console.WriteLine($"Failed to process diagram: {ex.Message}");
        }
    }
}
