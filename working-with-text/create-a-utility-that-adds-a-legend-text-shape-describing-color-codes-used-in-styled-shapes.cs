using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramLegendUtility <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Collect fill foreground colors from shapes that are not deleted
            Dictionary<string, int> colorUsage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                string fillColor = shape.Fill.FillForegnd.Value;
                if (!string.IsNullOrWhiteSpace(fillColor))
                {
                    if (colorUsage.ContainsKey(fillColor))
                        colorUsage[fillColor]++;
                    else
                        colorUsage[fillColor] = 1;
                }
            }

            // Build legend text
            var legendLines = new List<string>();
            legendLines.Add("Legend (Fill Color - Shape Count):");
            foreach (var kvp in colorUsage)
            {
                legendLines.Add($"{kvp.Key} - {kvp.Value}");
            }

            string legendText = string.Join("\n", legendLines);

            // Determine placement for the legend (top‑left corner of the page)
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
            double pinX = 1.0;                     // 1 inch from left edge
            double pinY = pageHeight - 1.0;        // 1 inch from top edge
            double legendWidth = 5.0;              // width in inches
            double legendHeight = 2.0;             // height in inches
            string fontName = "Arial";
            string fontColor = "#000000";          // black text
            double fontSizeInches = 0.2;           // approx 14 pt (14/72)

            // Add the legend as a text shape
            Shape legendShape = page.AddText(pinX, pinY, legendWidth, legendHeight,
                                            legendText, fontName, fontColor, fontSizeInches);

            // Optionally, give the legend a light background fill for readability
            legendShape.Fill.FillPattern.Value = 1;               // solid fill
            legendShape.Fill.FillForegnd.Value = "#FFFFE0";       // light yellow
            legendShape.Line.LinePattern.Value = 0;              // no border

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Legend added and diagram saved to '{outputPath}'.");
        }
    }