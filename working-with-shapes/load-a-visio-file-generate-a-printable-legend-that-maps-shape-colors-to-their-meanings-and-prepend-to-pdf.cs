using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output PDF path (second argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPdf = args.Length > 1 ? args[1] : "output.pdf";

        try
        {
            // Load the Visio diagram
            var diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // 1. Collect distinct fill colors used in the diagram (excluding deleted shapes)
            // -----------------------------------------------------------------
            var colorSet = new HashSet<string>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get the foreground fill color (hex string like "#FF0000")
                    string fillColor = shape.Fill.FillForegnd.Value;
                    if (!string.IsNullOrWhiteSpace(fillColor))
                        colorSet.Add(fillColor);
                }
            }

            // -----------------------------------------------------------------
            // 2. Create a new legend page and add it to the diagram
            // -----------------------------------------------------------------
            var legendPage = new Page();
            // Add the legend page (will be appended; prepend not supported directly)
            diagram.Pages.Add(legendPage);

            // Page dimensions (in inches)
            double pageWidth = legendPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = legendPage.PageSheet.PageProps.PageHeight.Value;

            // Layout parameters for the legend entries
            double startX = 1.0;               // left margin
            double startY = pageHeight - 1.0;  // start from top, leaving a top margin
            double entrySpacingY = 0.6;        // vertical space between entries
            double rectWidth = 0.5;
            double rectHeight = 0.3;
            double textOffsetX = 0.7;          // distance from rectangle to text

            int index = 0;
            foreach (string colorHex in colorSet)
            {
                double currentY = startY - index * entrySpacingY;

                // -------------------------------------------------------------
                // Draw a small rectangle filled with the color
                // -------------------------------------------------------------
                long rectId = legendPage.DrawRectangle(startX, currentY, rectWidth, rectHeight);
                Shape rectShape = legendPage.Shapes.GetShape(rectId);
                rectShape.Fill.FillForegnd.Value = colorHex;   // set fill color
                rectShape.Fill.FillPattern.Value = 1;         // solid fill
                rectShape.Line.LinePattern.Value = 0;         // no border

                // -------------------------------------------------------------
                // Add a text label next to the rectangle
                // -------------------------------------------------------------
                double textPinX = startX + textOffsetX;
                double textPinY = currentY;
                double textWidth = pageWidth - textPinX - 1.0; // leave right margin
                double textHeight = rectHeight;
                string label = $"Color {colorHex}";
                legendPage.AddText(textPinX, textPinY, textWidth, textHeight, label);

                index++;
            }

            // -----------------------------------------------------------------
            // 3. Save the diagram (with the legend page) as PDF
            // -----------------------------------------------------------------
            var pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // fallback font for missing glyphs
            diagram.Save(outputPdf, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}