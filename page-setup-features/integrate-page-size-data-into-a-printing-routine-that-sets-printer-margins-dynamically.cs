using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string diagramPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Example: set margins to 10% of page dimensions
                double marginLeft = pageWidth * 0.10;
                double marginRight = pageWidth * 0.10;
                double marginTop = pageHeight * 0.10;
                double marginBottom = pageHeight * 0.10;

                // Apply margins to the page's print properties
                page.PageSheet.PrintProps.PageLeftMargin.Value = marginLeft;
                page.PageSheet.PrintProps.PageRightMargin.Value = marginRight;
                page.PageSheet.PrintProps.PageTopMargin.Value = marginTop;
                page.PageSheet.PrintProps.PageBottomMargin.Value = marginBottom;

                // Configure print options (optional)
                PrintSaveOptions printOptions = new PrintSaveOptions();
                // Example: set default font for missing fonts
                printOptions.DefaultFont = "Arial";

                try
                {
                    // Print to the default printer
                    diagram.Print(printOptions);
                    Console.WriteLine("Printing completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Printing failed: {ex.Message}");
                    throw;
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
