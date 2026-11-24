using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Define output path (original name with suffix)
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_adjusted.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Simple offset to apply when moving overlapping shapes (in inches)
            const double offsetX = 0.5;
            const double offsetY = 0.0; // only horizontal shift for this example

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes into a list to allow index-based comparison
                var shapes = new System.Collections.Generic.List<Shape>();
                foreach (Shape s in page.Shapes)
                {
                    shapes.Add(s);
                }

                // Compare each pair of shapes for overlap
                for (int i = 0; i < shapes.Count; i++)
                {
                    Shape shapeA = shapes[i];
                    // Skip shapes without geometry (e.g., connectors) by checking Width/Height
                    if (shapeA.XForm.Width == null || shapeA.XForm.Height == null)
                        continue;

                    // Compute bounding rectangle for shape A
                    double aLeft = shapeA.XForm.PinX.Value - shapeA.XForm.Width.Value / 2.0;
                    double aRight = shapeA.XForm.PinX.Value + shapeA.XForm.Width.Value / 2.0;
                    double aTop = shapeA.XForm.PinY.Value + shapeA.XForm.Height.Value / 2.0;
                    double aBottom = shapeA.XForm.PinY.Value - shapeA.XForm.Height.Value / 2.0;

                    for (int j = i + 1; j < shapes.Count; j++)
                    {
                        Shape shapeB = shapes[j];
                        if (shapeB.XForm.Width == null || shapeB.XForm.Height == null)
                            continue;

                        // Compute bounding rectangle for shape B
                        double bLeft = shapeB.XForm.PinX.Value - shapeB.XForm.Width.Value / 2.0;
                        double bRight = shapeB.XForm.PinX.Value + shapeB.XForm.Width.Value / 2.0;
                        double bTop = shapeB.XForm.PinY.Value + shapeB.XForm.Height.Value / 2.0;
                        double bBottom = shapeB.XForm.PinY.Value - shapeB.XForm.Height.Value / 2.0;

                        // Check for overlap (simple AABB intersection)
                        bool overlap = aLeft < bRight && aRight > bLeft && aBottom < bTop && aTop > bBottom;

                        if (overlap)
                        {
                            // Adjust shape B position to the right by offsetX
                            double newPinX = shapeB.XForm.PinX.Value + offsetX;
                            shapeB.XForm.PinX.Value = newPinX;

                            // Optionally, also adjust shape A to the left to spread them
                            // double newPinX_A = shapeA.XForm.PinX.Value - offsetX;
                            // shapeA.XForm.PinX.Value = newPinX_A;

                            // Recalculate rectangle for shape B after move (optional for further checks)
                            bLeft = shapeB.XForm.PinX.Value - shapeB.XForm.Width.Value / 2.0;
                            bRight = shapeB.XForm.PinX.Value + shapeB.XForm.Width.Value / 2.0;
                        }
                    }
                }
            }

            // Save the adjusted diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Adjusted diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}