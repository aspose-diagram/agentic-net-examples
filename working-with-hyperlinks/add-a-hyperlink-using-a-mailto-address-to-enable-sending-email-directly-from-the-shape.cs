using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Add a rectangle shape to the page (master name "Rectangle")
            long shapeId = page.AddShape(4.25, 5.5, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a hyperlink that uses a mailto: address
            Hyperlink mailLink = new Hyperlink();
            mailLink.Address.Value = "mailto:someone@example.com";

            // Attach the hyperlink to the shape
            shape.Hyperlinks.Add(mailLink);

            // Save the diagram to a VSDX file
            diagram.Save("HyperlinkDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
