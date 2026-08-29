using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Validate argument count: input file, output file, offset X, offset Y
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputPath> <outputPath> <offsetX> <offsetY>");
            return;
        }

        // Assign and guard input file path
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign output file path (no existence guard needed)
        string outputPath = args[1];

        // Parse offset values with validation
        if (!double.TryParse(args[2], out double offsetX))
        {
            Console.Error.WriteLine($"Invalid offsetX value: {args[2]}");
            return;
        }
        if (!double.TryParse(args[3], out double offsetY))
        {
            Console.Error.WriteLine($"Invalid offsetY value: {args[3]}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0) of the diagram
            Page page = diagram.Pages[0];

            // Locate the first hexagon shape by checking its master name
            Shape hexagonShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has an associated master before accessing its name
                if (shape.Master != null && shape.Master.Name == "Hexagon")
                {
                    hexagonShape = shape;
                    break;
                }
            }

            // If no hexagon shape is found, report and exit
            if (hexagonShape == null)
            {
                Console.Error.WriteLine("Hexagon shape not found on the first page.");
                return;
            }

            // Retrieve original position and size of the hexagon
            double originalPinX = hexagonShape.XForm.PinX.Value;
            double originalPinY = hexagonShape.XForm.PinY.Value;
            double originalWidth = hexagonShape.XForm.Width.Value;
            double originalHeight = hexagonShape.XForm.Height.Value;

            // Compute new position by applying the offset
            double newPinX = originalPinX + offsetX;
            double newPinY = originalPinY + offsetY;

            // Add a new shape using the same master name at the new location
            // The fourth parameter 'isCalculate' is set to false to avoid automatic layout recalculation
            long newShapeId = page.AddShape(newPinX, newPinY, hexagonShape.Master.Name, false);

            // Retrieve the newly added shape instance
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Copy text content from the original hexagon to the new shape (if any)
            string originalText = hexagonShape.Text.Value.ToString();
            if (!string.IsNullOrWhiteSpace(originalText))
            {
                // Clear any existing text in the new shape
                newShape.Text.Value.Clear();
                // Add the original text as a new text run
                newShape.Text.Value.Add(new Txt(originalText));
            }

            // Optionally copy other visual properties (fill, line) if desired
            // Example: copy fill foreground color
            newShape.Fill.FillForegnd.Value = hexagonShape.Fill.FillForegnd.Value;
            // Example: copy line color
            newShape.Line.LineColor.Value = hexagonShape.Line.LineColor.Value;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}