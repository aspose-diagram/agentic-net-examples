using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, master name, isCalculate (bool)
            long rectId = page.AddShape(5.0, 5.0, "Rectangle", false);

            // Retrieve the shape object using the returned ID
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Clear any existing text and add new display text
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("Visit Aspose"));

            // Create a hyperlink that points to the desired website
            Hyperlink link = new Hyperlink();
            link.Name = "AsposeLink";
            link.Address.Value = "https://www.aspose.com";

            // Add the hyperlink to the shape's Hyperlinks collection
            rectShape.Hyperlinks.Add(link);

            // Save the diagram to a VSDX file
            diagram.Save("RectangleWithHyperlink.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
