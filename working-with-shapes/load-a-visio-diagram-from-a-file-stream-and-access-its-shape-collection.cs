using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "example.vsdx";

            // Open a read‑only file stream
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream using the Diagram(Stream) constructor
                Diagram diagram = new Diagram(stream);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the shape collection of the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example: output shape ID and its universal name
                        Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
