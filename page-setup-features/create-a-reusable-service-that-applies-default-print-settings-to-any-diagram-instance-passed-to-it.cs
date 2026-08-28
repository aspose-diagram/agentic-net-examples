using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

public class PrintSettingsService
{
    // Applies default print settings to all pages of the diagram.
    public void ApplyDefaultPrintSettings(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        foreach (Page page in diagram.Pages)
        {
            // Ensure PageSheet and PrintProps are available.
            var printProps = page.PageSheet?.PrintProps;
            if (printProps == null) continue;

            // Orientation: Landscape.
            printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

            // Scaling: 75% (both X and Y).
            printProps.ScaleX.Value = 0.75;
            printProps.ScaleY.Value = 0.75;

            // Fit to a single sheet.
            printProps.OnPage.Value = BOOL.True;
            printProps.PagesX.Value = 1;
            printProps.PagesY.Value = 1;

            // Margins: 0.5 inches on each side.
            printProps.PageTopMargin.Value = 0.5;
            printProps.PageBottomMargin.Value = 0.5;
            printProps.PageLeftMargin.Value = 0.5;
            printProps.PageRightMargin.Value = 0.5;
        }
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load a diagram from a file.
            string inputPath = "example.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Apply the default print settings.
            PrintSettingsService service = new PrintSettingsService();
            service.ApplyDefaultPrintSettings(diagram);

            // Save the modified diagram (optional).
            string outputPath = "example_modified.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}