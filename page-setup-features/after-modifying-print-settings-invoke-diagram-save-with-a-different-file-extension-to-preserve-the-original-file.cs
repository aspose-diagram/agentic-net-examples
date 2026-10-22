using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first command‑line argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define an output path with a different extension (e.g., PDF) to preserve the original file
        string outputPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page before modifying print settings
            if (diagram.Pages.Count > 0)
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the PrintProps object for page setup
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set page orientation to Landscape
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set scaling factors (e.g., 75% of original size)
                printProps.ScaleX.Value = 0.75;
                printProps.ScaleY.Value = 0.75;

                // Enable fit‑to‑sheet and define pages across/down
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;

                // Set page margins (values are in inches)
                printProps.PageTopMargin.Value = 0.5;
                printProps.PageBottomMargin.Value = 0.5;
                printProps.PageLeftMargin.Value = 0.5;
                printProps.PageRightMargin.Value = 0.5;
            }
            else
            {
                Console.Error.WriteLine("The diagram contains no pages to modify.");
                return;
            }

            // Save the modified diagram to a different format (PDF) while preserving the original file
            diagram.Save(outputPath, SaveFileFormat.Pdf);

            // Dispose the diagram to free resources
            diagram.Dispose();

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}