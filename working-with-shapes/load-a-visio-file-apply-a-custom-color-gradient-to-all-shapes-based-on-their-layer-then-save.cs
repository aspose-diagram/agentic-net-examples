using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: VisioGradientExample <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        // Guard to ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the document
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the collection of layers on the current page
                LayerCollection layers = page.PageSheet.Layers;

                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get the layer membership string (e.g., "0;2")
                    string layerMember = shape.LayerMem.LayerMember.Value;

                    if (string.IsNullOrEmpty(layerMember))
                        continue; // Shape is not assigned to any layer

                    // Determine which layer(s) the shape belongs to
                    foreach (Layer layer in layers)
                    {
                        // Layer index as string (used in the semicolon‑separated list)
                        string layerIndexStr = layer.IX.ToString();

                        // Check if the shape's layer membership contains this layer index
                        if (layerMember.Split(';', StringSplitOptions.RemoveEmptyEntries).Contains(layerIndexStr))
                        {
                            // Choose gradient colors based on the layer index (simple example)
                            string startColor;
                            string endColor;

                            int idx = layer.IX % 3; // Cycle through three color schemes
                            switch (idx)
                            {
                                case 0:
                                    startColor = "#FF0000"; // Red
                                    endColor = "#FFFF00";   // Yellow
                                    break;
                                case 1:
                                    startColor = "#00FF00"; // Green
                                    endColor = "#00FFFF";   // Cyan
                                    break;
                                default:
                                    startColor = "#0000FF"; // Blue
                                    endColor = "#FF00FF";   // Magenta
                                    break;
                            }

                            // Apply gradient fill to the shape
                            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                            shape.Fill.GradientFill.GradientDir.Value = 0; // Left‑to‑right
                            shape.Fill.GradientFill.GradientStops.Clear();

                            // Add gradient stop at position 0 (start color)
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(0, MeasureConst.NUM),
                                new ColorValue(startColor, MeasureConst.Undefined));

                            // Add gradient stop at position 1 (end color)
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(1, MeasureConst.NUM),
                                new ColorValue(endColor, MeasureConst.Undefined));

                            // Once applied for the first matching layer, break to avoid reapplying
                            break;
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with gradient fills to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}