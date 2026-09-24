using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output_watermarked.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to apply the watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Retrieve printable margins (in inches)
                    double leftMargin = page.PageSheet.PrintProps.PageLeftMargin.Value;
                    double rightMargin = page.PageSheet.PrintProps.PageRightMargin.Value;
                    double topMargin = page.PageSheet.PrintProps.PageTopMargin.Value;
                    double bottomMargin = page.PageSheet.PrintProps.PageBottomMargin.Value;

                    // Calculate printable area
                    double printableWidth = pageWidth - leftMargin - rightMargin;
                    double printableHeight = pageHeight - topMargin - bottomMargin;

                    // Define watermark size (e.g., 80% of printable width, 10% of printable height)
                    double watermarkWidth = printableWidth * 0.8;
                    double watermarkHeight = printableHeight * 0.1;

                    // Center the watermark within the printable area
                    double pinX = leftMargin + printableWidth / 2.0;
                    double pinY = bottomMargin + printableHeight / 2.0;

                    // Add the watermark text.
                    // Font size is specified in inches (0.5 inches ≈ 36 points).
                    page.AddText(
                        pinX,
                        pinY,
                        watermarkWidth,
                        watermarkHeight,
                        "CONFIDENTIAL",
                        "Arial",
                        "#CCCCCC",   // Light gray color in hex
                        0.5          // Font size in inches
                    );
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
