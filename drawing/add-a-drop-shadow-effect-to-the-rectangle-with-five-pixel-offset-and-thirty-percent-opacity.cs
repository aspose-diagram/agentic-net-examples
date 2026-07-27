using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Access the first page (avoid using ActivePage as per rules)
            Page page = diagram.Pages[0];

            // Define rectangle geometry (pin position and size in inches)
            double pinX = 5.0;   // X coordinate of the rectangle's center
            double pinY = 5.0;   // Y coordinate of the rectangle's center
            double width = 2.0;  // Width of the rectangle
            double height = 1.0; // Height of the rectangle

            // Draw the rectangle; returns the shape ID (long)
            long rectId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the shape object using the ID (GetShape expects an int)
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // -----------------------------------------------------------------
            // Apply drop shadow effect
            // -----------------------------------------------------------------
            // Enable simple shadow
            rectShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

            // Shadow color (black)
            rectShape.Fill.ShdwForegnd.Value = "#000000";

            // Shadow opacity: 30% transparent (0.3 means 30% transparent)
            rectShape.Fill.ShdwForegndTrans.Value = 0.3;

            // Offset of the shadow.
            // Approximate 5‑pixel offset assuming 96 DPI (~0.052 inches).
            rectShape.Fill.ShapeShdwOffsetX.Value = 0.05;
            rectShape.Fill.ShapeShdwOffsetY.Value = 0.05;

            // Save the diagram to a VSDX file
            diagram.Save("RectangleWithShadow.vsdx", SaveFileFormat.Vsdx);
        }
    }