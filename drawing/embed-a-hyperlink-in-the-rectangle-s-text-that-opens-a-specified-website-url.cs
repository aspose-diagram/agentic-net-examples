using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new diagram
            Diagram diagram = new Diagram();

            // Access the first page (default page)
            Page page = diagram.Pages[0];

            // Draw a rectangle shape (pinX, pinY, width, height)
            // Position at (2, 2) inches with size 2x1 inches
            long rectId = page.DrawRectangle(2.0, 2.0, 2.0, 1.0);

            // Retrieve the shape object using the returned ID
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Clear any existing text and add new text
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("Visit Example.com"));

            // Create a hyperlink and assign the target URL
            Hyperlink link = new Hyperlink
            {
                Name = "ExampleLink"
            };
            link.Address.Value = "https://www.example.com";

            // Add the hyperlink to the shape's Hyperlinks collection
            rectShape.Hyperlinks.Add(link);

            // Save the diagram to a VSDX file
            diagram.Save("RectangleWithHyperlink.vsdx", SaveFileFormat.Vsdx);
        }
    }