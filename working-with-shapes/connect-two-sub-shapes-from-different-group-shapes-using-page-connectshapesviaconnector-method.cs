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
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the first group shape by its universal name
            Shape group1 = null;
            Shape group2 = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "Group1")
                    group1 = shape;
                else if (shape.NameU == "Group2")
                    group2 = shape;
            }

            if (group1 == null || group2 == null)
            {
                throw new Exception("Required group shapes not found.");
            }

            // Find sub‑shapes inside each group by their universal names
            Shape subShape1 = null;
            Shape subShape2 = null;

            foreach (Shape sub in group1.Shapes)
            {
                if (sub.NameU == "Rect1")
                {
                    subShape1 = sub;
                    break;
                }
            }

            foreach (Shape sub in group2.Shapes)
            {
                if (sub.NameU == "Rect2")
                {
                    subShape2 = sub;
                    break;
                }
            }

            if (subShape1 == null || subShape2 == null)
            {
                throw new Exception("Required sub‑shapes not found inside the groups.");
            }

            // Add a dynamic connector shape to the page
            long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set connector routing style (optional)
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Connect the two sub‑shapes via the connector
            page.ConnectShapesViaConnector(
                subShape1.ID,
                ConnectionPointPlace.Bottom,
                subShape2.ID,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
