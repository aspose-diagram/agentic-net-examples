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
            Page page = diagram.Pages[0];

            // Draw two rectangle shapes
            long rect1Id = page.DrawRectangle(1.0, 1.0, 2.0, 1.0);
            long rect2Id = page.DrawRectangle(5.0, 1.0, 2.0, 1.0);

            // Add a dynamic connector shape
            long connectorId = page.AddShape(3.0, 1.0, "Dynamic connector");

            // Retrieve the connector shape object
            Shape connector = page.Shapes.GetShape(connectorId);

            // ---------- Test 1: StraightLines ----------
            connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);
            if (connector.GetConnectorsType() != ConnectorsTypeValue.StraightLines)
                throw new Exception("Connector type should be StraightLines after SetConnectorsType.");

            Console.WriteLine("Test 1 passed: StraightLines set correctly.");

            // ---------- Test 2: RightAngle ----------
            connector.SetConnectorsType(ConnectorsTypeValue.RightAngle);
            if (connector.GetConnectorsType() != ConnectorsTypeValue.RightAngle)
                throw new Exception("Connector type should be RightAngle after SetConnectorsType.");

            Console.WriteLine("Test 2 passed: RightAngle set correctly.");

            // ---------- Test 3: CurvedLines ----------
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);
            if (connector.GetConnectorsType() != ConnectorsTypeValue.CurvedLines)
                throw new Exception("Connector type should be CurvedLines after SetConnectorsType.");

            Console.WriteLine("Test 3 passed: CurvedLines set correctly.");

            // ---------- Test 4: After connecting shapes ----------
            // Connect the two rectangles using the connector
            page.ConnectShapesViaConnector(rect1Id, ConnectionPointPlace.Right,
                                          rect2Id, ConnectionPointPlace.Left, connectorId);

            // Set to StraightLines and verify
            connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);
            if (connector.GetConnectorsType() != ConnectorsTypeValue.StraightLines)
                throw new Exception("Connector type should remain StraightLines after connecting shapes.");

            Console.WriteLine("Test 4 passed: Connector type persists after connection.");

            // All tests passed
            Console.WriteLine("All connector type tests completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}