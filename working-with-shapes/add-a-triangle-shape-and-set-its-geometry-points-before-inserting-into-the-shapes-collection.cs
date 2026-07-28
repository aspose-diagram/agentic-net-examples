using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Create a new shape instance (triangle)
            Shape triangle = new Shape();

            // Set basic shape properties
            triangle.Type = TypeValue.Shape;          // Shape type
            triangle.Name = "Triangle";                // Optional name

            // Define geometry for an equilateral triangle
            // Coordinates are in inches; adjust as needed
            Geom geom = new Geom();

            // Move to the first vertex (0,0)
            MoveTo move = new MoveTo();
            move.X.Value = 0.0;
            move.Y.Value = 0.0;
            geom.CoordinateCol.Add(move);

            // Line to second vertex (1,0)
            LineTo line1 = new LineTo();
            line1.X.Value = 1.0;
            line1.Y.Value = 0.0;
            geom.CoordinateCol.Add(line1);

            // Line to third vertex (0.5,0.866) – height of equilateral triangle
            LineTo line2 = new LineTo();
            line2.X.Value = 0.5;
            line2.Y.Value = 0.8660254037844386; // sqrt(3)/2
            geom.CoordinateCol.Add(line2);

            // Close the triangle by returning to the first vertex
            LineTo line3 = new LineTo();
            line3.X.Value = 0.0;
            line3.Y.Value = 0.0;
            geom.CoordinateCol.Add(line3);

            // Add the geometry to the shape
            triangle.Geoms.Add(geom);

            // Set the shape's size and position on the page
            triangle.XForm.Width.Value = 1.0;   // Width of the triangle
            triangle.XForm.Height.Value = 0.8660254037844386; // Height matches geometry
            triangle.XForm.PinX.Value = 2.0;   // Horizontal position on page
            triangle.XForm.PinY.Value = 2.0;   // Vertical position on page

            // Insert the shape into the page's Shapes collection
            page.Shapes.Add(triangle);

            // Save the diagram to a VSDX file
            diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);

            Console.WriteLine("Triangle shape added and diagram saved successfully.");
        }
    }