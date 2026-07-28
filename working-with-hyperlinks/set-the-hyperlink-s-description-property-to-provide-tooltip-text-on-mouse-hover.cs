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

            // Access the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Ensure the shape has at least one hyperlink; create one if none exist
            if (shape.Hyperlinks.Count == 0)
            {
                Hyperlink newLink = new Hyperlink();
                // Example address; adjust as needed
                newLink.Address.Value = "https://example.com";
                shape.Hyperlinks.Add(newLink);
            }

            // Retrieve the first hyperlink associated with the shape
            Hyperlink hyperlink = shape.Hyperlinks[0];

            // Set the description (tooltip) for the hyperlink
            hyperlink.Description.Value = "Open example website";

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
