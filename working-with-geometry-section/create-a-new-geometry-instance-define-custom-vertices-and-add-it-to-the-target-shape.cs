using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a new page to the diagram
                diagram.Pages.Add(new Page());
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page (using the built‑in "Rectangle" master)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape has at least one geometry section
                if (shape.Geoms.Count == 0)
                {
                    // If no geometry exists, create a new one and add it to the collection
                    Geom newGeom = new Geom();
                    shape.Geoms.Add(newGeom);
                }

                // Get the first geometry (Geom) of the shape
                Geom geom = (Geom)shape.Geoms[0];

                // Clear any existing vertices (optional, depending on desired outcome)
                geom.CoordinateCol.Clear();

                // Define custom vertices:
                // 1. MoveTo – starting point of the path
                MoveTo moveTo = new MoveTo();
                moveTo.X.Value = 0.0;   // X coordinate in inches
                moveTo.Y.Value = 0.0;   // Y coordinate in inches
                geom.CoordinateCol.Add(moveTo);

                // 2. LineTo – first line segment
                LineTo line1 = new LineTo();
                line1.X.Value = 1.5;
                line1.Y.Value = 0.0;
                geom.CoordinateCol.Add(line1);

                // 3. LineTo – second line segment
                LineTo line2 = new LineTo();
                line2.X.Value = 1.5;
                line2.Y.Value = 1.0;
                geom.CoordinateCol.Add(line2);

                // 4. LineTo – third line segment (closing the shape)
                LineTo line3 = new LineTo();
                line3.X.Value = 0.0;
                line3.Y.Value = 1.0;
                geom.CoordinateCol.Add(line3);

                // 5. LineTo – back to the start point (optional, Visio will close automatically)
                LineTo line4 = new LineTo();
                line4.X.Value = 0.0;
                line4.Y.Value = 0.0;
                geom.CoordinateCol.Add(line4);

                // Save the diagram to verify the custom geometry
                diagram.Save("CustomGeometry.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram with custom geometry saved successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }