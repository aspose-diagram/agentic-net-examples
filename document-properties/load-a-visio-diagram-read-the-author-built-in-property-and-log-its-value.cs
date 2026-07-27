using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx"); // replace with your file path

            // Read the built‑in "Author" property (mapped to Creator)
            string author = diagram.DocumentProps.Creator;

            // Log the author value
            Console.WriteLine($"Author: {author}");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
