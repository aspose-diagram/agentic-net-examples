using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Wrap all Aspose operations in a try/catch to handle potential errors.
        try
        {
            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Get the first page (creates one by default).
            Page page = diagram.Pages[0];

            // ----- Create a pentagon using DrawPolyline -----
            // Define pentagon vertices (closed shape, repeat first point at end).
            double[] pentagonPoints = new double[]
            {
                2.0, 5.0,   // Point 1
                4.0, 7.0,   // Point 2
                6.0, 5.0,   // Point 3
                5.0, 3.0,   // Point 4
                3.0, 3.0,   // Point 5
                2.0, 5.0    // Close back to Point 1
            };
            long pentagonId = page.DrawPolyline(pentagonPoints);
            Shape pentagon = page.Shapes.GetShape(pentagonId);

            // ----- Create a square using DrawRectangle -----
            // Center at (8,5) with width and height of 2 inches.
            double squarePinX = 8.0;
            double squarePinY = 5.0;
            double squareSize = 2.0;
            long squareId = page.DrawRectangle(squarePinX, squarePinY, squareSize, squareSize);
            Shape square = page.Shapes.GetShape(squareId);

            // ----- Add a dynamic connector -----
            // Place the connector roughly between the two shapes.
            double connectorPinX = 5.0;
            double connectorPinY = 5.0;
            // The fourth argument is a bool indicating whether to calculate geometry; use false.
            long connectorId = page.AddShape(connectorPinX, connectorPinY, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set arrowheads on both ends (value 4 corresponds to a standard arrow).
            connector.Line.BeginArrow.Value = 4;
            connector.Line.EndArrow.Value = 4;

            // Optionally set line weight for better visibility (in inches).
            connector.Line.LineWeight.Value = 0.02;

            // ----- Connect pentagon to square using the connector -----
            // Use Bottom of pentagon and Top of square as connection points.
            page.ConnectShapesViaConnector(
                pentagonId,
                ConnectionPointPlace.Bottom,
                squareId,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the diagram to a VSDX file.
            string outputPath = "PentagonSquareConnector.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream and exit.
            Console.Error.WriteLine($"Error: {ex.Message}");
            return;
        }
    }
}