using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation; // for ConnectionPointPlace enum

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio diagram (replace with actual file path)
        string diagramPath = "input.vsdx";

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Collect the first three group shapes on the page
            List<Shape> groupShapes = new List<Shape>();
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group)
                {
                    groupShapes.Add(shape);
                    if (groupShapes.Count == 3) break;
                }
            }

            // Verify that we have three groups to work with
            if (groupShapes.Count < 3)
            {
                Console.Error.WriteLine("The diagram must contain at least three group shapes on the first page.");
                return;
            }

            // For simplicity, use the group shapes themselves as the sub‑shapes to connect.
            // In a real scenario you could retrieve a child shape via shape.Group[0] etc.

            // Store the IDs of the shapes we will connect
            long[] shapeIds = new long[3];
            for (int i = 0; i < 3; i++)
                shapeIds[i] = groupShapes[i].ID;

            // Create three separate dynamic connector shapes
            long[] connectorIds = new long[3];
            for (int i = 0; i < 3; i++)
            {
                // Add a dynamic connector at (0,0); the last argument isCalculate = false
                connectorIds[i] = page.AddShape(0, 0, "Dynamic connector", false);
            }

            // Connect shape0 -> shape1, shape1 -> shape2, shape2 -> shape0 using the three connectors
            page.ConnectShapesViaConnector(shapeIds[0], ConnectionPointPlace.Bottom,
                                          shapeIds[1], ConnectionPointPlace.Top,
                                          connectorIds[0]);

            page.ConnectShapesViaConnector(shapeIds[1], ConnectionPointPlace.Bottom,
                                          shapeIds[2], ConnectionPointPlace.Top,
                                          connectorIds[1]);

            page.ConnectShapesViaConnector(shapeIds[2], ConnectionPointPlace.Bottom,
                                          shapeIds[0], ConnectionPointPlace.Top,
                                          connectorIds[2]);

            // List the IDs of the created connector shapes
            Console.WriteLine("Connector shape IDs created:");
            for (int i = 0; i < connectorIds.Length; i++)
                Console.WriteLine($"Connector {i + 1}: {connectorIds[i]}");

            // Additionally, list all connection records (FromSheet -> ToSheet)
            Console.WriteLine("\nConnection records in the page:");
            foreach (Connect conn in page.Connects)
            {
                Console.WriteLine($"FromShape ID {conn.FromSheet} to ToShape ID {conn.ToSheet}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}