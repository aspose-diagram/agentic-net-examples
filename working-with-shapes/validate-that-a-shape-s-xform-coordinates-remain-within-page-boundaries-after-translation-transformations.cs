using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <translateX> <translateY>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse translation offsets.
        if (!double.TryParse(args[1], out double translateX) ||
            !double.TryParse(args[2], out double translateY))
        {
            Console.Error.WriteLine("Translation values must be valid numbers.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Iterate through each shape on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply translation to the shape's position.
                    shape.XForm.PinX.Value += translateX;
                    shape.XForm.PinY.Value += translateY;

                    // Refresh shape data after modification (optional but safe).
                    shape.RefreshData();

                    // Calculate shape bounds.
                    double halfWidth = shape.XForm.Width.Value / 2.0;
                    double halfHeight = shape.XForm.Height.Value / 2.0;
                    double left = shape.XForm.PinX.Value - halfWidth;
                    double right = shape.XForm.PinX.Value + halfWidth;
                    double bottom = shape.XForm.PinY.Value - halfHeight;
                    double top = shape.XForm.PinY.Value + halfHeight;

                    // Validate bounds against page dimensions.
                    bool withinHorizontal = left >= 0 && right <= pageWidth;
                    bool withinVertical = bottom >= 0 && top <= pageHeight;

                    if (withinHorizontal && withinVertical)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} is within page bounds after translation.");
                    }
                    else
                    {
                        Console.WriteLine($"Shape ID {shape.ID} is OUT OF BOUNDS after translation:");
                        if (!withinHorizontal)
                            Console.WriteLine($"  Horizontal: left={left:F2}, right={right:F2}, pageWidth={pageWidth:F2}");
                        if (!withinVertical)
                            Console.WriteLine($"  Vertical:   bottom={bottom:F2}, top={top:F2}, pageHeight={pageHeight:F2}");
                    }
                }
            }

            // Save the modified diagram to a new file.
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "translated_output.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}