using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Find the first three group shapes on the page.
                List<Shape> groupShapes = new List<Shape>();
                for (int i = 0; i < page.Shapes.Count && groupShapes.Count < 3; i++)
                {
                    Shape shp = page.Shapes[i];
                    if (shp.Type == TypeValue.Group)
                    {
                        groupShapes.Add(shp);
                    }
                }

                if (groupShapes.Count < 3)
                {
                    Console.Error.WriteLine("The diagram does not contain at least three group shapes.");
                    return;
                }

                // Retrieve one sub‑shape from each group (the first child shape).
                List<long> subShapeIds = new List<long>();
                foreach (Shape group in groupShapes)
                {
                    if (group.Shapes.Count == 0)
                    {
                        Console.Error.WriteLine("A group shape has no child shapes.");
                        return;
                    }

                    // Get the first child shape of the group.
                    Shape child = group.Shapes[0];
                    subShapeIds.Add(child.ID);
                }

                // Create three separate dynamic connector shapes.
                List<long> connectorIds = new List<long>();
                for (int i = 0; i < 3; i++)
                {
                    // Add a dynamic connector at (0,0). The last parameter isCalculate must be false.
                    long connId = page.AddShape(0, 0, "Dynamic connector", false);
                    connectorIds.Add(connId);
                }

                // Connect sub‑shapes using the connectors to form a triangle.
                // Connection 1: Shape 0 -> Shape 1
                page.ConnectShapesViaConnector(
                    subShapeIds[0],
                    ConnectionPointPlace.Bottom,
                    subShapeIds[1],
                    ConnectionPointPlace.Top,
                    connectorIds[0]);

                // Connection 2: Shape 1 -> Shape 2
                page.ConnectShapesViaConnector(
                    subShapeIds[1],
                    ConnectionPointPlace.Bottom,
                    subShapeIds[2],
                    ConnectionPointPlace.Top,
                    connectorIds[1]);

                // Connection 3: Shape 2 -> Shape 0
                page.ConnectShapesViaConnector(
                    subShapeIds[2],
                    ConnectionPointPlace.Bottom,
                    subShapeIds[0],
                    ConnectionPointPlace.Top,
                    connectorIds[2]);

                // List resulting connection IDs.
                Console.WriteLine("Resulting connections:");
                for (int i = 0; i < page.Connects.Count; i++)
                {
                    Connect conn = page.Connects[i];
                    Console.WriteLine($"Connection {i + 1}: FromShapeId={conn.FromSheet}, ToShapeId={conn.ToSheet}");
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}