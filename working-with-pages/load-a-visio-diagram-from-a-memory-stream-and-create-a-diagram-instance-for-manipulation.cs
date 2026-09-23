using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio file bytes into a memory stream (replace with your source of bytes)
            byte[] visioBytes = File.ReadAllBytes("input.vsdx");
            using (MemoryStream stream = new MemoryStream(visioBytes))
            {
                // Create a Diagram instance from the memory stream
                Diagram diagram = new Diagram(stream);

                // Diagram is now ready for manipulation
                // Example: output the number of pages in the diagram
                Console.WriteLine($"Number of pages: {diagram.Pages.Count}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
