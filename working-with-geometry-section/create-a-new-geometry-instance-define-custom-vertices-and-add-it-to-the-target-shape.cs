using System.IO;
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

            // Use the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape at (2,2) with width=1 and height=1
            long shapeId = page.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Ensure the shape has at least one geometry section
            if (shape.Geoms.Count == 0)
            {
                Geom emptyGeom = new Geom();
                shape.Geoms.Add(emptyGeom);
            }

            // Get the first geometry section
            Geom geom = (Geom)shape.Geoms[0];

            // Optional: clear existing vertices
            geom.CoordinateCol.Clear();

            // Define custom vertices (a triangle)
            // Move to the first point (0,0)
            MoveTo move = new MoveTo();
            move.X.Value = 0.0;
            move.Y.Value = 0.0;
            geom.CoordinateCol.Add(move);

            // Line to second point (1,0)
            LineTo line1 = new LineTo();
            line1.X.Value = 1.0;
            line1.Y.Value = 0.0;
            geom.CoordinateCol.Add(line1);

            // Line to third point (0.5,1)
            LineTo line2 = new LineTo();
            line2.X.Value = 0.5;
            line2.Y.Value = 1.0;
            geom.CoordinateCol.Add(line2);

            // Close the shape by returning to the first point
            LineTo line3 = new LineTo();
            line3.X.Value = 0.0;
            line3.Y.Value = 0.0;
            geom.CoordinateCol.Add(line3);

            // Save the diagram with the custom geometry
            diagram.Save("CustomGeometry.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
