using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape ID to clone, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <shapeId> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long originalShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (modify if a different page is required)
                Page page = diagram.Pages[0];

                // Retrieve the original shape by its ID
                Shape originalShape = page.Shapes.GetShape(originalShapeId);
                if (originalShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {originalShapeId} not found on the first page.");
                    return;
                }

                // Ensure the shape has an associated master (required for cloning)
                if (originalShape.Master == null)
                {
                    Console.Error.WriteLine("The original shape does not have a master; cannot clone.");
                    return;
                }

                // Store master name for reuse
                string masterName = originalShape.Master.Name;

                // Add a new shape on the same page using the same master and position
                long clonedShapeId = page.AddShape(
                    originalShape.XForm.PinX.Value,
                    originalShape.XForm.PinY.Value,
                    masterName,
                    false); // isCalculate = false

                // Retrieve the newly added shape instance
                Shape clonedShape = page.Shapes.GetShape(clonedShapeId);
                if (clonedShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the cloned shape.");
                    return;
                }

                // Copy geometry (size) from the original shape
                clonedShape.XForm.Width.Value = originalShape.XForm.Width.Value;
                clonedShape.XForm.Height.Value = originalShape.XForm.Height.Value;

                // Copy text content
                clonedShape.Text.Value.Clear();
                foreach (var item in originalShape.Text.Value)
                {
                    // Preserve only text runs (Txt); ignore formatting runs (Cp)
                    if (item is Txt txt)
                    {
                        clonedShape.Text.Value.Add(new Txt(txt.Text));
                    }
                }

                // Copy fill properties (foreground color and transparency)
                clonedShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
                clonedShape.Fill.FillForegndTrans.Value = originalShape.Fill.FillForegndTrans.Value;

                // Preserve gluing: rewire any connectors that were glued to the original shape
                // Retrieve IDs of connectors glued to the original shape
                long[] gluedConnectorIds = originalShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);
                if (gluedConnectorIds != null)
                {
                    foreach (long connectorId in gluedConnectorIds)
                    {
                        // Update the connection definitions (Connect objects) that reference the original shape
                        foreach (Connect conn in page.Connects)
                        {
                            if (conn.FromSheet == originalShapeId)
                            {
                                conn.FromSheet = clonedShapeId;
                            }
                            if (conn.ToSheet == originalShapeId)
                            {
                                conn.ToSheet = clonedShapeId;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
