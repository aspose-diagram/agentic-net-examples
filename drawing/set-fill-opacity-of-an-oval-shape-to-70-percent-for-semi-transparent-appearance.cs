using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Draw an oval (ellipse) on the page
        // Parameters: PinX, PinY, Width, Height
        long ovalId = page.DrawEllipse(2.0, 2.0, 4.0, 2.0);

        // Retrieve the shape object using the returned ID
        Shape oval = page.Shapes.GetShape((int)ovalId);

        // Set the fill foreground color (optional, here green)
        oval.Fill.FillForegnd.Value = "#00FF00";

        // Set fill opacity to 70% (70% transparent)
        // FillForegndTrans expects a percentage (0 = opaque, 100 = fully transparent)
        oval.Fill.FillForegndTrans.Value = 70;

        // Save the diagram to a VSDX file
        diagram.Save("OvalWithOpacity.vsdx", SaveFileFormat.Vsdx);
    }
}
