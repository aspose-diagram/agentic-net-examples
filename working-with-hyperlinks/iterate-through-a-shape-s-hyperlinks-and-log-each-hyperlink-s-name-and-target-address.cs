using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access a specific page and shape (example uses the first page and first shape)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Iterate through all hyperlinks attached to the shape
            foreach (Hyperlink hyperlink in shape.Hyperlinks)
            {
                // Log the hyperlink's name and its target address
                Console.WriteLine($"Name: {hyperlink.Name}, Address: {hyperlink.Address}");
            }

            // Save the diagram if any changes were made (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
