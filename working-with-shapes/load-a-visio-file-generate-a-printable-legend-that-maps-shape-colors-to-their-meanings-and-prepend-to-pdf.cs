using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect unique fill colors and their associated meanings (assumed stored in Data1)
                Dictionary<string, string> legendMap = new Dictionary<string, string>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        string color = shape.Fill.FillForegnd.Value;
                        string meaning = shape.Data1; // adjust if meaning is stored elsewhere

                        if (!string.IsNullOrWhiteSpace(color) && !string.IsNullOrWhiteSpace(meaning))
                        {
                            if (!legendMap.ContainsKey(color))
                                legendMap[color] = meaning;
                        }
                    }
                }

                // Add legend entries to the first page (prepend visually)
                Page firstPage = diagram.Pages[0];
                double startX = 0.5;      // left margin (inches)
                double startY = 0.5;      // top margin (inches)
                double boxSize = 0.3;     // size of color box (inches)
                double lineSpacing = 0.4; // vertical spacing between entries (inches)

                int entryIndex = 0;
                foreach (var kvp in legendMap)
                {
                    double currentY = startY + entryIndex * lineSpacing;

                    // Draw a small rectangle filled with the color
                    long rectId = firstPage.DrawRectangle(startX, currentY, boxSize, boxSize);
                    Shape rectShape = firstPage.Shapes.GetShape(rectId);
                    rectShape.Fill.FillForegnd.Value = kvp.Key;
                    // Remove border line
                    rectShape.Line.LinePattern.Value = 0;

                    // Add a text shape next to the rectangle with the meaning
                    double textX = startX + boxSize + 0.1;
                    double textWidth = 2.0;
                    double textHeight = boxSize;
                    Shape textShape = firstPage.AddText(textX, currentY, textWidth, textHeight, kvp.Value);
                    // Optional: set text font size (in inches, e.g., 0.12 inches ≈ 9 pt)
                    textShape.TextXForm.TxtHeight.Value = 0.12;

                    entryIndex++;
                }

                // Save the modified diagram as a PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }