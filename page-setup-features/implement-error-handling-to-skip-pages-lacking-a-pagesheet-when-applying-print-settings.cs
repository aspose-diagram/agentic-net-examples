using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // Skip pages that do not have a PageSheet
                        if (page.PageSheet == null)
                        {
                            Console.WriteLine($"Page ID {page.ID} has no PageSheet. Skipping.");
                            continue;
                        }

                        // Apply print settings
                        // Set orientation to Landscape
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set scaling to 75%
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                        page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                        // Enable fit to sheet (one page per sheet)
                        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                        page.PageSheet.PrintProps.PagesX.Value = 1;
                        page.PageSheet.PrintProps.PagesY.Value = 1;

                        // Set margins (0.5 inches on each side)
                        page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                        page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;

                        Console.WriteLine($"Print settings applied to page ID {page.ID}.");
                    }
                    catch (Exception ex)
                    {
                        // Log the error and continue with the next page
                        Console.WriteLine($"Error processing page ID {page.ID}: {ex.Message}");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
