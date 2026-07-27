using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page
        Page page = diagram.Pages[0];

        // Define points for a diamond shape (closed polyline)
        double[] points = new double[] { 5, 6, 6, 5, 5, 4, 4, 5, 5, 6 };
        // Draw the diamond and get its shape ID
        long shapeId = page.DrawPolyline(points);

        // Retrieve the shape object
        Shape diamond = page.Shapes.GetShape(shapeId);

        // Clear any existing text and add a new label
        diamond.Text.Value.Clear();
        diamond.Text.Value.Add(new Aspose.Diagram.Txt("Centered Label"));

        // Center the text block inside the shape
        diamond.TextXForm.TxtLocPinX.Value = 0.5;
        diamond.TextXForm.TxtLocPinY.Value = 0.5;

        // Apply custom font formatting to the text
        Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
        ch.IX = 0; // first character index
        ch.FontName.Value = "CustomFont"; // replace with the desired font name
        ch.Size.Value = 12.0 / 72.0; // 12 pt expressed in inches
        ch.Color.Value = "#000000"; // black color
        diamond.Chars.Add(ch);

        // Save the diagram as VSDX
        diagram.Save("DiamondWithLabel.vsdx", SaveFileFormat.Vsdx);
    }
}
