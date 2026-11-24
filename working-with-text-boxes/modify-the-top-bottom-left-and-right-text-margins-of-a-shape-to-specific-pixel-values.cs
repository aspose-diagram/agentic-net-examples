using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0: input diagram path
        // 1: shape NameU to modify
        // 2: left margin in pixels
        // 3: right margin in pixels
        // 4: top margin in pixels
        // 5: bottom margin in pixels
        // 6 (optional): output diagram path
        if (args.Length < 6)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeNameU> <leftPx> <rightPx> <topPx> <bottomPx> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string shapeNameU = args[1];
        string outputPath = args.Length >= 7
            ? args[6]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse pixel values; fallback to 0 on parse failure.
        double leftPx = double.TryParse(args[2], out double lp) ? lp : 0;
        double rightPx = double.TryParse(args[3], out double rp) ? rp : 0;
        double topPx = double.TryParse(args[4], out double tp) ? tp : 0;
        double bottomPx = double.TryParse(args[5], out double bp) ? bp : 0;

        // Convert pixels to inches (96 DPI is the standard conversion).
        const double dpi = 96.0;
        double leftInches = leftPx / dpi;
        double rightInches = rightPx / dpi;
        double topInches = topPx / dpi;
        double bottomInches = bottomPx / dpi;

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the shape with the specified NameU across all pages.
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.Equals(shapeNameU, StringComparison.OrdinalIgnoreCase))
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with NameU \"{shapeNameU}\" not found in the diagram.");
                    return;
                }

                // Create DoubleValue objects using inches as the unit.
                DoubleValue leftMargin = new DoubleValue(leftInches, MeasureConst.IN);
                DoubleValue rightMargin = new DoubleValue(rightInches, MeasureConst.IN);
                DoubleValue topMargin = new DoubleValue(topInches, MeasureConst.IN);
                DoubleValue bottomMargin = new DoubleValue(bottomInches, MeasureConst.IN);

                // Apply margins to the shape's TextBlock section.
                targetShape.TextBlock.LeftMargin = leftMargin;
                targetShape.TextBlock.RightMargin = rightMargin;
                targetShape.TextBlock.TopMargin = topMargin;
                targetShape.TextBlock.BottomMargin = bottomMargin;

                // Save the modified diagram using the correct overload.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}