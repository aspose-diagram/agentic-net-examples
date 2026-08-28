using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Read the built‑in Author (Creator) property
                string author = diagram.DocumentProps.Creator;

                // Log the author value
                Console.WriteLine($"Author: {author}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
