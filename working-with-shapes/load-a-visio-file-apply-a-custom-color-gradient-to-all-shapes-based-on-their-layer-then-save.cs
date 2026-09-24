using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

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
                    string layerMember = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(layerMember))
                        continue;

                    // Use the first layer index to decide the gradient colors
                    string[] layerIndices = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (layerIndices.Length == 0)
                        continue;

                    if (!int.TryParse(layerIndices[0], out int layerIdx))
                        continue;

                    // Choose gradient colors based on the layer index
                    // Even layers: blue → green, Odd layers: red → yellow
                    string startColor = (layerIdx % 2 == 0) ? "#0000FF" : "#FF0000";
                    string endColor   = (layerIdx % 2 == 0) ? "#00FF00" : "#FFFF00";

                    // Apply a left‑to‑right gradient fill to the shape
                    shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                    shape.Fill.GradientFill.GradientDir.Value = 0; // Left to right
                    shape.Fill.GradientFill.GradientStops.Clear();

                    // Gradient stop at position 0 (start color)
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue(startColor, MeasureConst.Undefined));

                    // Gradient stop at position 1 (end color)
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
