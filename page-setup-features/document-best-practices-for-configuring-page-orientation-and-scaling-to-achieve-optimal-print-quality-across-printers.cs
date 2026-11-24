using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Iterate through all pages to apply consistent print settings.
                foreach (Page page in diagram.Pages)
                {
                    // ------------------------------------------------------------
                    // 1. Set page orientation.
                    //    Landscape is often preferred for wide drawings; Portrait for tall ones.
                    // ------------------------------------------------------------
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // ------------------------------------------------------------
                    // 2. Configure scaling factors.
                    //    ScaleX and ScaleY define the percentage of the original size when printing.
                    //    1.0 = 100%; values < 1 shrink, > 1 enlarge.
                    // ------------------------------------------------------------
                    page.PageSheet.PrintProps.ScaleX.Value = 0.75; // 75% width
                    page.PageSheet.PrintProps.ScaleY.Value = 0.75; // 75% height

                    // ------------------------------------------------------------
                    // 3. Set page margins (in inches).
                    //    Margins help avoid clipping on printers with non‑printable edges.
                    // ------------------------------------------------------------
                    PrintProps printProps = page.PageSheet.PrintProps;
                    printProps.PageLeftMargin.Value = 0.25;   // left margin
                    printProps.PageRightMargin.Value = 0.25;  // right margin
                    printProps.PageTopMargin.Value = 0.25;    // top margin
                    printProps.PageBottomMargin.Value = 0.25; // bottom margin

                    // ------------------------------------------------------------
                    // 4. Enable "Fit to Sheet" (fit drawing to a specific number of pages).
                    //    This forces the printer driver to scale the output to the defined sheet count.
                    // ------------------------------------------------------------
                    printProps.OnPage.Value = BOOL.True; // activate fit‑to‑sheet
                    printProps.PagesX.Value = 1;         // pages horizontally
                    printProps.PagesY.Value = 1;         // pages vertically
                }

                // ------------------------------------------------------------
                // 5. Save the modified diagram using the VSDX format.
                //    No additional save options are required for this task.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram exceptions and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
