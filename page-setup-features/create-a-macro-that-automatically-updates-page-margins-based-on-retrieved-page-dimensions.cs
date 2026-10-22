using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

            // Iterate through all pages to update margins based on page size.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page width and height (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate margins as 5% of the respective dimensions.
                double leftMargin = pageWidth * 0.05;
                double rightMargin = pageWidth * 0.05;
                double topMargin = pageHeight * 0.05;
                double bottomMargin = pageHeight * 0.05;

                // Ensure custom margins are used.
                page.PageSheet.PrintProps.OnPage.Value = BOOL.True;

                // Apply the calculated margins.
                page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
            }

            // Save the updated diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}