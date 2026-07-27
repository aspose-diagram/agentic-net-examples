using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Define position and size for the oval (in inches)
            double pinX = 5.0;   // center X
            double pinY = 5.0;   // center Y
            double width = 4.0;  // horizontal diameter
            double height = 2.5; // vertical diameter

            // Draw an oval shape on the page; returns the shape ID (long)
            long shapeId = page.DrawEllipse(pinX, pinY, width, height);

            // Retrieve the Shape object using the ID
            Shape oval = page.Shapes.GetShape((int)shapeId);

            // Set a solid fill color (optional, here light blue)
            oval.Fill.FillForegnd.Value = "#ADD8E6";

            // Set fill transparency to achieve 70% opacity.
            // Transparency is expressed as a percentage (0 = opaque, 100 = fully transparent).
            // 70% opacity => 30% transparency.
            oval.Fill.FillForegndTrans.Value = 30;

            // Save the diagram to a VSDX file
            diagram.Save("OvalWithOpacity.vsdx", SaveFileFormat.Vsdx);
        }
    }