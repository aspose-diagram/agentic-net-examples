using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add the first rectangle shape using the built‑in master "Rectangle"
            long rect1Id = page.AddShape(
                pinX: 2.0,
                pinY: 5.0,
                width: 2.0,
                height: 1.0,
                masterName: "Rectangle",
                isCalculate: false);

            // Add the second rectangle shape
            long rect2Id = page.AddShape(
                pinX: 8.0,
                pinY: 5.0,
                width: 2.0,
                height: 1.0,
                masterName: "Rectangle",
                isCalculate: false);

            // Add a dynamic connector shape (initially zero size)
            long connectorId = page.AddShape(
                pinX: 0.0,
                pinY: 0.0,
                width: 0.0,
                height: 0.0,
                masterName: "Dynamic connector",
                isCalculate: false);

            // Retrieve the connector shape to configure its routing
            Shape connector = page.Shapes.GetShape(connectorId);
            // Set the connector to use a smooth spline (curved) line
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Connect the two rectangles via the connector
            page.ConnectShapesViaConnector(
                shapeFromId: rect1Id,
                placeFrom: ConnectionPointPlace.Right,
                shapeToId: rect2Id,
                placeTo: ConnectionPointPlace.Left,
                connectorId: connectorId);

            // Save the diagram as VSDX
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}