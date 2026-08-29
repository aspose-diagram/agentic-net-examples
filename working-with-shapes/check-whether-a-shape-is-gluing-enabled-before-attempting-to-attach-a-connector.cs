using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Use the first page in the diagram
            Page page = diagram.Pages[0];

            // Find two shapes to connect (example: first two non‑deleted shapes)
            Shape shape1 = null;
            Shape shape2 = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.True) continue; // skip deleted shapes

                if (shape1 == null)
                    shape1 = shp;
                else if (shape2 == null)
                {
                    shape2 = shp;
                    break;
                }
            }

            if (shape1 == null || shape2 == null)
            {
                Console.WriteLine("Not enough shapes to connect.");
                return;
            }

            // Check if both shapes have gluing enabled (AllowDynamicGlue)
            bool shape1Gluable = shape1.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;
            bool shape2Gluable = shape2.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;

            if (!shape1Gluable || !shape2Gluable)
            {
                Console.WriteLine("One or both shapes are not gluing‑enabled. Connector will not be attached.");
                return;
            }

            // Add a dynamic connector shape to the page
            long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Optionally set connector routing style (right‑angle)
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Connect shape1 to shape2 using the connector
            page.ConnectShapesViaConnector(
                shape1.ID,
                ConnectionPointPlace.Bottom,
                shape2.ID,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Connector attached and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
