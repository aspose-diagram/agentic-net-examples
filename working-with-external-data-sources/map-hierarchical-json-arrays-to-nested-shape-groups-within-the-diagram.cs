using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace HierarchyToDiagram
{
    public class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; set; } = new();
    }

    class Program
    {
        static void Main(string[] args)
        {
            string jsonPath = "hierarchy.json";
            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"File not found: {jsonPath}");
                return;
            }

            string jsonContent = File.ReadAllText(jsonPath);
            List<Node> rootNodes = JsonSerializer.Deserialize<List<Node>>(jsonContent);
            if (rootNodes == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON.");
                return;
            }

            Diagram diagram;
            try
            {
                diagram = new Diagram();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Aspose error (diagram creation): {ex.Message}");
                return;
            }

            Page page;
            try
            {
                page = diagram.ActivePage;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Aspose error (access active page): {ex.Message}");
                return;
            }

            double startX = 1.0;
            double startY = 1.0;
            double offsetX = 5.0;

            foreach (Node node in rootNodes)
            {
                Shape topShape = CreateShapeRecursive(node, page, startX, startY);
                if (topShape == null)
                {
                    Console.Error.WriteLine($"Failed to create shape for node '{node.Name}'.");
                    return;
                }
                startX += offsetX;
            }

            try
            {
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Aspose error (save): {ex.Message}");
            }
        }

        private static Shape CreateShapeRecursive(Node node, Page page, double posX, double posY)
        {
            try
            {
                // Leaf node: simple rectangle
                if (node.Children == null || node.Children.Count == 0)
                {
                    long shapeId = page.AddShape(posX, posY, 2.0, 1.0, "Rectangle");
                    Shape shape = page.Shapes.GetShape(shapeId);
                    shape.Name = node.Name;
                    return shape;
                }

                // Non‑leaf: create child shapes
                List<Shape> childShapes = new List<Shape>();
                double childPosX = posX;
                double childPosY = posY + 2.0;
                double childOffsetX = 3.0;

                foreach (Node child in node.Children)
                {
                    Shape childShape = CreateShapeRecursive(child, page, childPosX, childPosY);
                    if (childShape == null)
                    {
                        Console.Error.WriteLine($"Failed to create child shape for node '{child.Name}'.");
                        return null;
                    }
                    childShapes.Add(childShape);
                    childPosX += childOffsetX;
                }

                // Group child shapes
                Shape groupShape = page.Shapes.Group(childShapes.ToArray());
                groupShape.Name = node.Name;
                return groupShape;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Aspose error (node '{node.Name}'): {ex.Message}");
                return null;
            }
        }
    }
}