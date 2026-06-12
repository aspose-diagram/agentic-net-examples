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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (default page is created automatically)
            Page page = diagram.Pages[0];

            // Add a temporary rectangle shape – we will replace its geometry with a triangle
            long shapeId = page.AddShape(2.0, 2.0, 2.0, 2.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Remove any existing geometry definitions
            shape.Geoms.Clear();

            // Create a new geometry collection for the triangle
            Geom triangleGeom = new Geom();

            // Define the triangle vertices using MoveTo and LineTo segments
            MoveTo start = new MoveTo();
            start.X.Value = 0.0;
            start.Y.Value = 0.0;
            triangleGeom.CoordinateCol.Add(start);

            LineTo pt1 = new LineTo();
            pt1.X.Value = 2.0;
            pt1.Y.Value = 0.0;
            triangleGeom.CoordinateCol.Add(pt1);

            LineTo pt2 = new LineTo();
            pt2.X.Value = 1.0;
            pt2.Y.Value = 2.0;
            triangleGeom.CoordinateCol.Add(pt2);

            // Close the triangle by returning to the start point
            LineTo pt3 = new LineTo();
            pt3.X.Value = 0.0;
            pt3.Y.Value = 0.0;
            triangleGeom.CoordinateCol.Add(pt3);

            // Attach the new geometry to the shape
            shape.Geoms.Add(triangleGeom);

            // Optional: set fill and line colors for visibility
            shape.Fill.FillForegnd.Value = "#FF0000"; // red fill
            shape.Line.LineColor.Value = "#000000"; // black border

            // Save the diagram to a VSDX file
            diagram.Save("TriangleShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
