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

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Add a rectangle shape (master name "Rectangle") with initial size
            // PinX = 2, PinY = 2, Width = 4, Height = 2
            long shapeId = page.AddShape(2.0, 2.0, 4.0, 2.0, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Ensure the shape has a geometry section; add one if missing
            if (shape.Geoms.Count == 0)
            {
                shape.Geoms.Add(new Geom());
            }

            // Work with the first geometry (index 0)
            Geom geom = (Geom)shape.Geoms[0];

            // Clear any existing geometry commands
            geom.CoordinateCol.Clear();

            // ---- Define geometry using ShapeSheet formulas ----
            // MoveTo (0,0) – start at the shape's origin
            MoveTo move = new MoveTo();
            move.X.Ufe.F = "0";
            move.Y.Ufe.F = "0";
            geom.CoordinateCol.Add(move);

            // LineTo (Width,0) – right edge
            LineTo line1 = new LineTo();
            line1.X.Ufe.F = "Width";
            line1.Y.Ufe.F = "0";
            geom.CoordinateCol.Add(line1);

            // LineTo (Width,Height) – top‑right corner
            LineTo line2 = new LineTo();
            line2.X.Ufe.F = "Width";
            line2.Y.Ufe.F = "Height";
            geom.CoordinateCol.Add(line2);

            // LineTo (0,Height) – top‑left corner
            LineTo line3 = new LineTo();
            line3.X.Ufe.F = "0";
            line3.Y.Ufe.F = "Height";
            geom.CoordinateCol.Add(line3);

            // Close the shape by returning to the start point
            LineTo line4 = new LineTo();
            line4.X.Ufe.F = "0";
            line4.Y.Ufe.F = "0";
            geom.CoordinateCol.Add(line4);

            // Save the diagram; the geometry will update automatically if the shape size changes
            diagram.Save("DynamicGeometry.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
