using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            if (diagram.Pages.Count == 0)
                throw new Exception("The diagram contains no pages.");

            // Modify PrintProps of the first page
            Page page = diagram.Pages[0];
            PrintProps printProps = page.PageSheet.PrintProps;

            // Set orientation to Landscape
            printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

            // Set scaling to 80%
            printProps.ScaleX.Value = 0.8;
            printProps.ScaleY.Value = 0.8;

            // Enable fit to sheet (OnPage) and set pages to 1x1
            printProps.OnPage.Value = BOOL.True;
            printProps.PagesX.Value = 1;
            printProps.PagesY.Value = 1;

            // Set margins (in inches)
            printProps.PageTopMargin.Value = 0.5;
            printProps.PageBottomMargin.Value = 0.5;
            printProps.PageLeftMargin.Value = 0.5;
            printProps.PageRightMargin.Value = 0.5;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Reload the saved diagram
            Diagram reloadedDiagram = new Diagram(outputPath);

            if (reloadedDiagram.Pages.Count == 0)
                throw new Exception("The reloaded diagram contains no pages.");

            // Retrieve PrintProps from the reloaded diagram
            Page reloadedPage = reloadedDiagram.Pages[0];
            PrintProps reloadedPrintProps = reloadedPage.PageSheet.PrintProps;

            // Validate that the properties were retained
            const double tolerance = 0.0001;

            if (reloadedPrintProps.PrintPageOrientation.Value != PrintPageOrientationValue.Landscape)
                throw new Exception("PrintPageOrientation was not retained.");

            if (Math.Abs(reloadedPrintProps.ScaleX.Value - 0.8) > tolerance ||
                Math.Abs(reloadedPrintProps.ScaleY.Value - 0.8) > tolerance)
                throw new Exception("Scale values were not retained.");

            if (reloadedPrintProps.OnPage.Value != BOOL.True)
                throw new Exception("OnPage flag was not retained.");

            if (reloadedPrintProps.PagesX.Value != 1 || reloadedPrintProps.PagesY.Value != 1)
                throw new Exception("PagesX/Y values were not retained.");

            if (Math.Abs(reloadedPrintProps.PageTopMargin.Value - 0.5) > tolerance ||
                Math.Abs(reloadedPrintProps.PageBottomMargin.Value - 0.5) > tolerance ||
                Math.Abs(reloadedPrintProps.PageLeftMargin.Value - 0.5) > tolerance ||
                Math.Abs(reloadedPrintProps.PageRightMargin.Value - 0.5) > tolerance)
                throw new Exception("Margin values were not retained.");

            Console.WriteLine("PrintProps retained successfully after saving and reloading.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
