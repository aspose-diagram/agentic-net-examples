using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

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

            // Total number of pages in the diagram.
            int pageCount = diagram.Pages.Count;

            // Iterate through each page to add a custom footer.
            for (int i = 0; i < pageCount; i++)
            {
                // Retrieve the current page.
                Page page = diagram.Pages[i];

                // Get page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Determine vertical offset as 5% of the page height.
                double verticalOffset = pageHeight * 0.05;

                // Define footer text (including page number placeholders).
                string footerText = $"Page {i + 1} of {pageCount}";

                // Define a modest height for the footer text shape.
                double footerShapeHeight = 0.2; // inches

                // Add a text shape that spans the full page width, positioned using the calculated offset.
                // PinX = 0 (left edge), PinY = verticalOffset (distance from bottom), Width = pageWidth, Height = footerShapeHeight.
                page.AddText(0, verticalOffset, pageWidth, footerShapeHeight, footerText);
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}