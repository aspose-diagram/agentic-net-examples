using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, watermark text
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <watermark text>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string watermarkText = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Retrieve page margins (in inches)
                double leftMargin = page.PageSheet.PrintProps.PageLeftMargin.Value;
                double rightMargin = page.PageSheet.PrintProps.PageRightMargin.Value;
                double topMargin = page.PageSheet.PrintProps.PageTopMargin.Value;
                double bottomMargin = page.PageSheet.PrintProps.PageBottomMargin.Value;

                // Calculate printable area dimensions
                double printableWidth = pageWidth - leftMargin - rightMargin;
                double printableHeight = pageHeight - topMargin - bottomMargin;

                // Ensure printable dimensions are positive
                if (printableWidth <= 0 || printableHeight <= 0)
                {
                    Console.Error.WriteLine($"Page {page.Name} has invalid margins resulting in non‑positive printable area.");
                    continue;
                }

                // Position the watermark at the top‑left corner of the printable area
                double pinX = leftMargin;
                double pinY = topMargin;

                // Define watermark appearance
                string fontName = "Calibri";
                string fontColor = "#a5a5a5"; // Light gray
                double fontSize = 0.25; // In inches (approx. 18 pt)

                // Add the watermark text confined to the printable area
                page.AddText(pinX, pinY, printableWidth, printableHeight, watermarkText, fontName, fontColor, fontSize);
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}