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

            // Get the first (and only) page
            Page page = diagram.Pages[0];

            // -------------------------------------------------
            // Add a pentagon using DrawPolyline (closed polygon)
            // -------------------------------------------------
            // Points: (2,2) -> (4,2) -> (5,4) -> (3,6) -> (1,4) -> back to (2,2)
            long pentagonId = page.DrawPolyline(new double[]
            {
                2, 2,   // Point 1
                4, 2,   // Point 2
                5, 4,   // Point 3
                3, 6,   // Point 4
                1, 4,   // Point 5
                2, 2    // Close polygon
            });

            // -------------------------------------------------
            // Add a square using DrawRectangle
            // -------------------------------------------------
            // Center at (8,4), width 3, height 3
            double squareCenterX = 8;
            double squareCenterY = 4;
            double squareSize = 3;
            long squareId = page.DrawRectangle(squareCenterX, squareCenterY, squareSize, squareSize);

            // -------------------------------------------------
            // Add a dynamic connector shape
            // -------------------------------------------------
            // Place the connector roughly between the two shapes
            double connectorPinX = (squareCenterX + 2) / 2; // approximate midpoint
            double connectorPinY = (squareCenterY + 4) / 2;
            long connectorId = page.AddShape(connectorPinX, connectorPinY, "Dynamic connector", false);

            // Retrieve the connector shape to set its appearance
            Shape connector = page.Shapes.GetShape(connectorId);
            // Set arrowheads at both ends
            connector.Line.BeginArrow.Value = 4; // Arrow style (integer value)
            connector.Line.EndArrow.Value = 4;
            // Optional: set line weight and color
            connector.Line.LineWeight.Value = 0.02; // inches
            connector.Line.LineColor.Value = "#000000"; // black

            // Set routing style (right‑angle)
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // -------------------------------------------------
            // Connect the pentagon and square with the connector
            // -------------------------------------------------
            // Use Bottom of pentagon and Top of square as connection points
            page.ConnectShapesViaConnector(
                pentagonId,
                ConnectionPointPlace.Bottom,
                squareId,
                ConnectionPointPlace.Top,
                connectorId);

            // -------------------------------------------------
            // Save the diagram to a VSDX file
            // -------------------------------------------------
            diagram.Save("PentagonSquareConnector.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
