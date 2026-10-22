using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_with_watermark.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // ------------------------------
            // Set page margins (in inches)
            // ------------------------------
            // Example margins: 0.5 inch on each side
            double leftMargin = 0.5;
            double rightMargin = 0.5;
            double topMargin = 0.5;
            double bottomMargin = 0.5;

            page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
            page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
            page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
            page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;

            // ------------------------------
            // Calculate printable area dimensions
            // ------------------------------
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            double printableWidth = pageWidth - leftMargin - rightMargin;
            double printableHeight = pageHeight - topMargin - bottomMargin;

            if (printableWidth <= 0 || printableHeight <= 0)
            {
                Console.Error.WriteLine("Margins are too large, resulting in non‑positive printable area.");
                return;
            }

            // ------------------------------
            // Add watermark text within printable area
            // ------------------------------
            // Position the watermark at the top‑left of the printable rectangle.
            // Width and height cover the entire printable area so the text can be centered automatically.
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Calibri";
            string fontColor = "#a5a5a5"; // Light gray
            double fontSize = 0.25; // In inches (approx 18 pt)

            // AddText parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize
            page.AddText(
                leftMargin,               // pinX
                topMargin,                // pinY
                printableWidth,           // width
                printableHeight,          // height
                watermarkText,
                fontName,
                fontColor,
                fontSize
            );

            // ------------------------------
            // Save the modified diagram
            // ------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Watermarked diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}