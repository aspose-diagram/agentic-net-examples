using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Entry point
        static void Main()
        {
            try
            {

                // Sample hierarchical JSON (could be read from a file)
                string json = @"
                [
                    {
                        ""name"": ""RootGroup"",
                        ""children"": [
                            { ""name"": ""ChildShape1"" },
                            {
                                ""name"": ""SubGroup"",
                                ""children"": [
                                    { ""name"": ""GrandChildShape1"" },
                                    { ""name"": ""GrandChildShape2"" }
                                ]
                            },
                            { ""name"": ""ChildShape2"" }
                        ]
                    }
                ]";

                // Parse JSON
                JsonDocument doc = JsonDocument.Parse(json);
                JsonElement rootArray = doc.RootElement;

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one page
                Page page = diagram.Pages[0];

                // Starting coordinates for the first root group
                double startX = 2.0;
                double startY = 2.0;
                double horizontalSpacing = 3.0;
                double verticalSpacing = 2.5;

                // Process each top‑level element
                int index = 0;
                foreach (JsonElement element in rootArray.EnumerateArray())
                {
                    double offsetX = startX + index * horizontalSpacing;
                    double offsetY = startY;

                    // Recursively create shapes/groups
                    CreateNode(page, element, offsetX, offsetY, horizontalSpacing, verticalSpacing);
                    index++;
                }

                // Save the diagram as VSDX
                diagram.Save("HierarchicalDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Recursively creates a shape (rectangle) for the current node,
        // creates child shapes, and groups them together.
        // Returns the ID of the created group (or the shape itself if no children).
        static long CreateNode(Page page, JsonElement node, double posX, double posY,
                               double hSpacing, double vSpacing)
        {
            // Create a rectangle shape representing this node
            // Width and height are fixed for simplicity
            double shapeWidth = 1.5;
            double shapeHeight = 0.8;
            long shapeId = page.AddShape(posX, posY, shapeWidth, shapeHeight, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(node.GetProperty("name").GetString() ?? "Unnamed"));

            // Check for children
            if (node.TryGetProperty("children", out JsonElement children) && children.ValueKind == JsonValueKind.Array)
            {
                List<Shape> childShapes = new List<Shape>();
                int childIndex = 0;
                foreach (JsonElement child in children.EnumerateArray())
                {
                    // Position children below the parent, offset horizontally
                    double childX = posX + childIndex * hSpacing;
                    double childY = posY + vSpacing;

                    long childId = CreateNode(page, child, childX, childY, hSpacing, vSpacing);
                    Shape childShape = page.Shapes.GetShape(childId);
                    childShapes.Add(childShape);
                    childIndex++;
                }

                // Include the parent shape in the group
                List<Shape> groupMembers = new List<Shape> { shape };
                groupMembers.AddRange(childShapes);

                // Create the group
                Shape groupShape = page.Shapes.Group(groupMembers.ToArray());

                // Return the group's ID
                return groupShape.ID;
            }

            // No children – return the shape's ID
            return shapeId;
        }
    }