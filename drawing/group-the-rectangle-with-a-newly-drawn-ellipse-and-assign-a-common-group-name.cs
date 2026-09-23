using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new blank diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Draw a rectangle (pinX, pinY, width, height)
        long rectId = page.DrawRectangle(2.0, 2.0, 2.0, 1.0);

        // Draw an ellipse (pinX, pinY, width, height)
        long ellipseId = page.DrawEllipse(5.0, 2.0, 2.0, 1.0);

        // Retrieve the shape objects using their IDs
        Shape rectShape = page.Shapes.GetShape(rectId);
        Shape ellipseShape = page.Shapes.GetShape(ellipseId);

        // Group the rectangle and ellipse together
        Shape groupShape = page.Shapes.Group(new Shape[] { rectShape, ellipseShape });

        // Assign a common name to the group
        groupShape.NameU = "MyGroup";
        groupShape.Name = "MyGroup";

        // Save the diagram to a VSDX file
        diagram.Save("GroupedDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
