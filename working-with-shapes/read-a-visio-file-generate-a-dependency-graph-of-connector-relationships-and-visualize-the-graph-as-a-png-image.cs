using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputPngPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "dependency_graph.png";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (sourceDiagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The source diagram contains no pages.");
                    return;
                }

                Page sourcePage = sourceDiagram.Pages[0];

                // Collect IDs of non‑connector shapes (these will become graph nodes)
                HashSet<long> nodeIds = new HashSet<long>();
                foreach (Shape shape in sourcePage.Shapes)
                {
                    // 1‑D shapes are connectors; skip them
                    if (!shape.OneD)
                    {
                        nodeIds.Add(shape.ID);
                    }
                }

                // Build edge list from the Connects collection (source → target)
                List<(long fromId, long toId)> edges = new List<(long, long)>();
                foreach (Connect connect in sourcePage.Connects)
                {
                    edges.Add((connect.FromSheet, connect.ToSheet));
                }

                // Create a new diagram that will hold the dependency graph
                using (Diagram graphDiagram = new Diagram())
                {
                    // The diagram is created with a default page; retrieve it
                    if (graphDiagram.Pages.Count == 0)
                    {
                        Console.Error.WriteLine("Failed to create a page in the graph diagram.");
                        return;
                    }

                    Page graphPage = graphDiagram.Pages[0];

                    // Map original shape IDs to newly created node shape IDs
                    Dictionary<long, long> idMap = new Dictionary<long, long>();

                    // Simple grid layout parameters (in inches)
                    const double startX = 1.0;
                    const double startY = 1.0;
                    const double stepX = 2.5;
                    const double stepY = 1.5;
                    const int columns = 5;

                    int index = 0;
                    foreach (long originalId in nodeIds)
                    {
                        // Calculate grid position for the node
                        int col = index % columns;
                        int row = index / columns;
                        double pinX = startX + col * stepX;
                        double pinY = startY + row * stepY;

                        // Add a rectangle shape representing the node
                        long newShapeId = graphPage.AddShape(pinX, pinY, "Rectangle", false); // BOOL.False replaced with false
                        idMap[originalId] = newShapeId;

                        // Retrieve the shape object to set its text
                        Shape nodeShape = graphPage.Shapes.GetShape(newShapeId);
                        if (nodeShape != null && nodeShape.Text != null)
                        {
                            // Clear any existing text and add the original shape ID as label
                            nodeShape.Text.Value.Clear();
                            nodeShape.Text.Value.Add(new Txt(originalId.ToString()));
                        }

                        index++;
                    }

                    // Add connectors for each edge
                    foreach (var edge in edges)
                    {
                        // Both ends must correspond to node shapes; skip otherwise
                        if (!idMap.TryGetValue(edge.fromId, out long fromShapeId) ||
                            !idMap.TryGetValue(edge.toId, out long toShapeId))
                        {
                            continue;
                        }

                        // Add a dynamic connector shape (position is irrelevant; it will be glued)
                        long connectorId = graphPage.AddShape(0, 0, "Dynamic connector", false); // BOOL.False replaced with false

                        // Connect the two node shapes via the connector
                        graphPage.ConnectShapesViaConnector(
                            fromShapeId,
                            ConnectionPointPlace.Right,
                            toShapeId,
                            ConnectionPointPlace.Left,
                            connectorId);
                    }

                    // Export the graph diagram to a PNG image
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = 0,
                        PageCount = 1
                    };

                    graphDiagram.Save(outputPath, pngOptions);
                    Console.WriteLine($"Dependency graph saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
