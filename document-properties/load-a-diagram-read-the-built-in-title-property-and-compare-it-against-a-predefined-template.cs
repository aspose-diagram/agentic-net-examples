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

            // Load the diagram using the built‑in constructor (lifecycle rule)
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the built‑in Title property from the document properties
            string actualTitle = diagram.DocumentProps.Title;

            // Predefined template title to compare against
            const string expectedTitle = "My Template Title";

            // Compare the actual title with the expected template title
            if (string.Equals(actualTitle, expectedTitle, StringComparison.Ordinal))
            {
                Console.WriteLine("Title matches the template.");
            }
            else
            {
                Console.WriteLine($"Title mismatch. Actual: \"{actualTitle}\", Expected: \"{expectedTitle}\"");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
