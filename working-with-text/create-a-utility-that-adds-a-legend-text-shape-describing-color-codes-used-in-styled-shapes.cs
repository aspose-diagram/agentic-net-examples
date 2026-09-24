using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramLegendUtility <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Use the first page (you can adjust as needed)
            Page page = diagram.Pages[0];

            // Collect unique fill foreground colors from all shapes on the page
            HashSet<string> uniqueColors = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Retrieve the fill foreground color (hex string like "#FF0000")
                string color = shape.Fill?.FillForegnd?.Value;
                if (!string.IsNullOrWhiteSpace(color))
                {
                    uniqueColors.Add(color.Trim());
                }
            }

            // Build legend text
            List<string> lines = new List<string>();
            lines.Add("Legend (Color Codes):");
            foreach (string color in uniqueColors)
            {
                lines.Add($"• {color}");
            }
            string legendText = string.Join("\n", lines);

            // Determine size of the legend shape
            double width = 2.5; // inches
            double lineHeight = 0.2; // inches per line
            double height = lineHeight * lines.Count + 0.2; // extra padding

            // Position the legend near the top‑left corner of the page
            double pinX = 0.5; // inches from left edge
            double pinY = page.PageSheet.PageProps.PageHeight.Value - 0.5; // inches from bottom (Visio Y axis grows upwards)

            // Add a text shape for the legend
            Shape legendShape = page.AddText(pinX, pinY, width, height, legendText);

            // Optional: set a light background fill for readability
            legendShape.Fill.FillPattern.Value = 1; // solid fill
            legendShape.Fill.FillForegnd.Value = "#FFFFFF"; // white background
            legendShape.Line.LinePattern.Value = 0; // no border

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Legend added and diagram saved to '{outputPath}'.");
        }
    }