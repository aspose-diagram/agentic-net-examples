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

            // ID of the connector shape to modify (replace with actual ID)
            long connectorId = 5;

            // Retrieve the connector shape from the first page
            Shape connector = diagram.Pages[0].Shapes.GetShape(connectorId);

            // Access the Layout property (demonstration purpose)
            Layout layout = connector.Layout;

            // Modify the connector's position.
            // Coordinates are stored in the XForm of the shape.
            connector.XForm.PinX.Value = 5.0; // New X coordinate
            connector.XForm.PinY.Value = 3.0; // New Y coordinate

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
