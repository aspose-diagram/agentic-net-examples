using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (semicolon‑separated indexes).
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue; // Shape does not belong to any layer.

                        // Use the first layer index for gradient selection.
                        string[] layerIndices = layerMember.Split(';');
                        if (layerIndices.Length == 0)
                            continue;

                        if (!int.TryParse(layerIndices[0], out int layerIdx))
                            continue; // Invalid index.

                        // Ensure the index is within the page's layer collection range.
                        if (layerIdx < 0 || layerIdx >= page.PageSheet.Layers.Count)
                            continue; // Index out of range.

                        // Obtain the layer object and its name.
                        Layer layer = page.PageSheet.Layers[layerIdx];
                        string layerName = layer.Name.Value ?? string.Empty;

                        // Determine gradient colors based on the layer name.
                        string startColor;
                        string endColor;

                        if (layerName.IndexOf("Red", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            startColor = "#FFCCCC";
                            endColor = "#FF0000";
                        }
                        else if (layerName.IndexOf("Green", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            startColor = "#CCFFCC";
                            endColor = "#00FF00";
                        }
                        else if (layerName.IndexOf("Blue", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            startColor = "#CCCCFF";
                            endColor = "#0000FF";
                        }
                        else
                        {
                            // Default gradient.
                            startColor = "#EEEEEE";
                            endColor = "#555555";
                        }

                        // Apply a linear gradient fill to the shape.
                        shape.Fill.FillPattern.Value = 25; // Gradient fill pattern.
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                        shape.Fill.GradientFill.GradientDir.Value = 0; // Left‑to‑right direction.

                        // Clear any existing gradient stops.
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add new gradient stops (position 0 and 1).
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0, MeasureConst.NUM),
                            new ColorValue(startColor, MeasureConst.Undefined));

                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(1, MeasureConst.NUM),
                            new ColorValue(endColor, MeasureConst.Undefined));
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}