using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? "",
                Path.GetFileNameWithoutExtension(inputPath) + "_with_legend.vsdx");

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect fill foreground colors used in all shapes (excluding deleted shapes)
                Dictionary<string, int> colorCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the fill foreground color if set
                        string fillColor = shape.Fill?.FillForegnd?.Value;

                        if (!string.IsNullOrEmpty(fillColor))
                        {
                            if (colorCounts.ContainsKey(fillColor))
                                colorCounts[fillColor]++;
                            else
                                colorCounts[fillColor] = 1;
                        }
                    }
                }

                // Build legend text
                string legendText = "Legend of Fill Colors:\n";
                foreach (KeyValuePair<string, int> kvp in colorCounts)
                {
                    legendText += $"{kvp.Key}: {kvp.Value} shape(s)\n";
                }

                if (colorCounts.Count == 0)
                {
                    legendText += "No colored shapes found.";
                }

                // Add a text shape on the active page to display the legend
                // Position and size are arbitrary; adjust as needed
                double pinX = 1.0;   // X coordinate (in inches)
                double pinY = 10.0;  // Y coordinate (in inches)
                double width = 5.0;  // Width of the text box (in inches)
                double height = 5.0; // Height of the text box (in inches)

                // AddText returns a Shape object (per API)
                Shape legendShape = diagram.ActivePage.AddText(pinX, pinY, width, height, legendText);

                // Ensure the legend shape has a visible solid background fill
                legendShape.Fill.FillPattern.Value = 1; // Solid fill

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with legend to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}