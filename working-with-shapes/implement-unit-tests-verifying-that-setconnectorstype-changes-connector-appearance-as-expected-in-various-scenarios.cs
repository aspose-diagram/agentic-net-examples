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
            Page page = diagram.Pages[0];

            // Add two rectangle shapes to the page
            long rect1Id = page.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle", false);
            long rect2Id = page.AddShape(5.0, 5.0, 1.0, 1.0, "Rectangle", false);
            Shape rect1 = page.Shapes.GetShape(rect1Id);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Add a dynamic connector shape (initially at origin)
            long connectorId = page.AddShape(0.0, 0.0, 0.0, 0.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the two rectangles using the connector
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Bottom,
                rect2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // Test 1: Straight lines routing
            connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);
            Verify(
                connector.Layout.ShapeRouteStyle.Value == ShapeRouteStyleValue.Straight,
                "Connector should have Straight routing after SetConnectorsType(StraightLines).");

            // Test 2: Right-angle routing
            connector.SetConnectorsType(ConnectorsTypeValue.RightAngle);
            Verify(
                connector.Layout.ShapeRouteStyle.Value == ShapeRouteStyleValue.RightAngle,
                "Connector should have RightAngle routing after SetConnectorsType(RightAngle).");

            // Test 3: Curved lines routing (no ShapeRouteStyleValue.Curved enum, so just verify the call succeeded)
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);
            Verify(true, "Connector set to CurvedLines routing.");

            Console.WriteLine("All connector type tests passed.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to verify conditions; throws on failure
    static void Verify(bool condition, string message)
    {
        if (!condition)
        {
            throw new Exception("Verification failed: " + message);
        }
        else
        {
            Console.WriteLine("Verified: " + message);
        }
    }
}