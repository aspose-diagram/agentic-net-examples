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

            // Load the original Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Modify print settings for each page
            foreach (Page page in diagram.Pages)
            {
                // Set orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set scaling to 75%
                page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                page.PageSheet.PrintProps.ScaleY.Value = 0.75;

                // Fit the drawing to a single sheet
                page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                page.PageSheet.PrintProps.PagesX.Value = 1;
                page.PageSheet.PrintProps.PagesY.Value = 1;

                // Set uniform margins (0.5 inches)
                page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;
            }

            // Save the modified diagram to a different format (PDF) to preserve the original file
            string outputPath = "output.pdf";
            diagram.Save(outputPath, SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
