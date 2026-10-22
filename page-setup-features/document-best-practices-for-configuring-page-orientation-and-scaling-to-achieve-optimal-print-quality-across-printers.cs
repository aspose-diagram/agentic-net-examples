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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to configure print settings.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the print properties collection for the current page.
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // ------------------------------------------------------------
                    // Best practice: Set page orientation based on content layout.
                    // Landscape is often preferred for wide diagrams; Portrait for tall ones.
                    // Here we set Landscape as an example.
                    // ------------------------------------------------------------
                    printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // ------------------------------------------------------------
                    // Best practice: Adjust scaling to fit the printer's printable area.
                    // Scaling values are expressed as a factor (1.0 = 100%).
                    // 0.75 scales the diagram to 75% of its original size, improving fit.
                    // ------------------------------------------------------------
                    printProps.ScaleX.Value = 0.75; // Horizontal scaling factor.
                    printProps.ScaleY.Value = 0.75; // Vertical scaling factor.

                    // ------------------------------------------------------------
                    // Best practice: Enable "Fit to Sheet" when you want the diagram
                    // to automatically scale to a specific number of pages.
                    // Setting OnPage to TRUE and defining PagesX/PagesY forces the
                    // diagram to fit within the given sheet layout.
                    // ------------------------------------------------------------
                    printProps.OnPage.Value = BOOL.True;   // Enable fit‑to‑sheet.
                    printProps.PagesX.Value = 1;           // Number of pages horizontally.
                    printProps.PagesY.Value = 1;           // Number of pages vertically.

                    // ------------------------------------------------------------
                    // Best practice: Define printer margins (in inches).
                    // Small margins maximize printable area; adjust as needed.
                    // ------------------------------------------------------------
                    printProps.PageLeftMargin.Value = 0.25;   // 0.25 inch left margin.
                    printProps.PageRightMargin.Value = 0.25;  // 0.25 inch right margin.
                    printProps.PageTopMargin.Value = 0.25;    // 0.25 inch top margin.
                    printProps.PageBottomMargin.Value = 0.25; // 0.25 inch bottom margin.
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}