using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: inputPath, outputPath, offsetX, offsetY
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <offsetX> <offsetY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string offsetXStr = args[2];
        string offsetYStr = args[3];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse offset values
        if (!double.TryParse(offsetXStr, out double offsetX) ||
            !double.TryParse(offsetYStr, out double offsetY))
        {
            Console.Error.WriteLine("Offset values must be valid numbers.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume the shape is on the first page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Locate the first hexagon shape (master name "Hexagon")
                Shape hexagonShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an associated master before checking its name
                    if (shape.Master != null && shape.Master.Name == "Hexagon")
                    {
                        hexagonShape = shape;
                        break;
                    }
                }

                if (hexagonShape == null)
                {
                    Console.Error.WriteLine("No hexagon shape found on the first page.");
                    return;
                }

                // Calculate new position using the provided offsets
                double newPinX = hexagonShape.XForm.PinX.Value + offsetX;
                double newPinY = hexagonShape.XForm.PinY.Value + offsetY;

                // Add a new shape based on the same master at the new location
                // page.AddShape returns the ID of the newly added shape
                long newShapeId = page.AddShape(newPinX, newPinY, hexagonShape.Master.Name);

                // Retrieve the newly added shape object to copy size and other properties
                Shape newShape = page.Shapes.GetShape(newShapeId);
                if (newShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                    return;
                }

                // Copy width and height from the original hexagon
                newShape.XForm.Width.Value = hexagonShape.XForm.Width.Value;
                newShape.XForm.Height.Value = hexagonShape.XForm.Height.Value;

                // Optionally, copy other visual properties (fill, line, etc.) if needed
                // Example: copy fill foreground color
                newShape.InheritFill.FillForegnd.Value = hexagonShape.InheritFill.FillForegnd.Value;
                // Example: copy line pattern
                newShape.InheritLine.LinePattern.Value = hexagonShape.InheritLine.LinePattern.Value;

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Hexagon shape copied successfully. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}