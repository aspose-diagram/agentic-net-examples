using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (pinX, pinY, width, height, master name, isCalculate)
            long shapeId = page.AddShape(2.0, 2.0, 2.0, 2.0, "Rectangle", false);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a new Geom object (represents custom geometry)
            Geom customGeom = new Geom();

            // Define custom vertices (MoveTo + LineTo segments)

            // Move to the starting point (0,0)
            MoveTo move = new MoveTo();
            move.X.Value = 0.0;
            move.Y.Value = 0.0;
            customGeom.CoordinateCol.Add(move);

            // Line to (3,0)
            LineTo line1 = new LineTo();
            line1.X.Value = 3.0;
            line1.Y.Value = 0.0;
            customGeom.CoordinateCol.Add(line1);

            // Line to (3,2)
            LineTo line2 = new LineTo();
            line2.X.Value = 3.0;
            line2.Y.Value = 2.0;
            customGeom.CoordinateCol.Add(line2);

            // Line back to the start point (0,0) to close the shape
            LineTo line3 = new LineTo();
            line3.X.Value = 0.0;
            line3.Y.Value = 0.0;
            customGeom.CoordinateCol.Add(line3);

            // Add the custom geometry to the shape's Geoms collection
            shape.Geoms.Add(customGeom);

            // Save the diagram to a VSDX file
            diagram.Save("CustomGeometry.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}