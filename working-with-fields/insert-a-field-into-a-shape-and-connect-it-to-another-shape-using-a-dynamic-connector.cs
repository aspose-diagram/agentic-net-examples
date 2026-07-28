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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add two rectangle shapes
            long rect1Id = page.AddShape(2.0, 2.0, "Rectangle");
            Shape rect1 = page.Shapes.GetShape(rect1Id);

            long rect2Id = page.AddShape(5.0, 5.0, "Rectangle");
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Insert a custom field into the first rectangle
            Field customField = new Field();
            // Set the field type (Undefined is a safe default)
            customField.Type.Value = TypeFieldValue.Undefined;
            // Set the field's displayed value
            customField.Value.Val = "CustomValue";
            // Add the field to the shape's Fields collection
            rect1.Fields.Add(customField);

            // Add a dynamic connector shape
            long connectorId = page.AddShape(3.5, 3.5, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the first rectangle to the second rectangle using the connector
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Bottom,
                rect2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the diagram to a VSDX file
            diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
