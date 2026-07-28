using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramJsonMapper
{
    // Model representing a node in the hierarchical JSON
    public class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; set; } = new();
    }

    public class Program
    {
        // Entry point
        public static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramJsonMapper <jsonFilePath> <outputVsdxPath>");
                return;
            }

            string jsonPath = args[0];
            string outputPath = args[1];

            // Load and deserialize JSON
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            string jsonContent = File.ReadAllText(jsonPath);
            Node rootNode;
            try
            {
                rootNode = JsonSerializer.Deserialize<Node>(jsonContent);
                if (rootNode == null)
                {
                    Console.WriteLine("Failed to deserialize JSON.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                return;
            }

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page(0));
            }

            Page page = diagram.Pages[0];

            // Starting coordinates for the root shape
            double startX = 2.0;
            double startY = 2.0;

            // Build the diagram recursively
            Shape rootGroup = BuildGroup(diagram, page, rootNode, startX, startY, 0);

            // Optionally, you could set the name of the root group
            if (rootGroup != null)
            {
                rootGroup.Name = rootNode.Name ?? "RootGroup";
            }

            // Save the diagram as VSDX
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }

        // Recursively creates shapes and groups them according to the hierarchy
        private static Shape BuildGroup(Diagram diagram, Page page, Node node, double posX, double posY, int depth)
        {
            // Create a rectangle shape for the current node
            // Using master name "Rectangle" which is available in the default stencil
            long rectId = diagram.AddShape(posX, posY, "Rectangle", 0);
            Shape rectShape = page.Shapes.GetShape((int)rectId);
            rectShape.Name = node.Name ?? $"Node_{depth}";
            // Add text to the rectangle
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt(node.Name ?? $"Node_{depth}"));

            // If there are no children, return the rectangle shape itself
            if (node.Children == null || node.Children.Count == 0)
            {
                return rectShape;
            }

            // Prepare a list to hold the parent rectangle and all child shapes/groups
            List<Shape> groupMembers = new List<Shape> { rectShape };

            // Layout children vertically with an offset
            double childOffsetX = 2.5; // horizontal offset between parent and children
            double childOffsetY = 2.0; // vertical spacing between sibling children
            double currentY = posY;

            foreach (Node child in node.Children)
            {
                // Recursively build child group/shape
                Shape childShape = BuildGroup(diagram, page, child, posX + childOffsetX, currentY, depth + 1);
                if (childShape != null)
                {
                    groupMembers.Add(childShape);
                }
                // Move down for the next sibling
                currentY += childOffsetY;
            }

            // Create a group containing the parent rectangle and its children
            Shape groupShape = page.Shapes.Group(groupMembers.ToArray());

            // Optionally, set the group's name
            groupShape.Name = node.Name ?? $"Group_{depth}";

            return groupShape;
        }
    }
}