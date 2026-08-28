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

            // Draw a circle (ellipse with equal width and height)
            // Parameters: pinX, pinY, width, height
            long circleId = page.DrawEllipse(2.0, 2.0, 1.5, 1.5);

            // Draw an oval (ellipse with different width and height)
            long ovalId = page.DrawEllipse(5.0, 2.0, 2.0, 1.0);

            // Retrieve the Shape objects from their IDs
            Shape circleShape = page.Shapes.GetShape(circleId);
            Shape ovalShape = page.Shapes.GetShape(ovalId);

            // Group the two shapes together
            Shape groupShape = page.Shapes.Group(new Shape[] { circleShape, ovalShape });

            // Export the group as a single SVG file
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Optionally, set the page index if needed (default is 0)
            svgOptions.PageIndex = 0;

            // Save the group shape to SVG
            groupShape.ToSvg("GroupedShapes.svg", svgOptions);

            // Optional: inform the user
            Console.WriteLine("Grouped circle and oval have been exported to GroupedShapes.svg");
        }
    }