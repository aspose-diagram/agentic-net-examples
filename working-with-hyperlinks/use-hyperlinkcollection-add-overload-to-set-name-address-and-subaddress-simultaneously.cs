using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (PinX, PinY, master name, page index)
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Retrieve the shape instance from the page using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a hyperlink and set Name, Address, and SubAddress in one step
            Hyperlink hyperlink = new Hyperlink();               // instantiate hyperlink object
            hyperlink.Name = "MyLink";                           // internal identifier
            hyperlink.Address.Value = "https://example.com";     // external URL
            hyperlink.SubAddress.Value = "Page1";                // internal target page name

            // Add the prepared hyperlink to the shape's collection
            shape.Hyperlinks.Add(hyperlink);

            // Optionally, set a description (tooltip) for the hyperlink
            if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
            {
                // The last added hyperlink is the one we just created
                Hyperlink link = shape.Hyperlinks[shape.Hyperlinks.Count - 1];
                link.Description.Value = "Open Example.com";
            }

            // Save the diagram to a VSDX file
            diagram.Save("HyperlinkDemo.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}