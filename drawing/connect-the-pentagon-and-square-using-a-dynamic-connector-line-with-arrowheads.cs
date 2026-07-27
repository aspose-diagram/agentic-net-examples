using System;
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

                // Use the first (default) page
                Page page = diagram.Pages[0];

                // Draw a pentagon using a polyline (closed by repeating the first point)
                // Points: (2,2) -> (3,4) -> (5,4) -> (6,2) -> (4,1) -> (2,2)
                long pentagonId = page.DrawPolyline(new double[]
                {
                    2, 2,
                    3, 4,
                    5, 4,
                    6, 2,
                    4, 1,
                    2, 2
                });

                // Draw a square (center at 8,3 with width and height of 2)
                long squareId = page.DrawRectangle(8, 3, 2, 2);

                // Add a dynamic connector shape (initial position is arbitrary)
                long connectorId = page.AddShape(5, 3, "Dynamic connector", false);

                // Retrieve the connector shape to set its arrowheads
                Shape connector = page.Shapes.GetShape(connectorId);
                connector.Line.BeginArrow.Value = 4; // Arrow style (integer value)
                connector.Line.EndArrow.Value = 4;   // Arrow style (integer value)

                // Connect the pentagon to the square using the dynamic connector
                page.ConnectShapesViaConnector(
                    pentagonId,
                    ConnectionPointPlace.Bottom,
                    squareId,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the diagram to a VSDX file
                diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }