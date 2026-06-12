using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to add a watermark that stays within printable margins
                foreach (Page page in diagram.Pages)
                {
                    // Page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Printable area margins (in inches)
                    double leftMargin = page.PageSheet.PrintProps.PageLeftMargin.Value;
                    double rightMargin = page.PageSheet.PrintProps.PageRightMargin.Value;
                    double topMargin = page.PageSheet.PrintProps.PageTopMargin.Value;
                    double bottomMargin = page.PageSheet.PrintProps.PageBottomMargin.Value;

                    // Compute printable width and height
                    double printableWidth = pageWidth - leftMargin - rightMargin;
                    double printableHeight = pageHeight - topMargin - bottomMargin;

                    // Center position within printable area
                    double centerX = leftMargin + printableWidth / 2.0;
                    double centerY = bottomMargin + printableHeight / 2.0;

                    // Add watermark text covering the printable area
                    // Font size is specified in inches (e.g., 0.5 inches ≈ 36 points)
                    double fontSizeInInches = 0.5;
                    page.AddText(
                        pinX: centerX,
                        pinY: centerY,
                        width: printableWidth,
                        height: printableHeight,
                        text: "CONFIDENTIAL",
                        fontName: "Arial",
                        fontColor: "#808080",
                        size: fontSizeInInches);
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
