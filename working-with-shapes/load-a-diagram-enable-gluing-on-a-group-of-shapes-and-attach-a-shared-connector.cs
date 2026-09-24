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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Create two sample shapes (rectangles) that will be grouped
            // -----------------------------------------------------------------
            long rect1Id = page.AddShape(2.0, 2.0, "Rectangle", false);
            long rect2Id = page.AddShape(4.0, 2.0, "Rectangle", false);

            Shape rect1 = page.Shapes.GetShape(rect1Id);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Group the two rectangles into a single group shape
            Shape group = page.Shapes.Group(new Shape[] { rect1, rect2 });

            // -----------------------------------------------------------------
            // Enable gluing on the group shape
            // -----------------------------------------------------------------
            // Allow dynamic glue so other shapes/connectors can be glued to this group
            group.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

            // -----------------------------------------------------------------
            // Create a shared connector (dynamic connector) and attach it
            // -----------------------------------------------------------------
            long connectorId = page.AddShape(0, 0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set the connector routing style (optional)
            connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);

            // Connect the group shape to the second rectangle using the same connector
            // Glue the start of the connector to the bottom of the group
            // Glue the end of the connector to the top of the second rectangle
            page.ConnectShapesViaConnector(
                group.ID,
                ConnectionPointPlace.Bottom,
                rect2.ID,
                ConnectionPointPlace.Top,
                connector.ID);

            // -----------------------------------------------------------------
            // Save the modified diagram
            // -----------------------------------------------------------------
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
