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
            var diagram = new Diagram("input.vsdx");

            // Get the first page (adjust index as needed)
            var page = diagram.Pages[0];

            // Get a specific shape (adjust index or search criteria as needed)
            var shape = page.Shapes[0];

            // Iterate through all hyperlinks of the shape
            foreach (Hyperlink hyperlink in shape.Hyperlinks)
            {
                // Retrieve the hyperlink's name (if set) and its target address
                string name = hyperlink.Name;
                string address = hyperlink.Address?.Value;

                // Log the information to the console
                Console.WriteLine($"Hyperlink Name: {name}, Address: {address}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
