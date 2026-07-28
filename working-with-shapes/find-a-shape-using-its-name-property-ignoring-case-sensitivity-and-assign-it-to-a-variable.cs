using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the shape to find (case‑insensitive)
            string targetName = "MyShape";

            // Search the first page (adjust index if needed) for a shape whose Name matches ignoring case
            Shape foundShape = diagram.Pages[0].Shapes
                .FirstOrDefault(s => string.Equals(s.Name, targetName, StringComparison.OrdinalIgnoreCase));

            // foundShape now holds the matching shape or null if not found
            if (foundShape != null)
            {
                // Shape was found – you can work with foundShape here
            }
            else
            {
                // No shape with the specified name exists (ignoring case)
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
