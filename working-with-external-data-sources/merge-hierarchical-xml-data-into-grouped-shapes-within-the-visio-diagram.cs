using System.IO;
using System;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;

public class Program
{
    // Recursively creates shapes for XML nodes and groups them hierarchically.
    private static Shape ProcessNode(XmlNode node, Page page, ref double currentY)
    {
        // Define basic shape dimensions and position.
        double pinX = 2.0;
        double pinY = currentY;
        double width = 2.0;
        double height = 1.0;

        // Create a rectangle shape representing the current XML element.
        long shapeId = page.DrawRectangle(pinX, pinY, width, height);
        Shape shape = page.Shapes.GetShape(shapeId);

        // Set shape text to the element name.
        shape.Text.Value.Clear();
        shape.Text.Value.Add(new Txt(node.Name));

        // Store element name and inner text in shape data fields.
        shape.Data1 = node.Name;
        shape.Data2 = node.InnerText?.Trim();

        // Prepare for child elements.
        double childY = pinY + height + 0.5; // start position for children
        List<Shape> childShapes = new List<Shape>();

        // Process each child element recursively.
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
            {
                Shape childShape = ProcessNode(child, page, ref childY);
                childShapes.Add(childShape);
            }
        }

        // Update the current Y position for the next sibling.
        currentY = childY;

        // If there are child shapes, group them with the parent shape.
        if (childShapes.Count > 0)
        {
            List<Shape> groupMembers = new List<Shape>();
            groupMembers.Add(shape);
            groupMembers.AddRange(childShapes);

            // Create a group shape containing the parent and its children.
            Shape groupShape = page.Shapes.Group(groupMembers.ToArray());

            // Optionally set group text to the element name.
            groupShape.Text.Value.Clear();
            groupShape.Text.Value.Add(new Txt(node.Name));

            return groupShape;
        }

        // Leaf node: return the created shape.
        return shape;
    }

    public static void Main()
    {
        try
        {

            // Load or create a new Visio diagram.
            Diagram diagram = new Diagram();

            // Ensure there is at least one page.
            Page page = diagram.Pages[0];

            // Load hierarchical XML data.
            string xmlPath = "data.xml"; // Path to the XML file.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Start positioning at Y = 1.0 inches.
            double startY = 1.0;

            // Process the root element.
            XmlNode rootNode = xmlDoc.DocumentElement;
            if (rootNode != null)
            {
                ProcessNode(rootNode, page, ref startY);
            }

            // Save the resulting diagram.
            string outputPath = "MergedDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
