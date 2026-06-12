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

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define a set of base colors to use for gradients
                string[] baseColors = new string[]
                {
                    "#FF0000", // Red
                    "#00FF00", // Green
                    "#0000FF", // Blue
                    "#FFFF00", // Yellow
                    "#FF00FF", // Magenta
                    "#00FFFF"  // Cyan
                };

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (e.g., "0;2")
                        string layerMember = shape.LayerMem?.LayerMember?.Value ?? string.Empty;

                        // If the shape is not assigned to any layer, skip it
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Use the first layer index for determining the gradient colors
                        string[] parts = layerMember.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 0)
                            continue;

                        if (!int.TryParse(parts[0], out int layerIndex))
                            continue;

                        // Determine start and end colors based on the layer index
                        string startColor = baseColors[layerIndex % baseColors.Length];
                        string endColor = baseColors[(layerIndex + 1) % baseColors.Length];

                        // Apply a linear gradient fill to the shape
                        shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                        shape.Fill.GradientFill.GradientDir.Value = 0; // Direction (0 = left to right)

                        // Clear any existing gradient stops
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add gradient stops: start at position 0, end at position 1
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0, MeasureConst.NUM),
                            new ColorValue(startColor, MeasureConst.Undefined));

                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(1, MeasureConst.NUM),
                            new ColorValue(endColor, MeasureConst.Undefined));
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }