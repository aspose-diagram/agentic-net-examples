using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (first argument) or default.
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

            // Output Visio file path (second argument) or default.
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Size threshold in inches (third argument) or default 2.0 inches.
            double sizeThreshold = args.Length > 2 ? double.Parse(args[2]) : 2.0;

            // Load the diagram.
            var diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve shape dimensions.
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // If either dimension exceeds the threshold, add a red border.
                    if (width > sizeThreshold || height > sizeThreshold)
                    {
                        // Set line (border) color to red.
                        shape.Line.LineColor.Value = "#FF0000";

                        // Set line weight (thickness) – 0.02 inches as an example.
                        shape.Line.LineWeight.Value = 0.02;

                        // Optional: ensure a solid line pattern.
                        shape.Line.LinePattern.Value = LinePatternValue.Solid;
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
