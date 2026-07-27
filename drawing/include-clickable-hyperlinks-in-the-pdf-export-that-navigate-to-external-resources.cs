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

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Define shape geometry
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 2.0;
            double height = 1.0;

            // Add a rectangle shape; returns a long shape ID
            long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle", false);

            // Retrieve the shape instance
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Create a hyperlink to an external URL
            Hyperlink link = new Hyperlink();
            link.Name = "ExternalLink";
            link.Address.Value = "https://www.example.com";
            link.Description.Value = "Visit Example.com";

            // Attach the hyperlink to the shape
            shape.Hyperlinks.Add(link);

            // Add visible text to the shape (optional)
            shape.Text.Value.Add(new Txt("Click me"));

            // Export the diagram to PDF; the hyperlink will be clickable
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            diagram.Save("HyperlinkedDiagram.pdf", pdfOptions);

            Console.WriteLine("PDF exported with clickable hyperlink.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
