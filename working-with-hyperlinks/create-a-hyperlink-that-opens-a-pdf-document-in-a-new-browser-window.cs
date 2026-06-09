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

        // Ensure there is at least one page in the diagram
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Get the first page
        Page page = diagram.Pages[0];

        // Draw a rectangle shape on the page
        double pinX = 5.0;   // X coordinate of the shape center
        double pinY = 5.0;   // Y coordinate of the shape center
        double width = 2.0;  // Width of the rectangle
        double height = 1.0; // Height of the rectangle
        long shapeId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the shape object using its ID
        Shape shape = page.Shapes.GetShape(shapeId);

        // Create a hyperlink that points to a PDF and opens in a new browser window
        Hyperlink link = new Hyperlink();
        link.Name = "PdfLink";
        link.Address.Value = "https://example.com/document.pdf";
        link.NewWindow.Value = BOOL.True; // Open in a new window/tab
        link.Description.Value = "Open PDF in new window";

        // Add the hyperlink to the shape's Hyperlinks collection
        shape.Hyperlinks.Add(link);

        // Save the diagram to a VSDX file
        diagram.Save("HyperlinkDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
