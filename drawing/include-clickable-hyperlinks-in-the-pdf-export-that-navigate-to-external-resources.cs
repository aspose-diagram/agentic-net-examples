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

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Define shape dimensions and position
            double pinX = 5.0;   // X coordinate (inches)
            double pinY = 5.0;   // Y coordinate (inches)
            double width = 2.0;  // Width (inches)
            double height = 1.0; // Height (inches)

            // Add a rectangle shape to the page
            // The last parameter 'false' indicates that the shape size is not calculated automatically
            long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle", false);

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Create a hyperlink that points to an external URL
            Hyperlink link = new Hyperlink();
            link.Address.Value = "https://example.com";

            // Optionally set a description (tooltip) for the hyperlink
            link.Description.Value = "Visit Example.com";

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(link);

            // Prepare PDF save options (default options are sufficient for hyperlinks)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Export the diagram to PDF; the hyperlink will be clickable in the resulting file
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
