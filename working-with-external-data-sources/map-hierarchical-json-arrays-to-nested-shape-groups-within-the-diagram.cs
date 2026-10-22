using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Represents a node in the hierarchical JSON structure.
    private class Node
    {
        public string? Name { get; set; }
        public List<Node>? Children { get; set; } = new();
    }

    // Tracks the next Y position for placing shapes to avoid overlap.
    private static double _nextY = 0.0;

    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputJsonPath> <outputDiagramPath>");
            return;
        }

        string jsonPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Input JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Read and deserialize the JSON hierarchy.
            string jsonContent = File.ReadAllText(jsonPath);
            Node? rootNode = JsonSerializer.Deserialize<Node>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (rootNode == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON content.");
                return;
            }

            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Use the first page (default page) for adding shapes.
            Page page = diagram.Pages[0];

            // Recursively build shapes and groups based on the hierarchy.
            Shape topGroup = BuildGroup(page, rootNode, 0);

            // Optionally, set the name of the top-level group.
            if (!string.IsNullOrEmpty(topGroup?.Name))
            {
                topGroup.Name = "RootGroup";
            }

            // Save the diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively creates a shape for the current node, processes its children,
    /// and groups them together. Returns the group shape representing this node.
    /// </summary>
    /// <param name="page">The page to which shapes are added.</param>
    /// <param name="node">The hierarchical node.</param>
    /// <param name="depth">Current depth in the hierarchy (used for positioning).</param>
    /// <returns>The group shape containing the node and its children.</returns>
    private static Shape BuildGroup(Page page, Node node, int depth)
    {
        // Calculate position based on depth and a running Y offset.
        double x = depth * 2.5;          // Horizontal offset per level.
        double y = _nextY;               // Vertical position.
        _nextY += 2.5;                   // Increment for the next shape.

        // Create a rectangle shape for the current node.
        // DrawRectangle returns a shape ID (long).
        long shapeId = page.DrawRectangle(x, y, 1.5, 0.8);
        Shape nodeShape = page.Shapes.GetShape(shapeId);

        // Set the displayed text to the node's name.
        nodeShape.Text.Value.Clear();
        nodeShape.Text.Value.Add(new Txt(node.Name ?? string.Empty));

        // Collect shapes to be grouped: start with the node's own shape.
        List<Shape> shapesToGroup = new List<Shape> { nodeShape };

        // Process child nodes, if any.
        if (node.Children != null && node.Children.Count > 0)
        {
            foreach (Node child in node.Children)
            {
                // Recursively build the child's group.
                Shape childGroup = BuildGroup(page, child, depth + 1);
                if (childGroup != null)
                {
                    shapesToGroup.Add(childGroup);
                }
            }
        }

        // Group the node shape with all its child groups.
        // The Group method returns the new group shape.
        Shape groupShape = page.Shapes.Group(shapesToGroup.ToArray());

        // Optionally, assign a name to the group for easier identification.
        if (!string.IsNullOrEmpty(groupShape?.Name))
        {
            groupShape.Name = node.Name ?? "Group";
        }

        return groupShape;
    }
}
