using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting arguments: <diagramPath> <shapeId> <leftPx> <rightPx> <topPx> <bottomPx>
        if (args.Length != 6)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId> <leftPx> <rightPx> <topPx> <bottomPx>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse numeric arguments with validation
        if (!int.TryParse(args[1], out int shapeId))
        {
            Console.Error.WriteLine("Error: Invalid shapeId.");
            return;
        }
        if (!int.TryParse(args[2], out int leftPx) ||
            !int.TryParse(args[3], out int rightPx) ||
            !int.TryParse(args[4], out int topPx) ||
            !int.TryParse(args[5], out int bottomPx))
        {
            Console.Error.WriteLine("Error: One or more margin values are not valid integers.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Convert pixel values to points (1 pixel = 0.75 point at 96 DPI)
            double leftPoints = leftPx * 0.75;
            double rightPoints = rightPx * 0.75;
            double topPoints = topPx * 0.75;
            double bottomPoints = bottomPx * 0.75;

            // Create DoubleValue objects with PT (points) unit
            DoubleValue leftMargin = new DoubleValue(leftPoints, MeasureConst.PT);
            DoubleValue rightMargin = new DoubleValue(rightPoints, MeasureConst.PT);
            DoubleValue topMargin = new DoubleValue(topPoints, MeasureConst.PT);
            DoubleValue bottomMargin = new DoubleValue(bottomPoints, MeasureConst.PT);

            // Apply margins to the shape's TextBlock section
            shape.TextBlock.LeftMargin = leftMargin;
            shape.TextBlock.RightMargin = rightMargin;
            shape.TextBlock.TopMargin = topMargin;
            shape.TextBlock.BottomMargin = bottomMargin;

            // Prepare output file path
            string outputDir = Path.GetDirectoryName(diagramPath);
            string outputFileName = Path.GetFileNameWithoutExtension(diagramPath) + "_modified.vsdx";
            string outputPath = Path.Combine(outputDir, outputFileName);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}