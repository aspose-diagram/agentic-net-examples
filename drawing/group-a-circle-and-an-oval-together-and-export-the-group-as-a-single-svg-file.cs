using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page (avoid using ActivePage)
        Page page = diagram.Pages[0];

        // Draw a circle (ellipse with equal width and height)
        // Parameters: pinX, pinY (center), width, height
        long circleId = page.DrawEllipse(2.0, 2.0, 1.0, 1.0);
        Shape circle = page.Shapes.GetShape(circleId);

        // Draw an oval (ellipse with different width and height)
        long ovalId = page.DrawEllipse(4.0, 2.0, 2.0, 1.0);
        Shape oval = page.Shapes.GetShape(ovalId);

        // Group the circle and oval together
        Shape group = page.Shapes.Group(new Shape[] { circle, oval });

        // Export the group as a single SVG file
        SVGSaveOptions svgOptions = new SVGSaveOptions();
        group.ToSvg("group.svg", svgOptions);
    }
}
