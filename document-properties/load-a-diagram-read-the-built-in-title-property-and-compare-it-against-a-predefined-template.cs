using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file to be loaded
            string diagramPath = "input.vsdx";

            // Load the diagram using the constructor that accepts a file name (load rule)
            Diagram diagram = new Diagram(diagramPath);

            // Read the built‑in Title property from the document properties collection
            string actualTitle = diagram.DocumentProps.Title;

            // Predefined template title to compare against
            string expectedTitle = "My Diagram Template";

            // Compare the actual title with the expected title (case‑insensitive)
            bool titlesMatch = string.Equals(actualTitle, expectedTitle, StringComparison.OrdinalIgnoreCase);

            // Output the comparison result
            Console.WriteLine($"Actual Title: \"{actualTitle}\"");
            Console.WriteLine($"Expected Title: \"{expectedTitle}\"");
            Console.WriteLine($"Titles match: {titlesMatch}");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
