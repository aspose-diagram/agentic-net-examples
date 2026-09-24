using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Identify the shape that will be deleted (by its universal name)
            string targetNameU = "TargetShape";
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.NameU == targetNameU)
                {
                    targetShape = s;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Shape with NameU '{targetNameU}' not found.");
                return;
            }

            // Disable gluing on the target shape
            targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

            // Detach (delete) all connector shapes attached to the target shape
            foreach (Shape shape in page.Shapes)
            {
                // Connectors are 1‑D shapes
                if (shape.OneD)
                {
                    // Retrieve IDs of shapes connected to this connector
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);
                    if (connectedIds != null)
                    {
                        foreach (long id in connectedIds)
                        {
                            if (id == targetShape.ID)
                            {
                                // Mark the connector for deletion
                                shape.Del = BOOL.True;
                                break;
                            }
                        }
                    }
                }
            }

            // Optionally mark the target shape itself for deletion
            targetShape.Del = BOOL.True;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Connectors detached and shape prepared for deletion.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
