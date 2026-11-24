using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file to be processed.
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Retrieve the original page dimensions (in inches).
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // ---------- ScaleX = 1.0 ----------
            page.PageSheet.PrintProps.ScaleX.Value = 1.0;
            // Effective printed dimensions after applying ScaleX.
            double printedWidthScale1 = pageWidth * page.PageSheet.PrintProps.ScaleX.Value;
            double printedHeightScale1 = pageHeight * page.PageSheet.PrintProps.ScaleY.Value; // ScaleY defaults to 1.0

            Console.WriteLine("=== ScaleX = 1.0 ===");
            Console.WriteLine($"Original Page Width:  {pageWidth} inches");
            Console.WriteLine($"Original Page Height: {pageHeight} inches");
            Console.WriteLine($"Printed Width:  {printedWidthScale1} inches");
            Console.WriteLine($"Printed Height: {printedHeightScale1} inches");
            Console.WriteLine();

            // ---------- ScaleX = 0.5 ----------
            page.PageSheet.PrintProps.ScaleX.Value = 0.5;
            // Effective printed dimensions after applying ScaleX = 0.5.
            double printedWidthScaleHalf = pageWidth * page.PageSheet.PrintProps.ScaleX.Value;
            double printedHeightScaleHalf = pageHeight * page.PageSheet.PrintProps.ScaleY.Value; // ScaleY still 1.0

            Console.WriteLine("=== ScaleX = 0.5 ===");
            Console.WriteLine($"Printed Width:  {printedWidthScaleHalf} inches");
            Console.WriteLine($"Printed Height: {printedHeightScaleHalf} inches");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}