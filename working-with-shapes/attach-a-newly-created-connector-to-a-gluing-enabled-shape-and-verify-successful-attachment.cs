using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Output file path for the generated diagram
        string outputPath = "GluedDiagram.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the active page where shapes will be added
            Page page = diagram.ActivePage;

            // -----------------------------------------------------------------
            // Add the first rectangle shape (gluing-enabled by default)
            // -----------------------------------------------------------------
            long rect1Id = page.AddShape(2.0, 2.0, "Rectangle", false);
            Shape rect1 = page.Shapes.GetShape(rect1Id);

            // -----------------------------------------------------------------
            // Add the second rectangle shape
            // -----------------------------------------------------------------
            long rect2Id = page.AddShape(5.0, 2.0, "Rectangle", false);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // -----------------------------------------------------------------
            // Add a dynamic connector shape
            // -----------------------------------------------------------------
            long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set connector routing style (straight line)
            connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);

            // -----------------------------------------------------------------
            // Attach (glue) the connector between the two rectangle shapes
            // -----------------------------------------------------------------
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Right,
                rect2Id,
                ConnectionPointPlace.Left,
                connectorId);

            // -----------------------------------------------------------------
            // Verify that the connector has been attached successfully
            // -----------------------------------------------------------------
            bool connectionFound = false;
            foreach (Connect conn in page.Connects)
            {
                // A successful connection will have FromSheet and ToSheet matching our shapes
                if (conn.FromSheet == rect1Id && conn.ToSheet == rect2Id)
                {
                    connectionFound = true;
                    break;
                }
            }

            if (connectionFound)
            {
                Console.WriteLine("Connector successfully attached between the two shapes.");
            }
            else
            {
                throw new Exception("Failed to attach connector to the shapes.");
            }

            // -----------------------------------------------------------------
            // Save the diagram to a VSDX file
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}