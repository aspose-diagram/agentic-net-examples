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

            // Access the first page (modify index as needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape from the page (modify index as needed)
            Shape shape = page.Shapes[0];

            // Iterate through each hyperlink in the shape's Hyperlinks collection
            foreach (Hyperlink hyperlink in shape.Hyperlinks)
            {
                // Log the hyperlink's name and its target address
                Console.WriteLine($"Hyperlink Name: {hyperlink.Name}, Address: {hyperlink.Address}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
