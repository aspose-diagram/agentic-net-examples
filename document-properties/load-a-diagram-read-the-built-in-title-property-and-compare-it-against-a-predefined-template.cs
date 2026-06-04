using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram to be examined
            string diagramPath = @"C:\Diagrams\sample.vsdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the built‑in Title property from the document properties
            // DocumentProps.Title returns the title set in the Visio file
            string diagramTitle = diagram.DocumentProps.Title;

            // Predefined template title to compare against
            string templateTitle = "Standard Diagram Title";

            // Compare the titles (case‑insensitive)
            bool titlesMatch = string.Equals(diagramTitle, templateTitle, StringComparison.OrdinalIgnoreCase);

            // Output the result
            Console.WriteLine("Diagram Title: \"{0}\"", diagramTitle ?? "<null>");
            Console.WriteLine("Template Title: \"{0}\"", templateTitle);
            Console.WriteLine("Titles match: {0}", titlesMatch);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
