using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a hyperlink that uses a mailto: address
            Hyperlink link = new Hyperlink();
            link.Name = "EmailLink";
            link.Address.Value = "mailto:someone@example.com";
            link.Description.Value = "Send Email";

            // Attach the hyperlink to the shape
            shape.Hyperlinks.Add(link);

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
