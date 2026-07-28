using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page
                Page page = diagram.Pages[0];

                // Add two shapes (Rectangle and Ellipse) and a dynamic connector
                long rectId = page.AddShape(2.0, 2.0, "Rectangle");
                long ellipseId = page.AddShape(5.0, 2.0, "Ellipse");
                long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector");

                // Retrieve the shape objects
                Shape rectShape = page.Shapes.GetShape(rectId);
                Shape ellipseShape = page.Shapes.GetShape(ellipseId);
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Connect the rectangle to the ellipse using the connector
                page.ConnectShapesViaConnector(
                    rectId,
                    ConnectionPointPlace.Bottom,
                    ellipseId,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Dictionary to map shape IDs to their custom connection point collections
                Dictionary<long, List<Connection>> shapeConnectionMap = new Dictionary<long, List<Connection>>();

                // Helper method to capture connections for a given shape
                void CaptureConnections(Shape shape)
                {
                    // Ensure the shape has a Connections collection
                    if (shape.Connections != null)
                    {
                        List<Connection> connections = new List<Connection>();
                        foreach (Connection conn in shape.Connections)
                        {
                            connections.Add(conn);
                        }
                        shapeConnectionMap[shape.ID] = connections;
                    }
                    else
                    {
                        // Store an empty list if no custom connections exist
                        shapeConnectionMap[shape.ID] = new List<Connection>();
                    }
                }

                // Capture connections for each shape we added
                CaptureConnections(rectShape);
                CaptureConnections(ellipseShape);
                CaptureConnections(connectorShape);

                // Example batch update: shift all custom connection points 0.5 inches right
                foreach (KeyValuePair<long, List<Connection>> entry in shapeConnectionMap)
                {
                    foreach (Connection conn in entry.Value)
                    {
                        // X and Y are stored as formulas; adjust using simple offset
                        // Note: This example assumes the formulas are simple numeric values.
                        if (double.TryParse(conn.X.Ufe.F, out double xVal) &&
                            double.TryParse(conn.Y.Ufe.F, out double yVal))
                        {
                            conn.X.Ufe.F = (xVal + 0.5).ToString();
                            conn.Y.Ufe.F = yVal.ToString(); // Y unchanged
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }