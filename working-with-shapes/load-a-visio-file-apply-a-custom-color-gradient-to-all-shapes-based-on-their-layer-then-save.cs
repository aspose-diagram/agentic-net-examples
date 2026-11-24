using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Define gradient colors per layer index (customizable)
        var layerGradients = new Dictionary<int, (string start, string end)>
        {
            { 0, ("#FF0000", "#FFFF00") }, // Red to Yellow
            { 1, ("#00FF00", "#00FFFF") }, // Green to Cyan
            { 2, ("#0000FF", "#FF00FF") }, // Blue to Magenta
            // Add more mappings as needed
        };

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine the first layer index the shape belongs to
                        int layerIndex = -1;
                        if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                        {
                            string layerMember = shape.LayerMem.LayerMember.Value;
                            if (!string.IsNullOrEmpty(layerMember))
                            {
                                // Split semicolon‑separated list and take the first entry
                                string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                if (parts.Length > 0 && int.TryParse(parts[0], out int parsedIndex))
                                {
                                    layerIndex = parsedIndex;
                                }
                            }
                        }

                        // Retrieve gradient colors for the layer (fallback to default if not defined)
                        (string start, string end) colors;
                        if (!layerGradients.TryGetValue(layerIndex, out colors))
                        {
                            // Default gradient: light gray to dark gray
                            colors = ("#CCCCCC", "#666666");
                        }

                        // Apply gradient fill to the shape
                        // Set fill pattern to gradient (value 25)
                        shape.Fill.FillPattern.Value = 25;

                        // Enable gradient fill
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                        // Set gradient direction (0 = left‑to‑right)
                        shape.Fill.GradientFill.GradientDir.Value = 0;

                        // Clear any existing gradient stops
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add start color stop at position 0
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0, MeasureConst.NUM),
                            new ColorValue(colors.start, MeasureConst.Undefined));

                        // Add end color stop at position 1
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(1, MeasureConst.NUM),
                            new ColorValue(colors.end, MeasureConst.Undefined));
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}