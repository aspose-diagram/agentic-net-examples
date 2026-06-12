using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate a group shape on the page
            Shape groupShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group)
                {
                    groupShape = shape;
                    break;
                }
            }

            if (groupShape == null)
            {
                Console.WriteLine("No group shape found on the page.");
                return;
            }

            // Enable dynamic gluing for the group shape
            groupShape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

            // Add a shared connector (Dynamic connector) near the group shape
            double pinX = groupShape.XForm.PinX.Value;
            double pinY = groupShape.XForm.PinY.Value;
            long connectorId = page.AddShape(pinX, pinY, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Optional: set connector routing style
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Glue the connector to the center of the group shape
            page.GlueShapes(groupShape.ID, ConnectionPointPlace.Center, connectorId);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
