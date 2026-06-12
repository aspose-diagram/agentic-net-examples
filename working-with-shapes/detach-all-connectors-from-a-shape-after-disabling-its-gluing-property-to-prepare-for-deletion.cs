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

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the shape to be removed (replace "TargetShape" with the actual shape name)
            long targetShapeId = -1;
            foreach (Shape s in page.Shapes)
            {
                if (s.NameU == "TargetShape")
                {
                    targetShapeId = s.ID;
                    break;
                }
            }

            if (targetShapeId == -1)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Retrieve the shape instance
            Shape targetShape = page.Shapes.GetShape(targetShapeId);

            // Disable dynamic gluing for the shape
            targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

            // Get all 1‑D connector shapes glued to this shape
            long[] gluedConnectorIds = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

            if (gluedConnectorIds != null)
            {
                foreach (long connId in gluedConnectorIds)
                {
                    // Retrieve each connector shape
                    Shape connector = page.Shapes.GetShape(connId);
                    // Mark the connector as deleted
                    connector.Del = BOOL.True;
                }
            }

            // Optionally mark the target shape itself as deleted
            targetShape.Del = BOOL.True;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
