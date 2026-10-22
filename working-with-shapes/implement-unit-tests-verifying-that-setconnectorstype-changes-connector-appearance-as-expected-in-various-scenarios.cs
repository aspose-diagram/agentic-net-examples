using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // No external input files are required for this test.
        try
        {
            // Create a new empty diagram inside a using block as required.
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram does not contain any pages.");
                    return;
                }

                // Use the first page.
                Page page = diagram.Pages[0];

                // Add two rectangle shapes to act as source and target.
                long rect1Id = page.AddShape(1.0, 1.0, "Rectangle", false);
                long rect2Id = page.AddShape(5.0, 5.0, "Rectangle", false);

                // Retrieve the rectangle shapes (not used further but ensures they exist).
                Shape rect1 = page.Shapes.GetShape(rect1Id);
                Shape rect2 = page.Shapes.GetShape(rect2Id);

                // Add a dynamic connector shape.
                long connectorId = page.AddShape(2.0, 2.0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Verify that the added shape is indeed a dynamic connector.
                if (connector.Master == null || connector.Master.Name != "Dynamic connector")
                {
                    Console.Error.WriteLine("Failed to create a dynamic connector.");
                    return;
                }

                // Connect the two rectangles using the connector.
                page.ConnectShapesViaConnector(rect1Id, ConnectionPointPlace.Bottom, rect2Id, ConnectionPointPlace.Top, connectorId);

                // Define test scenarios for each ConnectorsTypeValue.
                ConnectorsTypeValue[] testValues = new ConnectorsTypeValue[]
                {
                    ConnectorsTypeValue.StraightLines,
                    ConnectorsTypeValue.RightAngle,
                    ConnectorsTypeValue.CurvedLines
                };

                // Iterate over each scenario, apply the type, and verify the result.
                foreach (ConnectorsTypeValue typeValue in testValues)
                {
                    // Set the connector appearance.
                    connector.SetConnectorsType(typeValue);

                    // After setting, the ShapeRouteStyle should reflect the same value.
                    ShapeRouteStyleValue actualRouteStyle = connector.Layout.ShapeRouteStyle.Value;

                    // Compare expected and actual values.
                    if (actualRouteStyle != (ShapeRouteStyleValue)typeValue)
                    {
                        throw new Exception($"Connector type mismatch. Expected: {typeValue}, Actual: {actualRouteStyle}");
                    }
                    else
                    {
                        Console.WriteLine($"Connector type set and verified successfully: {typeValue}");
                    }
                }

                // All tests passed.
                Console.WriteLine("All SetConnectorsType tests passed.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}