using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Open the Visio file as a read‑only stream
            using (FileStream stream = new FileStream("input.vsdx", FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream using the Diagram(Stream) constructor
                Diagram diagram = new Diagram(stream);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example operation: output the shape ID
                        Console.WriteLine($"Page ID: {page.ID}, Shape ID: {shape.ID}");
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
