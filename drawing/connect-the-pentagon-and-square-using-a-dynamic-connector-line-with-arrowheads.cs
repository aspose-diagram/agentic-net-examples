using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the directory for the output exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block
            using (Diagram diagram = new Diagram())
            {
                // Get the first page (a new diagram always contains at least one page)
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // Draw a pentagon using DrawPolyline (closed shape)
                // -------------------------------------------------
                double[] pentagonPoints = new double[]
                {
                    2.0, 2.0,   // Point 1
                    4.0, 2.0,   // Point 2
                    5.0, 4.0,   // Point 3
                    3.0, 6.0,   // Point 4
                    1.0, 4.0,   // Point 5
                    2.0, 2.0    // Close back to Point 1
                };
                long pentagonId = page.DrawPolyline(pentagonPoints);
                Shape pentagon = page.Shapes.GetShape(pentagonId);

                // ---------------------------------
                // Draw a square using DrawRectangle
                // ---------------------------------
                double squareCenterX = 8.0;
                double squareCenterY = 4.0;
                double squareSize = 3.0;
                // DrawRectangle expects (pinX, pinY, width, height)
                long squareId = page.DrawRectangle(squareCenterX, squareCenterY, squareSize, squareSize);
                Shape square = page.Shapes.GetShape(squareId);

                // ---------------------------------
                // Add a dynamic connector shape
                // ---------------------------------
                // AddShape overload: (pinX, pinY, masterName, isCalculate)
                long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Set connector appearance: straight line with arrowheads at both ends
                connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);
                connector.Line.BeginArrow.Value = 4; // Arrow style code
                connector.Line.EndArrow.Value = 4;   // Arrow style code

                // -------------------------------------------------
                // Connect the pentagon and square via the connector
                // -------------------------------------------------
                page.ConnectShapesViaConnector(
                    pentagonId,
                    ConnectionPointPlace.Bottom,
                    squareId,
                    ConnectionPointPlace.Top,
                    connectorId);

                // -----------------
                // Save the diagram
                // -----------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}