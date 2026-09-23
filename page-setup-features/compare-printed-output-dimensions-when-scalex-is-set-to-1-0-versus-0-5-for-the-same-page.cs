using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "sample.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Scenario 1: ScaleX = 1.0 (no scaling)
                page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                page.PageSheet.PrintProps.ScaleY.Value = 1.0; // keep Y scaling at 100%
                double printedWidthFull = pageWidth * page.PageSheet.PrintProps.ScaleX.Value;
                double printedHeightFull = pageHeight * page.PageSheet.PrintProps.ScaleY.Value;
                Console.WriteLine($"ScaleX = 1.0 -> Printed Width: {printedWidthFull} inches, Height: {printedHeightFull} inches");

                // Scenario 2: ScaleX = 0.5 (50% scaling)
                page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                // ScaleY remains unchanged (1.0)
                double printedWidthHalf = pageWidth * page.PageSheet.PrintProps.ScaleX.Value;
                double printedHeightHalf = pageHeight * page.PageSheet.PrintProps.ScaleY.Value;
                Console.WriteLine($"ScaleX = 0.5 -> Printed Width: {printedWidthHalf} inches, Height: {printedHeightHalf} inches");

                // Reset ScaleX to original value if further processing is needed
                page.PageSheet.PrintProps.ScaleX.Value = 1.0;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
