using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Output file path
        string outputPath = "ConnectedDiagram.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (created by default)
            Page page = diagram.Pages[0];

            // ---------- Create a pentagon shape ----------
            // Define pentagon vertices (closed polygon)
            double[] pentagonPoints = new double[]
            {
                2, 0,   // Point 1
                4, 0,   // Point 2
                5, 2,   // Point 3
                3, 4,   // Point 4
                1, 2,   // Point 5
                2, 0    // Close back to Point 1
            };
            // Add pentagon to the page and obtain its shape ID
            long pentagonId = page.DrawPolyline(pentagonPoints);

            // ---------- Create a square shape ----------
            double[] squarePoints = new double[]
            {
                8, 0,   // Point 1
                12, 0,  // Point 2
                12, 4,  // Point 3
                8, 4,   // Point 4
                8, 0    // Close back to Point 1
            };
            long squareId = page.DrawPolyline(squarePoints);

            // ---------- Add a dynamic connector ----------
            // Add the connector shape using the built‑in master name "Dynamic connector"
            long connectorId = page.AddShape(0, 0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set arrowheads at both ends (value 4 corresponds to a standard arrow)
            connector.Line.BeginArrow.Value = 4;
            connector.Line.EndArrow.Value = 4;

            // Set connector routing type to straight lines
            connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);

            // ---------- Connect pentagon to square ----------
            // Use connection points: bottom of pentagon to top of square
            page.ConnectShapesViaConnector(
                pentagonId,
                ConnectionPointPlace.Bottom,
                squareId,
                ConnectionPointPlace.Top,
                connectorId);

            // ---------- Save the diagram ----------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}