using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        // Run all tests
        TestSetConnectorStraight();
        TestSetConnectorRightAngle();
        TestSetConnectorCurved();

        Console.WriteLine("All tests passed.");
    }

    static void TestSetConnectorStraight()
    {
        // Create a new diagram with a single page
        Diagram diagram = new Diagram();
        Page page = diagram.Pages[0];

        // Create two simple rectangle shapes using DrawRectangle
        long rect1Id = page.DrawRectangle(1, 1, 2, 2);
        long rect2Id = page.DrawRectangle(4, 1, 2, 2);

        // Create a line shape (1‑D) that will act as a connector
        long lineId = page.DrawLine(2, 2, 5, 2);
        Shape connector = page.Shapes.GetShape(lineId);

        // Ensure the shape is a 1‑D connector
        if (!connector.OneD)
            throw new Exception("Connector shape is not 1‑D.");

        // Set connector type to StraightLines
        connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);

        // Verify the type was set correctly
        ConnectorsTypeValue actual = connector.GetConnectorsType();
        if (actual != ConnectorsTypeValue.StraightLines)
            throw new Exception($"Expected StraightLines, got {actual}.");

        Console.WriteLine("TestSetConnectorStraight passed.");
    }

    static void TestSetConnectorRightAngle()
    {
        Diagram diagram = new Diagram();
        Page page = diagram.Pages[0];

        long rect1Id = page.DrawRectangle(1, 1, 2, 2);
        long rect2Id = page.DrawRectangle(4, 1, 2, 2);
        long lineId = page.DrawLine(2, 2, 5, 2);
        Shape connector = page.Shapes.GetShape(lineId);

        if (!connector.OneD)
            throw new Exception("Connector shape is not 1‑D.");

        // Set connector type to RightAngle
        connector.SetConnectorsType(ConnectorsTypeValue.RightAngle);

        // Verify
        ConnectorsTypeValue actual = connector.GetConnectorsType();
        if (actual != ConnectorsTypeValue.RightAngle)
            throw new Exception($"Expected RightAngle, got {actual}.");

        Console.WriteLine("TestSetConnectorRightAngle passed.");
    }

    static void TestSetConnectorCurved()
    {
        Diagram diagram = new Diagram();
        Page page = diagram.Pages[0];

        long rect1Id = page.DrawRectangle(1, 1, 2, 2);
        long rect2Id = page.DrawRectangle(4, 1, 2, 2);
        long lineId = page.DrawLine(2, 2, 5, 2);
        Shape connector = page.Shapes.GetShape(lineId);

        if (!connector.OneD)
            throw new Exception("Connector shape is not 1‑D.");

        // Set connector type to CurvedLines
        connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

        // Verify
        ConnectorsTypeValue actual = connector.GetConnectorsType();
        if (actual != ConnectorsTypeValue.CurvedLines)
            throw new Exception($"Expected CurvedLines, got {actual}.");

        Console.WriteLine("TestSetConnectorCurved passed.");
    }
}
