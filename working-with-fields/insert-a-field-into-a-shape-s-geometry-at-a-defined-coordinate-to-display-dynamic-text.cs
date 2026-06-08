using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Use the first page (default page is created automatically)
                Page page = diagram.Pages[0];

                // Add a rectangle shape at (2, 2) inches
                double pinX = 2.0;
                double pinY = 2.0;
                long shapeId = page.AddShape(pinX, pinY, "Rectangle");
                Shape shape = page.Shapes.GetShape(shapeId);

                // -------------------------------------------------
                // 1. Insert a geometry vertex at a defined coordinate
                // -------------------------------------------------
                // Ensure the shape has at least one geometry section
                if (shape.Geoms.Count > 0)
                {
                    // Get the first geometry section
                    Geom geom = (Geom)shape.Geoms[0];

                    // Create a MoveTo vertex at (3, 3) inches
                    MoveTo moveTo = new MoveTo();
                    moveTo.X.Value = 3.0;
                    moveTo.Y.Value = 3.0;

                    // Append the vertex to the geometry's coordinate collection
                    geom.CoordinateCol.Add(moveTo);
                }

                // -------------------------------------------------
                // 2. Add a dynamic field to the shape's text
                // -------------------------------------------------
                // Create a new field object
                Field field = new Field();

                // Set the field's formula – for example, display the shape's width multiplied by its height
                field.Value.Ufev.F = "Width*Height";
                field.Value.Ufev.Unit = MeasureConst.Undefined; // No specific unit

                // Add the field to the shape's Fields collection
                shape.Fields.Add(field);

                // Add a text run that references the field (Visio will replace the field placeholder with the evaluated value)
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Area = "));
                // The field itself will be displayed after the preceding text
                // (Visio automatically renders fields that are present in the Fields collection)

                // -------------------------------------------------
                // Save the diagram to a VSDX file
                // -------------------------------------------------
                string outputPath = "OutputDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }