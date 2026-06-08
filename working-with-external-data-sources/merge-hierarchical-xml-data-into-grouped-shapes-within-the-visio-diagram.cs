using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the Visio file and the hierarchical XML file
                string visioPath = "input.vsdx";
                string xmlPath = "data.xml";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);
                // Use the first page (index 0)
                Page page = diagram.Pages[0];

                // Load the hierarchical XML
                XDocument xmlDoc = XDocument.Load(xmlPath);
                XElement rootElement = xmlDoc.Root;

                // Dictionary to keep track of created shape IDs for each XML element
                Dictionary<XElement, long> elementShapeMap = new Dictionary<XElement, long>();

                // Recursively create shapes for each XML node
                CreateShapeRecursive(diagram, page, rootElement, elementShapeMap, 2.0, 2.0, 1.5, 1.0);

                // After all shapes are created, group parent with its children
                GroupHierarchy(page, rootElement, elementShapeMap);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Recursively creates a shape for the given XML element and its children.
        // The position parameters are updated for each new shape to avoid overlap.
        private static void CreateShapeRecursive(Diagram diagram, Page page, XElement element,
            Dictionary<XElement, long> map, double startX, double startY, double offsetX, double offsetY)
        {
            // Create a rectangle shape using the master name "Rectangle"
            long shapeId = diagram.AddShape(startX, startY, "Rectangle", 0);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the shape's text to the element's "Name" attribute (or element name if missing)
            string nodeName = (string)element.Attribute("Name") ?? element.Name.LocalName;
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(nodeName));

            // Store the mapping
            map[element] = shapeId;

            // Position for child shapes
            double childX = startX + offsetX;
            double childY = startY + offsetY;

            // Process child nodes
            foreach (XElement child in element.Elements())
            {
                CreateShapeRecursive(diagram, page, child, map, childX, childY, offsetX, offsetY);
                // Move down for the next sibling
                childY += offsetY * 2;
            }
        }

        // Groups each parent shape with its immediate child shapes.
        private static void GroupHierarchy(Page page, XElement element, Dictionary<XElement, long> map)
        {
            // Get the shape for the current element
            if (!map.TryGetValue(element, out long parentId))
                return;

            // Collect child shapes
            List<Shape> childShapes = new List<Shape>();
            foreach (XElement child in element.Elements())
            {
                if (map.TryGetValue(child, out long childId))
                {
                    Shape childShape = page.Shapes.GetShape(childId);
                    childShapes.Add(childShape);
                    // Recursively group deeper levels first
                    GroupHierarchy(page, child, map);
                }
            }

            // If there are child shapes, group them with the parent
            if (childShapes.Count > 0)
            {
                // Include the parent shape in the group
                Shape parentShape = page.Shapes.GetShape(parentId);
                List<Shape> groupMembers = new List<Shape> { parentShape };
                groupMembers.AddRange(childShapes);

                // Perform grouping
                page.Shapes.Group(groupMembers.ToArray());
            }
        }
    }