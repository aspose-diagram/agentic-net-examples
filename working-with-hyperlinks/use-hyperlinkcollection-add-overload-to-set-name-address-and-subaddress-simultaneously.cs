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
            Diagram diagram = new Diagram(@"Input.vsdx");

            // Assume we work with the first page and first shape
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Create a new Hyperlink instance
            Hyperlink hyperlink = new Hyperlink();

            // Set the hyperlink name
            hyperlink.Name = "MyHyperlink";

            // Set the address (URL or file path)
            hyperlink.Address.Value = "https://www.example.com";

            // Set the subaddress (location within the target document)
            hyperlink.SubAddress.Value = "Sheet1!A1";

            // Add the hyperlink to the shape's Hyperlink collection
            shape.Hyperlinks.Add(hyperlink);

            // Save the modified diagram
            diagram.Save(@"Output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
