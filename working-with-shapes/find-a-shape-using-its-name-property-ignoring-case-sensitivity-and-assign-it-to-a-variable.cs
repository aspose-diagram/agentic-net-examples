using System.IO;
using Aspose.Diagram;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the shape to find (case‑insensitive)
            string targetName = "MyShape";

            // Variable to hold the found shape
            Shape foundShape = null;

            // Search through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                // Find the first shape whose Name matches targetName ignoring case
                foundShape = page.Shapes.FirstOrDefault(s =>
                    string.Equals(s.Name, targetName, StringComparison.OrdinalIgnoreCase));

                if (foundShape != null)
                    break; // Exit loop once the shape is found
            }

            // Example usage: output the shape ID if found
            if (foundShape != null)
                Console.WriteLine($"Found shape ID: {foundShape.ID}");
            else
                Console.WriteLine("Shape not found.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
