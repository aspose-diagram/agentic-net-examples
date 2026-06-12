using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Define circle parameters
            double centerX = 5.0;          // X coordinate of the circle center (in inches)
            double centerY = 5.0;          // Y coordinate of the circle center (in inches)
            double radius = 2.0;           // Desired radius (in inches)

            // DrawEllipse expects width and height (diameter), so calculate them
            double diameter = radius * 2.0;

            // Add the circle (ellipse with equal width and height) to the page
            long shapeId = page.DrawEllipse(centerX, centerY, diameter, diameter);

            // Retrieve the created shape (optional, for further modifications)
            Shape circleShape = page.Shapes.GetShape(shapeId);

            // Example: set a fill color for the circle
            circleShape.Fill.FillForegnd.Value = "#FF0000"; // Red fill

            // Save the diagram to a VSDX file
            diagram.Save("CircleDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }