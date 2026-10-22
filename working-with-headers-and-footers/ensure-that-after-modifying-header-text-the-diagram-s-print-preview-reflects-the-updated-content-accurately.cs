using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -------------------- Modify Header Text --------------------
            diagram.HeaderFooter.HeaderLeft = "Custom Header Left";
            diagram.HeaderFooter.HeaderCenter = "Custom Header Center";
            diagram.HeaderFooter.HeaderRight = "Custom Header Right";

            // -------------------- Configure Header Font --------------------
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFont.FaceName = "Arial";          // Font family
            headerFont.Weight = 700;                // Bold weight
            headerFont.Height = 12;                 // Font size (points)
            headerFont.Italic = BOOL.False;         // No italic
            headerFont.Underline = BOOL.False;      // No underline

            // -------------------- Set Header Color --------------------
            diagram.HeaderFooter.HeaderFooterColor = Color.Black;

            // -------------------- Adjust Header Margin --------------------
            // Margin is in inches; set to half an inch from the page edge
            diagram.HeaderFooter.HeaderMargin.Value = 0.5;

            // -------------------- Ensure Print Settings Include Header --------------------
            // Access the first page's print properties
            Page page = diagram.Pages[0];
            PrintProps printProps = page.PageSheet.PrintProps;
            // Enable printing of headers/footers
            printProps.OnPage.Value = BOOL.True;

            // -------------------- Save Diagram as PDF (Print Preview Target) --------------------
            // PDF export will reflect the updated header content and print settings
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved successfully with updated header to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}