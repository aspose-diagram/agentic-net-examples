using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access a specific page (e.g., the first page)
            Page page = diagram.Pages[0];

            // Access a specific shape on the page (e.g., the first shape)
            Shape shape = page.Shapes[0]; // Adjust the index or use shape ID as needed

            // Enumerate all hyperlinks associated with the shape
            foreach (Hyperlink hyperlink in shape.Hyperlinks)
            {
                // Output hyperlink properties
                Console.WriteLine("Address: " + hyperlink.Address);
                Console.WriteLine("Description: " + hyperlink.Description);
                Console.WriteLine("SubAddress: " + hyperlink.SubAddress);
                Console.WriteLine("ExtraInfo: " + hyperlink.ExtraInfo);
                Console.WriteLine("---------------------------");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
