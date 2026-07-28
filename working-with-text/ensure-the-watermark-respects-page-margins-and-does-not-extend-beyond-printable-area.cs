using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages to add a watermark that fits within printable area
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

                    // Center position for the watermark (pin point)
                    double pinX = leftMargin + printableWidth / 2.0;
                    double pinY = bottomMargin + printableHeight / 2.0;

                    // Watermark text and styling
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Calibri";
                    string fontColor = "#A0A0A0"; // Light gray in hex
                    double fontSizeInPoints = 72; // 1 inch = 72 points
                    double fontSizeInInches = fontSizeInPoints / 72.0;

                    // Add the watermark as a text shape that occupies the printable width
                    // Height is set to a small value; the text will be rendered within the shape bounds
                    page.AddText(
                        pinX,                     // PinX (center X)
                        pinY,                     // PinY (center Y)
                        printableWidth,           // Width of the text shape (fits printable area)
                        fontSizeInInches * 2,    // Height (enough to display the text)
                        watermarkText,
                        fontName,
                        fontColor,
                        fontSizeInInches          // Font size in inches
                    );
                }

                // Save the modified diagram
                string outputPath = "output_with_watermark.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Watermark added and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }