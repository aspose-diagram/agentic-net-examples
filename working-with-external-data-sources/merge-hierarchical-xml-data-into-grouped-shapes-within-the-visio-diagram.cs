using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio diagram path
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Input hierarchical XML data path
        string xmlPath = "data.xml";
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Output Visio diagram path
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Load hierarchical XML data
                XDocument xDoc = XDocument.Load(xmlPath);
                if (xDoc.Root == null)
                {
                    Console.Error.WriteLine("XML does not contain a root element.");
                    return;
                }

                // Use the first page for shape creation
                Page page = diagram.Pages[0];

                // Positioning offsets for groups and shapes
                double groupOffsetX = 2.0;
                double groupOffsetY = 2.0;
                double shapeSpacing = 2.0;

                // Iterate over top‑level XML elements
                foreach (XElement topElement in xDoc.Root.Elements())
                {
                    List<Shape> childShapes = new List<Shape>();
                    double currentX = groupOffsetX;
                    double currentY = groupOffsetY;

                    // Process child elements of the current top element
                    foreach (XElement childElement in topElement.Elements())
                    {
                        // Add a rectangle shape using the master name "Rectangle"
                        long shapeId = page.AddShape(currentX, currentY, "Rectangle", false);
                        Shape shape = page.Shapes.GetShape(shapeId);

                        // Store XML element name and value in custom data fields
                        shape.Data1 = childElement.Name.LocalName;
                        shape.Data2 = childElement.Value;

                        // Set the shape's displayed text
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(childElement.Name.LocalName));

                        // Add shape to the collection for later grouping
                        childShapes.Add(shape);

                        // Update position for the next shape to avoid overlap
                        currentX += shapeSpacing;
                        currentY += shapeSpacing;
                    }

                    // If child shapes were created, group them together
                    if (childShapes.Count > 0)
                    {
                        Shape groupShape = page.Shapes.Group(childShapes.ToArray());

                        // Store the top‑level element name in the group's custom data field
                        groupShape.Data1 = topElement.Name.LocalName;

                        // Set a label for the group
                        groupShape.Text.Value.Clear();
                        groupShape.Text.Value.Add(new Txt(topElement.Name.LocalName));

                        // Move the next group offset further away
                        groupOffsetX += shapeSpacing * 5;
                        groupOffsetY += shapeSpacing * 5;
                    }
                    else
                    {
                        // No child elements – create a single shape for the top element
                        long shapeId = page.AddShape(groupOffsetX, groupOffsetY, "Rectangle", false);
                        Shape shape = page.Shapes.GetShape(shapeId);
                        shape.Data1 = topElement.Name.LocalName;
                        shape.Data2 = topElement.Value;
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(topElement.Name.LocalName));

                        // Advance offset for the next top‑level element
                        groupOffsetX += shapeSpacing * 5;
                        groupOffsetY += shapeSpacing * 5;
                    }
                }

                // Refresh the diagram to apply all changes
                diagram.Refresh();

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
