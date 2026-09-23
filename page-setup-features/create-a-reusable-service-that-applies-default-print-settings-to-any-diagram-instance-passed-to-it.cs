using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

/// <summary>
/// Service that applies a set of default print settings to every page of a Diagram.
/// </summary>
public static class PrintSettingsService
{
    /// <summary>
    /// Applies default print configuration to all pages of the provided diagram.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
    public static void ApplyDefaultPrintSettings(Diagram diagram)
    {
        if (diagram == null)
            throw new ArgumentNullException(nameof(diagram));

        // Iterate over each page explicitly (do not use var in foreach as per rules)
        foreach (Page page in diagram.Pages)
        {
            // Ensure the page has a valid PageSheet and PrintProps
            if (page?.PageSheet?.PrintProps == null)
                continue;

            PrintProps printProps = page.PageSheet.PrintProps;

            // Set orientation to Landscape
            printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

            // Set scaling to 75%
            printProps.ScaleX.Value = 0.75;
            printProps.ScaleY.Value = 0.75;

            // Enable "Fit to sheet" and define one sheet across and down
            printProps.OnPage.Value = BOOL.True;
            printProps.PagesX.Value = 1;
            printProps.PagesY.Value = 1;

            // Set uniform margins (0.5 inches on each side)
            // Margins are expressed directly in inches
            double marginInInches = 0.5;
            printProps.PageTopMargin.Value = marginInInches;
            printProps.PageBottomMargin.Value = marginInInches;
            printProps.PageLeftMargin.Value = marginInInches;
            printProps.PageRightMargin.Value = marginInInches;
        }
    }
}

/// <summary>
/// Entry point for the console application demonstrating the PrintSettingsService.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default placeholder)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Apply the default print settings to every page
            PrintSettingsService.ApplyDefaultPrintSettings(diagram);

            // Define output path (same directory, different file name)
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output.vsdx");

            // Save the modified diagram using the correct SaveFileFormat enum
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with default print settings to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}