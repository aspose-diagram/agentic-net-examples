using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            RunConnectorTypeTest("StraightLines", ConnectorsTypeValue.StraightLines);
            RunConnectorTypeTest("RightAngle", ConnectorsTypeValue.RightAngle);
            RunConnectorTypeTest("CurvedLines", ConnectorsTypeValue.CurvedLines);
            Console.WriteLine("All connector type tests passed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Test execution failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a diagram, adds a rectangle and a dynamic connector,
    /// sets the connector type, and verifies that the routing style matches the expected value.
    /// </summary>
    /// <param name="testName">Name of the test scenario.</param>
    /// <param name="connectorType">Connector type to set.</param>
    private static void RunConnectorTypeTest(string testName, ConnectorsTypeValue connectorType)
    {
        Console.WriteLine($"Running test: {testName}");

        // Use a using block for Diagram as required.
        using (Diagram diagram = new Diagram())
        {
            try
            {
                // Ensure there is at least one page.
                Page page = diagram.Pages[0];

                // Add a rectangle shape using the built‑in master name "Rectangle".
                long rectId = page.AddShape(1.0, 1.0, 2.0, 1.0, "Rectangle", false);
                Shape rectangle = page.Shapes.GetShape(rectId);

                // Add a dynamic connector shape.
                long connId = page.AddShape(2.0, 2.0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connId);

                // Set the desired connector type.
                connector.SetConnectorsType(connectorType);

                // Determine the expected routing style based on the connector type.
                ShapeRouteStyleValue expectedStyle = GetExpectedRouteStyle(connectorType);

                // Verify that the connector's routing style matches the expected style.
                // CurvedLines does not have a distinct ShapeRouteStyleValue, so skip verification for that case.
                if (connectorType != ConnectorsTypeValue.CurvedLines)
                {
                    if (connector.Layout.ShapeRouteStyle.Value != expectedStyle)
                    {
                        throw new Exception($"Connector type verification failed. Expected routing style {expectedStyle}, but got {connector.Layout.ShapeRouteStyle.Value}.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Wrap any Aspose operation errors with test context.
                throw new Exception($"Error in test '{testName}': {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Maps a ConnectorsTypeValue to the corresponding ShapeRouteStyleValue.
    /// </summary>
    private static ShapeRouteStyleValue GetExpectedRouteStyle(ConnectorsTypeValue connectorType)
    {
        return connectorType switch
        {
            ConnectorsTypeValue.StraightLines => ShapeRouteStyleValue.Straight,
            ConnectorsTypeValue.RightAngle => ShapeRouteStyleValue.RightAngle,
            // CurvedLines does not have a dedicated ShapeRouteStyleValue; default to Straight for verification purposes.
            ConnectorsTypeValue.CurvedLines => ShapeRouteStyleValue.Straight,
            _ => throw new ArgumentOutOfRangeException(nameof(connectorType), $"Unsupported connector type: {connectorType}")
        };
    }
}
