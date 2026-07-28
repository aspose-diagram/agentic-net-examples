using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Use the first page (a new diagram contains one default page)
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            double pinX = 4.25;   // X coordinate of the shape's pin
            double pinY = 5.5;    // Y coordinate of the shape's pin
            string masterName = "Rectangle";
            long shapeId = page.AddShape(pinX, pinY, masterName);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a hyperlink that opens the default mail client
            Hyperlink hyperlink = new Hyperlink();
            hyperlink.Address.Value = "mailto:someone@example.com";
            hyperlink.Description.Value = "Send email";

            // Attach the hyperlink to the shape
            shape.Hyperlinks.Add(hyperlink);

            // Save the diagram to a VSDX file
            diagram.Save("HyperlinkDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
