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
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram.
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Ensure the source diagram has at least one page.
                if (sourceDiagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The source diagram contains no pages.");
                    return;
                }

                // Work with the first page of the source diagram.
                Page sourcePage = sourceDiagram.Pages[0];

                // -----------------------------------------------------------------
                // Collect non‑connector shapes (nodes) from the source page.
                // -----------------------------------------------------------------
                List<long> nodeShapeIds = new List<long>();
                foreach (Shape shape in sourcePage.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // 1‑D shapes are connectors; we keep only 2‑D shapes.
                    if (!shape.OneD)
                    {
                        nodeShapeIds.Add(shape.ID);
                    }
                }

                // Map original shape ID -> index in node list (optional, for layout).
                Dictionary<long, int> shapeIdToIndex = new Dictionary<long, int>();
                for (int i = 0; i < nodeShapeIds.Count; i++)
                {
                    shapeIdToIndex[nodeShapeIds[i]] = i;
                }

                // -----------------------------------------------------------------
                // Add a new page that will contain the dependency graph.
                // -----------------------------------------------------------------
                Page graphPage = new Page();
                graphPage.Name = "DependencyGraph";
                sourceDiagram.Pages.Add(graphPage); // Add the new page to the diagram.

                // Simple grid layout parameters (in inches).
                const double cellWidth = 2.5;
                const double cellHeight = 1.5;
                const double startX = 1.0;
                const double startY = 1.0;
                int columns = (int)Math.Ceiling(Math.Sqrt(nodeShapeIds.Count));
                if (columns == 0) columns = 1;

                // Mapping from original shape ID to the newly created graph shape ID.
                Dictionary<long, long> originalToGraphShapeId = new Dictionary<long, long>();

                // -----------------------------------------------------------------
                // Create a rectangle shape for each node and copy its text.
                // -----------------------------------------------------------------
                for (int i = 0; i < nodeShapeIds.Count; i++)
                {
                    long originalId = nodeShapeIds[i];
                    int row = i / columns;
                    int col = i % columns;
                    double pinX = startX + col * cellWidth;
                    double pinY = startY + row * cellHeight;

                    // Add a rectangle master shape on the graph page.
                    long newShapeId = graphPage.AddShape(pinX, pinY, "Rectangle", false);
                    Shape newShape = graphPage.Shapes.GetShape(newShapeId);

                    // Retrieve the original shape's text.
                    Shape originalShape = sourcePage.Shapes.GetShape(originalId);
                    string text = string.Empty;
                    if (originalShape.Text != null)
                    {
                        text = originalShape.Text.Value.ToString();
                    }

                    // Clear any existing text and set the copied text.
                    newShape.Text.Value.Clear();
                    newShape.Text.Value.Add(new Txt(text));

                    originalToGraphShapeId[originalId] = newShapeId;
                }

                // -----------------------------------------------------------------
                // Iterate over all connections in the source page to create edges.
                // -----------------------------------------------------------------
                foreach (Connect connect in sourcePage.Connects)
                {
                    long fromId = connect.FromSheet;
                    long toId = connect.ToSheet;

                    // Ensure both ends are nodes we have added to the graph.
                    if (!originalToGraphShapeId.ContainsKey(fromId) ||
                        !originalToGraphShapeId.ContainsKey(toId))
                    {
                        continue; // Skip connectors that involve non‑node shapes.
                    }

                    long graphFromId = originalToGraphShapeId[fromId];
                    long graphToId = originalToGraphShapeId[toId];

                    // Add a dynamic connector shape.
                    long connectorId = graphPage.AddShape(0, 0, "Dynamic connector", false);
                    Shape connectorShape = graphPage.Shapes.GetShape(connectorId);

                    // Optionally set connector routing style (right‑angle).
                    connectorShape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                    // Connect the two node shapes via the connector.
                    graphPage.ConnectShapesViaConnector(
                        graphFromId,
                        ConnectionPointPlace.Right,
                        graphToId,
                        ConnectionPointPlace.Left,
                        connectorId);
                }

                // -----------------------------------------------------------------
                // Save only the graph page as a PNG image.
                // -----------------------------------------------------------------
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export only the newly added graph page.
                pngOptions.PageIndex = sourceDiagram.Pages.Count - 1; // zero‑based index of the graph page.
                pngOptions.PageCount = 1;

                sourceDiagram.Save(outputPath, pngOptions);
            }

            Console.WriteLine($"Dependency graph saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}