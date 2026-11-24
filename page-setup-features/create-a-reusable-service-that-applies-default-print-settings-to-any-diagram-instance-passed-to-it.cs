using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class PrintSettingsService
{
    /// <summary>
    /// Applies default print settings to all pages of the provided diagram.
    /// </summary>
    /// <param name="diagram">The Diagram instance to modify.</param>
    public void ApplyDefaultPrintSettings(Diagram diagram)
    {
        // Iterate through each page in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Access the print properties of the current page.
            var printProps = page.PageSheet.PrintProps;

            // Set scaling to 100% (no scaling).
            printProps.ScaleX.Value = 1.0;
            printProps.ScaleY.Value = 1.0;

            // Set uniform margins (0.5 inches on each side).
            printProps.PageLeftMargin.Value = 0.5;
            printProps.PageRightMargin.Value = 0.5;
            printProps.PageTopMargin.Value = 0.5;
            printProps.PageBottomMargin.Value = 0.5;

            // Set page orientation to Portrait.
            printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

            // Ensure the page is printed (OnPage = true).
            printProps.OnPage.Value = BOOL.True;
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
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply default print settings via the reusable service.
                var printService = new PrintSettingsService();
                printService.ApplyDefaultPrintSettings(diagram);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}