using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create an empty diagram
            Diagram diagram = new Diagram();

            // Add a blank page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Add a rectangle shape (master name "Rectangle") with initial size
            // PinX and PinY are the center of the shape; width and height are in inches
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 4.0;
            double height = 3.0;
            long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle");

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Ensure the shape has at least one geometry section
            // (most masters already contain a geometry; we use the first one)
            Geom geom = (Geom)shape.Geoms[0];

            // Clear existing vertices (optional – depends on the master)
            // Adding new vertices will define the shape's outline dynamically
            // using formulas that reference the shape's Width and Height cells.

            // Vertex 1: MoveTo (0,0) – lower‑left corner of the shape's bounding box
            MoveTo v1 = new MoveTo();
            v1.X.Ufe.F = "0";
            v1.Y.Ufe.F = "0";
            geom.CoordinateCol.Add(v1);

            // Vertex 2: LineTo (Width,0) – lower‑right corner
            LineTo v2 = new LineTo();
            v2.X.Ufe.F = "Width";
            v2.Y.Ufe.F = "0";
            geom.CoordinateCol.Add(v2);

            // Vertex 3: LineTo (Width/2, Height) – top middle point (creates a triangle)
            LineTo v3 = new LineTo();
            v3.X.Ufe.F = "Width/2";
            v3.Y.Ufe.F = "Height";
            geom.CoordinateCol.Add(v3);

            // Vertex 4: LineTo (0,0) – close the path back to the start point
            LineTo v4 = new LineTo();
            v4.X.Ufe.F = "0";
            v4.Y.Ufe.F = "0";
            geom.CoordinateCol.Add(v4);

            // Save the diagram to a VSDX file
            diagram.Save("DynamicTriangle.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
