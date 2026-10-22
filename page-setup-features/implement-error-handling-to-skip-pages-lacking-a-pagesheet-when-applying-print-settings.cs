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
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Skip pages that do not have a PageSheet.
                if (page.PageSheet == null)
                {
                    Console.WriteLine($"Skipping page '{page.Name}' because it lacks a PageSheet.");
                    continue;
                }

                // Access the print properties of the page.
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set page orientation to Landscape.
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set uniform margins (in inches).
                printProps.PageLeftMargin.Value = 0.5;
                printProps.PageRightMargin.Value = 0.5;
                printProps.PageTopMargin.Value = 0.5;
                printProps.PageBottomMargin.Value = 0.5;

                // Set scaling factors.
                printProps.ScaleX.Value = 1;
                printProps.ScaleY.Value = 1;

                // Enable Fit‑to‑Sheet and define sheet layout.
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}