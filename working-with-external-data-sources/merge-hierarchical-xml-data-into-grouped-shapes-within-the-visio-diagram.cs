using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input file paths (adjust as needed)
                string diagramPath = "input.vsdx";
                string xmlPath = "data.xml";
                string outputPath = "merged_output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Use the first page of the diagram
                Page page = diagram.Pages[0];

                // Load hierarchical XML data
                XDocument xmlDoc = XDocument.Load(xmlPath);
                XElement root = xmlDoc.Root;

                // Simple layout offsets for placing shapes
                double startX = 2.0;
                double startY = 2.0;
                double offsetX = 3.0;
                double offsetY = 2.0;

                // Iterate over each top‑level element (treated as a group parent)
                foreach (XElement parentElement in root.Elements())
                {
                    List<Shape> childShapeList = new List<Shape>();
                    double currentX = startX;
                    double currentY = startY;

                    // Create a shape for each child element
                    foreach (XElement childElement in parentElement.Elements())
                    {
                        // Add a rectangle shape (master name "Rectangle") at the calculated position
                        long shapeId = page.AddShape(currentX, currentY, "Rectangle", false);
                        Shape shape = page.Shapes.GetShape(shapeId);

                        // Set the shape's visible text to the child element name
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(childElement.Name.LocalName));

                        // Store the element's value in Data1 (custom data field)
                        shape.Data1 = childElement.Value;

                        // Add the shape to the collection for later grouping
                        childShapeList.Add(shape);

                        // Update position for the next shape
                        currentX += offsetX;
                        if (currentX > startX + offsetX * 4) // simple wrap logic
                        {
                            currentX = startX;
                            currentY += offsetY;
                        }
                    }

                    // If there are child shapes, group them together
                    if (childShapeList.Count > 0)
                    {
                        Shape[] shapesArray = childShapeList.ToArray();
                        Shape groupShape = page.Shapes.Group(shapesArray);

                        // Set group text to the parent element name
                        groupShape.Text.Value.Clear();
                        groupShape.Text.Value.Add(new Txt(parentElement.Name.LocalName));

                        // Store the parent element's value (if any) in Data1 of the group
                        groupShape.Data1 = parentElement.Value;
                    }

                    // Move start position for the next group to avoid overlap
                    startY += offsetY * 5;
                    startX = 2.0;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }