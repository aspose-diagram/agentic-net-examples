using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Define the values we will set on the first page's PrintProps.
        const double topMargin = 0.5;      // inches
        const double bottomMargin = 0.5;   // inches
        const double leftMargin = 0.5;     // inches
        const double rightMargin = 0.5;    // inches
        const double scaleX = 0.75;        // 75%
        const double scaleY = 0.75;        // 75%
        const int pagesX = 2;
        const int pagesY = 2;
        const PrintPageOrientationValue orientation = PrintPageOrientationValue.Landscape;
        const BOOL onPage = BOOL.True;

        Diagram diagram = null;
        Diagram reloadedDiagram = null;

        try
        {
            // Load the original Visio diagram.
            diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page.
            Page page = diagram.Pages[0];

            // Modify PrintProps.
            page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
            page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
            page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
            page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
            page.PageSheet.PrintProps.ScaleX.Value = scaleX;
            page.PageSheet.PrintProps.ScaleY.Value = scaleY;
            page.PageSheet.PrintProps.PagesX.Value = pagesX;
            page.PageSheet.PrintProps.PagesY.Value = pagesY;
            page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
            page.PageSheet.PrintProps.OnPage.Value = onPage;

            // Save the modified diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during modification or saving: {ex.Message}");
            return;
        }
        finally
        {
            // Dispose the original diagram if it implements IDisposable.
            diagram?.Dispose();
        }

        try
        {
            // Reload the saved diagram.
            reloadedDiagram = new Diagram(outputPath);

            // Ensure the reloaded diagram has at least one page.
            if (reloadedDiagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Reloaded diagram contains no pages.");
                return;
            }

            // Access the first page of the reloaded diagram.
            Page reloadedPage = reloadedDiagram.Pages[0];
            var props = reloadedPage.PageSheet.PrintProps;

            // Validate each property. Throw if any mismatch is detected.
            if (Math.Abs(props.PageTopMargin.Value - topMargin) > 0.0001)
                throw new Exception("PageTopMargin was not preserved after saving.");

            if (Math.Abs(props.PageBottomMargin.Value - bottomMargin) > 0.0001)
                throw new Exception("PageBottomMargin was not preserved after saving.");

            if (Math.Abs(props.PageLeftMargin.Value - leftMargin) > 0.0001)
                throw new Exception("PageLeftMargin was not preserved after saving.");

            if (Math.Abs(props.PageRightMargin.Value - rightMargin) > 0.0001)
                throw new Exception("PageRightMargin was not preserved after saving.");

            if (Math.Abs(props.ScaleX.Value - scaleX) > 0.0001)
                throw new Exception("ScaleX was not preserved after saving.");

            if (Math.Abs(props.ScaleY.Value - scaleY) > 0.0001)
                throw new Exception("ScaleY was not preserved after saving.");

            if (props.PagesX.Value != pagesX)
                throw new Exception("PagesX was not preserved after saving.");

            if (props.PagesY.Value != pagesY)
                throw new Exception("PagesY was not preserved after saving.");

            if (props.PrintPageOrientation.Value != orientation)
                throw new Exception("PrintPageOrientation was not preserved after saving.");

            if (props.OnPage.Value != onPage)
                throw new Exception("OnPage flag was not preserved after saving.");

            // All validations passed.
            Console.WriteLine("PrintProps validation succeeded: all modified values were retained after saving and reloading.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation error: {ex.Message}");
        }
        finally
        {
            // Dispose the reloaded diagram.
            reloadedDiagram?.Dispose();
        }
    }
}