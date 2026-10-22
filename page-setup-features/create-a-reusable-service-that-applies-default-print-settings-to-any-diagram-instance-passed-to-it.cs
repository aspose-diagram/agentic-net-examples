using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class PrintSettingsService
{
    /// <summary>
    /// Applies a set of default print settings to every page of the provided diagram.
    /// </summary>
    /// <param name="diagram">The Diagram instance to modify.</param>
    public void ApplyDefaultPrintSettings(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        // Iterate through all pages and configure their PrintProps.
        foreach (Page page in diagram.Pages)
        {
            // Access the PrintProps collection for the current page.
            PrintProps printProps = page.PageSheet.PrintProps;

            // Set orientation to Portrait.
            printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

            // Set scaling to 100% (no scaling).
            printProps.ScaleX.Value = 1.0;
            printProps.ScaleY.Value = 1.0;

            // Set uniform margins (0.5 inches on each side).
            printProps.PageLeftMargin.Value = 0.5;
            printProps.PageRightMargin.Value = 0.5;
            printProps.PageTopMargin.Value = 0.5;
            printProps.PageBottomMargin.Value = 0.5;

            // Disable "Fit to page" behavior.
            printProps.OnPage.Value = BOOL.False;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PrintSettingsService <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the diagram from the specified file.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        try
        {
            // Apply default print settings using the reusable service.
            var printService = new PrintSettingsService();
            printService.ApplyDefaultPrintSettings(diagram);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error applying print settings: {ex.Message}");
            diagram?.Dispose();
            return;
        }

        try
        {
            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released.
            diagram?.Dispose();
        }
    }
}