using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty Visio diagram
                Diagram diagram = new Diagram();

                // Add two rectangle shapes on the first page (page index 0)
                long shapeAId = diagram.AddShape(2.0, 5.0, "Rectangle", 0);
                long shapeBId = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

                // Retrieve the shape objects from the page
                Page page = diagram.Pages[0];
                Shape shapeA = page.Shapes.GetShape(shapeAId);
                Shape shapeB = page.Shapes.GetShape(shapeBId);

                // Assign recognizable names to the shapes (optional but helpful)
                shapeA.Name = "ShapeA";
                shapeB.Name = "ShapeB";

                // Create a new text field on ShapeA
                Field proportionalField = new Field();
                shapeA.Fields.Add(proportionalField);

                // Set the field's formula to reference the Height of ShapeB
                // Visio formula syntax: Height of shape "ShapeB"
                proportionalField.Value.Ufev.F = "Height of shape \"ShapeB\"";

                // Optionally set a default display value (will be overridden by the formula at render time)
                proportionalField.Value.Val = "";

                // Save the diagram to a VSDX file
                diagram.Save("ProportionalFieldDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }