using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a blank page to the diagram
            diagram.Pages.Add(new Page());

            // Get the first (and only) page
            Page page = diagram.Pages[0];

            // Define the circle radius (in inches)
            double radius = 1.0;

            // Calculate the center point (PinX, PinY) and the diameter (width, height)
            double pinX = radius;               // X coordinate of the circle center
            double pinY = radius;               // Y coordinate of the circle center
            double diameter = radius * 2.0;     // Width and height for a perfect circle

            // Draw the circle using the DrawEllipse method (width == height)
            long circleShapeId = page.DrawEllipse(pinX, pinY, diameter, diameter);

            // Retrieve the created shape from the Shapes collection
            Shape circleShape = page.Shapes.GetShape(circleShapeId);

            // Optional: set a name for the shape (useful for later identification)
            circleShape.Name = "MyCircle";

            // The diagram now contains a circle shape in its Shapes collection.
            // (Further processing or saving can be performed here if needed.)
        }
    }