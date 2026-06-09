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

            // Use the first page (a diagram always contains at least one page)
            Page page = diagram.Pages[0];

            // Add a rectangle shape at the specified position
            double pinX = 4.25; // X coordinate (in inches)
            double pinY = 5.5;  // Y coordinate (in inches)
            long shapeId = page.AddShape(pinX, pinY, "Rectangle");

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a new hyperlink instance
            Hyperlink hyperlink = new Hyperlink();

            // Set the mailto address (this will open the default mail client)
            hyperlink.Address.Value = "mailto:someone@example.com";

            // Optional: provide a description that appears in the tooltip
            hyperlink.Description.Value = "Send Email";

            // Add the hyperlink to the shape's Hyperlinks collection
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
