using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: VisioXmlMerge <diagramPath> <xmlPath> <outputPath>");
                return;
            }

            string diagramPath = args[0];
            string xmlPath = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the hierarchical XML
            XDocument xDoc = XDocument.Load(xmlPath);
            XElement rootElement = xDoc.Root;
            if (rootElement == null)
            {
                Console.WriteLine("XML does not contain a root element.");
                return;
            }

            // Use the first page (or create one if none exist)
            Page page;
            if (diagram.Pages.Count > 0)
            {
                page = diagram.Pages[0];
            }
            else
            {
                page = new Page();
                diagram.Pages.Add(page);
            }

            // Starting coordinates for placing groups
            double startX = 2.0;
            double startY = 2.0;
            double groupSpacingX = 5.0;
            double groupSpacingY = 5.0;

            // Process each top‑level element as a separate group
            int groupIndex = 0;
            foreach (XElement groupElement in rootElement.Elements())
            {
                // Create a list to hold child shape references
                List<Shape> childShapes = new List<Shape>();

                // Positioning within the group
                double childX = startX + (groupIndex % 5) * groupSpacingX;
                double childY = startY + (groupIndex / 5) * groupSpacingY;
                double offsetX = 0.0;
                double offsetY = 0.0;
                double shapeWidth = 1.5;
                double shapeHeight = 1.0;

                // Iterate over child elements of the group
                foreach (XElement item in groupElement.Elements())
                {
                    // Add a rectangle shape for each child
                    long shapeId = page.AddShape(childX + offsetX, childY + offsetY, "Rectangle", false);
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Set the shape's text to the element name (or value if present)
                    string text = item.HasElements ? item.Name.LocalName : item.Value;
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(text));

                    // Optionally store the original XML value in Data1
                    shape.Data1 = item.Value;

                    childShapes.Add(shape);

                    // Simple layout: shift next shape to the right
                    offsetX += shapeWidth + 0.5;
                }

                // Group the created shapes if there are at least two
                if (childShapes.Count > 1)
                {
                    Shape groupShape = page.Shapes.Group(childShapes.ToArray());

                    // Set group text to the group element name
                    groupShape.Text.Value.Clear();
                    groupShape.Text.Value.Add(new Txt(groupElement.Name.LocalName));

                    // Optionally move the group to a distinct location
                    groupShape.MoveTo(startX + (groupIndex % 5) * groupSpacingX,
                                      startY + (groupIndex / 5) * groupSpacingY);
                }

                groupIndex++;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }