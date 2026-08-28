using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Search for the triangle shape by its name (case‑insensitive)
                foreach (Shape shape in page.Shapes)
                {
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.Equals("Triangle", StringComparison.OrdinalIgnoreCase))
                    {
                        // Move the triangle to the absolute position (200, 150) on the page
                        shape.MoveTo(200.0, 150.0);
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
