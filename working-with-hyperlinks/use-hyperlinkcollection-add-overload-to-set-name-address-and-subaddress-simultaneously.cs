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

            // Access a shape (example: first shape on the first page)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Create a new Hyperlink instance
            Hyperlink hyperlink = new Hyperlink();

            // Set the hyperlink's name
            hyperlink.Name = "MyLink";

            // Set the hyperlink's address (URL or file path)
            hyperlink.Address.Value = "https://www.example.com";

            // Set the hyperlink's subaddress (location within the target document)
            hyperlink.SubAddress.Value = "Sheet1!A1";

            // Add the configured hyperlink to the shape's Hyperlink collection
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
