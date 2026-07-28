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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Ensure there are at least two shapes to connect
            if (page.Shapes.Count < 2)
            {
                Console.WriteLine("The page does not contain enough shapes to create a connector.");
                return;
            }

            // Retrieve the first two shapes on the page
            Shape sourceShape = page.Shapes[0];
            Shape targetShape = page.Shapes[1];

            // Check whether each shape allows dynamic glue (gluing-enabled)
            bool sourceGlueEnabled = sourceShape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;
            bool targetGlueEnabled = targetShape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;

            if (!sourceGlueEnabled || !targetGlueEnabled)
            {
                Console.WriteLine("One or both shapes are not gluing-enabled. Connector will not be attached.");
                return;
            }

            // Add a dynamic connector shape (position will be adjusted after connecting)
            long connectorId = page.AddShape(0, 0, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Optional: set a routing style for the connector
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Connect the source shape's bottom to the target shape's top using the connector
            page.ConnectShapesViaConnector(
                sourceShape.ID,
                ConnectionPointPlace.Bottom,
                targetShape.ID,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Connector attached and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
