using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Temporary PDF path (legend will be added later)
        string tempPdfPath = Path.Combine(Path.GetDirectoryName(outputPath) ?? "", "temp_legend.pdf");

        try
        {
            // Load the Visio diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // 1. Collect unique fill colors and associate them with shape texts
                // -----------------------------------------------------------------
                var colorMeanings = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve fill color (fallback to white if not set)
                        string fillColor = "#FFFFFF";
                        if (shape.Fill != null && shape.Fill.FillForegnd != null && !string.IsNullOrEmpty(shape.Fill.FillForegnd.Value))
                        {
                            fillColor = shape.Fill.FillForegnd.Value;
                        }

                        // Retrieve shape text (plain string)
                        string meaning = shape.Text?.Value?.Text ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(meaning))
                            meaning = "(no text)";

                        // Store in dictionary
                        if (!colorMeanings.TryGetValue(fillColor, out var list))
                        {
                            list = new List<string>();
                            colorMeanings[fillColor] = list;
                        }
                        if (!list.Contains(meaning))
                            list.Add(meaning);
                    }
                }

                // -----------------------------------------------------------------
                // 2. Create a new page that will hold the legend (added at the end)
                // -----------------------------------------------------------------
                Page legendPage = new Page();
                diagram.Pages.Add(legendPage);

                // Layout parameters for legend entries (in inches)
                double startX = 1.0;
                double startY = 1.0;
                double entryHeight = 0.6;
                double rectWidth = 0.4;
                double rectHeight = 0.4;
                double textOffsetX = 0.6;

                double currentY = startY;

                foreach (var kvp in colorMeanings)
                {
                    string color = kvp.Key;
                    List<string> meanings = kvp.Value;

                    // ---- Rectangle shape representing the color ----
                    long rectId = legendPage.AddShape(startX, currentY, "Rectangle", false); // changed BOOL.False to bool false
                    Shape rectShape = legendPage.Shapes.GetShape(rectId);
                    rectShape.Fill.FillPattern.Value = 1; // solid fill
                    rectShape.Fill.FillForegnd.Value = color;
                    rectShape.XForm.Width.Value = rectWidth;
                    rectShape.XForm.Height.Value = rectHeight;

                    // ---- Text shape describing the meanings ----
                    string text = $"{color}: {string.Join(", ", meanings)}";
                    Shape textShape = legendPage.AddText(startX + textOffsetX, currentY, 4.0, rectHeight, text);
                    // Clear any existing runs and add our text
                    textShape.Text.Value.Clear();
                    textShape.Text.Value.Add(new Txt(text));

                    // Move to next entry position
                    currentY += entryHeight;
                }

                // -----------------------------------------------------------------
                // 3. Save the diagram (including the legend) as a PDF to a temp file
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;
                pdfOptions.JpegQuality = 100;
                pdfOptions.ExportHiddenPage = false;
                pdfOptions.DefaultFont = "Arial";

                diagram.Save(tempPdfPath, pdfOptions);
            }

            // -----------------------------------------------------------------
            // 4. Reorder PDF pages so that the legend page becomes the first page
            //    (using Aspose.Pdf with fully qualified names to avoid ambiguity)
            // -----------------------------------------------------------------
            // Load the temporary PDF
            var pdfDoc = new Aspose.Pdf.Document(tempPdfPath);

            // If there is more than one page, move the last page (legend) to the front
            if (pdfDoc.Pages.Count > 1)
            {
                // Extract the legend page
                Aspose.Pdf.Page legendPdfPage = pdfDoc.Pages[pdfDoc.Pages.Count];
                // Remove it from its original position
                pdfDoc.Pages.Delete(pdfDoc.Pages.Count);
                // Insert it at the beginning (index 1 because Aspose.Pdf pages are 1‑based)
                pdfDoc.Pages.Insert(1, legendPdfPage);
            }

            // Save the reordered PDF to the final output path
            pdfDoc.Save(outputPath);
            // Clean up temporary file
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);

            Console.WriteLine($"PDF successfully created at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
