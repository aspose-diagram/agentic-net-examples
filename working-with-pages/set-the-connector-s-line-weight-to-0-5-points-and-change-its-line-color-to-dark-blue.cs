using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add two rectangle shapes using the correct DrawRectangle overload (x1, y1, x2, y2)
            long rect1Id = page.DrawRectangle(2.0, 2.0, 3.0, 3.0); // rectangle from (2,2) to (3,3)
            long rect2Id = page.DrawRectangle(5.0, 5.0, 6.0, 6.0); // rectangle from (5,5) to (6,6)

            // Add a dynamic connector shape; the last argument is the isCalculate flag (false)
            long connectorId = page.AddShape(3.5, 3.5, "Dynamic connector", false);

            // Retrieve the connector shape object for property modifications
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set line weight to 0.5 points (convert points to inches: 1 point = 1/72 inch)
            double points = 0.5;
            double inches = points / 72.0;
            connector.Line.LineWeight.Value = inches;

            // Set line color to dark blue using a HEX color code
            connector.Line.LineColor.Value = "#00008B";

            // Connect the two rectangles via the connector (optional)
            page.ConnectShapesViaConnector(
                rect1Id, ConnectionPointPlace.Bottom,
                rect2Id, ConnectionPointPlace.Top,
                connectorId);

            // Save the diagram to a VSDX file
            diagram.Save("ConnectorStyled.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}