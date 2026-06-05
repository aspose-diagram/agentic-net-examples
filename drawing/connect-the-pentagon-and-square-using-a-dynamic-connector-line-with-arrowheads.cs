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

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // ----- Add a pentagon (using DrawPolyline) -----
            // Coordinates: (3,6) -> (2,5.5) -> (2,4.5) -> (3,4) -> (4,4.5) -> (4,5.5) -> back to (3,6)
            long pentagonId = page.DrawPolyline(new double[]
            {
                3, 6,
                2, 5.5,
                2, 4.5,
                3, 4,
                4, 4.5,
                4, 5.5,
                3, 6   // close the shape
            });

            // ----- Add a square (using DrawRectangle) -----
            // Position (7,5) with width and height of 2 units
            long squareId = page.DrawRectangle(7, 5, 2, 2);

            // ----- Add a dynamic connector -----
            // The connector can be placed anywhere; its position will be adjusted by the glue operation
            long connectorId = page.AddShape(0, 0, "Dynamic connector");

            // Retrieve the connector shape for configuration
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set arrowheads on both ends of the connector
            connector.Line.BeginArrow.Value = 4;               // Arrow style (example value)
            connector.Line.EndArrow.Value = 4;
            connector.Line.BeginArrowSize.Value = ArrowSizeValue.Large;
            connector.Line.EndArrowSize.Value = ArrowSizeValue.Large;

            // Set connector routing style (right‑angle routing)
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Connect the pentagon to the square using the dynamic connector
            // Use Bottom of the pentagon and Top of the square as connection points
            page.ConnectShapesViaConnector(
                pentagonId,
                ConnectionPointPlace.Bottom,
                squareId,
                ConnectionPointPlace.Top,
                connectorId);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}