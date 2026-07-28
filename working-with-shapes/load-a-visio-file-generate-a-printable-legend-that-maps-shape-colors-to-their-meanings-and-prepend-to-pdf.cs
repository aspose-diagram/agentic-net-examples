using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: DiagramLegendGenerator <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        // Guard to ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPdfPath = args[1];

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to map fill color (hex string) to concatenated shape names
            var colorMap = new System.Collections.Generic.Dictionary<string, string>();

            // Iterate through all pages and shapes to collect colors
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the foreground fill color; if empty, skip
                    string fillColor = shape.Fill.FillForegnd.Value;
                    if (string.IsNullOrWhiteSpace(fillColor))
                        continue;

                    // Use shape's NameU as its meaning; fallback to "Unnamed"
                    string meaning = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : "Unnamed";

                    if (colorMap.ContainsKey(fillColor))
                    {
                        // Append additional meaning
                        colorMap[fillColor] += ", " + meaning;
                    }
                    else
                    {
                        colorMap[fillColor] = meaning;
                    }
                }
            }

            // Create a new page for the legend
            Page legendPage = new Page();
            diagram.Pages.Add(legendPage);

            // Set legend page size to match the first page (if any)
            if (diagram.Pages.Count > 1)
            {
                Page firstPage = diagram.Pages[0];
                legendPage.PageSheet.PageProps.PageWidth.Value = firstPage.PageSheet.PageProps.PageWidth.Value;
                legendPage.PageSheet.PageProps.PageHeight.Value = firstPage.PageSheet.PageProps.PageHeight.Value;
            }

            // Layout parameters for legend entries
            double startX = 1.0;          // inches from left
            double startY = 1.0;          // inches from top
            double rectWidth = 0.5;
            double rectHeight = 0.3;
            double verticalSpacing = 0.4;
            double textOffsetX = 0.6;     // space between rectangle and text

            double currentY = startY;

            // Generate legend entries for each color
            foreach (var kvp in colorMap)
            {
                string colorHex = kvp.Key;
                string description = kvp.Value;

                // Draw a small rectangle filled with the color
                long rectId = legendPage.DrawRectangle(startX, currentY, rectWidth, rectHeight);
                Shape rectShape = legendPage.Shapes.GetShape((int)rectId);
                rectShape.Fill.FillForegnd.Value = colorHex;
                rectShape.Fill.FillPattern.Value = 1; // solid fill
                rectShape.Line.LinePattern.Value = 0; // no border

                // Add a text shape next to the rectangle
                Shape textShape = legendPage.AddText(startX + textOffsetX, currentY, 3.0, rectHeight, $"{colorHex}: {description}");
                // Ensure text uses a readable font size (in inches)
                textShape.TextXForm.TxtHeight.Value = 0.2; // approx 14pt

                // Move to next line
                currentY += verticalSpacing;
            }

            // Save the diagram as PDF with the legend page prepended
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save(outputPdfPath, pdfOptions);

            Console.WriteLine($"PDF with legend saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}