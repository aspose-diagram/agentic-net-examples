using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (must exist in the working directory)
            string inputPath = "sample.vsdx";
            // Output file where modifications will be saved
            string outputPath = "modifiedPrintProps.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (ensure at least one page exists)
            Page page = diagram.Pages[0];

            // ----- Modify PrintProps -----
            // Margins (in inches)
            page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
            page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
            page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
            page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;

            // Orientation
            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

            // Scaling (75%)
            page.PageSheet.PrintProps.ScaleX.Value = 0.75;
            page.PageSheet.PrintProps.ScaleY.Value = 0.75;

            // Fit to sheet (2x2 pages)
            page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
            page.PageSheet.PrintProps.PagesX.Value = 2;
            page.PageSheet.PrintProps.PagesY.Value = 2;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            diagram.Dispose();

            // Reload the saved diagram
            Diagram reloaded = new Diagram(outputPath);
            Page reloadedPage = reloaded.Pages[0];

            // ----- Validation -----
            // Helper local function for tolerance comparison
            bool AreEqual(double a, double b, double tolerance = 0.0001) => Math.Abs(a - b) <= tolerance;

            if (!AreEqual(reloadedPage.PageSheet.PrintProps.PageTopMargin.Value, 0.5))
                throw new Exception("PageTopMargin was not retained after reload.");
            if (!AreEqual(reloadedPage.PageSheet.PrintProps.PageBottomMargin.Value, 0.5))
                throw new Exception("PageBottomMargin was not retained after reload.");
            if (!AreEqual(reloadedPage.PageSheet.PrintProps.PageLeftMargin.Value, 0.5))
                throw new Exception("PageLeftMargin was not retained after reload.");
            if (!AreEqual(reloadedPage.PageSheet.PrintProps.PageRightMargin.Value, 0.5))
                throw new Exception("PageRightMargin was not retained after reload.");

            if (reloadedPage.PageSheet.PrintProps.PrintPageOrientation.Value != PrintPageOrientationValue.Landscape)
                throw new Exception("PrintPageOrientation was not retained after reload.");

            if (!AreEqual(reloadedPage.PageSheet.PrintProps.ScaleX.Value, 0.75))
                throw new Exception("ScaleX was not retained after reload.");
            if (!AreEqual(reloadedPage.PageSheet.PrintProps.ScaleY.Value, 0.75))
                throw new Exception("ScaleY was not retained after reload.");

            if (reloadedPage.PageSheet.PrintProps.OnPage.Value != BOOL.True)
                throw new Exception("OnPage flag was not retained after reload.");
            if (!AreEqual(reloadedPage.PageSheet.PrintProps.PagesX.Value, 2))
                throw new Exception("PagesX was not retained after reload.");
            if (!AreEqual(reloadedPage.PageSheet.PrintProps.PagesY.Value, 2))
                throw new Exception("PagesY was not retained after reload.");

            Console.WriteLine("All PrintProps values were successfully retained after saving and reloading.");

            reloaded.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
