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

            // Add a new blank page to the diagram (PageCollection.Add requires a Page instance)
            Page newPage = new Page();
            diagram.Pages.Add(newPage);

            // Parameters for the shape to be added
            double pinX = 5.0;               // X coordinate of the shape's center
            double pinY = 5.0;               // Y coordinate of the shape's center
            double width = 2.0;              // Width of the shape
            double height = 1.0;             // Height of the shape
            string masterName = "Rectangle"; // Master shape name to use
            int pageIndex = 0;               // Index of the page where the shape will be placed

            // Add a rectangle shape to the first page and obtain its ID
            long shapeId = diagram.AddShape(pinX, pinY, width, height, masterName, pageIndex);

            // Retrieve the shape instance from the page's shape collection
            Shape shape = diagram.Pages[pageIndex].Shapes.GetShape(shapeId);

            // Create a Hyperlink object and set its properties (Name, Address, SubAddress)
            Hyperlink hyperlink = new Hyperlink
            {
                Name = "VisitSite" // Internal identifier for the hyperlink
            };
            hyperlink.Address.Value = "https://www.example.com"; // External URL
            hyperlink.SubAddress.Value = "Section1";             // Internal bookmark or page reference

            // Add the configured hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(hyperlink);

            // Save the diagram to VSDX format
            diagram.Save("HyperlinkExample.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}