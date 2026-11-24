using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the page's print properties object
                PrintProps printProps = page.PageSheet.PrintProps;

                // Update page print settings:
                // Set margins (values are in inches)
                printProps.PageLeftMargin.Value = 0.5;   // left margin
                printProps.PageRightMargin.Value = 0.5;  // right margin
                printProps.PageTopMargin.Value = 0.5;    // top margin
                printProps.PageBottomMargin.Value = 0.5; // bottom margin

                // Set scaling factors (1.0 = 100%)
                printProps.ScaleX.Value = 1.0;
                printProps.ScaleY.Value = 1.0;

                // Set page orientation to portrait
                // PrintPageOrientationValue.Portrait = portrait, .Landscape = landscape
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                // Optionally enable fit-to-page (uncomment if needed)
                // printProps.OnPage.Value = BOOL.True;
                // printProps.PagesX.Value = 1;
                // printProps.PagesY.Value = 1;

                // Save the modified diagram to a new Visio file (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}