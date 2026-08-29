using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: program <inputVisioPath> [outputPngPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output path (default to same folder with .png extension)
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".png");

        try
        {
            // Load the source Visio diagram
            Diagram sourceDiagram = new Diagram(inputPath);

            // Use the first page for processing
            Page sourcePage = sourceDiagram.Pages[0];

            // Build a set of shape IDs that are actual nodes (non‑connector shapes)
            var nodeIds = new System.Collections.Generic.HashSet<long>();
            foreach (Connect conn in sourcePage.Connects)
            {
                // FromSheet and ToSheet refer to shape IDs; include both as nodes
                nodeIds.Add(conn.FromSheet);
                nodeIds.Add(conn.ToSheet);
            }

            // Filter out connector shapes (1‑D shapes) from the node set
            var actualNodeIds = new System.Collections.Generic.List<long>();
            foreach (long id in nodeIds)
            {
                Shape shape = sourcePage.Shapes.GetShape(id);
                // OneD == true indicates a connector; skip those
                if (!shape.OneD)
                {
                    actualNodeIds.Add(id);
                }
            }

            // Create a new diagram to render the dependency graph
            Diagram graphDiagram = new Diagram();
            // Ensure at least one page exists
            Page graphPage = graphDiagram.Pages[0];

            // Simple grid layout parameters
            const double nodeWidth = 2.0;   // inches
            const double nodeHeight = 1.0;  // inches
            const double hSpacing = 1.0;    // horizontal spacing
            const double vSpacing = 1.0;    // vertical spacing

            // Determine grid dimensions
            int columns = (int)Math.Ceiling(Math.Sqrt(actualNodeIds.Count));
            int rows = (int)Math.Ceiling((double)actualNodeIds.Count / columns);

            // Mapping from original node ID to rectangle shape ID in the graph diagram
            var nodeRectMap = new System.Collections.Generic.Dictionary<long, long>();

            // Create rectangle shapes for each node and assign text
            for (int i = 0; i < actualNodeIds.Count; i++)
            {
                long originalId = actualNodeIds[i];
                // Compute grid position
                int col = i % columns;
                int row = i / columns;
                double pinX = col * (nodeWidth + hSpacing);
                double pinY = row * (nodeHeight + vSpacing);

                // Draw a rectangle representing the node
                long rectId = graphPage.DrawRectangle(pinX, pinY, nodeWidth, nodeHeight);
                Shape rectShape = graphPage.Shapes.GetShape(rectId);

                // Retrieve the original shape to obtain its name for labeling
                Shape originalShape = sourcePage.Shapes.GetShape(originalId);
                string label = !string.IsNullOrWhiteSpace(originalShape.NameU) ? originalShape.NameU : $"Node_{originalId}";

                // Clear any existing text and add the label
                rectShape.Text.Value.Clear();
                rectShape.Text.Value.Add(new Txt(label));

                // Store mapping for later connector creation
                nodeRectMap[originalId] = rectId;
            }

            // Create connectors based on the original page's Connects collection
            foreach (Connect conn in sourcePage.Connects)
            {
                // Skip if either endpoint is a connector shape
                if (sourcePage.Shapes.GetShape(conn.FromSheet).OneD || sourcePage.Shapes.GetShape(conn.ToSheet).OneD)
                    continue;

                // Retrieve rectangle shape IDs for source and target nodes
                if (!nodeRectMap.TryGetValue(conn.FromSheet, out long fromRectId) ||
                    !nodeRectMap.TryGetValue(conn.ToSheet, out long toRectId))
                    continue; // safety check

                // Add a dynamic connector shape (position will be adjusted by the glue operation)
                long connectorId = graphPage.AddShape(0, 0, "Dynamic connector", false);
                Shape connectorShape = graphPage.Shapes.GetShape(connectorId);

                // Connect the rectangles using the connector
                graphPage.ConnectShapesViaConnector(
                    fromRectId, ConnectionPointPlace.Bottom,
                    toRectId,   ConnectionPointPlace.Top,
                    connectorId);

                // Set a right‑angle routing style for clarity
                connectorShape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
            }

            // Prepare PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Export only the first page (the graph page)
            pngOptions.PageIndex = 0;
            pngOptions.PageCount = 1;

            // Save the generated graph as a PNG image
            graphDiagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Dependency graph saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or runtime errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}