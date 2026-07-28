using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the shape to be removed (by its universal name)
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "TargetShape")
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Disable dynamic gluing for the target shape
            targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

            // Collect IDs of all connector shapes attached to the target shape
            List<long> connectorIds = new List<long>();
            foreach (Shape shape in page.Shapes)
            {
                // Connectors are 1‑D shapes
                if (shape.OneD)
                {
                    // Get IDs of shapes connected to this connector
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);
                    foreach (long id in connectedIds)
                    {
                        if (id == targetShape.ID)
                        {
                            connectorIds.Add(shape.ID);
                            break;
                        }
                    }
                }
            }

            // Mark each connector for deletion
            foreach (long connId in connectorIds)
            {
                Shape connector = page.Shapes.GetShape(connId);
                connector.Del = BOOL.True;
            }

            // Finally, mark the target shape for deletion
            targetShape.Del = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
