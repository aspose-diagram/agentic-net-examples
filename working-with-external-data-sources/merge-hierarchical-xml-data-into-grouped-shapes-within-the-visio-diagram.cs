using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, input XML file, output Visio file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <inputXmlPath> <outputVisioPath>");
            return;
        }

        string visioPath = args[0];
        string xmlPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Load the hierarchical XML data
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode rootNode = xmlDoc.DocumentElement;

            // Ensure there is at least one page to work with
            Page page;
            if (diagram.Pages.Count > 0)
                page = diagram.Pages[0];
            else
            {
                page = new Page();
                diagram.Pages.Add(page);
            }

            // Starting coordinates for shape placement
            double startX = 1.0;
            double startY = 1.0;
            double offsetX = 2.0; // horizontal spacing between shapes

            // Process the XML hierarchy and create grouped shapes
            ProcessXmlNode(rootNode, page, ref startX, startY, offsetX);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively processes an XML node, creates shapes for leaf items,
    /// groups them according to the XML hierarchy, and returns the resulting shape.
    /// </summary>
    /// <param name="xmlNode">Current XML node.</param>
    /// <param name="page">Page where shapes are added.</param>
    /// <param name="currentX">Reference to the current X coordinate.</param>
    /// <param name="currentY">Current Y coordinate (constant in this example).</param>
    /// <param name="offsetX">Horizontal offset for next shape.</param>
    /// <returns>The shape representing this node (either a leaf shape or a group).</returns>
    static Shape ProcessXmlNode(XmlNode xmlNode, Page page,
        ref double currentX, double currentY, double offsetX)
    {
        List<Shape> childShapes = new List<Shape>();

        // Iterate through child elements
        foreach (XmlNode child in xmlNode.ChildNodes)
        {
            if (child.NodeType != XmlNodeType.Element)
                continue;

            // Handle leaf items
            if (child.Name.Equals("Item", StringComparison.OrdinalIgnoreCase))
            {
                // Add a rectangle shape for the item
                long shapeId = page.AddShape(currentX, currentY, "Rectangle", false);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Assign a name if the XML provides one
                if (child.Attributes["name"] != null)
                {
                    shape.Name = child.Attributes["name"].Value;
                }

                childShapes.Add(shape);

                // Move X coordinate for the next shape
                currentX += offsetX;
            }
            // Handle nested groups
            else if (child.Name.Equals("Group", StringComparison.OrdinalIgnoreCase))
            {
                // Recursively process the subgroup
                Shape groupShape = ProcessXmlNode(child, page,
                    ref currentX, currentY, offsetX);

                if (groupShape != null)
                {
                    childShapes.Add(groupShape);
                }
            }
        }

        // If no shapes were created for this node, return null
        if (childShapes.Count == 0)
            return null;

        // If only one shape exists, no grouping is necessary
        if (childShapes.Count == 1)
            return childShapes[0];

        // Group multiple shapes together
        Shape[] shapeArray = childShapes.ToArray();
        Shape group = page.Shapes.Group(shapeArray);

        // Assign group name from XML attribute if present
        if (xmlNode.Attributes != null && xmlNode.Attributes["name"] != null)
        {
            group.Name = xmlNode.Attributes["name"].Value;
        }

        return group;
    }
}