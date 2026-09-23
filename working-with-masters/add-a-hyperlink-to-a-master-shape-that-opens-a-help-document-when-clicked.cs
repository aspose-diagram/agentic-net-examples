using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a shape based on a master named "MyMaster"
            // The fourth parameter (isCalculate) must be a boolean
            long shapeId = page.AddShape(2.0, 2.0, "MyMaster", false);

            // Retrieve the newly added shape instance
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a new hyperlink that points to a help document
            Hyperlink link = new Hyperlink();
            link.Name = "HelpLink";
            link.Address.Value = "file:///C:/HelpDocs/help.pdf";
            link.Description.Value = "Open Help Document";

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(link);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
