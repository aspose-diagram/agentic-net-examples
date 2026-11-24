using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define an output path with a different extension to preserve the original file
        string outputPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0) to modify its print settings
                Page page = diagram.Pages[0];

                // Retrieve the print properties collection
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set page orientation to Landscape
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set scaling factors (e.g., 75% of original size)
                printProps.ScaleX.Value = 0.75;
                printProps.ScaleY.Value = 0.75;

                // Enable fit-to-sheet and define pages across/down
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;

                // Define page margins in inches
                printProps.PageTopMargin.Value = 0.5;
                printProps.PageBottomMargin.Value = 0.5;
                printProps.PageLeftMargin.Value = 0.5;
                printProps.PageRightMargin.Value = 0.5;

                // Save the modified diagram to a new file with a different format (PDF)
                diagram.Save(outputPath, SaveFileFormat.Pdf);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}