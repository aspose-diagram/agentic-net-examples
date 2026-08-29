using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the connector shape (replace with the actual shape ID)
            int connectorShapeId = 2; // example ID
            Shape connector = diagram.Pages[0].Shapes.GetShape(connectorShapeId);

            // Set the connector's line jump style to "none" by using the page default style
            connector.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
