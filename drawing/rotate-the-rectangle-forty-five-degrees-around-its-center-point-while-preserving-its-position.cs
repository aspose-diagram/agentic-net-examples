using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (default page)
            Page page = diagram.Pages[0];

            // Define rectangle parameters
            double pinX = 5.0;   // X coordinate of the rectangle's center
            double pinY = 5.0;   // Y coordinate of the rectangle's center
            double width = 2.0;  // Width in inches
            double height = 1.0; // Height in inches

            // Draw the rectangle and obtain its shape ID
            long shapeId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the shape object using the ID
            Shape rectangle = page.Shapes.GetShape((int)shapeId);

            // Rotate the rectangle 45 degrees around its center (pin point)
            rectangle.SetAngle(45.0);

            // Save the diagram to a VSDX file
            diagram.Save("RotatedRectangle.vsdx", SaveFileFormat.Vsdx);
        }
    }