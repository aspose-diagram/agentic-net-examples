using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Access the first page (a default page is created automatically)
            Page page = diagram.Pages[0];

            // Add a rectangle shape at position (2,2) on the page
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Enable dynamic glue so connectors can automatically attach to this shape
            shape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

            // Save the diagram to a VSDX file
            diagram.Save("GluedShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
