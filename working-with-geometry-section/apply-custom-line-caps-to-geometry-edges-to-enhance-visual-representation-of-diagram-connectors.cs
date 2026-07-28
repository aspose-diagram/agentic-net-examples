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

            // Add a blank page to the diagram
            diagram.Pages.Add(new Page());

            // Access the first (and only) page
            Page page = diagram.Pages[0];

            // Draw two rectangle shapes on the page (pinX, pinY, width, height in inches)
            long rect1Id = page.DrawRectangle(2.0, 5.0, 1.5, 1.0);
            long rect2Id = page.DrawRectangle(6.0, 5.0, 1.5, 1.0);

            // Retrieve the shape objects for optional further manipulation
            Shape rect1 = page.Shapes.GetShape(rect1Id);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Add a dynamic connector shape (1‑D connector)
            // The last argument 'isCalculate' must be a bool, not an int
            long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Apply a rounded line cap to the connector (BOOL.True = rounded, BOOL.False = square)
            connector.Line.LineCap.Value = BOOL.True;

            // Set additional line properties for better visibility
            connector.Line.LineColor.Value = "#FF0000"; // red line
            connector.Line.LineWeight.Value = 0.02;    // thickness in inches

            // Connect the two rectangles using the connector
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Bottom,
                rect2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the diagram to a VSDX file
            diagram.Save("ConnectorLineCap.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}