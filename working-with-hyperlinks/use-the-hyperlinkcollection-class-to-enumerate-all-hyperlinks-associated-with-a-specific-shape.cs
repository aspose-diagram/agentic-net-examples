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

            // Identify the shape whose hyperlinks you want to enumerate.
            // Here we use a shape ID; adjust as needed (e.g., by name or index).
            long shapeId = 1; // example shape ID
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Enumerate all hyperlinks associated with the shape.
            foreach (Hyperlink hyperlink in shape.Hyperlinks)
            {
                // Output hyperlink details.
                Console.WriteLine($"Address    : {hyperlink.Address}");
                Console.WriteLine($"Description: {hyperlink.Description}");
                Console.WriteLine($"SubAddress : {hyperlink.SubAddress}");
                Console.WriteLine(new string('-', 30));
            }

            // Optionally save the diagram if any modifications were made.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
