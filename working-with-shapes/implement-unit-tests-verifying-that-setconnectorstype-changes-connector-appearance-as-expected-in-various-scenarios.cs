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

            // Test default connector type (should be StraightLines)
            TestConnectorType(ConnectorsTypeValue.StraightLines, "Default StraightLines");

            // Test setting RightAngle connector type
            TestConnectorType(ConnectorsTypeValue.RightAngle, "RightAngle");

            // Test setting CurvedLines connector type
            TestConnectorType(ConnectorsTypeValue.CurvedLines, "CurvedLines");

            Console.WriteLine("All connector type tests passed.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }

    static void TestConnectorType(ConnectorsTypeValue targetType, string testName)
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one page
        Page page = diagram.Pages[0];

        // Add two rectangle shapes
        long rect1Id = diagram.AddShape(1.0, 5.0, "Rectangle", 0);
        long rect2Id = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

        // Add a dynamic connector shape
        long connectorId = diagram.AddShape(3.0, 5.0, "Dynamic connector", 0);

        // Retrieve shape objects
        Shape rect1 = page.Shapes.GetShape(rect1Id);
        Shape rect2 = page.Shapes.GetShape(rect2Id);
        Shape connector = page.Shapes.GetShape(connectorId);

        // Connect the rectangles via the connector
        page.ConnectShapesViaConnector(rect1Id, ConnectionPointPlace.Bottom,
                                      rect2Id, ConnectionPointPlace.Top,
                                      connectorId);

        // Set the desired connector type
        connector.SetConnectorsType(targetType);

        // Verify the connector type
        ConnectorsTypeValue actualType = connector.GetConnectorsType();

        if (actualType != targetType)
        {
            throw new Exception($"{testName} test failed: expected {targetType}, got {actualType}.");
        }
        else
        {
            Console.WriteLine($"{testName} test passed.");
        }

        // Clean up
        diagram.Dispose();
    }
}
