using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

public static class DiagramPrintService
{
    // Applies default print settings to every page of the provided diagram.
    public static void ApplyDefaultPrintSettings(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        try
        {
            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Access the print properties of the current page.
                var printProps = page.PageSheet.PrintProps;

                // Set orientation to Landscape.
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set scaling to 100% (no scaling).
                printProps.ScaleX.Value = 1.0;
                printProps.ScaleY.Value = 1.0;

                // Enable fit-to-sheet: print on a single page (1x1).
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;

                // Define uniform margins (in inches).
                printProps.PageTopMargin.Value = 0.5;
                printProps.PageBottomMargin.Value = 0.5;
                printProps.PageLeftMargin.Value = 0.5;
                printProps.PageRightMargin.Value = 0.5;
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur while applying print settings.
            Console.Error.WriteLine($"Error applying print settings: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram instance.
        Diagram diagram = new Diagram();

        // Guard: ensure the diagram instance was created successfully.
        if (diagram == null)
        {
            Console.Error.WriteLine("Failed to create Diagram instance.");
            return;
        }

        // Apply default print settings to the diagram.
        try
        {
            DiagramPrintService.ApplyDefaultPrintSettings(diagram);
        }
        catch (Exception ex)
        {
            // Log any unexpected errors from the service call.
            Console.Error.WriteLine($"Service error: {ex.Message}");
        }
    }
}