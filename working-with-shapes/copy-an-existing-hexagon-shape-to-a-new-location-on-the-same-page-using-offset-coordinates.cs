using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [offsetX] [offsetY]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse optional offset values; default to 2.0 inches for both axes.
        double offsetX = 2.0;
        double offsetY = 2.0;
        if (args.Length >= 3 && !double.TryParse(args[2], out offsetX))
        {
            Console.Error.WriteLine("Invalid offsetX value. It must be a number.");
            return;
        }
        if (args.Length >= 4 && !double.TryParse(args[3], out offsetY))
        {
            Console.Error.WriteLine("Invalid offsetY value. It must be a number.");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Locate the first hexagon shape on the page.
                Shape hexagonShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has an associated master.
                    if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                    {
                        // Compare master name to "Hexagon" (case‑insensitive).
                        if (string.Equals(shape.Master.Name, "Hexagon", StringComparison.OrdinalIgnoreCase))
                        {
                            hexagonShape = shape;
                            break;
                        }
                    }
                }

                if (hexagonShape == null)
                {
                    Console.Error.WriteLine("No hexagon shape was found on the first page.");
                    return;
                }

                // Retrieve the original position of the hexagon.
                double originalPinX = hexagonShape.XForm.PinX.Value;
                double originalPinY = hexagonShape.XForm.PinY.Value;

                // Compute the new position using the provided offsets.
                double newPinX = originalPinX + offsetX;
                double newPinY = originalPinY + offsetY;

                // Add a new shape using the same master as the original hexagon.
                // The fourth parameter (isCalculate) must be a bool; false disables automatic geometry calculation.
                long newShapeId = page.AddShape(newPinX, newPinY, hexagonShape.Master.Name, false);

                // Retrieve the newly added shape instance (optional, for further tweaks).
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Example tweak: ensure the new shape has non‑zero size (in case the master defines zero size).
                if (newShape.XForm.Width.Value == 0)
                    newShape.XForm.Width.Value = 1.0;
                if (newShape.XForm.Height.Value == 0)
                    newShape.XForm.Height.Value = 1.0;

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Hexagon shape copied successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}