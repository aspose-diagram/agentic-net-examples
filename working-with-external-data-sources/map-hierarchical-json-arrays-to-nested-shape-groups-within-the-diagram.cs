using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Model representing a node in the hierarchical JSON
    private class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputJsonPath> <outputDiagramPath>");
            return;
        }

        string jsonPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Input JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Read and deserialize the hierarchical JSON
            string jsonContent = File.ReadAllText(jsonPath);
            List<Node> rootNodes = JsonSerializer.Deserialize<List<Node>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (rootNodes == null)
            {
                Console.Error.WriteLine("Failed to parse JSON or JSON is empty.");
                return;
            }

            // Create a new empty diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Use the first page for adding shapes
                Page page = diagram.Pages[0];

                // Process each top‑level node and create nested groups
                foreach (Node rootNode in rootNodes)
                {
                    AddNodeGroup(page, rootNode);
                }

                // Save the diagram to the specified output path as VSDX
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively creates a rectangle shape for the given node, processes its children,
    /// and groups the parent shape with all child groups into a single group shape.
    /// </summary>
    private static Shape AddNodeGroup(Page page, Node node)
    {
        // Add a rectangle shape for the current node (pinX, pinY, width, height)
        long rectId = page.DrawRectangle(2.0, 2.0, 2.0, 1.0);
        Shape rectShape = page.Shapes.GetShape(rectId);

        // Set the shape's displayed text to the node's name
        rectShape.Text.Value.Clear();
        rectShape.Text.Value.Add(new Txt(node.Name ?? string.Empty));

        // List to hold the shapes of all child groups
        List<Shape> childGroupShapes = new List<Shape>();

        // Process child nodes recursively
        if (node.Children != null)
        {
            foreach (Node child in node.Children)
            {
                Shape childGroup = AddNodeGroup(page, child);
                childGroupShapes.Add(childGroup);
            }
        }

        // Prepare the collection of shapes to be grouped: parent shape + child groups
        List<Shape> shapesToGroup = new List<Shape> { rectShape };
        shapesToGroup.AddRange(childGroupShapes);

        // Group the shapes; the Group method returns the new group shape
        Shape groupShape = page.Shapes.Group(shapesToGroup.ToArray());

        return groupShape;
    }
}