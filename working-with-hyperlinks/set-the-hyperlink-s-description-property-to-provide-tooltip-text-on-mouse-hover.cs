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

            // Access the first page and the first shape (adjust indices as needed)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Create a new hyperlink instance
            Hyperlink hyperlink = new Hyperlink();

            // Set the target address of the hyperlink
            hyperlink.Address.Value = "https://example.com";

            // Set the description which appears as a tooltip on mouse hover
            hyperlink.Description.Value = "Click to open the example website";

            // Add the hyperlink to the shape's hyperlink collection
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
