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

            // Retrieve a specific shape (example: shape with ID = 1 on the first page)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Enumerate all hyperlinks associated with the shape
            foreach (Hyperlink hyperlink in shape.Hyperlinks)
            {
                // Output hyperlink details
                Console.WriteLine($"Address: {hyperlink.Address}");
                Console.WriteLine($"Description: {hyperlink.Description}");
                Console.WriteLine($"SubAddress: {hyperlink.SubAddress}");
                Console.WriteLine($"NewWindow: {hyperlink.NewWindow}");
                Console.WriteLine(new string('-', 40));
            }

            // Optionally save the diagram after processing
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
