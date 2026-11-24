using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : inputPath;

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and update margins based on page dimensions
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page width and height (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate margins as 5% of respective dimensions
                double leftRightMargin = pageWidth * 0.05;
                double topBottomMargin = pageHeight * 0.05;

                // Update print margins (values are in inches)
                page.PageSheet.PrintProps.PageLeftMargin.Value = leftRightMargin;
                page.PageSheet.PrintProps.PageRightMargin.Value = leftRightMargin;
                page.PageSheet.PrintProps.PageTopMargin.Value = topBottomMargin;
                page.PageSheet.PrintProps.PageBottomMargin.Value = topBottomMargin;
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Successfully updated page margins and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}