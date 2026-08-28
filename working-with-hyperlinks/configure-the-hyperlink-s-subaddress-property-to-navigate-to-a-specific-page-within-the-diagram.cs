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

            // Select the shape to which the hyperlink will be added
            // Here we use the first shape on the first page as an example
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Create a new hyperlink instance
            Hyperlink hyperlink = new Hyperlink();

            // Since the link points to a page within the same document,
            // the Address can be left empty or set to an empty string
            hyperlink.Address.Value = "";

            // Set SubAddress to the target page name (e.g., "Page-2")
            hyperlink.SubAddress.Value = "Page-2";

            // Optional: provide a description for the hyperlink
            hyperlink.Description.Value = "Navigate to Page-2";

            // Add the hyperlink to the shape's Hyperlinks collection
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
