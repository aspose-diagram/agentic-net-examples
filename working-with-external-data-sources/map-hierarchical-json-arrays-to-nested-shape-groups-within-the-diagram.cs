using System;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Sample hierarchical JSON
                string json = @"
                {
                    ""name"": ""Root"",
                    ""children"": [
                        {
                            ""name"": ""Child 1"",
                            ""children"": [
                                { ""name"": ""Grandchild 1"", ""children"": [] },
                                { ""name"": ""Grandchild 2"", ""children"": [] }
                            ]
                        },
                        {
                            ""name"": ""Child 2"",
                            ""children"": []
                        }
                    ]
                }";

                // Parse JSON
                JsonDocument doc = JsonDocument.Parse(json);
                JsonElement rootElement = doc.RootElement;

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one page
                Page page = diagram.Pages[0];

                // Build groups recursively starting at (2,2) coordinates
                long rootGroupId = CreateGroupRecursive(rootElement, page, 2.0, 2.0, 0);

                // Optionally you can retrieve the root group shape for further processing
                Shape rootGroupShape = page.Shapes.GetShape((int)rootGroupId);

                // Save the diagram
                diagram.Save("HierarchicalDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Recursive method to create a shape for the current node, its children, and group them
        private static long CreateGroupRecursive(JsonElement element, Page page, double baseX, double baseY, int level)
        {
            // Determine a simple offset based on hierarchy level to avoid overlap
            double offsetX = baseX + level * 2.0;
            double offsetY = baseY + level * 2.0;

            // Create a rectangle shape representing the current node
            long parentShapeId = page.AddShape(offsetX, offsetY, "Rectangle", false);
            Shape parentShape = page.Shapes.GetShape((int)parentShapeId);
            // Set the shape's text to the node's name
            if (element.TryGetProperty("name", out JsonElement nameProp))
            {
                string nodeName = nameProp.GetString() ?? string.Empty;
                parentShape.Text.Value.Clear();
                parentShape.Text.Value.Add(new Txt(nodeName));
            }

            // Collect child shapes
            List<Shape> childShapes = new List<Shape>();
            if (element.TryGetProperty("children", out JsonElement childrenProp) && childrenProp.ValueKind == JsonValueKind.Array)
            {
                int childIndex = 0;
                foreach (JsonElement child in childrenProp.EnumerateArray())
                {
                    // Recursively create groups for each child
                    long childGroupId = CreateGroupRecursive(child, page, offsetX + 2.0, offsetY + 2.0, level + 1);
                    Shape childGroupShape = page.Shapes.GetShape((int)childGroupId);
                    childShapes.Add(childGroupShape);
                    childIndex++;
                }
            }

            // If there are child shapes, group them together with the parent shape
            if (childShapes.Count > 0)
            {
                // Prepare array of shapes to group (parent + children)
                Shape[] groupMembers = new Shape[childShapes.Count + 1];
                groupMembers[0] = parentShape;
                for (int i = 0; i < childShapes.Count; i++)
                {
                    groupMembers[i + 1] = childShapes[i];
                }

                // Create the group; the method returns the new group shape
                Shape groupShape = page.Shapes.Group(groupMembers);
                return groupShape.ID;
            }
            else
            {
                // No children – the parent shape itself acts as the group
                return parentShape.ID;
            }
        }
    }