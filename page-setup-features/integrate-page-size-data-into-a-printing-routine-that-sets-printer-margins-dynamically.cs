using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file
        string diagramPath = "input.vsdx";

        // Guard: ensure the diagram file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Name of the printer to use (replace with an actual printer name)
        string printerName = "Microsoft Print to PDF";

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate dynamic margins (e.g., 5% of page size)
                double leftMargin = pageWidth * 0.05;
                double rightMargin = pageWidth * 0.05;
                double topMargin = pageHeight * 0.05;
                double bottomMargin = pageHeight * 0.05;

                // Set the margins in the page's PrintProps (values are in inches)
                page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;

                // Create print options (optional configuration)
                PrintSaveOptions printOptions = new PrintSaveOptions
                {
                    // Print only foreground pages (property expects a bool)
                    SaveForegroundPagesOnly = true
                };

                // Execute the print job with the specified printer and options
                diagram.Print(printerName, printOptions);
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or printing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}