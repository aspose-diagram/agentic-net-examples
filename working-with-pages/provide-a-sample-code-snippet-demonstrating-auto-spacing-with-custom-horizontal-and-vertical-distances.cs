using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a few sample shapes to the page
                // Parameters: PinX, PinY, master name, page index
                long shapeId1 = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                long shapeId2 = diagram.AddShape(5.0, 2.0, "Rectangle", 0);
                long shapeId3 = diagram.AddShape(2.0, 5.0, "Rectangle", 0);
                long shapeId4 = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

                // Retrieve the shapes (optional, just to demonstrate retrieval)
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);
                Shape shape3 = page.Shapes.GetShape(shapeId3);
                Shape shape4 = page.Shapes.GetShape(shapeId4);

                // Set some text for each shape (optional)
                shape1.Text.Value.Add(new Txt("A"));
                shape2.Text.Value.Add(new Txt("B"));
                shape3.Text.Value.Add(new Txt("C"));
                shape4.Text.Value.Add(new Txt("D"));

                // Configure auto‑spacing options
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2.0, // horizontal distance in inches
                    DistanceInVertical = 2.0    // vertical distance in inches
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                // Save the diagram to a VSDX file
                diagram.Save("AutoSpacedDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }