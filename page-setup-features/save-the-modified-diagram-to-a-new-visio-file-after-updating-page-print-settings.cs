using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths for the source and the new Visio file
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Update print settings for each page
            foreach (Page page in diagram.Pages)
            {
                // Set orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set scaling to 75%
                page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                // Enable fit-to-sheet (print on a single page)
                page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                page.PageSheet.PrintProps.PagesX.Value = 1;
                page.PageSheet.PrintProps.PagesY.Value = 1;

                // Set page margins (in inches)
                page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;
            }

            // Save the modified diagram to a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
