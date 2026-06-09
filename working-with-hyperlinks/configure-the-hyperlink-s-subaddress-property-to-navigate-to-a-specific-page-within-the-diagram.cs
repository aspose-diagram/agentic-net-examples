using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Select a shape to which the hyperlink will be added (first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Create a new hyperlink instance
            Hyperlink hyperlink = new Hyperlink();

            // Set the SubAddress to the target page name within the same diagram (e.g., "Page-2")
            hyperlink.SubAddress.Value = "Page-2";

            // Address must be set (empty string indicates internal navigation)
            hyperlink.Address.Value = "";

            // Add the configured hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(hyperlink);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
