using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Dictionary to hold unique fill colors and their first encountered meaning
                var colorToMeaning = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages and shapes to collect color-meaning pairs
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True) continue;

                        // Retrieve the fill foreground color (hex string)
                        string fillColor = shape.Fill?.FillForegnd?.Value;
                        if (string.IsNullOrWhiteSpace(fillColor))
                            continue; // No fill color, ignore this shape

                        // Retrieve the shape's plain text
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;
                        string meaning = string.IsNullOrWhiteSpace(shapeText) ? "No Text" : shapeText.Trim();

                        // Store the first meaning encountered for each unique color
                        if (!colorToMeaning.ContainsKey(fillColor))
                        {
                            colorToMeaning[fillColor] = meaning;
                        }
                    }
                }

                // Create a new page that will hold the legend
                Page legendPage = new Page();
                diagram.Pages.Add(legendPage);
                legendPage.Name = "Legend";

                // Positioning variables for legend entries (Visio units are inches)
                double startX = 1.0;   // left margin
                double startY = 10.0;  // top margin
                double swatchWidth = 0.5;
                double entryHeight = 0.5;
                double entrySpacing = 0.2;

                // Iterate over collected colors to create legend entries
                foreach (var kvp in colorToMeaning)
                {
                    string colorHex = kvp.Key;
                    string meaning = kvp.Value;

                    // Add a rectangle shape as a color swatch
                    long swatchId = legendPage.AddShape(startX, startY, swatchWidth, entryHeight, "Rectangle", false);
                    Shape swatchShape = legendPage.Shapes.GetShape((int)swatchId);
                    swatchShape.Fill.FillPattern.Value = 1; // solid fill
                    swatchShape.Fill.FillForegnd.Value = colorHex;
                    swatchShape.XForm.Width.Value = swatchWidth;
                    swatchShape.XForm.Height.Value = entryHeight;

                    // Add a text shape next to the swatch
                    double textX = startX + swatchWidth + 0.2;
                    double textWidth = 3.0;
                    Shape textShape = legendPage.AddText(textX, startY, textWidth, entryHeight, meaning);
                    // Ensure the text shape has appropriate size
                    textShape.XForm.Width.Value = textWidth;
                    textShape.XForm.Height.Value = entryHeight;

                    // Move down for the next entry
                    startY -= (entryHeight + entrySpacing);
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram (including the legend page) as PDF
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
