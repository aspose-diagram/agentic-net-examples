using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect arguments: inputVisioPath outputVisioPath shapeId duplicateCount offsetX offsetY
        if (args.Length < 6)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputVisioPath> <shapeId> <duplicateCount> <offsetX> <offsetY>");
            return;
        }

        // Assign and guard input file path
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign output path (no existence guard needed)
        string outputPath = args[1];

        // Parse shape ID to duplicate
        if (!long.TryParse(args[2], out long originalShapeId))
        {
            Console.Error.WriteLine("Invalid shape ID.");
            return;
        }

        // Parse number of duplicates
        if (!int.TryParse(args[3], out int duplicateCount) || duplicateCount < 1)
        {
            Console.Error.WriteLine("Invalid duplicate count.");
            return;
        }

        // Parse X offset per duplicate
        if (!double.TryParse(args[4], out double offsetX))
        {
            Console.Error.WriteLine("Invalid offsetX value.");
            return;
        }

        // Parse Y offset per duplicate
        if (!double.TryParse(args[5], out double offsetY))
        {
            Console.Error.WriteLine("Invalid offsetY value.");
            return;
        }

        try
        {
            // Load the source Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Use the first page (index 0) for duplication
            Page page = diagram.Pages[0];

            // Retrieve the original shape by its ID
            Shape originalShape = page.Shapes.GetShape(originalShapeId);
            if (originalShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {originalShapeId} not found on page 0.");
                return;
            }

            // Ensure the original shape has an associated master (required for AddShape)
            if (originalShape.Master == null)
            {
                Console.Error.WriteLine("Original shape does not have a master; cannot duplicate.");
                return;
            }

            // Store the master name for reuse
            string masterName = originalShape.Master.Name;

            // Loop to create the requested number of duplicates
            for (int i = 1; i <= duplicateCount; i++)
            {
                // Compute new position by applying incremental offsets
                double newPinX = originalShape.XForm.PinX.Value + i * offsetX;
                double newPinY = originalShape.XForm.PinY.Value + i * offsetY;

                // Add a new shape using the same master; isCalculate set to false
                long newShapeId = page.AddShape(newPinX, newPinY, masterName, false);

                // Retrieve the newly added shape (optional: further customization can be done here)
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Copy text from the original shape to the new shape
                if (!string.IsNullOrWhiteSpace(originalShape.Text.Value.ToString()))
                {
                    newShape.Text.Value.Clear();
                    newShape.Text.Value.Add(new Txt(originalShape.Text.Value.ToString()));
                }

                // Copy fill color (foreground) if needed
                newShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;

                // Copy line color and weight if needed
                newShape.Line.LineColor.Value = originalShape.Line.LineColor.Value;
                newShape.Line.LineWeight.Value = originalShape.Line.LineWeight.Value;
            }

            // Save the modified diagram to the specified output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}