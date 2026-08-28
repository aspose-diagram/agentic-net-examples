using System;
using System.IO;
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

            // Get the first (default) page; ensure at least one page exists
            Page page = diagram.Pages[0];

            // Add a rectangle shape that will act as the hyperlink target
            // Parameters: PinX, PinY, Width, Height, Master name, isCalculate (bool)
            long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Set visible text for the shape
            shape.Text.Value.Add(new Txt("Open PDF"));

            // Create a hyperlink that points to a PDF file
            Hyperlink link = new Hyperlink();
            // Address of the PDF document (relative or absolute URL)
            link.Address.Value = "https://example.com/document.pdf";
            // Optional description (tooltip)
            link.Description.Value = "Open the PDF in a new browser window";
            // Instruct Visio to open the link in a new window/tab
            // NewWindow expects a BOOL value, not an integer
            link.NewWindow.Value = BOOL.True;

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(link);

            // Save the diagram to a VSDX file so the hyperlink can be tested in Visio or a viewer
            diagram.Save("HyperlinkDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}