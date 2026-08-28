using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the active page of the diagram
            Page page = diagram.ActivePage;

            // Add a rectangle shape at position (5,5) inches
            long rectId = page.AddShape(5.0, 5.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Set the visible text of the rectangle
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("Visit Example.com"));

            // Create a hyperlink that points to the desired website
            Hyperlink link = new Hyperlink();
            link.Name = "ExampleLink";
            link.Address.Value = "https://www.example.com";

            // Attach the hyperlink to the rectangle shape
            rectShape.Hyperlinks.Add(link);

            // Save the diagram to a VSDX file
            diagram.Save("RectangleWithLink.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
