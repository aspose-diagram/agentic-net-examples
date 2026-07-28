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

            // Add a rectangle shape to the active page (uses the built‑in "Rectangle" master)
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle", false);
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

            // Ensure the shape has an associated master and that the master contains at least one shape
            if (shape.Master != null && shape.Master.Shapes.Count > 0)
            {
                // The master shape (the template shape inside the master)
                Shape masterShape = shape.Master.Shapes[0];

                // Create a new hyperlink that points to a help document
                Hyperlink link = new Hyperlink();
                link.Name = "HelpLink";
                link.Address.Value = "https://example.com/help.pdf";   // URL or file path to the help document
                link.Description.Value = "Open Help Document";

                // Add the hyperlink to the master shape's Hyperlinks collection
                masterShape.Hyperlinks.Add(link);
            }

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
