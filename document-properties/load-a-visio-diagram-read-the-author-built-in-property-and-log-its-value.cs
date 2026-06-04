using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string visioFilePath = "sample.vsdx";

            // Load the Visio diagram using the Diagram constructor (load rule)
            Diagram diagram = new Diagram(visioFilePath);

            // Access the built‑in Author property (mapped to Creator in DocumentProperties)
            string author = diagram.DocumentProps.Creator;

            // Log the Author value to the console
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
