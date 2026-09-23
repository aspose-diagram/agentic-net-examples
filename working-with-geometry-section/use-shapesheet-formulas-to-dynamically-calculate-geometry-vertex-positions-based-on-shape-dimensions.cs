using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape; the fourth parameter (isCalculate) must be a bool
            long rectId = page.AddShape(2.0, 2.0, 4.0, 2.0, "Rectangle", false);

            // Retrieve the shape instance for further manipulation
            Shape rectShape = page.Shapes.GetShape(rectId);

            // Clear any existing geometry definitions
            rectShape.Geoms.Clear();

            // Create a new geometry section
            Geom geom = new Geom();

            // ---- MoveTo (starting point at the lower‑left corner) ----
            MoveTo move = new MoveTo();
            // Use ShapeSheet formulas so the geometry reacts to shape size changes
            move.X.Ufe.F = "0";               // X = 0 (relative to shape)
            move.Y.Ufe.F = "0";               // Y = 0
            geom.CoordinateCol.Add(move);

            // ---- LineTo (top edge) ----
            LineTo top = new LineTo();
            top.X.Ufe.F = "Width";            // X = shape's Width
            top.Y.Ufe.F = "0";                // Y = 0
            geom.CoordinateCol.Add(top);

            // ---- LineTo (right edge) ----
            LineTo right = new LineTo();
            right.X.Ufe.F = "Width";          // X = shape's Width
            right.Y.Ufe.F = "Height";         // Y = shape's Height
            geom.CoordinateCol.Add(right);

            // ---- LineTo (bottom edge) ----
            LineTo bottom = new LineTo();
            bottom.X.Ufe.F = "0";             // X = 0
            bottom.Y.Ufe.F = "Height";        // Y = shape's Height
            geom.CoordinateCol.Add(bottom);

            // ---- Close the shape by returning to the start point ----
            LineTo close = new LineTo();
            close.X.Ufe.F = "0";
            close.Y.Ufe.F = "0";
            geom.CoordinateCol.Add(close);

            // Add the constructed geometry to the shape
            rectShape.Geoms.Add(geom);

            // Save the diagram; use a valid SaveFileFormat enum member
            diagram.Save("DynamicGeometry.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
