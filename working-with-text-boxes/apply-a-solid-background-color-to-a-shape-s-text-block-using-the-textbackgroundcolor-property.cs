using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Insert text into the shape
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Apply a solid background color (red) to the shape's text block
            shape.TextBlock.TextBkgnd.Ufe.F = "RGB(255,0,0)";   // set background color
            shape.TextBlock.TextBkgndTrans.Value = 0;          // make it fully opaque

            // Save the diagram to a VSDX file
            diagram.Save("TextBackgroundColor.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
