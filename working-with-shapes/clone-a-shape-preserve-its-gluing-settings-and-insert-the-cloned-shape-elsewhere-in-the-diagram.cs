using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input Visio file path
        // 1 - output Visio file path
        // 2 - ID of the shape to clone
        // 3 - ID of a connector shape that is glued to the original shape (to preserve gluing)
        // 4 - target PinX for the cloned shape
        // 5 - target PinY for the cloned shape
        if (args.Length < 6)
        {
            Console.Error.WriteLine("Usage: <inputVisio> <outputVisio> <originalShapeId> <connectorShapeId> <targetPinX> <targetPinY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string originalShapeIdStr = args[2];
        string connectorShapeIdStr = args[3];
        string targetPinXStr = args[4];
        string targetPinYStr = args[5];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse numeric arguments
        if (!long.TryParse(originalShapeIdStr, out long originalShapeId))
        {
            Console.Error.WriteLine("Invalid originalShapeId.");
            return;
        }
        if (!long.TryParse(connectorShapeIdStr, out long connectorShapeId))
        {
            Console.Error.WriteLine("Invalid connectorShapeId.");
            return;
        }
        if (!double.TryParse(targetPinXStr, out double targetPinX))
        {
            Console.Error.WriteLine("Invalid targetPinX.");
            return;
        }
        if (!double.TryParse(targetPinYStr, out double targetPinY))
        {
            Console.Error.WriteLine("Invalid targetPinY.");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (adjust if a specific page is required)
                Page page = diagram.Pages[0];

                // Retrieve the original shape that will be cloned
                Shape originalShape = page.Shapes.GetShape((int)originalShapeId);
                if (originalShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {originalShapeId} not found on the page.");
                    return;
                }

                // Get the master name of the original shape (used for cloning)
                string masterName = originalShape.Master?.Name;
                if (string.IsNullOrEmpty(masterName))
                {
                    Console.Error.WriteLine("Original shape does not have an associated master.");
                    return;
                }

                // Add a new shape on the same page using the same master at the target location
                // The fourth parameter (isCalculate) is a boolean indicating whether to recalculate geometry
                long clonedShapeId = page.AddShape(targetPinX, targetPinY, masterName, false);
                Shape clonedShape = page.Shapes.GetShape((int)clonedShapeId);
                if (clonedShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the cloned shape.");
                    return;
                }

                // ----- Copy visual properties from the original shape to the cloned shape -----

                // Copy text
                clonedShape.Text.Value.Clear();
                string originalText = originalShape.Text.Value.Text;
                if (!string.IsNullOrEmpty(originalText))
                {
                    clonedShape.Text.Value.Add(new Txt(originalText));
                }

                // Copy fill properties (foreground color and pattern)
                clonedShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
                clonedShape.Fill.FillPattern.Value = originalShape.Fill.FillPattern.Value;

                // Copy line properties (color, weight, pattern)
                clonedShape.Line.LineColor.Value = originalShape.Line.LineColor.Value;
                clonedShape.Line.LineWeight.Value = originalShape.Line.LineWeight.Value;
                clonedShape.Line.LinePattern.Value = originalShape.Line.LinePattern.Value;

                // Preserve gluing: glue the specified connector to the cloned shape
                page.GlueShapes(clonedShapeId, ConnectionPointPlace.Center, connectorShapeId);

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}