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

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Initialize bounding box extremes.
                    double minLeft = double.MaxValue;
                    double maxRight = double.MinValue;
                    bool hasVisibleShape = false;

                    // Examine every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape geometry.
                        double pinX = shape.XForm.PinX.Value;      // Center X (in inches)
                        double width = shape.XForm.Width.Value;    // Width (in inches)

                        // Compute left and right extents.
                        double left = pinX - (width / 2.0);
                        double right = pinX + (width / 2.0);

                        // Update bounding box.
                        if (left < minLeft) minLeft = left;
                        if (right > maxRight) maxRight = right;
                        hasVisibleShape = true;
                    }

                    // If the page contains shapes, adjust its width.
                    if (hasVisibleShape)
                    {
                        // Optional margin (in inches) to avoid shapes touching the edge.
                        const double margin = 0.5;

                        double contentWidth = maxRight - minLeft;
                        double newPageWidth = contentWidth + (2 * margin);

                        // Apply the new width to the page.
                        page.PageSheet.PageProps.PageWidth.Value = newPageWidth;

                        // Optionally, shift shapes so they start after the left margin.
                        double shiftX = margin - minLeft;
                        if (Math.Abs(shiftX) > 0.0001)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Move each shape horizontally.
                                shape.XForm.PinX.Value += shiftX;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}