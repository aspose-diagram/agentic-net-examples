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

            // Get the first page (avoid using ActivePage as per rules)
            Page page = diagram.Pages[0];

            // Add a diamond shape to the page at position (2,2) inches
            long shapeId = page.AddShape(2.0, 2.0, "Diamond");

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set horizontal text alignment to center
            shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

            // Set vertical text alignment to middle
            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Add sample text to the shape
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Save the diagram to a VSDX file
            diagram.Save("DiamondAligned.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
