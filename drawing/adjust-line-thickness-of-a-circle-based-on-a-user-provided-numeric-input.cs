using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Prompt user for line thickness (points)
        Console.Write("Enter line thickness (in points): ");
        double thickness = double.Parse(Console.ReadLine());

        // Create a new diagram (lifecycle create rule)
        Diagram diagram = new Diagram();

        // Use the first page (a diagram always contains at least one page)
        Page page = diagram.Pages[0];

        // Circle parameters
        double centerX = 5.0;   // X coordinate of the circle's center
        double centerY = 5.0;   // Y coordinate of the circle's center
        double radius  = 2.0;   // Desired radius
        double width   = radius * 2; // Width of the ellipse (circle)
        double height  = radius * 2; // Height of the ellipse (circle)

        // Draw the circle (ellipse with equal width and height)
        long shapeId = page.DrawEllipse(centerX, centerY, width, height);

        // Retrieve the shape and set its line thickness
        Shape circle = page.Shapes.GetShape(shapeId);
        circle.Line.LineWeight.Value = thickness;

        // Save the diagram (lifecycle save rule)
        diagram.Save("Circle.vdx", SaveFileFormat.Vdx);
    }
}
