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

            // Retrieve the target shape (replace with the actual shape ID or name)
            long shapeId = 1; // example shape ID
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Get the Hyperlink collection of the shape
            HyperlinkCollection hyperlinks = shape.Hyperlinks;

            // Enumerate all hyperlinks and output their details
            for (int i = 0; i < hyperlinks.Count; i++)
            {
                Hyperlink hl = hyperlinks[i];
                Console.WriteLine($"Hyperlink {i + 1}:");
                Console.WriteLine($"  Address: {hl.Address}");
                Console.WriteLine($"  Description: {hl.Description}");
                Console.WriteLine($"  SubAddress: {hl.SubAddress}");
                Console.WriteLine($"  NewWindow: {hl.NewWindow}");
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
