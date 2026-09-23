using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw a circle (equal width and height)
            // Parameters: pinX, pinY, width, height
            long circleId = page.DrawEllipse(2.0, 2.0, 2.0, 2.0);

            // Draw an oval (different width and height)
            long ovalId = page.DrawEllipse(5.0, 2.0, 3.0, 2.0);

            // Retrieve the shape objects
            Shape circleShape = page.Shapes.GetShape(circleId);
            Shape ovalShape = page.Shapes.GetShape(ovalId);

            // Group the circle and oval together
            Shape groupShape = page.Shapes.Group(new Shape[] { circleShape, ovalShape });

            // Export the group as a single SVG file
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            groupShape.ToSvg("GroupedShape.svg", svgOptions);

            Console.WriteLine("Group exported to GroupedShape.svg");
        }
    }