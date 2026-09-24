using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect unique fill colors and their occurrence counts
            Dictionary<string, int> colorCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages and shapes to count fill colors
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the foreground fill color (hex string)
                    string fillColor = shape.Fill.FillForegnd.Value;
                    if (!string.IsNullOrWhiteSpace(fillColor))
                    {
                        if (colorCounts.ContainsKey(fillColor))
                            colorCounts[fillColor]++;
                        else
                            colorCounts[fillColor] = 1;
                    }
                }
            }

            // Create a new page for the legend and add it to the diagram
            Page legendPage = new Page();
            diagram.Pages.Add(legendPage); // Append the legend page (Insert not supported)

            // Layout parameters for legend entries (in inches)
            double startX = 1.0;      // left margin
            double startY = 1.0;      // top margin
            double boxSize = 0.3;     // size of the color box
            double spacingY = 0.4;    // vertical spacing between entries
            double textOffsetX = 0.4; // horizontal offset for the label

            int entryIndex = 0;
            foreach (KeyValuePair<string, int> kvp in colorCounts)
            {
                double posY = startY + entryIndex * spacingY;

                // Draw a small rectangle filled with the color
                long rectId = legendPage.DrawRectangle(startX, posY, boxSize, boxSize);
                Shape rectShape = legendPage.Shapes.GetShape(rectId);
                rectShape.Fill.FillPattern.Value = 1;          // solid fill
                rectShape.Fill.FillForegnd.Value = kvp.Key;    // set hex color

                // Add a text label next to the rectangle
                string label = $"{kvp.Key} – {kvp.Value} shape(s)";
                Shape textShape = legendPage.AddText(startX + textOffsetX, posY, 3.0, 0.2, label);
                // Clear any existing text runs and add the label
                textShape.Text.Value.Clear();
                textShape.Text.Value.Add(new Txt(label));

                // Set a modest font size (points converted to inches)
                double fontSizeInches = 10.0 / 72.0;
                if (textShape.Chars.Count > 0)
                {
                    textShape.Chars[0].Size.Value = fontSizeInches;
                }

                entryIndex++;
            }

            // Save the diagram as a PDF with the legend page appended
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}