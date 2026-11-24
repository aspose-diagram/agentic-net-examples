using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Find the first non‑connector shape to duplicate.
                Shape originalShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // shape.OneD is true for connectors; we need a regular shape.
                    if (!shape.OneD)
                    {
                        originalShape = shape;
                        break;
                    }
                }

                if (originalShape == null)
                {
                    Console.Error.WriteLine("No suitable shape found to duplicate.");
                    return;
                }

                // Retrieve the master name of the original shape.
                string masterName = originalShape.Master?.Name;
                if (string.IsNullOrEmpty(masterName))
                {
                    Console.Error.WriteLine("The original shape does not have an associated master.");
                    return;
                }

                // Calculate a new position for the duplicated shape (offset by 2 inches).
                double offset = 2.0;
                double newPinX = originalShape.XForm.PinX.Value + offset;
                double newPinY = originalShape.XForm.PinY.Value + offset;

                // Add the duplicated shape to the page.
                // The last parameter (isCalculate) must be a bool, not BOOL.
                long duplicateShapeId = page.AddShape(newPinX, newPinY, masterName, false);
                Shape duplicateShape = page.Shapes.GetShape(duplicateShapeId);

                // Copy size from the original shape.
                duplicateShape.XForm.Width.Value = originalShape.XForm.Width.Value;
                duplicateShape.XForm.Height.Value = originalShape.XForm.Height.Value;

                // Add a dynamic connector shape (initially placed at (0,0); position will be handled by the connection).
                long connectorShapeId = page.AddShape(0, 0, "Dynamic connector", false);
                Shape connectorShape = page.Shapes.GetShape(connectorShapeId);

                // Optional: set arrow styles on the connector.
                connectorShape.Line.BeginArrow.Value = 4; // Arrow style code (e.g., 4 = Arrow)
                connectorShape.Line.EndArrow.Value = 4;

                // Connect the original shape (bottom) to the duplicated shape (top) using the connector.
                page.ConnectShapesViaConnector(
                    originalShape.ID,
                    ConnectionPointPlace.Bottom,
                    duplicateShapeId,
                    ConnectionPointPlace.Top,
                    connectorShapeId);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}