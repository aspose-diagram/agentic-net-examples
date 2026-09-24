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

        // Ensure there is at least one page
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Get the first page
        Page page = diagram.Pages[0];

        // Add a rectangle shape (pinX, pinY, width, height)
        long shapeId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

        // Retrieve the shape object
        Shape shape = page.Shapes.GetShape(shapeId);

        // Set the shape text
        shape.Text.Value.Clear();
        shape.Text.Value.Add(new Txt("Diagonal Text"));

        // Rotate the text 45 degrees (convert to radians)
        double angleDeg = 45.0;
        double angleRad = (Math.PI / 180.0) * angleDeg;
        shape.TextXForm.TxtAngle.Value = angleRad;

        // Adjust text block margins (0.1 inch on each side)
        shape.TextBlock.LeftMargin = new DoubleValue(0.1, MeasureConst.IN);
        shape.TextBlock.RightMargin = new DoubleValue(0.1, MeasureConst.IN);
        shape.TextBlock.TopMargin = new DoubleValue(0.1, MeasureConst.IN);
        shape.TextBlock.BottomMargin = new DoubleValue(0.1, MeasureConst.IN);

        // Save the diagram to VSDX format
        diagram.Save("DiagonalText.vsdx", SaveFileFormat.Vsdx);
    }
}
